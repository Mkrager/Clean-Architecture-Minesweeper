const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7171/hub/notifications")
    .build();

connection.on("GameStateUpdated", function (gameState) {
    updateBoard(gameState.newlyOpenedCells);
    updateBoard(gameState.updatedCell);
});

connection.start();

document.getElementById('game-board').addEventListener('click', function (e) {
    const td = e.target.closest('td.closed');
    if (!td) return;

    const x = td.getAttribute('data-x');
    const y = td.getAttribute('data-y');

    fetch(`/minesweeper/openCell`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            gameId: window.gameId,
            x: x,
            y: y
        })
    });
});

document.getElementById('game-board').addEventListener('contextmenu', function (e) {
    e.preventDefault();

    const td = e.target.closest('td');
    if (!td || td.classList.contains('opened')) return;

    const x = td.getAttribute('data-x');
    const y = td.getAttribute('data-y');

    fetch('/minesweeper/toggleFlag', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            gameId: window.gameId,
            x: parseInt(x),
            y: parseInt(y)
        })
    })
});

function updateBoard(cells) {
    if (!cells) return;

    if (!Array.isArray(cells)) {
        cells = [cells];
    }

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
        } else if (cell.hasFlag) {
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
