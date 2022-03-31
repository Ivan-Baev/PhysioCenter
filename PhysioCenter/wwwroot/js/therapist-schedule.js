var date = new Date();
var startDate = new Date(date.setHours(date.getHours() + 1));
var endDate = new Date(date.setMonth(date.getMonth() + 2))

function getTherapistSchedule(e) {
    $(".datepicker").datetimepicker('remove');

    e.preventDefault();
    var select = $("#therapistDropDown").val();
    $.ajax({
        url: "https://localhost:7124/api/Therapist/GetTherapistSchedule",
        method: "GET",
        data: { 'id': select },
        success: function (data) {
            var disabledtimes_mapping = data;
            console.log(data);

            function formatDate(datestr) {
                var date = new Date(datestr);
                var day = date.getDate(); day = day > 9 ? day : "0" + day;
                var month = date.getMonth() + 1; month = month > 9 ? month : "0" + month;
                return day + "/" + month + "/" + date.getFullYear();
            };

            $(".datepicker").datetimepicker({
                format: 'dd/mm/yyyy hh:00',
                onRenderHour: function (date) {
                    if (disabledtimes_mapping.indexOf(formatDate(date) + " " + date.getUTCHours()) > -1) {
                        return ['disabled'];
                    }
                },
                clearBtn: true,
                minuteStep: 60,
                minView: 1,
                daysOfWeekDisabled: [0, 6],
                weekStart: 1,
                startDate: startDate,
                endDate: endDate,
                hoursDisabled: [0, 1, 2, 3, 4, 5, 6, 7, 8, 13, 20, 21, 22, 23, 24],
                autoclose: true,
            });
        },
        error: function (err) {
            console.log(err);
        }
    })
}

document.querySelector('#therapistDropDown').addEventListener('change', getTherapistSchedule, false);