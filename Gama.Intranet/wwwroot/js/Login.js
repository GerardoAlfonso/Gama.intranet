
$(document).ready(function () {

    // success validate
    $.validator.setDefaults({
        submitHandler: function () {
            sendLogin();
        }
    });
    // validate form
    $('#quickForm').validate({
        rules: {
            email: {
                required: true,
                email: false,
            },
            password: {
                required: true
                /*minlength: 5*/
            }
        },
        messages: {
            email: {
                required: "Debe ingresar un nombre de usuario",
                email: ""
            },
            password: {
                required: "Debe ingresar una contraseña",
                minlength: "Your password must be at least 5 characters long"
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });



    function sendLogin() {

        $('#modal').iziModal('open');

        var obj = {
            User: $('#email').val(),
            Password: $('#password').val()
        }
        debugger;
        $.ajax({
            type: "POST",
            url: getHostName() + "/Auth/LogIn",
            dataType: "json",
            contentType: "Application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                if (result.status == 1) {
                    window.localStorage.setItem("token", result.token);

                    setTimeout(() => {
                        $('#modal').iziModal('close');
                        window.location = getHostName();
                    }, "3000")
                    
                    
                }
                else {
                    alert(result.message)
                }
            },
            error: function (err) {
                alert("error");
            },
            complete: function () {

            }
        });
    }

    $("#modal").iziModal({
        /*timeout: 2000,*/
        overlayClose: false,
        closeOnEscape: false,
        timeoutProgressbar: true,
        pauseOnHover: false,
        timeoutProgressbarColor: 'rgba(255,255,255,0.5)',
        transitionIn: 'comingIn',
        transitionOut: 'comingOut',
        transitionInOverlay: 'fadeIn',
        transitionOutOverlay: 'fadeOut',
        width: 200,
    });


    $(document).on('click', '.trigger', function (event) {
        event.preventDefault();
        $('#modal').iziModal('open');
    });



});