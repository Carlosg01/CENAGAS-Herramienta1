

$("#Residencia").change(function () {

    $("#Gasoducto").empty();
    $("#Tramo").empty();

    alert("HERE");

    /*
    if ($("#Residencia").val() != "-1") {
        $.ajax({
            type: "Post",
            url: "/ADC_Anexo1/getGasoductos",
            data: { "id_residencia": $("#Residencia").val() },
            success: function (response) {
                var items = "<option>--- Selecciona una opción ---</option>";
                $(response).each(function () {
                    items += "<option value='" + this.value + "'>" + this.text + "</option>";
                })
                $("#Gasoducto").html(items);
            },
            failure: function (response) {
                alert("Failure: " + response.responseText);
            },
            error: function (response) {
                alert("Error: " + response.responseText);
            }

        });
    }
    */


});

$("#Gasoducto").change(function () {
    $("#Tramo").empty();
    if ($("#Gasoducto").val() != "--- Selecciona una opción ---") {
        $.ajax({
            type: "Post",
            url: "/ADC_Anexo1/getTramos",
            data: { "ut_gasoducto": $("#Gasoducto").val() },
            success: function (response) {
                var items = "<option>--- Selecciona una opción ---</option>";
                $(response).each(function () {
                    items += "<option value='" + this.value + "'>" + this.text + "</option>";
                })
                $("#Tramo").html(items);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }

        });
    }

});