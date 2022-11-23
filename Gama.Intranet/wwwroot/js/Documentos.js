
var CurrentRoute = [];

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
                    sessionStorage.setItem("path", result.data[0] + "\\GAMA\\Public\\Documentos");
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
        html += '<div class="col-lg-3 mb-2 contenedor">'
        html += '    <div class="card bg-light rounded p-4 mb-4">'
        html += '        <center><i class="fa fa-3x fa-folder-open text-primary mb-4"></i></center>'
        html += '        <div class=contenedor > <center><h7 class="mb-4">' + element + '<span class="d-block text-body"></span></h7></center></div>'
        html += '        <div class="card-footer bg-light" > <center><button type="button" onclick="OpenFolder(' + "'" + element + "'" + ')" class="btn btn-info">Abrir</button></center></div>'
        html += '    </div>'
        html += '</div>'

    });
    $("#files-area").append(html);
    html = '';

    files.forEach((element) => {
        html += '<div class="col-lg-3 mb-2 contenedor">'
        html += '    <div class="card bg-light rounded p-4 mb-4">'
        html += '        <center><i class="fa fa-3x fa-file-pdf text-primary mb-4"></i></center>'
        html += '        <div class=contenedor > <center><h7 class="mb-4">' + element + '<span class="d-block text-body"></span></h7></center></div>'
        html += '        <div class="card-footer bg-light" > <center><button type="button" onclick="Downloadfile(' + "'" + element + "'" + ')" class="btn btn-info">PDF</button></center></div>'
        html += '    </div>'
        html += '</div>'

    });
    $("#files-area").append(html);

}

function AddFolderToRoute() {
    var route = '';
    route += sessionStorage.getItem("path");
    CurrentRoute.forEach((element) => {
        route += '\\' + element
    });
    sessionStorage.setItem("CurrentRoute", route);
}

//function ReplaceDirectory(directory) {
//    return directory.replace(/\\/g, " ");
//}


// esto no sale hoy xD
function Downloadfile(name) {

    window.location = '/Files/DownloadPublic?name=' + name + '&amp';
}

//function UploadFile() {

//}


//
function OpenFolder(name) {
    $("#files-area").html(null);
    // create a new route
    CurrentRoute.push(name);
    AddFolderToRoute();
    UpdateSiteMap();
    LoadFolder();
}

//
function GetRootFiles() {
    $.ajax({
        type: "GET",
        url: getHostName() + "/Files/GetDocumentosFiles",
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

function UpdateSiteMap() {
    $('#folders-nav').html(null);
    var html = '';
    html += '<li class="breadcrumb-item disabled" onclick="BackFolder()"><a href="#" ><i class="fa fa-2.5px  fa-chevron-left"></i></a></li>'
    CurrentRoute.forEach((element) => {
        html += '<li class="breadcrumb-item active" aria-current="page">' + element + '</li>'
    });
    $('#folders-nav').append(html);
}


function BackFolder() {
    CurrentRoute.pop();
    $("#files-area").html(null);
    // create a new route
    AddFolderToRoute();
    UpdateSiteMap();
    LoadFolder();
}


function LoadFolder() {
    var obj =
    {
        Route: sessionStorage.getItem("CurrentRoute"),
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


//$(function () {
//    $('.disabled a').click(function (ev) {
//        ev.preventDefault(); //Una de estas 2 líneas debería funcionar bien.
//        return false;
//    });
//});


////Funcion para remover la clase del icono de atras
//function prueba() {
//    $('.disabled').removeClass("disabled").addClass("active");
//    alert("hola");
//}

