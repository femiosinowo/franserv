<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterBrandsAndLinks.ascx.cs" Inherits="UserControls_FooterBrandsAndLinks" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="FooterBrandsAndLinks" %>


<!-- footer rotator mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer rotator-->
<div class="footer_rotator_wrapper  clearfix">
    <div class="footer_rotator clearfix">
        <div class="container_24">
            <div class="grid_24">
                <!-- nnnnnnnnnnnn Partner Logos/Buttons nnnnnnnnnnnn -->
                <div id="footer-partner-icons">
                    <asp:ListView ID="lvBrands" runat="server">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li><a href="<%# Eval("Link") %>">
                                <img src="<%# Eval("ImagePath") %>" alt="<%# Eval("Title") %>" /></a></li>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <!-- #footer-partner-icons -->
            </div>
            <!--end grid 24-->
        </div>
        <!-- end container_24  -->
    </div>
    <!-- end lowerfooter  -->
</div>
<!-- end upperfooter_wrapper -->
<div class="clear"></div>
<!-- footer links mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer links-->
<div class="footer_links_wrapper">
    <div class="footer_links">
        <div class="container_24">
            <div class="grid_24">
                <cms:ContentBlock ID="cbCopyRight" runat="server" DisplayXslt="/XmlFiles/Copyright.xslt" DoInitFill="false" />
                <div id="siteLangSelector" visible="true" runat="server">
                    <ul>
                         <li id="country">
                             <a id="languageTranslate" class="languageTranslate skiptranslate" href="javascript:void(0)">
                               <span id="siteLangText">English</span>
                               <span id="siteLangImg"><img id="langFlag" alt="" src="/images/country-flags/en.gif"/></span>
                             </a>            
                        </li>
                    </ul>
                    <div class="googleLangTranslator">
                        <div id="google_translate_element"></div>
                        <script type="text/javascript">
                            function googleTranslateElementInit() {
                                new google.translate.TranslateElement({ pageLanguage: 'en', layout: google.translate.TranslateElement.InlineLayout.SIMPLE }, 'google_translate_element');
                            }
                        </script>
                        <script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
                    </div>
                    <div id="language-table" class="language-table skiptranslate" style="display:none;">
                            <table border="0" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr valign="top">
                                    <td><a class="goog-te-menu2-item" id="en" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">English</span></div>
                                    </a><a class="goog-te-menu2-item" id="ar" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Arabic</span></div>
                                    </a><a class="goog-te-menu2-item" id="be" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Belarusian</span></div>
                                    </a><a class="goog-te-menu2-item" id="kxd" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Brunei</span></div>
                                    </a><a class="goog-te-menu2-item" id="bg" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Bulgarian</span></div>
                                    </a><a class="goog-te-menu2-item" id="ca" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Catalan</span></div>
                                    </a><a class="goog-te-menu2-item" id="zh-CN" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Chinese (Simplified)</span></div>
                                    </a><a class="goog-te-menu2-item" id="zh-TW" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Chinese (Traditional)</span></div>
                                    </a><a class="goog-te-menu2-item" id="hr" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Croatian</span></div>
                                    </a>
                                    </td>
                                    <td class="goog-te-menu2-colpad">&nbsp;</td>
                                    <td><a class="goog-te-menu2-item" id="cs" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Czech</span></div>
                                    </a><a class="goog-te-menu2-item" id="da" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Danish</span></div>
                                    </a><a class="goog-te-menu2-item" id="nl" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Dutch</span></div>
                                    </a><a class="goog-te-menu2-item" id="et" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Estonian</span></div>
                                    </a><a class="goog-te-menu2-item" id="tl" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Filipino</span></div>
                                    </a><a class="goog-te-menu2-item" id="fi" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Finnish</span></div>
                                    </a><a class="goog-te-menu2-item" id="fr" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">French</span></div>
                                    </a><a class="goog-te-menu2-item" id="gl" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Galician</span></div>
                                    </a><a class="goog-te-menu2-item" id="de" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">German</span></div>
                                    </a>
                                    </td>
                                    <td class="goog-te-menu2-colpad">&nbsp;</td>
                                    <td><a class="goog-te-menu2-item" id="el" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Greek</span></div>
                                    </a><a class="goog-te-menu2-item" id="iw" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Hebrew</span></div>
                                    </a><a class="goog-te-menu2-item" id="hu" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Hungarian</span></div>
                                    </a><a class="goog-te-menu2-item" id="is" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Icelandic</span></div>
                                    </a><a class="goog-te-menu2-item" id="hi" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Indian</span></div>
                                    </a><a class="goog-te-menu2-item" id="id" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Indonesian</span></div>
                                    </a><a class="goog-te-menu2-item" id="it" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Italian</span></div>
                                    </a><a class="goog-te-menu2-item" id="ja" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Japanese</span></div>
                                    </a><a class="goog-te-menu2-item" id="ko" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Korean</span></div>
                                    </a>
                                    </td>
                                    <td class="goog-te-menu2-colpad">&nbsp;</td>
                                    <td><a class="goog-te-menu2-item" id="lv" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Latvian</span></div>
                                    </a><a class="goog-te-menu2-item" id="lt" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Lithuanian</span></div>
                                    </a><a class="goog-te-menu2-item" id="ms" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Malaysian</span></div>
                                    </a><a class="goog-te-menu2-item" id="mt" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Maltese</span></div>
                                    </a><a class="goog-te-menu2-item" id="no" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Norwegian</span></div>
                                    </a><a class="goog-te-menu2-item" id="pl" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Polish</span></div>
                                    </a><a class="goog-te-menu2-item" id="pt" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Portuguese</span></div>
                                    </a><a class="goog-te-menu2-item" id="ro" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Romanian</span></div>
                                    </a><a class="goog-te-menu2-item" id="ru" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Russian</span></div>
                                    </a>
                                    </td>                                            
                                    <td class="goog-te-menu2-colpad">&nbsp;</td>
                                    <td><a class="goog-te-menu2-item" id="sr" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Serbian</span></div>
                                    </a><a class="goog-te-menu2-item" id="sk" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Slovak</span></div>
                                    </a><a class="goog-te-menu2-item" id="sl" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Slovenian</span></div>
                                    </a><a class="goog-te-menu2-item" id="af" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">South African</span></div>
                                    </a><a class="goog-te-menu2-item" id="es" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Spanish</span></div>
                                    </a><a class="goog-te-menu2-item" id="sv" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Swedish</span></div>
                                    </a><a class="goog-te-menu2-item" id="th" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Thai</span></div>
                                    </a><a class="goog-te-menu2-item" id="tr" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Turkish</span></div>
                                    </a><a class="goog-te-menu2-item" id="uk" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Ukrainian</span></div>
                                    </a><a class="goog-te-menu2-item" id="vi" href="javascript:void(0)">
                                        <div style="white-space: nowrap;"><span class="indicator">›</span><span class="text">Vietnamese</span></div>
                                    </a>
                                    </td>                                    
                                </tr>
                            </tbody>
                        </table> 
                    </div>
                </div>
            </div>
            <!-- end grid_24 -->            
        </div>
        <!-- end container_24 -->
    </div>
    <!-- end footer_links -->
</div>
<!-- end footer_links_wrapper -->

<script type="text/javascript">
    //site language selector code
    document.onclick = check;
    function check(e) {
        var target = (e && e.target) || (event && event.srcElement);
        var obj = document.getElementById('language-table');
        var langSelectorText = document.getElementById('siteLangText');
        var langSelectorImg = document.getElementById('langFlag');
        if (target == langSelectorText || target == langSelectorImg)
        { $('.language-table').show('slide', null, 500, null); }
        else if (target == obj)
        { $('.language-table').show(); }
        else
        { $('.language-table').hide(); }
    }

    function setCookie(cname, cvalue, exdays, domain) {
        var expires;
        if (exdays === 0) {
            expires = '';
        } else {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            expires = "expires=" + d.toGMTString();
        }
        
        var domainVal = (typeof domain === "undefined") ? '' : "; domain=" + domain;
        document.cookie = cname + "=" + cvalue + "; expires=" + expires + "; path=/" + domainVal;
        
        
    }

    function getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i].trim();
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    $(document).ready(function () {
        $('#country').click(function () {
            $('#language-table').show('slide', null, 500, null);
        });

        $('a.goog-te-menu2-item').click(function () {
            var selectedLang = $(this).find('span.text').text();
            $('#siteLangText').text(selectedLang);
            $('#siteLangImg img').attr('src', '/images/country-flags/' + $(this).attr('id') + '.gif');
            var selectedLangCode = '/en/' + $(this).attr('id');
            $('#language-table').hide();
            setCookie('googtrans', selectedLangCode, 1, window.location.host);
            var $frame = $('.goog-te-menu-frame:first');
            if (!$frame.size()) {
                alert("Error: Could not find Google translate frame.");
                return false;
            }
            $frame.contents().find('.goog-te-menu2-item span.text:contains(' + selectedLang + ')').get(0).click();
        });
        $('#google_translate_element').hide();
        var selectedLangCodeInit = getCookie('googtrans');
        if (selectedLangCodeInit != undefined && selectedLangCodeInit != '') {
            var langCode = selectedLangCodeInit.substring(4);
            $('#siteLangImg img').attr('src', '/images/country-flags/' + langCode + '.gif');
            var selectedLangText = $('#' + langCode).find('span.text').text();
            $('#siteLangText').text(selectedLangText);
        }
        else {
            $('#siteLangImg img').attr('src', '/images/country-flags/en.gif');
        }
    });
</script>