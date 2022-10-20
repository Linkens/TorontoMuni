/* Toggle between adding and removing the "responsive" class to topnav when the user clicks on the icon */

function AnyFileSaveAs(bytesBase64, mimeType, fileName) {
    var fileUrl = "data:" + mimeType + ";base64," + bytesBase64;
    fetch(fileUrl)
        .then(response => response.blob())
        .then(blob => {
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: mimeType });
            link.download = fileName;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });
}

function ScrollToSmooth(PageID) {
    document.getElementById(PageID).scrollIntoView({ behavior: 'smooth' })
}

function Throttle(ID, Time) {
    if (Time > 0) {
        document.getElementById(ID).disabled = true;
        setTimeout(function () {
            document.getElementById(ID).disabled = false;
        }, Time*1000);
    }
}