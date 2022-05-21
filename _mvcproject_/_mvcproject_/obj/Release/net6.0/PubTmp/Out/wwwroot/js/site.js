
var placeholderElement = $("#placeholder");
$('button[data-bs-toggle = "modal"]').click(function (event) {
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    })
    
})

placeholderElement.on('click', '[data-save="modal"]', function (event) {
    var form = $(this).parents('.modal').find('form');
    var actionurl = form.attr("action");
    var senddata = form.serialize();
    $.post(actionurl, senddata).done(function (data) {
        placeholderElement.find('.modal').modal('hide');
    })
})