// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {

    $(".nutri-inputs").on("input", function (e) {
        //here goes the api post
        var targetId = $(this).attr('class').split(' ')[1];
        var elements = $("." + targetId);

        var food = {
            Id: targetId,
            ProteinGrams: elements[3].value,
            LeucineMilligrams: elements[4].value
        };

        if (food.ProteinGrams === "")
            return;

        $.ajax({
            type: 'POST',
            url: '/api/Records',
            data: JSON.stringify(food),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                elements[4].value = data.leucineMilligrams;
            }
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

    $(".add-button").on("click", function (e) {
        //here goes the api post
        var targetId = $(this).attr('class').split(' ')[1];

        $.ajax({
            type: 'PUT',
            url: '/api/Records/' + targetId
        }).done(function () {
            location.reload();
        });
    });

    $(".food-select").selectize({
        plugins: ['remove_button'],
        valueField: 'id',
        labelField: 'name',
        searchField: 'name',
        create: false,
        load: function (query, callback) {
            $.ajax({
                url: "api/Foods?query=" + query + "&page_limit=10",
                type: 'GET',
                error: function () {
                    callback();
                },
                success: function (res) {
                    callback(res);
                }
            });
        }
    });

    $(".food-select").on("change", function (e) {
        e.preventDefault();
        //here goes the api post
        var targetId = $(this).attr('class').split(' ')[1];
        var elements = $("." + targetId);

        if (elements[0].value === "") {
            return;
        }

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
});
