
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
    });
});



