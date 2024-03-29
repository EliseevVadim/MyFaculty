﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.TaskSubmissionReviews.Commands.CreateTaskSubmissionReview;
using MyFaculty.Application.Features.TaskSubmissionReviews.Commands.DeleteTaskSubmissionReview;
using MyFaculty.Application.Features.TaskSubmissionReviews.Commands.UpdateTaskSubmissionReview;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Hubs;
using MyFaculty.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskSubmissionReviewsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly NotificationsHub _notificationsHub;
        private readonly string _appDomain;

        public TaskSubmissionReviewsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, NotificationsHub notificationsHub)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _notificationsHub = notificationsHub;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Добавляет отзыв к решению задания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /tasksubmissionreviews
        /// {
        ///     "textContent": "string",
        ///     "submissionAttachments": "list of files",
        ///     "rate": 5,
        ///     "newStatus": 2,
        ///     "submissionId": 1,
        ///     "reviewerId": 1
        /// }
        /// </remarks>
        /// <param name="createTaskSubmissionReviewDto">Объект CreateCommentDto</param>
        /// <returns>Возвращает объект TaskSubmissionReviewViewModel</returns>
        /// <response code="201">Отзыва успешно добавлен</response>
        /// <response code="404">Решение не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskSubmissionReviewViewModel>> Create([FromForm] CreateTaskSubmissionReviewDto createTaskSubmissionReviewDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createTaskSubmissionReviewDto.ReviewerId)
                return Forbid();
            List<Attachment> sumbissionAttachments = new List<Attachment>();
            Guid sumbissionReviewAttachmentsUid = Guid.NewGuid();
            List<Attachment> newFiles = ProcessNewFiles(createTaskSubmissionReviewDto.Attachments, sumbissionReviewAttachmentsUid);
            if (newFiles != null)
                sumbissionAttachments.AddRange(newFiles);
            CreateTaskSubmissionReviewCommand command = _mapper.Map<CreateTaskSubmissionReviewCommand>(createTaskSubmissionReviewDto);
            command.Attachments = sumbissionAttachments.Count > 0 ? JsonConvert.SerializeObject(sumbissionAttachments) : null;
            command.SubmissionReviewAttachmentsUid = sumbissionReviewAttachmentsUid;
            TaskSubmissionReviewViewModel review = await Mediator.Send(command);
            await _notificationsHub.MakeUserLoadNotificationsAsync(review.SubmissionAuthorId);
            return Created(nameof(TaskSubmissionReviewsController), review);
        }

        /// <summary>
        /// Редактирует отзыв к решению задания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /tasksubmissionreviews
        /// {
        ///     "id": 1,
        ///     "textContent": "string",
        ///     "rate": 5,
        ///     "newStatus": 2,
        ///     "oldAttachments": "json",
        ///     "actualAttachments": "json",
        ///     "newFiles": "list of files",
        ///     "submissionReviewAttachmentsUid": "guid"
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="updateTaskSubmissionReviewDto">Объект UpdateTaskSubmissionReviewDto</param>
        /// <returns>Возвращает объект TaskSubmissionReviewViewModel</returns>
        /// <response code="200">Отзыв успешно обновлен</response>
        /// <response code="404">Отзыв не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response>  
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPut]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskSubmissionReviewViewModel>> Update([FromForm] UpdateTaskSubmissionReviewDto updateTaskSubmissionReviewDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateTaskSubmissionReviewDto.IssuerId)
                return Forbid();
            string oldAttachments = updateTaskSubmissionReviewDto.OldAttachments;
            string actualAttachments = updateTaskSubmissionReviewDto.ActualAttachments;
            List<Attachment> commentAttachments = actualAttachments == null ? new List<Attachment>() : JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
            List<Attachment> newFiles = ProcessNewFiles(updateTaskSubmissionReviewDto.NewFiles, updateTaskSubmissionReviewDto.SubmissionReviewAttachmentsUid);
            if (newFiles != null)
                commentAttachments.AddRange(newFiles);
            UpdateTaskSubmissionReviewCommand command = _mapper.Map<UpdateTaskSubmissionReviewCommand>(updateTaskSubmissionReviewDto);
            command.Attachments = commentAttachments.Count > 0 ? JsonConvert.SerializeObject(commentAttachments) : null;
            TaskSubmissionReviewViewModel review = await Mediator.Send(command);
            if (oldAttachments != null)
            {
                List<Attachment> oldFiles = JsonConvert.DeserializeObject<List<Attachment>>(oldAttachments);
                List<Attachment> currentFiles = JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
                List<Attachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            await _notificationsHub.MakeUserLoadNotificationsAsync(review.SubmissionAuthorId);
            return Ok(review);
        }

        /// <summary>
        /// Удаляет отзыв к решению задания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /tasksubmissionreviews/1
        /// </remarks>
        /// <param name="id">id отзыва (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Отзыв успешно удален</response>
        /// <response code="404">Отзыв не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DeleteTaskSubmissionReviewCommand command = new DeleteTaskSubmissionReviewCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            TaskSubmissionReviewViewModel review = await Mediator.Send(command);
            string attachmetsData = review.Attachments;
            if (attachmetsData != null)
            {
                List<Attachment> filesToDelete = JsonConvert.DeserializeObject<List<Attachment>>(attachmetsData);
                DeleteAttachments(filesToDelete);
            }
            await _notificationsHub.MakeUserLoadNotificationsAsync(review.SubmissionAuthorId);
            return NoContent();
        }

        private List<Attachment> ProcessNewFiles(List<IFormFile> files, Guid commentAttachmentsUid)
        {
            if (files == null)
                return null;
            List<Attachment> attachments = new List<Attachment>();
            string filePath = string.Empty;
            string savePath = string.Empty;
            string savePathTargetDirectory = string.Empty;
            foreach (var file in files)
            {
                filePath = $"submission_review{commentAttachmentsUid}_{file.FileName}";
                savePathTargetDirectory = file.ContentType.ToLower().StartsWith("image") ? "images" : "miscellaneous";
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"uploads/task-submissions-reviews-media/{savePathTargetDirectory}", filePath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                attachments.Add(new Attachment()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Length = file.Length,
                    Path = $"{_appDomain}uploads/task-submissions-reviews-media/{savePathTargetDirectory}/{filePath}",
                    Extension = Path.GetExtension(filePath),
                });
            }
            return attachments;
        }

        private void DeleteAttachments(List<Attachment> attachmentsToDelete)
        {
            foreach (Attachment attachment in attachmentsToDelete)
            {
                string path = attachment.Path
                    .Replace(_appDomain, $"{_webHostEnvironment.ContentRootPath}/");
                System.IO.File.Delete(path);
            }
        }
    }
}
