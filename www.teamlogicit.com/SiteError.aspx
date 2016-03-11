<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SiteError.aspx.cs" Inherits="SiteError" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TeamLogicIT | Site Error</title>
    <style type="text/css">
        .content {
            text-align: center;
            margin-top: 30%;
        }
        .top_header {
            background: none repeat scroll 0 0 rgba(0, 0, 0, 0.8);
            height: 125px !important;
            overflow: hidden;
            position: fixed;
            width: 97%;
            z-index: 200;
        }
        .clearfix {
            display: block;
        }
    </style>    
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="10" border="0" width="500">
            <tbody>
                <tr>
                    <td>
                        <div class="top_header clearfix">
                        <a href="/">
                            <img border="0" alt="Sir Speedy logo" src="/uploadedImages/SirSpeedy/Content/Global/logo(1).png" /></a>
                        <br />
                        <br />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>                        
                        <div class="content">
                            <h1>Site Error</h1>
                            <p></p>
                            <p>We’re sorry, but the page you are looking for is no longer available or the link is not currently functioning. Please visit our <a id="siteMapLink" runat="server" href="/site-map/">site map</a> and try again.</p>
                            <p></p>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
