<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SendAFileOnHeader.ascx.cs"
    Inherits="UserControls_SendAFileOnHeader" %>

<!-- login with google plus account-->
<script lang="javascript" type="text/javascript">
    var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
    var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
    var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
    var CLIENTID = <%=this.GoogleAPIClientId%>;
    var REDIRECT = 'http://' + document.location.hostname + '/';
    var LOGOUT = 'http://accounts.google.com/Logout';
    var TYPE = 'token';
    var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
    var acToken;
    var tokenType;
    var expiresIn;
    var user;
    var loggedIn = false;

    function GPluslogin() {       
        var win = window.open(_url, "windowname1", 'width=800, height=600');
        var pollTimer = window.setInterval(function () {
            try {
                console.log(win.document.URL);
                if (win.document.URL.indexOf(REDIRECT) != -1) {
                    window.clearInterval(pollTimer);
                    var url = win.document.URL;
                    acToken = gup(url, 'access_token');
                    tokenType = gup(url, 'token_type');
                    expiresIn = gup(url, 'expires_in');
                    win.close();
                    validateToken(acToken);
                }
            } catch (e) {
            }
        }, 500);
    }
    function validateToken(token) {
        $.ajax({
            url: VALIDURL + token,
            data: null,
            success: function (responseText) {
                console.log(responseText);
                getGoogleUserInfo();
                loggedIn = true;
            },
            error: function (err) {
                loggedIn = false;
                alert('Login failed. Please try again');
                window.location.href = window.location.href;
            },
            dataType: "jsonp"
        });
    }
    function getGoogleUserInfo() {
        $.ajax({
            url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
            data: null,
            success: function (resp) {
                user = resp;
                console.log(user);
                generateGSession(user);
            },
            error: function (err) {
                loggedIn = false;
                alert('Login failed. Please try again');
                window.location.href = window.location.href;
            },
            dataType: "jsonp"
        });
    }

    function generateGSession(response) {
        $.ajax({
            type: "POST",
            url: "/loginSession.aspx/addSession",
            data: "{'username':'" + response.name + "','useremail':'" + response.email + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                window.location.href = document.getElementById("<%= hddnSocialRegisterPageUrl.ClientID %>").value;
            },
            error: function (err) {
                loggedIn = false;
                alert('Login failed. Please try again');
                window.location.href = window.location.href;
            }
        });
        }

        //credits: http://www.netlobo.com/url_query_string_javascript.html
        function gup(url, name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\#&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(url);
            if (results == null)
                return "";
            else
                return results[1];
        }

        function startLogoutPolling() {
            //$('#loginText').show();
            //$('#logoutText').hide();
            loggedIn = false;
            //$('#uName').text('Welcome ');
            //$('#imgHolder').attr('src', 'none.jpg');
        }
  
</script>

<!-- login with openid-->


<!-- login with facebook account-->
<div id="fb-root">
</div>
<script type="text/javascript">
    window.fbAsyncInit = function () {
        FB.init({
            appId: <%=this.FacebookAppId%>, //'390446137764015', //TODO: Ask for client's App ID 
            channelUrl: 'http://' + document.location.hostname + '/channel.html', // Channel File
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });


        FB.Event.subscribe('auth.authResponseChange', function (response) {
            if (response.status === 'connected') {
                //document.getElementById("message").innerHTML += "<br>Connected to Facebook";
                //SUCCESS
            }
            else if (response.status === 'not_authorized') {
                //document.getElementById("message").innerHTML += "<br>Failed to Connect";
                //FAILED
            } else {
                //document.getElementById("message").innerHTML += "<br>Logged Out";
                //UNKNOWN ERROR
            }
        });

    };

    function FBLogin() {        
        FB.login(function (response) {
            if (response.authResponse) {
                getUserInfo();
            } else {
                console.log('User cancelled login or did not fully authorize.');
            }
        }, { scope: 'email,user_photos,user_videos' });


    }

    function getUserInfo() {
        FB.api('/me', function (response) {
            $.ajax({
                type: "POST",
                url: "/loginSession.aspx/addSession",
                data: "{'username':'" + response.name + "','useremail':'" + response.email + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    window.location.href = document.getElementById("<%= hddnSocialRegisterPageUrl.ClientID %>").value;
                },
                error: function (err) {
                    window.location.href = window.location.href;
                }
            });
        });
        }

        function getPhoto() {
            FB.api('/me/picture?type=normal', function (response) {
                //var str = "<br/><b>Pic</b> : <img src='" + response.data.url + "'/>";
            });

        }

        function Logout() {
            FB.logout(function () {document.location.reload(); });
        }

        // Load the SDK asynchronously
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        } (document));

</script>

<script type="text/javascript">
    function DoPostForTwitter() {
        __doPostBack('<%= TwitterLoginButtonConnectSocial.ClientID %>', 'onClickTwitter');
    }
    function DoPostForLinkTwitter() {
        __doPostBack('<%= TwitterLinkButton.ClientID %>', 'onClickTwitter');
    }
    function DoPostForEmailLogin() {
        __doPostBack('<%= send_file_sign_in_submit.ClientID %>', 'onClickEmailLogin');
    }
</script>
<div class="utility_content" id="send_file">
    <div class="grid_12 send_file_wrapper clearfix" id="returning_customers">
        <h2>
            Sign In <span>Returning Customers</span></h2>
        <div class="prefix_1 grid_22 suffix_1 send_file_content">
            <div class="connect_social">
                <h3>
                    Connect With A Social Network</h3>
                <ul>
                    <li class="sf-facebook"><a href="javascript:void('0')" onclick="FBLogin()">
                        <img alt="Facebook" src="/images/social-icons/send-file-social-fb.jpg"></a></li>
                    <li class="sf-twitter">
                        <asp:LinkButton ID="TwitterLoginButtonConnectSocial" runat="server" OnClick="btnTwitterLogin_Click">
                    <img alt="Twitter" src="/images/social-icons/send-file-social-tw.jpg"></asp:LinkButton></li>
                    <li class="sf-google-plus"><a href="javascript:void('0')" onclick="GPluslogin()">
                        <img alt="Google Plus" src="/images/social-icons/send-file-social-gp.jpg"></a></li>
                    <%--<li><a class="fancybox iframe view-map" id="OpenIdLogin" runat="server" title="OpenId Login" href="/OpenIdLogin.aspx">
                    <span><img alt="OpenId" src="/images/social-icons/marketing-tango-2.png"></span></a></li>--%>
                    <script type="IN/Login"></script>
                </ul>
            </div>
            <asp:HiddenField ID="hdnOpenIDLoginStatus" runat="server" Value="" />
            <!-- end connect_social -->
            <hr />
            <div class="form" id="send_file_sign_in">
                    <h3>Sign In With Your Email Address</h3>
                    <%--<input type="text" placeholder="Email address" runat="server" required="required" id="sign_in_email" />--%>
                    <asp:TextBox runat="server" ID="sign_in_email" placeholder="Email address" ValidationGroup="EmailSignIn" />
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="EmailSignIn" ErrorMessage="email is required"
                        ControlToValidate="sign_in_email" Style="color: #DF1B23;"></asp:RequiredFieldValidator>
               
                    <%--<input type="password" placeholder="Password" runat="server" required="required" id="sign_in_password" />--%>
                    <asp:TextBox runat="server" ID="sign_in_password" placeholder="Password" TextMode="Password"
                        ValidationGroup="EmailSignIn" />
                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ValidationGroup="EmailSignIn" ErrorMessage="password is required"
                        ControlToValidate="sign_in_password" Style="color: #DF1B23;"></asp:RequiredFieldValidator>
                
                <div class="clear">
                </div>
                <%--<input type="checkbox" id="sign_in_remember" /> 
            <label for="sign_in_remember">
                Remember Me</label>--%>
               <div class="remember_me_wrap">
                <asp:CheckBox ID="sign_in_remember" runat="server" Text="Remember Me" />
                </div>
                <div class="clear">
                </div>
                <asp:Label ID="lblError" runat="server"></asp:Label>
                 <div class="grid_24">
			   <div class="grid_6"> 
                    <%--<input class="red_btn" runat="server" type="submit" id="submit_sign_in" value="Sign In" />--%>
                    <asp:Button ID="send_file_sign_in_submit" runat="server" Text="Sign In" OnClick="btnLoginUser_Click"
                        CssClass="cta-button-wrap btn" ValidationGroup="EmailSignIn" />
                </div>
                <!-- end grid -->
                <div class="grid_18">
                    <p class="small-text">
                        <%--<a runat="server" href="#forgotPassword" class="fancybox red-text" style="font-size:11px"><span>Forgot your username and/or password?</span></a>--%>
                        <a class="fancybox iframe view-map small-text" id="forgot_password" runat="server"
                            title="Forgot your Password?" href="/Forgot-Password/">
                                Forgot your password?</a></p>
                    <%--<a href="#" class="red-text" style="font-size:11.5px">Forgot <span class=" red-text fancybox iframe view-mapshorten-text">your username and/or</span>
                        password?</a></p>--%>
                </div>
                <%--<ux:ForgotPassword ID="uxForgotPassword" runat="server" Visible="true" />--%>
                <!-- end grid -->
                </div>
            </div> <!-- end grid 24-->
        </div>
        <!-- end send_file_content -->
    </div>
    <!-- end grid -->
    <div class="grid_12 send_file_wrapper clearfix" id="new_guest_customers">
        <div id="new_customers">
            <h2>
                Sign In <span>New Customers</span></h2>
            <div class="grid_24 send_file_content">
                <div class="connect_social">
                    <h3>
                        Sign Up With a Social Network</h3>
                    <ul>
                        <li class="sf-facebook"><a href="javascript:void('0')" onclick="FBLogin()">
                            <img alt="Facebook" src="/images/social-icons/send-file-social-fb.jpg" /></a></li>
                        <li class="sf-twitter">
                            <asp:LinkButton ID="TwitterLinkButton" runat="server" OnClick="btnTwitterLogin_Click">
                            <img alt="Twitter" src="/images/social-icons/send-file-social-tw.jpg" /></asp:LinkButton></li>
                        <li class="sf-google-plus"><a href="javascript:void('0')" onclick="GPluslogin()">
                            <img alt="Google Plus" src="/images/social-icons/send-file-social-gp.jpg" /></a></li>
                        <%-- <li><a class="fancybox iframe view-map" id="OpenIdSignUp" runat="server" title="OpenId Login" href="/OpenIdLogin.aspx">
                    <span><img alt="OpenId" src="/images/social-icons/marketing-tango-2.png"></span></a></li>--%>
                    </ul>
                </div>
                <!-- end connect_social -->
                <hr />
                <div id="sign_up">
                    <h3>
                        Sign up with Your Email Address</h3>
                    <a href="/register/" class="cta-button-text">
                        <div class="cta-button-wrap white-btn">
                            <span>Register</span>
                        </div>
                    </a>
                    <!-- end cta-button-wrap -->
                </div>
            </div>
        </div>
        <!-- end grid send_file_content -->
        <div class="clear">
        </div>
        <div id="send_file_guest">
            <div class="grid_24 send_file_content">
                <div class="grid_12 alpha">
                    <h2>
                        Not Ready to Sign Up</h2>
                    <p class="small-text">
                        Sending Files is Quick and Easy</p>
                </div>
                <!-- end grid -->
                <div class="grid_12 omega">
                    <a href="/guest-send-a-file/" class="cta-button-text">
                        <div class="cta-button-wrap white-btn">
                            <span>Send Files as a Guest</span>
                        </div>
                    </a>
                </div>
                <!-- end grid -->
            </div>
            <!-- end send_file_content -->
        </div>
        <!-- end send_file_guest -->
    </div>
</div>
<input type="hidden" id="hddnSocialRegisterPageUrl" class="hddnSocialRegisterPageUrl" runat="server" />  
<!-- end col2 -->
<!-- end send_file -->
