﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms.Instrumentation;
using Ektron.Cms;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using ASPSnippets.TwitterAPI;
using System.Data;
using System.Configuration;
using SignalGraphics.CMS;

public partial class UserControls_SendAFileOnHeader : System.Web.UI.UserControl
{
    UserAPI userApi = new UserAPI();

    private static string twitterapiKey = ConfigurationManager.AppSettings["TwitterApiKey"];
    private static string twittersecretKey = ConfigurationManager.AppSettings["TwitterSharedSecret"];
    public string GoogleAPIClientId = "'" + ConfigurationManager.AppSettings["GoogleAPIClientId"] + "'";
    public string FacebookAppId = "'" + ConfigurationManager.AppSettings["FacebookAppId"] + "'";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
        var fransId = FransDataManager.GetFranchiseId();
        string domainName = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (!string.IsNullOrEmpty(fransId))
            hddnSocialRegisterPageUrl.Value = domainName + "/" + fransId + "/social-send-a-file/";
        else
            hddnSocialRegisterPageUrl.Value = domainName + "/social-send-a-file/";        

        if (!IsPostBack)
        {
            try
            {
                TwitterConnect.API_Key = twitterapiKey;
                TwitterConnect.API_Secret = twittersecretKey;
                if (TwitterConnect.IsAuthorized)
                {
                    DataTable dt = null;
                    TwitterConnect twitter = new TwitterConnect();
                    try
                    {
                        //LoggedIn User Twitter Profile Details
                        dt = twitter.FetchProfile();
                    }
                    catch { }

                    if (dt != null)
                    {
                        Session.Add("username", dt.Rows[0]["name"].ToString());
                        Session.Add("useremail", dt.Rows[0]["screen_name"].ToString());
                        Session.Add("externalLogin", "true");
                        Session.Add("twitterLogin", "true");

                        TwitterLinkButton.Enabled = false;
                        TwitterLoginButtonConnectSocial.Enabled = false;

                        if (!string.IsNullOrEmpty(fransId))
                            Response.Redirect("/" + fransId + "/social-send-a-file/", false);
                        else
                            Response.Redirect(@"/social-send-a-file/", false);
                    }
                    else if (dt == null && Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
                    {
                        string oauth_token = Request.QueryString["oauth_token"];
                        string oauth_verifier = Request.QueryString["oauth_verifier"];
                        OAuthHelper oauthhelper = new OAuthHelper();
                        oauthhelper.GetUserTwAccessToken(oauth_token, oauth_verifier);
                        if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                        {
                            Session.Add("username", oauthhelper.screen_name);
                            if (oauthhelper.screen_name != null && oauthhelper.screen_name.IndexOf("@") > 0)
                                Session.Add("useremail", oauthhelper.screen_name);
                            Session.Add("externalLogin", "true");
                            Session.Add("twitterLogin", "true");

                            TwitterLinkButton.Enabled = false;
                            TwitterLoginButtonConnectSocial.Enabled = false;

                            if (!string.IsNullOrEmpty(fransId))
                                Response.Redirect("/" + fransId + "/social-send-a-file/", false);
                            else
                                Response.Redirect("@/social-send-a-file/", false);
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = @"Sorry, an error has occurred Authenticating with Twitter credentials";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = @"Sorry, an error has occurred Authenticating with Twitter credentials";
                    }
                }
                if (TwitterConnect.IsDenied)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "key", "alert('User has denied access.')", true);
                    lblError.Visible = true;
                    lblError.Text = @"Sorry, User has denied access.";
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message + " : " + ex.StackTrace);
                lblError.Visible = true;
                lblError.Text = @"Sorry, an error has occurred Authenticating with Twitter credentials";
            }
        }
    }

    protected void btnTwitterLogin_Click(object sender, EventArgs e)
    {
        try
        {
			lblError.Text = "";
			ClearUserSession();
            var fransId = FransDataManager.GetFranchiseId();
            if (!TwitterConnect.IsAuthorized)
            {
                string domainName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"]; 
                string callBackUrl ="";
                if (!string.IsNullOrEmpty(fransId))
                    callBackUrl = "http://" + domainName + "/" + fransId + "/";
                else
                    callBackUrl = "http://" + domainName + "/";
                TwitterConnect twitter = new TwitterConnect();
                twitter.Authorize(callBackUrl);
            }
            else
            {
                if (!string.IsNullOrEmpty(fransId))
                    Response.Redirect("/" + fransId + "/social-send-a-file/");
                else
                    Response.Redirect(@"/social-send-a-file/");
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);           
        }
    }

    protected void btnLoginUser_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
			ClearUserSession();
            var udata1 = userApi.GetUserByUsername(sign_in_email.Text);
            if (udata1 != null && udata1.Id > 0)
            {
                udata1 = userApi.logInUser(sign_in_email.Text, sign_in_password.Text, Request.ServerVariables["SERVER_NAME"], "", "");
                var udata2 = UserDataManager.getUserInfoData(sign_in_email.Text);
                if (udata1.Id == 0)
                {
                    lblError.Visible = true;
                    lblError.Text = @"Login failed. Please Check Your Email and Password";
                }
                else
                {
                    userApi.SetAuthenticationCookie(udata1);
                    if (udata1.Email != null)
                    {
                        HttpContext.Current.Session.Add("username", udata1.FirstName + " " + udata1.LastName);
                        HttpContext.Current.Session.Add("useremail", udata1.DisplayName);
                        HttpContext.Current.Session.Add("userFirstName", udata1.FirstName);
                        HttpContext.Current.Session.Add("userLastName", udata1.LastName);
                    }
                    if (udata2 != null)
                    {
                        HttpContext.Current.Session.Add("username", udata2.UserFirstName + " " + udata2.UserLastName);
                        HttpContext.Current.Session.Add("userFirstName", udata2.UserFirstName);
                        HttpContext.Current.Session.Add("userLastName", udata2.UserLastName);
                        HttpContext.Current.Session.Add("useremail", udata2.UserEmail);
                        HttpContext.Current.Session.Add("userJobTitle", udata2.UserJobTitle);
                        HttpContext.Current.Session.Add("userCompanyName", udata2.UserCompanyName);
                        HttpContext.Current.Session.Add("userPhoneNumber", udata2.UserPhoneNumber);
                        HttpContext.Current.Session.Add("userCenterId", udata2.CenterId);
                    }
                    HttpContext.Current.Session.Add("externalLogin", "false");
                    HttpContext.Current.Session.Add("twitterLogin", "false");

                    var fransId = FransDataManager.GetFranchiseId();
                    if (udata2 != null)
                    {
                        if (!string.IsNullOrEmpty(fransId))
                            Response.Redirect("/" + fransId + "/send-a-file/?centerId=" + udata2.CenterId, false);
                        else
                            Response.Redirect("/send-a-file/?centerId=" + udata2.CenterId, false);
                    }                    
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = @"Error: No user found with provided credentials";
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
            lblError.Visible = true;
            lblError.Text = "Error: No user found with provided credentials";
        }
    }
	
	private void ClearUserSession()
    {
        Session.Remove("username");
        Session.Remove("userFirstName");
        Session.Remove("userLastName");
        Session.Remove("useremail");
        Session.Remove("userJobTitle");
        Session.Remove("userCompanyName");
        Session.Remove("userPhoneNumber");
        Session.Remove("userCenterId");
        Session.Remove("externalLogin");
        Session.Remove("twitterLogin");
    }
}