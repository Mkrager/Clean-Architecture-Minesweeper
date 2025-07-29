const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7171/hub/notifications")
    .build();

let gameOverHandled = false;
let isFirstMoveDone = false;
let timerInterval = null;
let seconds = 0;

connection.on("GameStateUpdated", function (gameState) {
    if (gameState.status === 1 && !gameOverHandled) {
        gameOverHandled = true;
        clearInterval(timerInterval);

        connection.invoke("GetGameState", window.gameId)
            .then(fullState => {
                if (fullState && fullState.cells) {
                    updateBoard(fullState.cells, true, fullState.explodedX, fullState.explodedY);
                }
            })
            .catch(console.error);

        return;
    } else if (gameState.status === 2) {
        clearInterval(timerInterval);
        document.getElementById("winModal").classList.add("show");
    }


    if (gameState.allCells) {
        updateBoard(gameState.allCells);
    } else {
        const cells = [];
        if (Array.isArray(gameState.newlyOpenedCells)) {
            cells.push(...gameState.newlyOpenedCells);
        }
        if (gameState.updatedCell) {
            cells.push(gameState.updatedCell);
        }
        updateBoard(cells);
    }
});

connection.start()
    .then(() => connection.invoke("JoinGameGroup", window.gameId))
    .catch(err => console.error("SignalR Connection Error:", err));

document.getElementById('game-board').addEventListener('click', function (e) {
    const td = e.target.closest('td.closed');
    if (!td) return;

    const x = parseInt(td.getAttribute('data-x'), 10);
    const y = parseInt(td.getAttribute('data-y'), 10);

    if (!isFirstMoveDone) {
        isFirstMoveDone = true;
        startTimer();
    }

    connection.invoke("OpenCell", window.gameId, x, y).catch(console.error);
});

document.getElementById('game-board').addEventListener('contextmenu', function (e) {
    e.preventDefault();

    const td = e.target.closest('td');
    if (!td || td.classList.contains('opened')) return;

    const x = parseInt(td.getAttribute('data-x'), 10);
    const y = parseInt(td.getAttribute('data-y'), 10);

    connection.invoke("ToggleFlag", window.gameId, x, y).catch(console.error);
});

function updateBoard(cells, gameOver = false, explodedX = null, explodedY = null) {
    if (!cells) return;

    cells.forEach(cell => {
        const td = document.querySelector(`td[data-x='${cell.x}'][data-y='${cell.y}']`);
        if (!td) return;

        if (cell.isOpened) {
            td.classList.remove('closed');
            td.classList.add('opened');

            if (cell.hasMine) {
                td.innerHTML = `<img src="/lib/cells/blast.svg" alt="blast" width="24" height="24" />`;
            } else if (cell.adjacentMines === 0) {
                td.innerHTML = `<img src="/lib/cells/celldown.svg" alt="Down" width="24" height="24" />`;
            } else {
                td.innerHTML = `<img src="/lib/cells/cell${cell.adjacentMines}.svg" alt="cell${cell.adjacentMines}" width="24" height="24" />`;
            }
            return;
        }

        if (gameOver) {
            if (cell.hasMine) {
                td.classList.remove('closed');
                td.classList.add('opened');

                if (cell.x === explodedX && cell.y === explodedY) {
                    td.innerHTML = `<img src="/lib/cells/blast.svg" alt="exploded" width="24" height="24" />`;
                } else {
                    td.innerHTML = `<img src="/lib/cells/cellmine.svg" alt="blast" width="24" height="24" />`;
                }
                return;
            }

            if (cell.hasFlag && !cell.hasMine) {
                td.classList.remove('opened');
                td.classList.add('closed');
                td.innerHTML = `<img src="/lib/cells/falsemine.svg" alt="falsemine" width="24" height="24" />`;
                return;
            }

            td.classList.remove('opened');
            td.classList.add('closed');
            td.innerHTML = `<img src="/lib/cells/cellup.svg" alt="Closed" width="24" height="24" />`;
            return;
        }

        if (cell.hasFlag) {
            td.classList.remove('opened');
            td.classList.add('closed');
            td.innerHTML = `<img src="/lib/cells/cellflag.svg" alt="Flag" width="24" height="24" />`;
        } else {
            td.classList.remove('opened');
            td.classList.add('closed');
            td.innerHTML = `<img src="/lib/cells/cellup.svg" alt="Closed" width="24" height="24" />`;
        }
    });
}

function submitPlayerName() {
    const playerName = document.getElementById("playerNameInput").value.trim();
    if (!playerName) {
        alert("Enter your name");
        return;
    }

    fetch("/LeaderboardEntry/create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            gameId: window.gameId,
            playerName: playerName
        })
    })
        .then(() => {
            alert('Your result saved!');
            document.getElementById("winModal").style.display = "none";
        })
        .catch(err => {
            console.error("Error:", err);
        });
}

function updateCounter(elementId, value) {
    const str = value.toString().padStart(3, '0');
    const container = document.getElementById(elementId);

    const existingImgs = container.querySelectorAll('img');

    if (existingImgs.length !== 3) {
        container.innerHTML = '';
        for (let char of str) {
            const img = document.createElement('img');
            img.src = `/lib/counter/counter${char}.svg`;
            img.width = 24;
            container.appendChild(img);
        }
        return;
    }

    for (let i = 0; i < 3; i++) {
        const digit = str[i];
        const img = existingImgs[i];
        const newSrc = `/lib/counter/counter${digit}.svg`;

        if (img.src !== location.origin + newSrc) {
            img.src = newSrc;
        }
    }
}


updateCounter("timer-counter", 0);
updateCounter("mine-counter", 10);

function startTimer() {
    if (timerInterval !== null) return;

    timerInterval = setInterval(() => {
        updateCounter("timer-counter", ++seconds);
    }, 1000);
}