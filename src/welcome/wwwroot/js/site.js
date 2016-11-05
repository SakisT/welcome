(function () {
    $("#selectLanguage select").change(function () {
        $(this).parent().submit();
    });
    InitJQueryUI();
});

function InitJQueryUI() {
    $("#selectLanguage select").change(function () {
        $(this).parent().submit();
    });

    if (!$.datepicker.initialized) {
        $(document).mousedown($.datepicker._checkExternalClick)
            .find(document.body).append($.datepicker.dpDiv);
        $.datepicker.initialized = true;
    }
    $(".datepicker").datepicker($.datepicker.regional["el"]);
    $(".datepicker").datepicker("option", "changeYear", true);
    $(".datepicker").datepicker("option", "changeMonth", true);
    $(".datepicker").datepicker("option", "dateFormat", "d/m/yy");
    $(document).on('keyup', '.numbertextbox', function () {
        var text = $(this).val();
        var lang = $("#selectLanguage option:selected").val();
        if (lang == 'el-GR') {
            text = text.toString().replace('.', ',');
        }
        else {
            text = text.toString().replace(',', '.');
        }
        //text = text.toString().replace(/,/g, '.');
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
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || (e.keyCode > 105 && e.keyCode != 188))) {
            e.preventDefault();
        }
    });
    $(document).on('mouseup', '.numbertextbox', function () {
        $(this).select();
    });
}

$(document).on('ready',function (e) {
    $(document).on('click', '.reservationrow', function () {
        $('#reservation-data').load($(this).data('link'), null, function (data, status) {
            $('#reservationstayrooms').load($('#reservationstayrooms').data('link'), null, function (data, status) {
                $('#reservations-editreservation-contact').load($('#reservations-editreservation-contact').data('link'), null, function (data, status) {
                    InitJQueryUI();
                });
            });
        });
    });
    $(document).on('click', '#savereservationsbutton', function () {
        event.preventDefault();
        document.getElementById('stayroomsform').submit();
        var item = $(this).find('#reservationstayrooms').closest(':#stayroomsform');
        item.css('background-color', 'red');
        item.submit(function (event) {
            debugger;
        });
    });
    $(document).on('click', '#Reservations-EditReservaton-Save', function (e) {
        $('form#singlereservationform').submit();
    });
    
});