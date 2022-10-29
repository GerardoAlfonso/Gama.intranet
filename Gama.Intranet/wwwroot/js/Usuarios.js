﻿$(document).ready(function () {

    LoadUsers();
    LoadControls();


    function DeleteUser() {

    }

    function AddUser() {

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
});

var dataSet = [];
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

                data: JSON.stringify(obj) ,
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
                            '   <a href="#" class="delete" title="Permisos" data-bs-toggle="modal" data-bs-target="#exampleModalPermisos" >' +
                            '       <i class="fa fa-1x fa-exclamation" title="Permisos"></i>' +
                            '   </a >' +
                            '</center > '
                        ])
                });
                console.log(dataSet);
                $('#example').DataTable({
                    data: dataSet
                });
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
            alert("error");
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
            alert("error");
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
            alert("error");
        }
    });
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
                debugger;
                $('#SelectRole').append(html)
            }
            else {
                alert(result.message)
            }
        },
        error: function (err) {
            alert("error");
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
                debugger;
                $('#SelectStatus').append(html)
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