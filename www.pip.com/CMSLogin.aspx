﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="loginMin" CodeFile="CMSLogin.aspx.cs" %>

<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>Login</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <meta content="text/html; charset=UTF-8" http-equiv="content-type" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <script type="text/javascript">
        <!--        //--><![CDATA[//><!--
        function GetLoginInfo() {
            var username = document.getElementById("username").value;
            var password = document.getElementById("password").value;

            return [username, password];
        }

        function SetLoginInfo() {
            try {
                var loginInfo = GetLoginInfo();
                Ektron.PrivateData.SetLoginInfo(loginInfo[0], loginInfo[1]);
            } catch (e) { }
            return true;
        }

        function ValidatePassword() {
            var password1 = document.getElementById('newpassword1').value;
            var password2 = document.getElementById('newpassword2').value;
            if (password1 != password2) {
                alert("<asp:literal id='passwordMismatchErrorString' runat='server' />");
                return false;
            }
            else {
                var validationString = "<asp:literal id='passwordValidationString' runat='server' />";
                if (validationString && validationString.length > 0) {
                    var errorMsgs = ValidateRegExMsgArray(password1, validationString);
                    if (errorMsgs.length > 0) {
                        alert(errorMsgs[0]);
                        return false;
                    }
                }
            }
            return true;
        }

        function ValidateRegExMsgArray(password, regexAndErrorMessages) {
            var errors = [];
            var regex, errorMessage;
            var parts = [];
            var raw = trimStart(regexAndErrorMessages, "[");
            raw = trimEnd(raw, "]");
            raw = raw.split("],[");

            for (var idx = 0; idx < raw.length; idx++) {
                parts = raw[idx].split("/,");
                regex = trimStart(parts[0], "/");

                errorMessage = this.trimStart(trimStart(parts[1], ' '), '\\"');
                errorMessage = this.trimEnd(errorMessage, '\\"');

                var re = new RegExp(regex);
                if (!re.test(password))
                    errors[errors.length] = errorMessage;
            }
            return errors;
        }

        function trimStart(text, toRemove) {
            if (text.indexOf(toRemove) == 0 && text.length >= toRemove.length)
                return text.substr(toRemove.length);
            return text;
        }

        function trimEnd(text, toRemove) {
            if (text.length > toRemove.length && toRemove == text.substr(text.length - toRemove.length))
                return text.substr(0, text.length - toRemove.length);
            return text;
        }
        //--><!]]>
    </script>
    <script type="text/javascript" src="Workarea/java/jfunct.js">
    </script>
    <script type="text/javascript">
        <!--        //--><![CDATA[//><!--
        function setFocus() {
            self.focus();
            if ((typeof (document.LoginRequestForm)).toLowerCase() != "undefined") {
                var loginRequestForm = $ektron("#LoginRequestForm");
                var loginRequestFormUsername = loginRequestForm.find("#username")
                if (loginRequestForm.length > 0 && loginRequestFormUsername.length > 0) {
                    loginRequestFormUsername.focus();
                }
            }
        }

        // Clears the authentication for DMS control
        function clearAuth() {
            try {
                document.execCommand("ClearAuthenticationCache");
            } catch (err) {
                // do nothing
            }
        }

        $ektron(document).ready(function () {
            // add hover effects for the inputButtons
            $ektron(".inputButton").hover(
                    function () {
                        $ektron(this).addClass("ui-state-hover");
                    },
                    function () {
                        $ektron(this).removeClass("ui-state-hover");
                    }
                );
            if ($ektron.browser.safari) {
                $ektron('.ek_btn_lft').css('margin-left', '115px');
            }
        });
        //--><!]]>
    </script>
    <style type="text/css">        
		form#LoginRequestForm {display: block; overflow: auto; height: 100%; position: relative;}
		.ui-widget #LoginBtn .ui-icon, .ui-widget #changePasswordBtn .ui-icon, .ui-widget #btn_skip .ui-icon, .ui-widget #LogoutBtn .ui-icon{left: .2em;margin: -8px 5px 0 0;position: absolute;top: 50%;}
        .ui-widget #loginCancel .ui-icon, .ui-widget #changePasswordCancel .ui-icon, .ui-widget #logoutCancel .ui-icon{left: .2em;margin: -8px 5px 0 0;position: absolute;top: 50%;}
		table td{padding: .2em;}
        .padTop{padding-top: 1em;}
        
        /* New or Modified css for CMSMIN Splash Screen */
        
        html, body{text-align: center;background: url("Workarea/images/en/BGIMAGE.jpg") repeat-x top center #fff !important;margin: 0;padding: 0;height: inherit;}
        table{margin: 0px auto; width:336px;}        
        .padBottom{padding-bottom:1em;}
        #loginWrapper{height: auto;float: inherit;overflow: auto;}
        .center{text-align: center;margin: 0 auto;}        
        input[type="text"], input[type="file"], input[type="password"], textarea{width: 200px;vertical-align: middle;}
        .textbox{background: url("Workarea/images/en/formfield_232x32.png") no-repeat;padding-top: 0px;margin: 2px;width: 232px;height: 32px;border: 0px;display: block;}
        .textbox select{width: 220px;border: none;margin: 6px;outline: 0;border: 0;background: none;}        
        .inpSelect{color: black;background: #fff;position: absolute;border: 0;}        
        .textbox input{background: none repeat scroll 0 0 transparent;border: 0 none transparent;height: 22px;margin: 5px 15px 2px 15px;padding: 0px;outline: 0;}
        .logo{background: url("Workarea/images/en/logo_114x93.png") no-repeat;border: 0px;margin: 0em auto;margin-bottom: 20px;padding: 0px;width: 114px;height: 93px;}
        .copyright{font-family: arial;font-size: 10px;color: #999;margin: 0px auto;position: relative;bottom: 0;}
        .ui-state-error, .ui-widget-content .ui-state-error, .ui-widget-header .ui-state-error{background: url("WorkArea/csslib/ektronTheme/images/ui-bg_diagonals-thick_20_ffa799_40x40.png") repeat scroll 50% 50% #FFA799;border: 1px solid #CD0A0A;color: #FFFFFF;text-align: left;}
        .ui-state-error .ui-icon, .ui-state-error-text .ui-icon{background-image: url("WorkArea/csslib/ektronTheme/images/ui-icons_ffd27a_256x240.png");}
        .ek_btn{margin-left: 20px;}
        .ek_btn_lft{background: url("Workarea/images/en/lft_bg.png") no-repeat scroll left top transparent;cursor: pointer;height: 41px;margin-left: -23px;margin-top: -9px;position: absolute;width: 26px;}
        .ek_btn_txt{background: url(Workarea/images/en/rte_bg.png) no-repeat right top;}
        a, a:hover,a:active,a:visited{text-decoration: none;}        
        .button{border: 0 none;color: #FFFFFF;cursor: pointer;font-family: arial;font-size: 17px;font-weight: bold;height: 21px;margin: 0;outline: 0 none;padding: 10px 24px 16px 0;text-align: center;text-transform: uppercase;}        
   
   @media screen and (-webkit-min-device-pixel-ratio:0) 
   {
      .ek_btn_lft{margin-top: -10px;} 
       }
    </style>
    <!--[if IE 8]>
    <style type="text/css">
        .ek_btn_lft{margin-left: -43px;margin-top: -7px; }
        .textbox input{margin:7px 15px 2px 15px;}
        </style>
        <![endif]-->
    <!--[if IE 9]>
    <style type="text/css">
        .ek_btn_lft{margin-left: -43px;margin-top: -7px; }
        </style>
        <![endif]-->
		<!--[if IE]><!-->
 <style>
  @media screen and (-ms-high-contrast: active), (-ms-high-contrast: none) {
  /* IE10 rule sets go here */
  .ek_btn_lft{margin-left: -43px;margin-top: -7px; }
  }
 </style>
 <!--<![endif]-->
    <!--[if IE 7]>
    <style type="text/css">
        .ek_btn_lft{margin-left: -23px;margin-top: 0px; }
        .textbox input{margin:7px 15px 2px 15px;}
        .textbox select {border:0;}
        </style>
        <![endif]-->
</head>
<body onload="setFocus();">
    <div class="bodybackground">
        <div id="loginWrapper">
            <form id="LoginRequestForm" method="post" runat="server">
            <!-- Top Logo -->
            <div class="logo">
            </div>
            <!-- Centerpanel with multiple views (Login, Logout, Password Expired) -->
            <asp:multiview id="LogInViews" activeviewindex="0" runat="server">
                <asp:view id="AnonymousView" runat="server">
                    <asp:panel id="LoginErrorPanel" runat="server" visible="False" cssclass="ui-widget">
                        <div class="ui-state-error ui-corner-all ui-helper-clearfix" style="padding: 3px .5em;
                            margin: 0">
                            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: 0.3em; margin-top: .2em">
                            </span>
                            <asp:literal id="ErrorText" runat="server" />
                        </div>
                    </asp:panel>
                    <asp:panel id="LoginRequestPanel" runat="server" visible="False" cssclass="" style="padding: 0.25em 0.2em">
                        <table class="fields">
                            <tbody>
                                <tr>
                                    <td class="label padTop">
                                        <label title="User" id="usernamelbl" for="username" runat="server" />
                                    </td>
                                    <td class="padTop">
                                        <div class="textbox">
                                            <input title="Enter Username here" type="text" name="username" id="username" runat="server" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <label title="Password" id="passwordlbl" for="password" runat="server" />
                                    </td>
                                    <td>
                                        <div class="textbox">
                                            <input title="Enter Password here" type="password" name="pwd" id="password" runat="server"
                                                autocomplete="off" /></div>
                                    </td>
                                </tr>
                                <tr id="TR_domain" runat="server" class="stripe">
                                    <td title="Domain" id="domainlbl" class="label" runat="server">
                                    </td>
                                    <td>
                                        <div class="textbox ">
                                            <asp:dropdownlist cssclass="inpSelect" tooltip="Select a Domain Name from the Drop Down Menu"
                                                id="domainname" runat="server">
                                            </asp:dropdownlist></div>
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="2" class="padTop center padBottom">
                                        <input type="submit" value="Login" onclick="SetLoginInfo()" style="position: absolute;
                                            top: -10000px;" />
                                        <asp:linkbutton id="LoginBtn" onclientclick="SetLoginInfo()" cssclass="ek_btn" runat="server">
                                            <span class="ek_btn_lft"></span>
                                            <asp:label id="loginLoginText" class="ek_btn_txt button" runat="server" />
                                        </asp:linkbutton>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                        <div class="copyright">
                            ektron <asp:Label id="lblversion" runat="server" /> &copy; 2013</div>
                    </asp:panel>
                    <asp:panel id="PasswordExpiredPanel" runat="server" visible="False" cssclass="ui-widget ui-helper-clearfix ui-corner-all"
                        style="padding: 0.2em 0.2em 0">
                        <asp:hiddenfield id="hdn_action" runat="server" />
                        <div class="ektronCaption" style="margin: 0em .25em 0em .25em">
                            <asp:literal id="passwordResetlbl" runat="server" /></div>
                        <table>
                            <tr>
                                <td class="label">
                                    <label title="New Password" id="newpassword1label" for="newpassword1" runat="server" />
                                </td>
                                <td>
                                    <div class="textbox">
                                        <input class="ektronTextXSmall" title="Enter Password here" type="password" name="newpassword1"
                                            id="newpassword1" autocomplete="off" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <label title="Confirm New Password" id="newpassword2label" for="newpassword2" runat="server" />
                                </td>
                                <td>
                                    <div class="textbox">
                                        <input class="ektronTextXSmall" title="Enter Password here to Confirm" type="password"
                                            name="newpassword2" id="newpassword2" autocomplete="off" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="padTop padBottom center">
                                    <asp:linkbutton id="changePasswordBtn" onclientclick="if (ValidatePassword()) {document.getElementById('hdn_action').value = 'reset'; document.forms[0].submit();} return false;"
                                        cssclass="ek_btn" runat="server">
                                        <span class="ek_btn_lft"></span>
                                        <asp:label id="changePasswordText" class="ek_btn_txt button" runat="server" />
                                    </asp:linkbutton>
                                    <asp:linkbutton onclientclick="document.getElementById('hdn_action').value = 'skip'; document.forms[0].submit(); return false;"
                                        id="btn_skip" cssclass="ek_btn" visible="false" runat="server">
                                        <span class="ek_btn_lft"></span>
                                        <asp:label id="skipText" class="ek_btn_txt button" runat="server" />
                                    </asp:linkbutton>
                                </td>
                            </tr>
                        </table>
                    </asp:panel>
                    <asp:literal id="ltr_olduser" runat="server"></asp:literal>
                    <asp:literal id="autologin" runat="server"></asp:literal>
                    <asp:literal id="WorkareaCloserJS" runat="server"></asp:literal>
                </asp:view>
                <asp:view id="LoggedInView" runat="server">
                    <div style="padding: 1em; text-align: center;">
                        <cms:Login ID="Login1" runat="server"></cms:Login>
                    </div>
                </asp:view>
            </asp:multiview>
            </form>
        </div>
    </div>
</body>
</html>
