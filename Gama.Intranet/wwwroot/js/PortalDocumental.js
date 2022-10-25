
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
        html += '<div class="col-lg-3 mb-2 contenedor">'
        html += '    <div class="card bg-light rounded p-4 mb-4">'
        html += '        <center><i class="fa fa-3x fa-folder-open text-primary mb-4"></i></center>'
        html += '        <div class=contenedor > <center><h7 class="mb-4">' + ReplaceDirectory(element).split(" ").slice(-1)[0] + '<span class="d-block text-body"></span></h7></center></div>'
        html += '        <div class="card-footer bg-light" > <center><button type="button" onclick="OpenFolder(' + "'" + ReplaceDirectory(element).split(" ").slice(-1)[0] + "'" + ')" class="btn btn-info">Abrir</button></center></div>'
        html += '    </div>'
        html += '</div>'

    });
    $("#files-area").append(html);
    html = '';

    files.forEach((element) => {
        html += '<div class="col-lg-3 mb-2 contenedor">'
        html += '    <div class="card bg-light rounded p-4 mb-4">'
        html += '        <center><i class="fa fa-3x fa-file-pdf text-primary mb-4"></i></center>'
        html += '        <div class=contenedor > <center><h7 class="mb-4">' + ReplaceDirectory(element).split(" ").slice(-1)[0] + '<span class="d-block text-body"></span></h7></center></div>'
        html += '        <div class="card-footer bg-light" > <center><button type="button" onclick="Downloadfile()" class="btn btn-info">PDF</button></center></div>'
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
    CurrentRoute.push(name);

    UpdateSiteMap();

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

function UpdateSiteMap() {
    $('#folders-nav').html(null);
    var html = '';
    html += '<li class="breadcrumb-item disabled"><a href="#" ><i class="fa fa-2.5px  fa-chevron-left"></i></a></li>'
    CurrentRoute.forEach((element) => {
        
        html += '<li class="breadcrumb-item"><a href="#">' + element + '</a></li>'

        /*html += '<li class="breadcrumb-item active" aria-current="page">Data</li>'*/

    });

    $('#folders-nav').append(html);
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

