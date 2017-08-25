
// code to find people
function SearchPeople(e) {
    $('#blankValidationMsg').html('');
    var name = { 'name': $('#searchTextBox').val(), 'isExactMatchRequested': false };

    if ($('#searchTextBox').val().trim().length > 0) {
        showProgress();
        $.ajax({
            url: "/People/Result",
            type: "GET",
            data: name,
            success: function (msg) {
                hideProgress();
                $('#peopleDetails').html(msg);
            },
            error: function (f) {
                console.log(f);
                hideProgress();
                $('#blankValidationMsg').html('No record found!');
                console.log($('#blankValidationMsg').text());
            }

        });
    } else {
        console.log("hi")
        $('#blankValidationMsg').html('Please enter first name or last name!');
        console.log($('#blankValidationMsg').text());
    }

};

//code to hit enter event
$("#searchTextBox").keyup(function (event) {
    if (event.keyCode == 13) {
        SearchPeople(event);
    }
});




// code to display spin
var spinnerVisible = false;
function showProgress() {
    if (!spinnerVisible) {
        $("div#spinner").fadeIn("fast");
        spinnerVisible = true;
    }
};
function hideProgress() {
    if (spinnerVisible) {
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
        spinnerVisible = false;
    }
};




