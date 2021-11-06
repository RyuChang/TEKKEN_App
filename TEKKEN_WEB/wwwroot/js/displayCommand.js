    $(function () {
    GetMoveListJson(18);
});

var test=''
function GetMoveListJson(character_code) {
    var url = '/User/MoveList/GetMoveList?character_code=' + character_code 

    $.getJSON(url, function (data) {
        test = data;
        var items = '';
        
        $.each(data, function (i, moveList) {
            //console.log(data);
            console.log(moveList.command);
            //keyMap[keyInfo.key] = keyInfo.code
            //document.write('------');

            var move = moveList.number + '   '+ moveList.command;
            items += move+ '<br>';
            
        });
        var result = '<object class="lever" data="/images/$1.svg" type="image/svg+xml"></object>'
        items = items.replace(/\[(\S+)\]/gi, result);

        //test=test.replaceAll(/\[(\S+)\]/gi,"$1");

        //$('#command').html('<object data ="/images/ru.svg" type="image/svg+xml"></object>');
        $('#command').html(items);
    });
}

//