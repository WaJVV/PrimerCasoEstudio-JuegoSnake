// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// board
var blockSize = 20;
var rows = 20;
var cols = 20;
var board;
var context;

// snake head
var snakeX = blockSize * 5;
var snakeY = blockSize * 5;

var velocityX = 0;
var velocityY = 0;

var snakeBody = [];

// food
var foodX;
var foodY;

var gameOver = false;
var gameStart = false;
var snakeEat = false;

// control the speed
var updateInterval = 150; // in milliseconds
var lastUpdateTime = 0;

window.onload = function () {
    board = document.getElementById("board");
    board.height = rows * blockSize;
    board.width = cols * blockSize;
    context = board.getContext("2d"); // used for drawing on the board

    placeFood();
    document.addEventListener("keyup", changeDirection);
    requestAnimationFrame(gameLoop);

    drawChessboard(); // Draw the initial chessboard
}

function drawChessboard() {
    for (let row = 0; row < rows; row++) {
        for (let col = 0; col < cols; col++) {
            // Determine the color based on row and column
            const color = (row + col) % 2 === 0 ? '#18EF19' : '#A3F06C';

            // Set the fill color
            context.fillStyle = color;

            // Draw the tile
            context.fillRect(col * blockSize, row * blockSize, blockSize, blockSize);
        }
    }
}


function gameLoop(currentTime) {
    if (gameOver) {
        return;
    }

    if (currentTime - lastUpdateTime >= updateInterval) {
        lastUpdateTime = currentTime;
        update();
    }

    requestAnimationFrame(gameLoop);
}

function update() {
    context.clearRect(0, 0, board.width, board.height); // Clear canvas before redraw

    drawChessboard(); // Redraw the chessboard

    drawCircle(foodX, foodY, "black");

    if (snakeX === foodX && snakeY === foodY) {
        snakeBody.push([foodX, foodY]);
        placeFood();
        snakeEat = true;
    }

    for (let i = snakeBody.length - 1; i > 0; i--) {
        snakeBody[i] = snakeBody[i - 1];
    }
    if (snakeBody.length) {
        snakeBody[0] = [snakeX, snakeY];
    }

    snakeX += velocityX * blockSize;
    snakeY += velocityY * blockSize

    drawCircle(snakeX, snakeY, "orange");
    for (let i = 0; i < snakeBody.length; i++) {
        drawCircle(snakeBody[i][0], snakeBody[i][1], "orange");
    }

    // game over conditions
    if (snakeX < 0 || snakeX >= cols * blockSize || snakeY < 0 || snakeY >= rows * blockSize) {
        gameOver = true;
        alert("Game Over");
        return;
    }

    for (let i = 0; i < snakeBody.length; i++) {
        if (snakeX === snakeBody[i][0] && snakeY === snakeBody[i][1]) {
            gameOver = true;
            alert("Game Over");
            return;
        }
    }
}

function changeDirection(e) {
    if (gameStart) {
        if (e.code === "ArrowUp" && velocityY !== 1) {
            velocityX = 0;
            velocityY = -1;
        } else if (e.code === "ArrowDown" && velocityY !== -1) {
            velocityX = 0;
            velocityY = 1;
        } else if (e.code === "ArrowLeft" && velocityX !== 1) {
            velocityX = -1;
            velocityY = 0;
        } else if (e.code === "ArrowRight" && velocityX !== -1) {
            velocityX = 1;
            velocityY = 0;
        }
    }
}

document.addEventListener("keydown", function (event) {
    if (event.key === "ArrowUp" || event.key === "ArrowDown") {
        event.preventDefault();
    }
});

function placeFood() {
    foodX = Math.floor(Math.random() * cols) * blockSize;
    foodY = Math.floor(Math.random() * rows) * blockSize;
}

function drawCircle(x, y, color) {
    context.fillStyle = color;
    context.beginPath();
    context.arc(x + blockSize / 2, y + blockSize / 2, blockSize / 2, 0, 2 * Math.PI);
    context.fill();
}

let segundos = 0;
let minutos = 0;
const elementoContador = document.getElementById('contador');

let score = 0;
const elementoScore = document.getElementById('score');

function incrementarContador() {
    if (gameStart && !gameOver) {
        if (segundos < 59) {
            segundos++;
            elementoContador.textContent = "Tiempo: " + segundos + " seg";
            if (minutos != 0) {
                elementoContador.textContent = "Tiempo: " + minutos + " min " + segundos + " seg";
            }
        } else {
            minutos++;
            segundos = 0;
            elementoContador.textContent = "Tiempo: " + minutos + " min " + segundos + " seg";
        }
    }
}

function actualizarScore() {
    if (snakeEat && gameStart) {
        score += 10;
        elementoScore.textContent = 
        elementoScore.textContent = "Puntaje: " + score + " pts";
        snakeEat = false;
    }
}


const botonCambiarEstado = document.getElementById('cambiarEstado');

function cambiarEstado() {
    if (!gameStart) {
        gameStart = true;
        setInterval(incrementarContador, 1000);
        setInterval(actualizarScore, 1000);
    }

}

botonCambiarEstado.addEventListener('click', cambiarEstado);

document.getElementById('submitBtn').addEventListener('click', function () {
    document.getElementById('hiddenTiempo').value = minutos + " min " + segundos + " seg";
    document.getElementById('hiddenScore').value = score + " pts";
    document.forms[0].submit();
});