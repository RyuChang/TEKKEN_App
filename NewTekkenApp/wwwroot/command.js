var command = document.getElementById("Command");
//var zaibatsuCommand = document.getElementById("zaibatsuCommand");

var timer = 0;
var rawCommand = command.value;

var clickedKey = [];
var resultKey = [];
var keyMap = [];

onload(){
    SetKeyMap();

}

command.addEventListener("keydown", function (event) {
    AddKey(event.key);
    timer++;
});

command.addEventListener("keyup", function (event) {
    RemoveKey(event.key);

    if (resultKey.length == 0) {
        AddCommand(event.key);
    }
});

function AddKey(key) {
    if (clickedKey.indexOf(key) < 0) {
        clickedKey.push(key);
        resultKey.push(key);
    }
}

function AddCommand(key) {
    var result = rawCommand;
    var formedKey = '';

    if (key == 'Backspace') {
        var lastIndex = rawCommand.lastIndexOf('/');
        SetCommand(rawCommand.substring(0, lastIndex));
        return;
    } else if (clickedKey.length == 1 && timer < 2) {
        formedKey = key.toUpperCase();
    } else if (clickedKey.length >= 2) {
        formedKey = clickedKey.sort().toString().replace(/,/gi, '+').toUpperCase();
    } else if (timer > 2) {
        formedKey = 'L' + key.toUpperCase();
    }
    var mapppedKey = MapKey(formedKey);
    if (mapppedKey != '') {
        result += '/' + mapppedKey;
    }
    SetCommand(result);
}

function MapKey(formedKey) {
    if (keyMap[formedKey] != undefined) {
        return '[' + keyMap[formedKey] + ']';
    }
    return '';
}


function SetCommand(result) {
    if (result.charAt(0) == '/') {
        result = result.substr(1);
    }

    rawCommand = result;
    command.value = rawCommand;
    displayCommand.value = rawCommand.replace(/\//gi, ' ');

    InitCommand();
}

function InitCommand() {
    clickedKey = [];
    timer = 0;
}

function AddState(state) {
    var result = rawCommand + '/{S:' + state + '}';
    //displayCommand.value = command;
    SetCommand(result);
}

function AddStateModal(stateType, stateCode) {
    var url;

    if (stateType == "MOVE") {
        url = "/Admin/Move/SelectMove";
    } else {
        url = "/Admin/MoveText/SelectMoveText";
    }


    var placeholderElement = $('#modal-placeholder');
    var character_code = $('#Character_code').val();

    $.get(url, { stateType: stateType, character_code: character_code }).done(function (data) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    });

    placeholderElement.off('click', '[data-save="modal"]').on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        if (stateType == "MOVE") {
            var result = rawCommand + '/{M:' + stateCode + ':' + $('#move').val() + '}';
        } else {
            var result = rawCommand + '/{T:' + stateCode + ':' + $('#move').val() + '}';
        }

        SetCommand(result);
        placeholderElement.find('.modal').modal('hide');

        //})
    });

    placeholderElement.off('click', '[data-bs-dismiss="modal"]').on('click', '[data-bs-dismiss="modal"]', function (event) {
        event.preventDefault();
        placeholderElement.find('.modal').modal('hide');
    });
}


function RemoveKey(key) {
    var keyIndex = resultKey.indexOf(key);
    if (keyIndex > -1) {
        resultKey.splice(keyIndex, 1);
    }
}

//function SetZaibatsuCommand() {
//    CommandArray.forEach(function (item, index, array) {
//        zaibatsuCommand += item;
//    });
//}

function SetKeyMap() {
    var requestURL = "/Admin/MoveCommand/GetKeyMap";
    var request = new XMLHttpRequest();
    request.open('GET', requestURL);
    request.responseType = 'json';
    request.send();

    var commands = request.response;

    //var items = '';
    $.each(commands, function (i, keyInfo) {
        keyMap[keyInfo.key] = keyInfo.code
    });
}




/*
$(function () {
    $("#TransCommand").click(function () {
        TransCommand();
        $("#Command").focus();

        $('.form-check-input').prop('checked', true);
    });
});


function TransCommand() {
    if (rawCommand.length > 1) {
        var translated_en = $("#TlanslatedCommand_en");
        var translated_ko = $("#TlanslatedCommand_ko");
        var translated_jp = $("#TlanslatedCommand_jp");

        SetTranslatedCommand(translated_en, 'en');
        SetTranslatedCommand(translated_ko, 'ko');
        SetTranslatedCommand(translated_jp, 'jp');
    }
}

function SetTranslatedCommand(elmTranslated, language_code) {
    $.ajax({
        type: "POST",
        url: "/Admin/MoveCommand/TransCommand",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { command: rawCommand, language_code: language_code },
        success: function (response) {
            elmTranslated.val(response);
        },

        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

$(function () {
    $('#StateGroup').change(function () {
        var url = '/Admin/State/GetJsonSelectListByStateGroup';
        var stateGroup_Code = $('#StateGroup').val();

        $.getJSON(url, { stateGroup_Code: stateGroup_Code }, function (data) {
            var items = '';
            $('#States').empty();
            $.each(data, function (i, States) {
                if (stateGroup_Code == 80000007) {
                    items += '<button type="button" class="col btn btn-primary" value="' + States.value + '" onClick="AddStateModal(\'MOVE\',this.value);" >' + States.text + '</button>';

                } else if (stateGroup_Code == 80000015) {
                    items += '<button type="button" class="col btn btn-primary" value="' + States.value + '" onClick="AddStateModal(\'TEXT\',this.value);" >' + States.text + '</button>';

                } else {
                    items += '<button type="button" class="col btn btn-primary" value="' + States.value + '" onClick="AddState(this.value);" >' + States.text + '</button>';
                }
            });
            $('#State').html(items);
        });

        var placeholderElement = $('#modal-placeholder');
        $('button[data-toggle="ajax-modal"]').click(function (event) {
            var url = $(this).data('url');
            $.get(url).done(function (data) {
                placeholderElement.html(data);
                placeholderElement.find('.modal').modal('show');
            });
        });

    });

    $('#Character').change(function () {
        var url = '/Admin/Move/Create';
        var character_code = $('#Character').val();
        location.href = url + '?character_code=' + character_code;
    });

    SetKeyMap();
});



$(".commandName").on('change', function (e) {
    var element = $(this);
    var language_code = element.attr('Language_Code');
    var changeId = 'Change_' + language_code;
    document.getElementById(changeId).checked = true;

});
*/