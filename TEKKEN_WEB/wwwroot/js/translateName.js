$(".commandName").on('keyup', function (e) {
    var element = $(this);
    var language_code = element.attr('Language_Code');
    var changeId = 'Change_' + language_code;
    document.getElementById(changeId).checked = true;
});