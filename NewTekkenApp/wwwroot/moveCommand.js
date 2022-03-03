class commandLib {
    constructor() {
        //this.timer = 0;
        //this.resultKey = [];
        //this.keyMap = [];
        this.command = document.getElementById('Command');
        this.displayCommand = document.getElementById('displayCommand');
        this.rawCommand = this.command.value;
        //this.clickedKey = [];
        //zaibatsuCommand = document.getElementById("zaibatsuCommand");
    }

    init() {
        //this.SetKeyMap();
        //this.SetKeyDown();
        ////this.SetKeyUp();
        this.SetStateGroup();
    }
    

}

    ////function SetZaibatsuCommand() {
    ////    CommandArray.forEach(function (item, index, array) {
    ////        zaibatsuCommand += item;
    ////    });
    ////}


    //$(function () {
    //    $("#TransCommand").click(function () {
    //        TransCommand();
    //        $("#Command").focus();

    //        $('.form-check-input').prop('checked', true);
    //    });
    //});


    //function TransCommand() {
    //    if (rawCommand.length > 1) {
    //        var translated_en = $("#TlanslatedCommand_en");
    //        var translated_ko = $("#TlanslatedCommand_ko");
    //        var translated_jp = $("#TlanslatedCommand_jp");

    //        SetTranslatedCommand(translated_en, 'en');
    //        SetTranslatedCommand(translated_ko, 'ko');
    //        SetTranslatedCommand(translated_jp, 'jp');
    //    }
    //}

    //function SetTranslatedCommand(elmTranslated, language_code) {
    //    $.ajax({
    //        type: "POST",
    //        url: "/Admin/MoveCommand/TransCommand",
    //        beforeSend: function (xhr) {
    //            xhr.setRequestHeader("XSRF-TOKEN",
    //                $('input:hidden[name="__RequestVerificationToken"]').val());
    //        },
    //        data: { command: rawCommand, language_code: language_code },
    //        success: function (response) {
    //            elmTranslated.val(response);
    //        },

    //        failure: function (response) {
    //            alert(response.responseText);
    //        },
    //        error: function (response) {
    //            alert(response.responseText);
    //        }
    //    });
    //}

    //$(function () {


    //        var placeholderElement = $('#modal-placeholder');
    //        $('button[data-toggle="ajax-modal"]').click(function (event) {
    //            var url = $(this).data('url');
    //            $.get(url).done(function (data) {
    //                placeholderElement.html(data);
    //                placeholderElement.find('.modal').modal('show');
    //            });
    //        });

    //    });

    //    $('#Character').change(function () {
    //        var url = '/Admin/Move/Create';
    //        var character_code = $('#Character').val();
    //        location.href = url + '?character_code=' + character_code;
    //    });

    //    SetKeyMap();
    //});



    //$(".commandName").on('change', function (e) {
    //    var element = $(this);
    //    var language_code = element.attr('Language_Code');
    //    var changeId = 'Change_' + language_code;
    //    document.getElementById(changeId).checked = true;

    //});

