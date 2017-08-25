$(document).ready(function () {
    console.log("ready!");
    HideWarnings();
});

//code to add image
function ChangeAttachment(e) {
    var files = e.target.files;
    console.log(files);

    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }

            console.log("before ajax call");
            $.ajax({
                type: "POST",
                url: '/People/UploadImage',
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    console.log(result);
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] == "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                }
            });
        } else {
            alert("This browser doesn't support HTML5 file uploads!");
        }
    }

}

//code to create people information
function AddPeople() {
    HideWarnings();

    if (isPeopleValid()) {

        console.log("Register button clicked.")
        var gender = $('#gender:checked').val();

        var peopleViewModel = {
            FirstName: $('#firstName').val().trim(),
            LastName: $('#lastName').val().trim(),
            Address: $('#address').val().trim(),
            DateOfBirth: $('#dob').val(),
            Interests: $('#interests').val(),
            Image: '',
            Gender: gender
        };

        var interests = $('#interests').val();
        var dob = $('#dob').val();


        console.log(interests);
        console.log(dob);
        showProgress();
        $.ajax({
            url: "/People/New",
            type: "POST",
            data: JSON.stringify(peopleViewModel),
            dataType: "json",
            traditional: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                hideProgress();
                console.log("People saved successfully");

                $('#successMessage').html('Success! People created sucessfully.');
                $('#successMessage').attr('class', 'alert alert-success');
            },
            error: function (err) {
                hideProgress();
                console.log("Error occured while saving a record");
                $('#errorMessage').html('Failure! Unexpected error occured while saving a record.');
                $('#errorMessage').attr('class', 'alert alert-warning   ')
            }
        });
    } else {
        $('#validationMessage').html('First name and birthday can not be left empty. Birthday should not exceed today\'s date.');
        $('#validationMessage').attr('class', 'alert alert-warning  col-sm-off-2 col-sm-10');

        $('#successMessage').html('');
        $('#successMessage').attr('class', '')

        $('#errorMessage').html('');
        $('#errorMessage').attr('class', '')
    }

}

//code to validate input
function isPeopleValid() {
    return $('#firstName').val().trim().length > 0 &&
        $('#dob').val().length > 0 &&
        new Date($('#dob').val()) < new Date();
}

function HideWarnings() {
    $('#validationMessage').html('');
    $('#validationMessage').attr('class', ' col-sm-off-4 col-sm-8')

    $('#successMessage').html('');
    $('#successMessage').attr('class', '')

    $('#errorMessage').html('');
    $('#errorMessage').attr('class', '')
}
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