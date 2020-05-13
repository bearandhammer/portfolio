function performCopy() {
    document.querySelector('#JeffIpsumContentTextArea').select();
    document.execCommand('Copy');
}

function copyToClipboard(buttonElement) {
    if (buttonElement) {
        buttonElement.innerText = "Copied!";

        performCopy();

        setTimeout(() => buttonElement.innerText = "Copy to Clipboard", 2000);
    }
} 

document.querySelector('#CopyToClipboardButton').addEventListener('click', el => copyToClipboard(el.target));