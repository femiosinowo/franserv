
$(document).ready(function () {
    
    String.prototype.startsWith = function (prefix) {
        return this.indexOf(prefix) === 0;
    }

    String.prototype.endsWith = function (suffix) {
        if (this.length < suffix.length)
            return false;
        return this.lastIndexOf(suffix) === this.length - suffix.length;
    }

    //globally to make local site urls
    $('body a').each(function () {       
        var href = $(this).attr('href');
        var cssClass = $(this).attr('class');
        if (href != undefined) {
            href = href.toLowerCase();
            if (href.startsWith('/') && (href.indexOf('.aspx') > -1 || href.endsWith('/') || href.indexOf('sid') > -1) && href.indexOf('workarea') == -1) {
                var centerId = $('.hddnCenterId').val();
                if (centerId != '' && cssClass != 'SiteMapLink') {
                    href = '/' + centerId + href;
                    $(this).attr('href', href);
                }
            }
        }
    });

    //global copyright date
    var currentTime = new Date();
    var year = currentTime.getFullYear();
    if ($('#copyRightYear')) {
        $('#copyRightYear').html(year);
    }

    //home page banners
    $('.flex-custom-control-nav a').mouseenter(function () {
        $(this).addClass('flex-active');
    }).mouseleave(function () {
        $(this).removeClass('flex-active');
    });
    

    //header Franchise content
    //$('a.franchiseopportunities').click(function (e) {        
    //    e.preventDefault();       
    //    $('#search_social_close_wrapper').delay(1500).addClass('find_location');
    //    $('#franchise_national').show();
    //});   

    //share tool & global print option
    $('a.print').click(function (e) {
        window.print();
    });

    //formatting the ordered List
    if($('.CustomOrderedList').length > 0)
    {
        $('.CustomOrderedList ol').each(function () {            
            var numOfLists = 2;
            $(this).addClass('number_block');
            var listLength = $(this).find("li").size();
            var numInRow = Math.floor(listLength / numOfLists);
            var difference = listLength - (numInRow * numOfLists);
            for (var i = 0; i < numOfLists; i++) {
                var itemsToTake = numInRow;
                if (i < difference) {
                    itemsToTake = itemsToTake + 1;
                } else {
                    itemsToTake = itemsToTake;
                }

                var listItems = $(this).find("li").slice(0, itemsToTake);
                var newList;
                if (i == 0)
                    newList = $('<span />', { "class": "left" }).append(listItems);
                else
                    newList = $('<span />', { "class": "right" }).append(listItems);
                $(this).append(newList);
            }
        });
    }

    //display phone number on request a call form
    if ($('#localCenterPhoneNumber').length > 0)
    {
        var contactNumber = $('.hddnContactNumber').val();       
        $('#localCenterPhoneNumber').text(contactNumber);
    }

    //sub-navigation IT solution
    $('.more_dropdown a').click(function () {
        window.location.href = $(this).attr('href');
    });
});