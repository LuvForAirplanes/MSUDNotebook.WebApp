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
            LeucineMilligrams: elements[4].value,
            WeightGrams: elements[5].value
        };

        if (food.ProteinGrams === "" && food.LeucineMilligrams === "" || food.WeightGrams === "")
            return;

        $.ajax({
            type: 'POST',
            url: '/api/Records',
            data: JSON.stringify(food),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                elements[3].value = data.proteinGrams;
                elements[4].value = data.leucineMilligrams;
                elements[5].value = data.weightGrams;
                $("#total-mg").text(data.luecineCount);
                $("#left-mg").text(data.leucineLeft);
            }
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
        valueField: 'id',
        labelField: 'name',
        searchField: 'name',
        create: function (input) {
            var targetId = $(this.$input).attr('class').split(' ')[1];

            $("#Food_Name").val(input);
            $("#Food_RecordId").val(targetId);

            showDiag();

            $("#Food_ProteinGrams").focus();
        },
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
        },
        onInitialize: function () {
            var selectize = this;
            if (selectize.getValue() === "")
                selectize.open();
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

    $("#leucine_exchange").on("input", function (e) {
        if (e.target.value !== "") {
            $.ajax({
                type: 'PUT',
                url: '/api/settings/exchange/' + e.target.value
            });
        }
    });

    $("#leucine_daily_count").on("input", function (e) {
        if (e.target.value !== "") {
            $.ajax({
                type: 'PUT',
                url: '/api/settings/dailycount/' + e.target.value
            });
        }
    });
});
