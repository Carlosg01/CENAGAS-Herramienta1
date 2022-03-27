// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz0123456789";
    especiales = [8, 46, 95];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function limpia() {
    var val = document.getElementById("miInput").value;
    var tam = val.length;
    for (i = 0; i < tam; i++) {
        if (!isNaN(val[i]))
            document.getElementById("miInput").value = '';
    }
}


$(function () {
    var placeholderElement = $("#PlaceHolderHere");
    $('button[data-toggle="modal"]').click(function (event) {
        //alert("ALERT 1");
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        //alert(decodeUrl);
        $.get(decodeUrl).done(function (data) {
            //alert("ALERT 2");
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
            //alert("ALERT 3");
        })
    })

    /*placeholderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            placeholderElement.find('.modal').modal('hide');
        })
    })*/
})

