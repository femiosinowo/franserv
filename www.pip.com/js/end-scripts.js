// JavaScript Document
$(document).ready(function() {

    var list;
    if (document.getElementsByClassName)//check to see if browser supports since IE8 does not
        list = document.getElementsByClassName('background_wrapper');
    else list = document.querySelectorAll('background_wrapper');

for (var i = 0; i < list.length; i++) {
var src = list[i].getAttribute('data-image');
list[i].style.backgroundImage="url('" + src + "')";
} 

});