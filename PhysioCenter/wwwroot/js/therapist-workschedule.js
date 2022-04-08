var date = new Date();
var startDate = new Date(date.setHours(date.getHours() + 1));
var endDate = new Date(date.setMonth(date.getMonth() + 2))

$(document).ready(function () {
    $(".datepicker").datetimepicker({
        format: 'dd/mm/yyyy',
        clearBtn: true,
        minuteStep: 60,
        minView: 2,
        daysOfWeekDisabled: [0, 6],
        weekStart: 1,
        startDate: startDate,
        endDate: endDate,
        autoclose: true,
    })
})