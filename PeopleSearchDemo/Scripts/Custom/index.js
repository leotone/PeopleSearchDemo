
// code to call controller to load data

$('#btnSubmit').on("click", function (event) {
    showProgress();

    console.log("before ajax call");
    $.ajax({
        url: "/Home/DataLoadResult",
        type: "GET",
        success: function (msg) {
            hideProgress();
            $('#successMessage').html(msg);
            console.log(msg);
        },
        error: function (err) {
            hideProgress();
            console.log(err);
            $('#errorMessage').html("Error occured while loading data.")
        }
    });


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