// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function AddUserToEvent() {

    var idEvent = $('#buttonEvent').data('eventid');

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Calendar/AddUserToEvent",
        async: true,
        data: JSON.stringify({
            id: idEvent
        }),

        datatype: "json",
        success: function (response) {
            if (response.success) {
                $('#calendarDetails').modal('toggle');
                $('#succesAdded').modal('toggle');

            }
            else {
                $('#errorMsg').text(response.responseText);
                $('#calendarDetails').modal('toggle');
                $('#errorAdded').modal('toggle');
            }
        },
        error: function (xmlhttprequest, textstatus, errorthrown) {
            $('#calendarDetails').modal('toggle');
            $('#errorAdded').modal('toggle');

            console.log("error: " + errorthrown);
        }

    });


}

$('#calendarDetails').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var eventid = button.data('eventid');
    var amount = button.data('amount');
    var fullname = button.data('fullname');
    var title = button.data('title');
    var time = button.data('time');

    var modal = $(this);
    modal.find('.modal-title').text('Details for event ' + title);
    modal.find('#amount').text(amount);
    modal.find('#fullname').text(fullname);
    modal.find('#title').text(title);
    modal.find('#time').text(time);
    modal.find('#buttonEvent').data("eventid", eventid);


});