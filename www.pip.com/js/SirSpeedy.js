
$(document).ready(function () {
    
    //global copyright date
    var currentTime = new Date();
    var year = currentTime.getFullYear();
    if ($('#copyRightYear')) {
        $('#copyRightYear').html(year);
    }

    $('a#footerFindLocation').click(function () {
        $('.find-location-wrap').find('.desktop-nav-link').removeClass('no-hover').addClass('faux-hover');
        $('.find_location_link').click();
        $('.megamenu-outer-wrap').show();
    });
 
});