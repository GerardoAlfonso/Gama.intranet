
$(document).ready(function () {

    LoadUsers();
   
    function LoadUsers() {

        var obj = {

        }

        $.ajax({
            type: "GET",
            url: getHostName() + "/Admin/GetUsers",
            dataType: "json",
            contentType: "Application/json",
            headers: {
                Authorization: 'Bearer ' + window.localStorage.getItem("token")
            },
            success: function (result) {
                if (result.status == 1) {
                    PrintTable(result.data);
                    
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

    }

    function DeleteUser() {

    }

    function AddUser() {

    }




    function PrintTable(list) {

        var html = '';
        $('#tbody').html(null);

        list.forEach((element) => {

            html += '<tr>'
            html += '    <td>1</td>'
            html += '    <td><a href="#">' + element.name + '</a></td>'
            html += '    <td>04/10/2013</td>'
            html += '    <td>Admin</td>'
            html += '    <td><span class="status text-success">&bull;</span> Active</td>'
            html += '    <td>'
            html += '        <a href="#" class="edit" title="Editar"><i class="fa fa-1x fa-pen" title="Editar" ></i></a>'
            html += '        <a href="#" class="delete" title="Delete"><i class="fa fa-1x fa-trash" title="Eliminar" ></i></a>'
            html += '    </td>'
            html += '</tr>'

        });

        $('#tbody').append(html);

    }


});