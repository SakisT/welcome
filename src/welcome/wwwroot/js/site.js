(function () {
    $("#selectLanguage select").change(function () {
        $(this).parent().submit();
    });
    $(".datepicker").datepicker($.datepicker.regional["el"]);
    $(".datepicker").datepicker("option", "changeYear", true);
    $(".datepicker").datepicker("option", "changeMonth", true);
    $(".datepicker").datepicker("option", "dateFormat", "d/m/yy");
}());