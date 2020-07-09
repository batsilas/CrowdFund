// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#Input_Country").countrySelect();

let showFundersTrends = $('.js-funders-trending');
showFundersTrends.hide();

let allcats = $('#allCategories');

let showFundsTrends = $('.js-funds-trending');
showFundsTrends.hide();

let fundsDropDown = $('.js-fundsReceived-item');
fundsDropDown.on('click', () => {
    showFundersTrends.hide();
    allcats.hide();
    showFundsTrends.show();
    categoryDrop.hide();
})

let fundersDropDown = $('.js-numOfFunders-item');
fundersDropDown.on('click', () => {
    showFundsTrends.hide();
    allcats.hide();
    showFundersTrends.show();
    categoryDrop.hide();
})

let trendsDrop = $('.js-fundsReceived-item');
trendsDrop.on('click', () => {
    showTrends.show();
})

let categoryDrop = $('.js-category-selection-btn');
categoryDrop.hide();

let basedOnCategories = $('.js-trendsByCategory-item');
basedOnCategories.on('click', () => {
    categoryDrop.show();
})

$('#CategoriesSelect').on('change', function () {
    allcats.show();
    categoryDrop.show();
    showFundsTrends.hide();
    showFundersTrends.hide();
    var e = document.getElementById("CategoriesSelect");
    var takeoption = e.options[e.selectedIndex].value;
    console.log(takeoption);

    if (takeoption == "Technology") {
        $("#Technology2").css("display", "block");
        $("#Games2").css("display", "none");
        $("#Art2").css("display", "none");

    }
    else if (takeoption == "Art") {
        $("#Art2").css("display", "block");
        $("#Technology2").css("display", "none");
        $("#Games2").css("display", "none");

    }
    else {
        $("#Games2").css("display", "block");
        $("#Technology2").css("display", "none");
        $("#Art2").css("display", "none");

    }
});


let button = $('.fund-button')
button.on('click', (event) => {



    let currentFundButton = $(event.currentTarget);
    var project_id = $("#project_id").attr("data-Project-id");
    //var fund_id = $(".fund-button").attr("data-Funding-id");



    var fund_id = currentFundButton.attr("data-Funding-id");
    console.log(project_id);

    console.log(fund_id);


    let dat = {
        ProjectId: project_id,
        FundingPackageId: fund_id
    };

    console.log(dat);

    var datJson = JSON.stringify(dat);


    $.ajax({
        type: 'POST',
        url: '/Customer/Project/Fund',
        data: datJson,
        contentType: "application/json",
        success: function () {
            toastr.success('You have succesfully funded this project ');
        },
        error: function () {
            toastr.error('Sorry, something bad happened');
        }
    });
});