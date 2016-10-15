﻿(function () {
    $("#selectLanguage select").change(function () {
        $(this).parent().submit();
    });
    $(".datepicker").datepicker($.datepicker.regional["el"]);
    $(".datepicker").datepicker("option", "changeYear", true);
    $(".datepicker").datepicker("option", "changeMonth", true);
    $(".datepicker").datepicker("option", "dateFormat", "d/m/yy");

    $(document).on('keyup', '.numbertextbox', function () {
        var text = $(this).val();
        text = text.toString().replace(/,/g, '.');
        $(this).val(text);
    });

    $(document).on('keydown', '.numbertextbox', function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $(document).on('mouseup', '.numbertextbox', function () {
        $(this).select();
    });


}());

$(document).ready(function () {
    //var url = "/Home/GetCulture/";
    //$.get(url, function (data) {
    //    alert(data);
    //});
});