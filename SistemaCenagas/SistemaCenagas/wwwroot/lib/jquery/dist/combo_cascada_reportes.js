


$("#Buscar").change(function () {
    alert($("#Buscar").val());
    $("#Filtrar").empty();

    if ($("#Filtrar").val() != "0") {
        $.ajax({
            type: "Post",
            url: "/Reportes/getFiltro",
            data: { "idBusqueda": $("#Buscar").val() },
            success: function (response) {
                var items = "<option>--- Selecciona una opción ---</option>";
                $(response).each(function () {
                    items += "<option value='" + this.value + "'>" + this.text + "</option>";
                })
                $("#Filtrar").html(items);
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

$("#Gasoducto").change(function () {
    $("#Tramo").empty();
    if ($("#Gasoducto").val() != "--- Selecciona una opción ---") {
        $.ajax({
            type: "Post",
            url: "/Anexo1/getTramos",
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