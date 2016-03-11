
$(document).ready(function () {
    
    //global copyright date
    var currentTime = new Date();
    var year = currentTime.getFullYear();
    if ($('#copyRightYear')) {
        $('#copyRightYear').html(year);
    }    
 
});