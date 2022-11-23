
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