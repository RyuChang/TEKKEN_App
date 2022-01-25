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
    


    //AddKey(key) {
    //    if (this.clickedKey.indexOf(key) < 0) {
    //        this.clickedKey.push(key);
    //        this.resultKey.push(key);
    //    }
    //}

    //SetKeyDown() {
    //    this.command.addEventListener("keydown", function (event) {
    //        commandUtil.AddKey(event.key);
    //        this.timer++;
    //    });
    //}

    //SetKeyUp() {
    //    this.command.addEventListener("keyup", function (event) {
    //        commandUtil.RemoveKey(event.key);

    //        if (commandUtil.resultKey.length == 0) {
    //            commandUtil.AddCommand(event.key);
    //        }
    //    });
    //}

    //RemoveKey(key) {
    //    let keyIndex = this.resultKey.indexOf(key);
    //    if (keyIndex > -1) {
    //        this.resultKey.splice(k    /*x, 1);
    //    }
    //}
}
    /*



MapKey(formedKey) {
    if (commandUtil.keyMap[formedKey] != undefined) {
        return '[' + commandUtil.keyMap[formedKey].trim() + ']';
    }
    return '';
}



InitCommand() {
    commandUtil.clickedKey = [];
    commandUtil.timer = 0;
}
AddState(state) {
    var result = commandUtil.rawCommand + '/{S:' + commandUtil.state + '}';
    //displayCommand.value = command;
     SetStateGroup() {
    const stateGroup = document.getElementById('StateGroup');
    const stateGroup_Code = stateGroup.value;
}


AddMovesModal(dataCode) {
    const result = this.rawCommand + '/{M:80000007:' + dataCode + '}';
    this.SetCommand(result);
}

AddStateModal(stateType, stateCode) {
    if (stateType == "MOVE") {
        var result = rawCommand + '/{M:' + stateCode + ':' + $('#move').val() + '}';
    } else {
        var result = rawCommand + '/{T:' + stateCode + ':' + $('#move').val() + '}';
    }

    SetCommand(result);
    placeholderElement.find('.modal').modal('hide');
}
al').modal('hide');
}
*/


    //async SetKeyMap() {
    //    //fetch('/api/commands').then(data => console.log(data));


    //    commandUtil.keyMap = await fetch('/api/commands', {
    //        method: 'GET',
    //        headers: {
    //            'Accept': 'application/json',
    //            'Content-Type': 'application/json',
    //            'Authorization': 'none'
    //        }
    //    }).then(res => res.json())
    //        .then(data => {
    //            let result = [];
    //            data.forEach(
    //                function (data) {
    //                    result[data.key] = data.code;
    //                }
    //            );
    //            return result;
    //        }).catch((err) => handl//rror(err));
    //}




    //const//ommandUtil = new commandLib();

    //export { commandLib, commandUtil };







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

