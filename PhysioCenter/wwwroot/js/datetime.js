var date = new Date();
var startDate = new Date(date.setHours(date.getHours()+1));
var endDate = new Date(date.setMonth(date.getMonth() + 2))

$(document).ready(function () {
    var disabledtimes_mapping = ["03/16/2022:0", "03/30/2022:17", "03/30/2022:15"];

    function formatDate(datestr) {
        var date = new Date(datestr);
        var day = date.getDate(); day = day > 9 ? day : "0" + day;
        var month = date.getMonth() + 1; month = month > 9 ? month : "0" + month;
        return month + "/" + day + "/" + date.getFullYear();
    }

    $(".datepicker").datetimepicker({
        format: 'dd/mm/yyyy hh:00',
        onRenderHour: function (date) {
            if (disabledtimes_mapping.indexOf(formatDate(date) + ":" + date.getUTCHours()) > -1) {
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
        
    });
});