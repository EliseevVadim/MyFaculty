﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.TaskSubmissions.Commands.CreateTaskSubmission;
using MyFaculty.Application.Features.TaskSubmissions.Commands.DeleteTaskSubmission;
using MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission;
using MyFaculty.Application.Features.TaskSubmissions.Queries.GetMineSubmissionsForTask;
using MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionInfo;
using MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionsForTask;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
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
    public class TaskSubmissionsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string _appDomain;

        public TaskSubmissionsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Возвращает список решений, принадлежащих кокретному заданию
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /tasksubmissions/task/1
        /// </remarks>
        /// <param name="id">id задания (integer)</param>
        /// <returns>Возвращает объект TaskSubmissionsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Задание не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("task/{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskSubmissionsListViewModel>> GetByTaskId(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetSubmissionsForTaskQuery query = new GetSubmissionsForTaskQuery()
            {
                ClubTaskId = id,
                IssuerId = requesterId
            };
            TaskSubmissionsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список решений текущего пользователя, принадлежащих кокретному заданию
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /tasksubmissions/mine/task/1
        /// </remarks>
        /// <param name="id">id задания (integer)</param>
        /// <returns>Возвращает объект TaskSubmissionsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Задание не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("mine/task/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskSubmissionsListViewModel>> GetMineSubmissionsByTaskId(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetMineSubmissionsForTaskQuery query = new GetMineSubmissionsForTaskQuery()
            {
                ClubTaskId = id,
                IssuerId = requesterId
            };
            TaskSubmissionsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает решение задания по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /tasksubmissions/1
        /// </remarks>
        /// <param name="id">id решения (integer)</param>
        /// <returns>Возвращает объект TaskSubmissionViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Решение не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskSubmissionViewModel>> Get(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetSubmissionInfoQuery query = new GetSubmissionInfoQuery()
            {
                Id = id,
                IssuerId = requesterId
            };
            TaskSubmissionViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новое решение задания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /tasksubmissions
        /// {
        ///     "title": "string",
        ///     "textContent": "string",
        ///     "submissionAttachments": "list of files",
        ///     "taskId": 1,
        ///     "authorId": 1
        /// }
        /// </remarks>
        /// <param name="createTaskSubmissionDto">Объект CreateCommentDto</param>
        /// <returns>Возвращает объект TaskSubmissionViewModel</returns>
        /// <response code="201">Решение успешно создано</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskSubmissionViewModel>> Create([FromForm] CreateTaskSubmissionDto createTaskSubmissionDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createTaskSubmissionDto.AuthorId)
                return Forbid();
            List<Attachment> sumbissionAttachments = new List<Attachment>();
            Guid sumbissionAttachmentsUid = Guid.NewGuid();
            List<Attachment> newFiles = ProcessNewFiles(createTaskSubmissionDto.SubmissionAttachments, sumbissionAttachmentsUid);
            if (newFiles != null)
                sumbissionAttachments.AddRange(newFiles);
            CreateTaskSubmissionCommand command = _mapper.Map<CreateTaskSubmissionCommand>(createTaskSubmissionDto);
            command.Attachments = sumbissionAttachments.Count > 0 ? JsonConvert.SerializeObject(sumbissionAttachments) : null;
            command.SubmissionAttachmentsUid = sumbissionAttachmentsUid;
            TaskSubmissionViewModel submission = await Mediator.Send(command);
            return Created(nameof(TaskSubmissionsController), submission);
        }

        /// <summary>
        /// Редактирует решение задания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /tasksubmissions
        /// {
        ///     "id": 1,
        ///     "title": "string",
        ///     "textContent": "string",
        ///     "oldAttachments": "json",
        ///     "actualAttachments": "json",
        ///     "newFiles": "list of files",
        ///     "submissionAttachmentsUid": "guid"
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="updateTaskSubmissionDto">Объект UpdateTaskSubmissionDto</param>
        /// <returns>Возвращает объект TaskSubmissionViewModel</returns>
        /// <response code="200">Решение успешно обновлено</response>
        /// <response code="404">Решение не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPut]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskSubmissionViewModel>> Update([FromForm] UpdateTaskSubmissionDto updateTaskSubmissionDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateTaskSubmissionDto.IssuerId)
                return Forbid();
            string oldAttachments = updateTaskSubmissionDto.OldAttachments;
            string actualAttachments = updateTaskSubmissionDto.ActualAttachments;
            List<Attachment> commentAttachments = actualAttachments == null ? new List<Attachment>() : JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
            List<Attachment> newFiles = ProcessNewFiles(updateTaskSubmissionDto.NewFiles, updateTaskSubmissionDto.SubmissionAttachmentsUid);
            if (newFiles != null)
                commentAttachments.AddRange(newFiles);
            UpdateTaskSubmissionCommand command = _mapper.Map<UpdateTaskSubmissionCommand>(updateTaskSubmissionDto);
            command.Attachments = commentAttachments.Count > 0 ? JsonConvert.SerializeObject(commentAttachments) : null;
            TaskSubmissionViewModel submission = await Mediator.Send(command);
            if (oldAttachments != null)
            {
                List<Attachment> oldFiles = JsonConvert.DeserializeObject<List<Attachment>>(oldAttachments);
                List<Attachment> currentFiles = JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
                List<Attachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            return Ok(submission);
        }

        /// <summary>
        /// Удаляет решение задания по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /tasksubmissions/1
        /// </remarks>
        /// <param name="id">id решения (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Решение успешно удалено</response>
        /// <response code="404">Решение не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DeleteTaskSubmissionCommand command = new DeleteTaskSubmissionCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            TaskSubmissionViewModel submission = await Mediator.Send(command);
            string attachmetsData = submission.Attachments;
            if (attachmetsData != null)
            {
                List<Attachment> filesToDelete = JsonConvert.DeserializeObject<List<Attachment>>(attachmetsData);
                DeleteAttachments(filesToDelete);
            }
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
                filePath = $"submission_{commentAttachmentsUid}_{file.FileName}";
                savePathTargetDirectory = file.ContentType.ToLower().StartsWith("image") ? "images" : "miscellaneous";
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"uploads/task-submissions-media/{savePathTargetDirectory}", filePath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                attachments.Add(new Attachment()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Length = file.Length,
                    Path = $"{_appDomain}uploads/task-submissions-media/{savePathTargetDirectory}/{filePath}",
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
