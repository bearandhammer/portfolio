/* TODO - full minification */
document.querySelector('#table-container').addEventListener('click', e => {
    if (e.target.matches('.player-row, .player-row *')) {
        window.location.href = `/profile?name=${ e.target.closest('.player-row').children[1].innerText.trim() }`;
    }
});