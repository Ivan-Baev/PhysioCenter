function getTherapistServices(e) {
    e.preventDefault();
    var select = $("#therapistDropDown").val();
    $.ajax({
        url: "/Admin/Appointments/GetTherapistServices",
        method: "GET",
        data: { 'id': select },
        success: function (data) {
            console.log(data);
            $('#servicesDropDown').html('');
            var options = '<option value="" disabled selected hidden>Choose a Service...</option>';

            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].value + '">' + data[i].text + '</option>';
            }
            $('#servicesDropDown').append(options);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

document.querySelector('#therapistDropDown').addEventListener('change', getTherapistServices, false);