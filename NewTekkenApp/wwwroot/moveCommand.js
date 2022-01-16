class commandLib {
    constructor() {
        this.timer = 0;
        this.resultKey = [];
        this.keyMap = [];
        this.command = document.getElementById('Command');
        this.rowCommand = this.command.value;
        this.clickedKey = [];
        //zaibatsuCommand = document.getElementById("zaibatsuCommand");
    }

    init() {
        this.command.addEventListener("keydown", function (event) {
            commandUtil.AddKey(event.key);
            commandUtil.timer++;
        });
        this.command.addEventListener("keyup", function (event) {
            commandUtil.RemoveKey(event.key);

            if (commandUtil.resultKey.length == 0) {
                commandUtil.AddCommand(event.key);
            }
        });
    }

    AddKey(key) {
        if (commandUtil.clickedKey.indexOf(key) < 0) {
            commandUtil.clickedKey.push(key);
            commandUtil.resultKey.push(key);
        }
    }

    RemoveKey(key) {
        let keyIndex = this.resultKey.indexOf(key);
        if (keyIndex > -1) {
            this.resultKey.splice(keyIndex, 1);
        }
    }

    AddCommand(key) {
        let result = this.rawCommand;
        let formedKey = '';

        if (key == 'Backspace') {
            let lastIndex = this.rawCommand.lastIndexOf('/');
            commandUtil.SetCommand(this.rawCommand.substring(0, lastIndex));
            return;
        } else if (this.clickedKey.length == 1 && this.timer < 2) {
            formedKey = key.toUpperCase();
        } else if (this.clickedKey.length >= 2) {
            formedKey = this.clickedKey.sort().toString().replace(/,/gi, '+').toUpperCase();
        } else if (this.timer > 2) {
            formedKey = 'L' + key.toUpperCase();
        }
        let mapppedKey = commandUtil.MapKey(formedKey);
        if (mapppedKey != '') {
            result += '/' + mapppedKey;
        }
        commandUtil.SetCommand(result);
    }


    MapKey(formedKey) {
        if (this.keyMap[formedKey] != undefined) {
            return '[' + this.keyMap[formedKey] + ']';
        }
        return '';
    }

    SetCommand(result) {
        if (result.charAt(0) == '/') {
            result = result.substr(1);
        }

        this.rawCommand = result;
        this.command.value = this.rawCommand;
        this.displayCommand.value = this.rawCommand.replace(/\//gi, ' ');

        this.InitCommand();
    }

    InitCommand() {
        this.clickedKey = [];
        this.timer = 0;
    }
    AddState(state) {
        var result = this.rawCommand + '/{S:' + this.state + '}';
        //displayCommand.value = command;
        this.SetCommand(result);
    }
}

const commandUtil = new commandLib();


export { commandLib, commandUtil };










//function AddStateModal(stateType, stateCode) {
//    var url;

//    if (stateType == "MOVE") {
//        url = "/Admin/Move/SelectMove";
//    } else {
//        url = "/Admin/MoveText/SelectMoveText";
//    }


//    var placeholderElement = $('#modal-placeholder');
//    var character_code = $('#Character_code').val();

//    $.get(url, { stateType: stateType, character_code: character_code }).done(function (data) {
//        placeholderElement.html(data);
//        placeholderElement.find('.modal').modal('show');
//    });

//    placeholderElement.off('click', '[data-save="modal"]').on('click', '[data-save="modal"]', function (event) {
//        event.preventDefault();
//        if (stateType == "MOVE") {
//            var result = rawCommand + '/{M:' + stateCode + ':' + $('#move').val() + '}';
//        } else {
//            var result = rawCommand + '/{T:' + stateCode + ':' + $('#move').val() + '}';
//        }

//        SetCommand(result);
//        placeholderElement.find('.modal').modal('hide');

//        //})
//    });

//    placeholderElement.off('click', '[data-bs-dismiss="modal"]').on('click', '[data-bs-dismiss="modal"]', function (event) {
//        event.preventDefault();
//        placeholderElement.find('.modal').modal('hide');
//    });
//}




////function SetZaibatsuCommand() {
////    CommandArray.forEach(function (item, index, array) {
////        zaibatsuCommand += item;
////    });
////}

//function SetKeyMap() {
//    $.getJSON("/Admin/MoveCommand/GetKeyMap", function (data) {
//        var items = '';
//        $.each(data, function (i, keyInfo) {
//            keyMap[keyInfo.key] = keyInfo.code
//        });
//    });
//}

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
//    $('#StateGroup').change(function () {
//        var url = '/Admin/State/GetJsonSelectListByStateGroup';
//        var stateGroup_Code = $('#StateGroup').val();

//        $.getJSON(url, { stateGroup_Code: stateGroup_Code }, function (data) {
//            var items = '';
//            $('#States').empty();
//            $.each(data, function (i, States) {
//                if (stateGroup_Code == 80000007) {
//                    items += '<button type="button" class="col btn btn-primary" value="' + States.value + '" onClick="AddStateModal(\'MOVE\',this.value);" >' + States.text + '</button>';

//                } else if (stateGroup_Code == 80000015) {
//                    items += '<button type="button" class="col btn btn-primary" value="' + States.value + '" onClick="AddStateModal(\'TEXT\',this.value);" >' + States.text + '</button>';

//                } else {
//                    items += '<button type="button" class="col btn btn-primary" value="' + States.value + '" onClick="AddState(this.value);" >' + States.text + '</button>';
//                }
//            });
//            $('#State').html(items);
//        });

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


