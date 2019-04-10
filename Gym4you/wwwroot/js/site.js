// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function AddUserToEvent(idEvent) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Calendar/AddUserToEvent",
        async: true,
        data: JSON.stringify({
            id: idEvent
        }),

        datatype: "json",
        success: function (result) {
            //do something
            alert("SUCCESS = " + result.d);
            console.log(result);
        },
        error: function (xmlhttprequest, textstatus, errorthrown) {
            alert(" conection to the server failed ");
            console.log("error: " + errorthrown);
        }
    });


}