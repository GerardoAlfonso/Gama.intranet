
function Checkout() {


    $.ajax({
        type: "POST",
        url: getHostName() + "/Admin/Checkout",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + window.localStorage.getItem("token"));
        },
        contentType: "application/json",
        dataType: "json",
        success: function (_result) {
            
        },
        error: function (data) {
            window.location = getHostName() + '/usuario/ingresar';
        }
    });
}
Checkout();


// global objects

var dataSet = [];
var PermisosUsuario = [];
var IdCurrentUser = 0;


// startup
$(document).ready(function () {
    LoadUsers();
    LoadControls();

    // events
    $('#SelectCategories').on('change', function () {
        LoadSelectFolders();
    });


});


// ============================================= //

function del(_id) {
    Swal.fire({
        title: '¿Esta seguro que desea eliminar este usuario?',
        text: "¡Este accion no se puede revertir!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Confirmar!'
    }).then((result) => {
        if (result.isConfirmed) {
            var obj = { "Id": parseInt(_id) }

            $.ajax({
                type: "POST",
                url: getHostName() + "/Admin/DeleteUser",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + window.localStorage.getItem("token"));
                },
                contentType: "application/json",
                dataType: "json",

                data: JSON.stringify(obj),
                success: function (_result) {
                    if (_result.status == 1) {
                        RefreshTable()
                        Swal.fire(
                            'Eliminado!',
                            'Este registro ha sido Eliminado.',
                            'success'
                        )
                    }
                },
                error: function (data) {
                    Swal.fire(
                        'Error!',
                        'errormesagge generic error xD.',
                        'error'
                    )
                }
            });
        }
         
    })
}

function LoadUsers() {
    var users = $("#inactivos").is(":checked") ? "GetAllUsers" : "GetUsers";
    debugger;
    $.ajax({
        type: "GET",
        url: getHostName() + "/Admin/" + users,
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {
            if (result.status == 1) {
                result.data.forEach((element) => {
                    dataSet.push(
                        [
                            element.id,
                            element.name,
                            element.status,
                            element.loginAttempts,
                            element.role,
                            element.token != null ? element.token.substring(0, 15) + '...' : '...',
                            element.shouldChangePassword,
                            element.lastAccess != null ? element.lastAccess.substring(0, 10) : '...',
                            element.userCreation != null ? '' : element.userCreation,
                            '<a href="#" class="edit" onclick="LoadUser(' + element.id + ')" title="Editar" data-bs-toggle="modal" data-bs-target="#exampleModal">' +
                            '   <i class="fa fa-1x fa-pen" title="Editar"></i>' +
                            '</a > ' +
                            '<a href="#" class="delete" title="Delete" onclick="del(' + element.id + ')">' +
                            '   <i class="fa fa-1x fa-trash" title="Eliminar"></i>' +
                            '</a > ',
                            // permisos
                            '<center> ' +
                            '   <a href="#" class="delete" title="Permisos" onclick="GetPermissions('+ element.id +')" data-bs-toggle="modal" data-bs-target="#exampleModalPermisos" >' +
                            '       <i class="fa fa-1x fa-exclamation" title="Permisos"></i>' +
                            '   </a >' +
                            '</center > '
                        ])
                });
                $('#example').DataTable({ data: dataSet });
            }
            else {
                //alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });
}


function LoadUser(_id) {
    $.ajax({
        type: "GET",
        url: getHostName() + "/Admin/GetUser",
        dataType: "json",
        data: {id: _id},
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {
            if (result.status == 1) {

                $('#iduser').val(result.data.id);
                $('#name').val(result.data.name);
                $('#password').val(result.data.password);
                $("#SelectStatus").val(result.data.status);
                $("#SelectRole").val(result.data.role);

            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });
}


function GenerateRandomPassword() {
    $.ajax({
        type: "GET",
        url: getHostName() + "/Admin/GenerateRandomPassword",
        dataType: "json",
        data: { id: parseInt($('#iduser').val()) },
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {
            if (result.status == 1) {

                $('#password').val(result.data);

            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });
}


function CreateUser() {
    var obj =
    {
        "name": $('#name').val(),
        "password": $('#password').val(),
        "status": parseInt($('#SelectStatus option:selected').val()),
        "role": parseInt($('#SelectRole option:selected').val())
    }
    debugger;
    $.ajax({
        type: "POST",
        url: getHostName() + "/Admin/AddUser",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + window.localStorage.getItem("token"));
        },
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (result) {
            debugger;
            if (result.status == 1) {
                $('#exampleModal').modal('hide');
                Swal.fire(
                    '!Hecho!',
                    'Registro creado con exito',
                    'success'
                )
                RefreshTable()

            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });
}




function UpdateUser() {
    var obj = 
    {
        "Id": parseInt($('#iduser').val()),
        "name": $('#name').val(),
        "password": $('#password').val(),
        "status": parseInt($('#SelectStatus option:selected').val()),
        "role": parseInt($('#SelectRole option:selected').val())
    }
    debugger;
    $.ajax({
        type: "POST",
        url: getHostName() + "/Admin/UpdateUser",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + window.localStorage.getItem("token"));
        },
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (result) {
            debugger;
            if (result.status == 1) {
                $('#exampleModal').modal('hide');
                Swal.fire(
                    'Actualizado!',
                    'Registro actualizado con exito',
                    'success'
                )
                RefreshTable()

            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });
}


function LoadControls() {

    // cargar roles
    $.ajax({
        type: "GET",
        url: getHostName() + "/Admin/GetRoles",
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {
            if (result.status == 1) {

                var html = ''
                $('#SelectRole').html(html)

                result.data.forEach((element) => {
                    html += '<option value = "' + element.id + '" >' + element.description + '</option >'
                })
                $('#SelectRole').append(html)
            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });

    // cargar status
    $.ajax({
        type: "GET",
        url: getHostName() + "/Admin/GetStatus",
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {
            if (result.status == 1) {

                var html = ''
                $('#SelectStatus').html(html)

                result.data.forEach((element) => {
                    html += '<option value = "' + element.id + '" >' + element.name + '</option >'
                })
                $('#SelectStatus').append(html)
            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });

    // cargar listado de categorias por carpeta
    $.ajax({
        type: "GET",
        url: getHostName() + "/Files/GetFolderCategories",
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        success: function (result) {
            if (result.status == 1) {
                var html = ''
                $('#SelectCategories').html(null)

                result.data.forEach((element) => {
                    html += '<option value = "' + element.id + '" >' + element.nombre + '</option >'
                })
                $('#SelectCategories').html(html)
            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        },
        complete: function () {
            // cargar listado de carpetas por categoria
            LoadSelectFolders();
        }
    });

    

}


function LoadSelectFolders() {
    // cargar listado de carpetas
    var obj = { "Id": parseInt($('#SelectCategories').val()) }
    debugger
    $.ajax({
        type: "POST",
        url: getHostName() + "/Files/FoldersFromCategories",
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        data: JSON.stringify(obj),
        success: function (result) {
            if (result.status == 1) {
                debugger;
                var html = ''
                $('#SelectFolders').html(null)

                result.folders.forEach((element) => {
                    html += '<option value="' + element.id + '">' + element.name + '</option>'
                })
                $('#SelectFolders').html(html)
            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
        }
    });
}




function GetPermissions(_id) {
    var obj =
    {
        "Id": parseInt(_id)
    }

    // cargar roles
    $.ajax({
        type: "POST",
        url: getHostName() + "/Admin/GetPermissions",
        dataType: "json",
        contentType: "Application/json",
        headers: {
            Authorization: 'Bearer ' + window.localStorage.getItem("token")
        },
        data: JSON.stringify(obj),
        success: function (result) {
            if (result.status == 1) {

                debugger;
                var html = ''
                $('#tbody-archivos').html(html)

                result.data.forEach((element) => {
                    html += '<tr>'
                    html += '    <td>' + element.id + '</td>'
                    html += '    <td><a href="#">' + element.idFolder + '</a></td>'
                    html += '    <td>' + element.read + '</td>'
                    html += '    <td>' + element.write + '</td>'
                    html += '    <td>'
                    html += '       <a href = "#" class="delete" title = "Delete" onclick="delPermission(this)" > '
                    html += '           <i class="fa fa-1x fa-trash" title="Eliminar"></i>'
                    html += '       </a>'
                    html += '    </td>'
                    html += '</tr>'
                })
                
                $('#tbody-archivos').append(html)
                IdCurrentUser = obj.Id
            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            //alert("error");
        }
    });

}


function AgregarPermiso() {
    
    var obj = {
        "id": parseInt($('#SelectFolders option:selected').val()),
        "Folder": parseInt($('#SelectFolders option:selected').val()),
        "Read": $("#chkLectura").is(":checked"),
        "Write": $("#chkEscritura").is(":checked")
    }
    PermisosUsuario.push(obj);

    var html = ''
    html += '<tr>'
    html += '    <td></td>'
    html += '    <td><a href="#">' + $('#SelectFolders option:selected').text(), + '</a></td>'
    html += '    <td>' + obj.Read + '</td>'
    html += '    <td>' + obj.Write + '</td>'
    html += '    <td>'
    html += '       <a href = "#" class="delete" title = "Delete" onclick="del(' + '' + ')" > '
    html += '           <i class="fa fa-1x fa-trash" title="Eliminar"></i>'
    html += '       </a>'
    html += '    </td>'
    html += '</tr>'
    $('#tbody-archivos').append(html);

    debugger;
}

function UpdatePermissions() {

    var json = TableToJson($('#TablePermissions'));
    // delete button X
    //json.forEach(function (v) { delete v.no });
    json.forEach(function (v) { delete v.accion });
    json.forEach(function (v) {
        v.nombrearchivo = v.nombrearchivo.replace('<a href="#">', '').replace('</a>', '')
    });

    var obj = []
    json.forEach((element) => {
        obj.push({
            'IdUSer': IdCurrentUser,
            'escritura': element.escritura === 'true',
            'lectura': element.lectura === 'true',
            'nombrearchivo': element.nombrearchivo
        })
    });


    $.ajax({
        type: "POST",
        url: getHostName() + "/Admin/SavePermissions",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + window.localStorage.getItem("token"));
        },
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (result) {
            console.log(result);
            
            Swal.fire(
                'Actualizado!',
                'Registro actualizado con exito',
                'success'
            )
            $('#exampleModalPermisos').modal('hide');
        },
        error: function (err) {
            //alert("error");
        }
    });

    


    debugger;
}


// ============================================= //

function ButtonGuardar() {
    if ($('#iduser').val() == '') {
        CreateUser();
    }
    else {
        UpdateUser();
    }
}

function RefreshTable() {
    dataSet = [];
    $('#example').DataTable().destroy();
    LoadUsers();
}

function ClearModal() {
    $('#iduser').val(''),
    $('#name').val(''),
    $('#password').val('')
    $("#SelectStatus").val('0')
    $("#SelectRole").val('0')

}

// ============================================= //

function ReplaceDirectory(directory) {
    return directory.replace(/\\/g, " ");
}

function delPermission(element) {
    element.parentNode.parentNode.remove();
}


// function to convert html table into json data
function TableToJson(table) {
    var data = [];
    var headers = [];
    for (var i = 0; i < table[0].rows[0].cells.length; i++) {
        headers[i] = table[0].rows[0].cells[i].innerHTML.toLowerCase().replace(/ /gi, '');
    }
    for (var i = 1; i < table[0].rows.length; i++) {
        var tableRow = table[0].rows[i];
        var rowData = {};
        for (var j = 0; j < tableRow.cells.length; j++) {
            rowData[headers[j]] = tableRow.cells[j].innerHTML;
        }
        data.push(rowData);
    }
    return data;
}



//function PrintTable(list) {

//    var html = '';
//    $('#tbody').html(null);

//    list.forEach((element) => {

//        html += '<tr>'
//        html += '    <td>1</td>'
//        html += '    <td><a href="#">' + element.name + '</a></td>'
//        html += '    <td>04/10/2013</td>'
//        html += '    <td>Admin</td>'
//        html += '    <td><span class="status text-success">&bull;</span> Active</td>'
//        html += '    <td>'
//        html += '        <a href="#" class="edit" title="Editar"><i class="fa fa-1x fa-pen" title="Editar" ></i></a>'
//        html += '        <a href="#" class="delete" title="Delete"><i class="fa fa-1x fa-trash" title="Eliminar" ></i></a>'
//        html += '    </td>'
//        html += '</tr>'

//    });
//    $('#tbody').append(html);
//}