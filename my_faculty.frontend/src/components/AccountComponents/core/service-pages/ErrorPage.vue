<template>
    <div>
        <div class="test-error">
            <div class="sign">
                <div class="neon-blue" id="title">
                    {{ codeAsArray[0] }}
                    <span id="fade">{{ codeAsArray[1] }}</span>
                    {{ codeAsArray[2] }}
                </div>
                <div class="neon-blue">{{ message }}<br>
                    <span class="neon-purple" id="trav">My</span>
                    <span class="neon-elov">Faculty</span></div>
            </div>
        </div>
        <router-link to="/" class="button">На главную</router-link>
    </div>
</template>

<script>
export default {
    name: "ErrorPage",
    props: ['errorCode', 'message'],
    computed: {
        codeAsArray() {
            return this.errorCode.split('');
        }
    },
    mounted() {
        this.$backgroundColorService.setBackgroundForErrorPage();
    },
    destroyed() {
        this.$backgroundColorService.restoreBackground();
    }
}
</script>

<style lang="css">
@import url("//fonts.googleapis.com/css?family=Nixie+One");
@import url("//fonts.googleapis.com/css?family=League+Script");

.test-error {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: center;
    align-items: center;
    background: #313131;
    font-family: 'Nixie One', Helvetica, Arial, sans-serif;
    font-size: 50px;
}

.sign {
    -ms-transform: rotate(-3deg);
    -webkit-transform: rotate(-3deg);
    transform: rotate(-3deg);
}

#title {
    font-size: 110px;
}

#trav {
    -webkit-animation: blink 0.01s infinite alternate;
    -moz-animation: blink 0.01s infinite alternate;
    -o-animation: blink 0.01s infinite alternate;
    animation: blink 0.01s infinite alternate;
}

#fade {
    opacity: 0.8;
    color: #ebffff;
    text-shadow: 2px 2px 1px rgba(0, 0, 0, 0.3), 0 0 15px #fff, 0 0 10px #38eeff, 0 0 50px #38eeff;
    -webkit-animation: fade 3s infinite alternate;
    -moz-animation: fade 3s infinite alternate;
    -o-animation: fade 3s infinite alternate;
    animation: fade 3s infinite alternate;
}

.neon-blue {
    margin: 0 auto;
    text-align: center;
    color: #ebffff;
    text-shadow: 2px 2px 1px rgba(0, 0, 0, 0.3), 0 0 15px #fff, 0 0 10px #38eeff, 0 0 50px #38eeff;
}

.neon-purple {
    font-family: 'League Script', Helvetica, Arial, sans-serif;
    font-size: 50px;
    margin: 0 auto;
    text-align: center;
    color: #ccf;
    text-shadow: 2px 2px 1px rgba(0, 0, 0, 0.5), 0 0 20px #fff, 0 0 10px #ffffff, 0 0 50px #ffffff;
}

.neon-elov {
    font-family: 'League Script', Helvetica, Arial, sans-serif;
    font-size: 50px;
    margin: 0 auto;
    text-align: center;
    color: #ccf;
    text-shadow: 2px 2px 1px rgba(0, 0, 0, 0.5), 0 0 20px #FFFF00, 0 0 10px #FFD700, 0 0 50px #FFD700;
}

@-moz-keyframes blink {
    70% {
        opacity: 0.7;
    }
}

@-webkit-keyframes blink {
    45% {
        opacity: 0.5;
    }
}

@-o-keyframes blink {
    70% {
        opacity: 0.7;
    }
}

@keyframes blink {
    70% {
        opacity: 0.7;
    }
}

@-moz-keyframes fade {
    40% {
        opacity: 0.8;
    }
    42% {
        opacity: 0.1;
    }
    43% {
        opacity: 0.8;
    }
    45% {
        opacity: 0.1;
    }
    46% {
        opacity: 0.8;
    }
}

@-webkit-keyframes fade {
    40% {
        opacity: 0.8;
    }
    42% {
        opacity: 0.1;
    }
    43% {
        opacity: 0.8;
    }
    45% {
        opacity: 0.1;
    }
    46% {
        opacity: 0.8;
    }
}

@-o-keyframes fade {
    40% {
        opacity: 0.8;
    }
    42% {
        opacity: 0.1;
    }
    43% {
        opacity: 0.8;
    }
    45% {
        opacity: 0.1;
    }
    46% {
        opacity: 0.8;
    }
}

@keyframes fade {
    40% {
        opacity: 0.8;
    }
    42% {
        opacity: 0.1;
    }
    43% {
        opacity: 0.8;
    }
    45% {
        opacity: 0.1;
    }
    46% {
        opacity: 0.8;
    }
}

.button {
    text-decoration: none;
    position: absolute;
    left: 50%;
    width: 800px;
    margin-left: -400px;
    cursor: pointer;
    font-family: 'League Script', Helvetica, Arial, sans-serif;
    font-size: 20px;
    display: inline-block;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    box-sizing: border-box;
    padding: 10px;
    border: none;
    color: white !important;
    text-decoration: normal;
    text-align: center;
    -o-text-overflow: clip;
    text-overflow: clip;
    white-space: pre;
    text-shadow: 0 0 10px rgba(255, 255, 255, 1), 0 0 20px rgba(255, 255, 255, 1), 0 0 30px rgba(255, 255, 255, 1), 0 0 40px #ff00de, 0 0 70px #ff00de, 0 0 80px #ff00de, 0 0 100px #ff00de;
    -webkit-transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
    -moz-transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
    -o-transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
    transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
}

.button:hover {
    text-shadow: 0 0 10px rgba(255, 255, 255, 1), 0 0 20px rgba(255, 255, 255, 1), 0 0 30px rgba(255, 255, 255, 1), 0 0 40px #00ffff, 0 0 70px #00ffff, 0 0 80px #00ffff, 0 0 100px #00ffff;
}
</style>