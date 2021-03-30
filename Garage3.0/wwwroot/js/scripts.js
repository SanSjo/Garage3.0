//If parking form is valid the confirmation modal opens

$("#submit-button").click(function (event) {
    event.preventDefault()

    if ($('#form').valid()) {
        $('.modal').modal('show')
    }
});