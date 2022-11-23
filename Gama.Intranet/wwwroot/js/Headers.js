
$(document).ready(function () {
    var html = '';
    debugger;
    $.ajax({
        type: "GET",
        url: getHostName() + "/Auth/LoadHeadersAdmin",
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {

            html = ''
            html =  '<li class="nav-item mr-5">'
            html +=     '<div class="btn-group" >'
            html +=         '<button type="button" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">'
            html +=             '<i class="fa fa-cog" aria-hidden="true"></i>'
            html +=         '</button>'
            html +=         '<div class="dropdown-menu dropdown-menu-right">'
            html +=             '<button class="dropdown-item" type="button" onclick="redirect(this)">Bitacora</button>'
            html +=             '<button class="dropdown-item" type="button" onclick="redirect(this)">Mantenimiento Usuarios</button>'
            html +=             '<button class="dropdown-item" type="button" onclick="redirect(this)">Mantenimiento General</button>'
            html +=             '<button class="dropdown-item" type="button" onclick="LogOut()">Cerrar sesion</button>'
            html +=         '</div>'
            html +=     '</div >'
            html += '</li>'

            $('#login').html(html);
            /*html += '<a href="~/usuario/salir" class="btn btn-sm btn-outline-primary">Cerrar sesion</a>'*/


        },
        error: function (err) {
            $.ajax({
                type: "GET",
                url: getHostName() + "/Auth/LoadHeadersUser",
                dataType: "json",
                contentType: "Application/json",
                headers: {
                    Authorization: 'Bearer ' + window.localStorage.getItem("token")
                },
                success: function (result) {
                    
                    html = '<li class="nav-item mr-5">'
                    html += '   <a href="#" onclick="LogOut()" class="btn btn-sm btn-outline-primary">Cerrar sesion</a>'
                    html += '</li>'
                    $('#login').html(html);

                },
                error: function (err) {
                    
                    html = '<li class="nav-item mr-5">'
                    html += '   <a href="' + getHostName() + '/usuario/ingresar" class="btn btn-sm btn-outline-primary">Iniciar sesion</a>'
                    html += '</li>'
                    

                    $('#login').html(html);
                },
            });
        },
    });


})

function LogOut() {
    debugger;
    window.localStorage.removeItem("token")
    window.location = getHostName();
}
function redirect(page) {
    debugger;
    window.location = getHostName() + '/home/' + page.innerText.replace(' ', '');
}


