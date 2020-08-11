// App.js

document.addEventListener('DOMContentLoaded', function()
{
    console.log('index: DOM fully loaded and parsed');
//    var myVar = setInterval(main_timer, 500);
    $.get(
        '/version',
        {'stam': 1},
        function (data) {
            console.log(data);
            document.getElementById("Version").innerHTML = " " + data;
        },
        'html');
});



function main_timer(){
//    console.log("myTimer");
}


function get_radio(group_name){
    var elements = document.getElementsByName(group_name);
    var val = 0;
    for (var i = 0; i < elements.length; i++){
        var elem = elements[i];
        if(elem.checked) {
            val = elem.value;
            break;
        }
    }
    return(val);
}


function call_item(item_id){
    console.log('goto' + item_id);
    $.get(
        '/goto',
        {'item_id': item_id},
        function (data) {
            console.log(data);
            window.location.replace("/");
        },
        'html');
}


function go_up(){
    console.log('goup');
    $.get(
        '/goup',
        {'stam': 1},
        function (data) {
            console.log(data);
            window.location.replace("/");
        },
        'html');
}

