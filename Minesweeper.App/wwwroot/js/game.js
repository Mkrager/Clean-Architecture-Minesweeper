const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7171/hub/notifications")
    .build();

connection.on("GameStateUpdated", function (gameState) {
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

    fetch(`/minesweeper/openCell`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ gameId: window.gameId, x, y })
    })
        .then(res => res.json())
        .then(result => {
            if (result.status === 1) {
                fetch(`/minesweeper/getGameState?gameId=${window.gameId}`)
                    .then(res => res.json())
                    .then(fullState => updateBoard(fullState.cells, true, x, y));
            }
        })
        .catch(console.error);
});

document.getElementById('game-board').addEventListener('contextmenu', function (e) {
    e.preventDefault();

    const td = e.target.closest('td');
    if (!td || td.classList.contains('opened')) return;

    const x = parseInt(td.getAttribute('data-x'), 10);
    const y = parseInt(td.getAttribute('data-y'), 10);

    fetch('/minesweeper/toggleFlag', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ gameId: window.gameId, x, y })
    }).catch(console.error);
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