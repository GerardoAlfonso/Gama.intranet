
//var CurrentRoute = [];

$(document).ready(function () {

    LoadPath();
    GetRootFiles();

    function LoadPath() {
        $.ajax({
            type: "GET",
            url: getHostName() + "/Files/GetPublicPath",
            dataType: "json",
            contentType: "Application/json",
            success: function (result) {
                if (result.status == 1) {
                    debugger;
                    sessionStorage.setItem("path", result.data[0]);
                }
                else {
                    alert(result.message)
                }
            },
            error: function (err) {
                alert("error");
            }
        });
    }

   
});

function PrintFilesAndFolders(folders, files) {

    $("#files-area").html(null);
    var html = '';
    var name = "";
    folders.forEach((element) => {
        html += '<div class="col-lg-3 mb-2">'
        html += '    <div class="service-item rounded p-4 mb-4">'
        html += '        <center><i class="fa fa-3x fa-folder-open text-primary mb-4" id="hed-icono"></i></center>'
        html += '        <center><h4 class="mb-4">' + ReplaceDirectory(element).split(" ").slice(-1)[0] + '<span class="d-block text-body"></span></h4></center>'
        html += '        <center><p class="m-0">Vero amet vero eos kasd justo ipsum diam sed elitr</p></center>'
        html += '        <br />'
        html += '        <center><button type="button" class="btn btn-info" onclick="OpenFolder(' + "'" + ReplaceDirectory(element).split(" ").slice(-1)[0] + "'" + ')" data-toggle="button" aria-pressed="false" autocomplete="off">ABRIR</button></center>'
        html += '    </div>'
        html += '</div>'

    });
    $("#files-area").append(html);
    html = '';

    files.forEach((element) => {
        html += '<div class="col-lg-3 mb-2">'
        html += '    <div class="service-item rounded p-4 mb-4">'
        html += '        <center><i class="fa fa-3x fa-file-pdf text-primary mb-4"></i></center>'
        html += ''
        html += '        <center><h4 class="mb-4">' + ReplaceDirectory(element).split(" ").slice(-1)[0] + '<span class="d-block text-body"></span></h4></center>'
        html += '        <center><p class="m-0">Vero amet vero eos kasd justo ipsum diam sed elitr</p></center>'
        html += '        <br />'
        html += '        <center><button type="button" class="btn btn-info" onclick="Downloadfile()" data-toggle="button" aria-pressed="false" autocomplete="off">Descargar PDF</button></center>'
        html += '    </div>'
        html += '</div>'

    });
    $("#files-area").append(html);

}



//function OpenFolder() {
//    $("#files-area").append(null);

//}

function AddFolderToRoute(folder) {
    sessionStorage.setItem("path", sessionStorage.getItem("path") + "\\" + folder);
}

function ReplaceDirectory(directory) {
    return directory.replace(/\\/g, " ");

}


// esto no sale hoy xD
function Downloadfile() {

    
    debugger;
    window.location = '/Files/DownloadPublic?name=' + "btncan.png" + '&amp';

    

}

//function UploadFile() {

//}


//
function OpenFolder(name) {
    $("#files-area").html(null);
    // create a new route
    AddFolderToRoute(name);
    //CurrentRoute.push(name);

    debugger;
    var obj =
    {
        Route: sessionStorage.getItem("path"),
        Token: "",
        IdUser: 0
    }

    $.ajax({
        type: "POST",
        url: getHostName() + "/Files/GetPublicFilesToFolder",
        dataType: "json",
        contentType: "Application/json",
        data: JSON.stringify(obj),
        success: function (result) {

            if (result.status == 1) {
                PrintFilesAndFolders(result.folders, result.files)
            }
            else {
                alert(result.message)
            }
        },
        error: function (error) {
            alert("error");
        }
    });
}

//
function GetRootFiles() {
    $.ajax({
        type: "GET",
        url: getHostName() + "/Files/GetPublicFiles",
        dataType: "json",
        contentType: "Application/json",
        success: function (result) {
            if (result.status == 1) {
                PrintFilesAndFolders(result.folders, result.files)
            }
            else {
                alert(result.message)
            }
        },
        error: function (error) {
            alert("error");
        }
    });
}