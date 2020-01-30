function previous() {
    let page = document.getElementById("previouspage").value;
    page = Number(page) - Number(1);

    $.ajax({
        type: 'GET',
        url: '/Home/Table',
        data: {
            id: page
        },
        success: function (response) {
            $('#replace').html(response);
        }
    })
}

function next() {
    let page = document.getElementById("currentpage").value;
    page = Number(page) + Number(1);

    $.ajax({
        type: 'GET',
        url: '/Home/Table',
        data: {
            id: page
        },
        success: function (response) {
            $('#replace').html(response);
        }
    })
}

window.addEventListener("load", () => {
    $.ajax({
        type: 'GET',
        url: '/Home/Table',
        data: {
            id: 1
        },
        success: function (response) {
            $('#replace').html(response);
        }
    })
});

function DownloadPDF(value) {
    window.location.replace(`/SpaceX/Details?id=${value}`);
}