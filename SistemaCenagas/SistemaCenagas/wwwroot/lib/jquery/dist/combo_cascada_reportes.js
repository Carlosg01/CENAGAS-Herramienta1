


$("#ReportesBuscar").change(function () {
    alert($("#ReportesBuscar").val());
    $("#ReportesFiltrar").empty();

    if ($("#Filtrar").val() != "0") {
        $.ajax({
            type: "Post",
            url: "/Reportes/getFiltro",
            data: { "idBusqueda": $("#ReportesBuscar").val() },
            success: function (response) {
                var items = "<option>--- Selecciona una opción ---</option>";
                $(response).each(function () {
                    items += "<option value='" + this.value + "'>" + this.text + "</option>";
                })
                $("#ReportesFiltrar").html(items);
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