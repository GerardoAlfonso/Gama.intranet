function alert() {
    if ($('#name').val().length == 0) {
        Swal.fire({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            title: 'Error!',
            text: 'Debes completar todos los campos',
            icon: 'error',
            confirmButtonText: 'Cool'
        })
        
    }
}


