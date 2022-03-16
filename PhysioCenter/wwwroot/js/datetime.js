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
        datesDisabled: ['2022-03-20'],
        autoclose: true,
        onRenderHour: function (date) {
            if (disabledtimes_mapping.indexOf(formatDate(date) + ":" + date.getUTCHours()) > -1) {
                return ['disabled'];
            }
        },
        clearBtn: true,
        todayBtn: true,
        todayHighlight: true,
        minuteStep: 60,
        minView: 1
    });
});