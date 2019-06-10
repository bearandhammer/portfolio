/* TODO - full minification */

// Monitor the table rows for a click event (redirecting to the profile view on click)
document.querySelector('#table-container').addEventListener('click', e => {
    if (e.target.matches('.player-row, .player-row *')) {
        window.location.href = `/profile?name=${ e.target.closest('.player-row').children[1].innerText.trim() }&type=${ e.target.closest('tbody').dataset.type }`;
    }
});