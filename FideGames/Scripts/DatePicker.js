$(function () {
    $('.datetimepicker').datepicker({
        format: 'dd-mm-yyyy',
        firstDay: 1,
        autoclose: true,
        todayBtn: false,
        pickerPosition: "bottom-left",
        language: 'es',
        weekStart: 1,
        todayHighLight: 1
    });
});