
$("#Residencia").ready(function () {

    $("#Gasoducto").empty();
    $("#Tramo").empty();

    if ($("#Residencia").val() != "-1") {
        $.ajax({
            type: "Post",
            url: "/Anexo1/getGasoductos",
            data: { "id_residencia": $("#Residencia").val() },
            success: function (response) {
                var items = "<option>--- Selecciona una opción ---</option>";
                $(response).each(function () {
                    items += "<option value='" + this.value + "'>" + this.text + "</option>";
                })
                $("#Gasoducto").html(items);
                $("#Gasoducto").val($("#Hidden_Gasoducto").val());

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
                        $("#Tramo").val($("#Hidden_Tramo").val());
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }

                });

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