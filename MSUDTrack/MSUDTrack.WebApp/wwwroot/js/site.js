// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $(".food-select").on("input", function (e) {
        e.preventDefault();
        //here goes the api post
        var targetId = $(this).attr('class').split(' ')[1];
        var elements = $("." + targetId);

        var record = {
            Id: targetId,
            FoodId: elements[0].value
        };

        $.ajax({
            type: 'POST',
            url: '/api/Records',
            data: JSON.stringify(record),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        }).done(function () {
            location.reload();
        });
    });

    $(".nutri-inputs").on("input", function (e) {
        //here goes the api post
        var targetId = $(this).attr('class').split(' ')[1];
        var elements = $("." + targetId);

        var food = {
            Id: elements[0].value,
            ProteinGrams: elements[1].value,
            LeucineMilligrams: elements[2].value
        };

        $.ajax({
            type: 'POST',
            url: '/api/Foods',
            data: JSON.stringify(food),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        });
    });

    $(".delete-button").on("click", function (e) {
        //here goes the api post
        var targetId = $(this).attr('class').split(' ')[1];

        $.ajax({
            type: 'DELETE',
            url: '/api/Records/' + targetId
        }).done(function () {
            location.reload();
        });
    });

    $("#child-select").on("input", function (e) {
        $.ajax({
            type: 'GET',
            url: '/api/Children/' + $('#' + e.target.id).find(":selected").text()
        }).done(function () {
            location.reload();
        });
    });
});
