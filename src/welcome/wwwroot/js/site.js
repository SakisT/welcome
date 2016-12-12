$(function () {
    $('#arrivaldate').datepicker();
    $('#departuredate').datepicker();
    $('#arrivaldate').datepicker("option", "changeYear", true);
    $('#arrivaldate').datepicker("option", "changeMonth", true);
    $('#departuredate').datepicker("option", "changeYear", true);
    $('#departuredate').datepicker("option", "changeMonth", true);
});

$(document).on('ready', function (e) {
    $(document).on('change', '#selectLanguage select', function () {
        $(this).parent().submit();
    });
    $(document).on('click', '.reservationrow', function () {
        $('#reservation-data').load($(this).data('link'), null, function (data, status) {
            $('#reservationstayrooms').load($('#reservationstayrooms').data('link'), null, function (data, status) {
                $('#reservations-editreservation-contact').load($('#reservations-editreservation-contact').data('link'), null, function (data, status) {

                    switch ($('#selectLanguage select').val()) {
                        case "en-US":
                            $('#arrivaldate').datepicker($.datepicker.regional[""]);
                            $('#arrivaldate').datepicker("option", "dateFormat", "m/d/yy");
                            $('#departuredate').datepicker($.datepicker.regional[""]);
                            $('#departuredate').datepicker("option", "dateFormat", "m/d/yy");
                            break;
                        case "el-GR":
                            $('#arrivaldate').datepicker($.datepicker.regional["el"]);
                            $('#arrivaldate').datepicker("option", "dateFormat", "d/m/yy");
                            $('#departuredate').datepicker($.datepicker.regional["el"]);
                            $('#departuredate').datepicker("option", "dateFormat", "d/m/yy");
                            break;
                    }
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
    $(document).on('keyup', '.numbertextbox', function () {
        var text = $(this).val();
        var lang = $("#selectLanguage option:selected").val();
        if (lang === 'el-GR') {
            text = text.toString().replace('.', ',');
        }
        else {
            text = text.toString().replace(',', '.');
        }
        //text = text.toString().replace(/,/g, '.');
        $(this).val(text);
    });
    $(document).on('keydown', '.numbertextbox', function (e) {
        // Allow: minus, backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [109, 46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || (e.keyCode > 105 && e.keyCode !== 188))) {
            e.preventDefault();
        }
    });
    $(document).on('mouseup', '.numbertextbox', function () {
        $(this).select();
    });
});

function InitializeNewDeposit(reservationid, depositid) {

    var url = $('#deposit-dialog').data('url') + "?id=" + reservationid;

    if (depositid !== undefined) { url = url + '&depositid=' + depositid; }

    $('#deposit-dialog').dialog("open");

    $('#deposit-dialog').load(url);

};

function deletedeposit(depositid) {
    var depositrowid = '#depositid-' + depositid;
    var deletemessage = $(depositrowid).data('confirmmessage');
    $('#deletedepositconfirmation').html(deletemessage);
    $('#deletedepositconfirmation').data('depoittodelete', depositid);
    $('#modaldeletedepositconfirmation').modal();
}

function deletedepositconfirmed() {
    var depositidtodelete = $('#deletedepositconfirmation').data('depoittodelete');
    var link = $('#deletedepositconfirmation').data('deletedepositlink');
    $.ajax({
        url: link,
        method :'POST',
        dataType :'json',
        data: { id: depositidtodelete },
        success: function (data, status) {
            $(data.rowtodelete).remove();
            debugger;
        },
        error: function (data, status) {
            debugger;
        },
        complete: function (jqXHR, textStatus) {
            $('#deletedepositconfirmation').data('depoittodelete', '');
        }
    });

}

function OnDepositMethodChanged(radio) {
    switch (radio) {
        case "cash":
            $('#creditcardorbankdata').hide(100);
            $("#creditcardorbankid").empty();
            //$('#deposit-dialog').height('350px');
            break;
        case "creditcard":
            $('#creditcardorbankdata').show(500);
            $('#creditcarddata').show(100);
            $.getJSON($('#createnewdepositdata').data('getcreditcardslink'), null, function (result) {
                $("#creditcardorbankid").empty();
                $.each(result, function (i, agent) {
                    $("#creditcardorbankid").append("<option value='" + agent.agentid + "'>" + agent.name + "</option>");
                });
            });
            break;
        default://bank
            $('#creditcardorbankdata').show(500);
            $('#creditcarddata').hide(100);
            $.getJSON($('#createnewdepositdata').data('getbankslink'), null, function (result) {
                $("#creditcardorbankid").empty();
                $.each(result, function (i, agent) {
                    $("#creditcardorbankid").append("<option value='" + agent.agentid + "'>" + agent.name + "</option>");
                });
            });
            break;
    }
    $('#containerdiv .depositmethod').each(function () {
        $(this).hide();
    });
    $(radio).show();
};

