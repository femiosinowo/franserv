<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="404-Not-Found.aspx.cs" Inherits="_404_Not_Found" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signal Graphics | Page Not Found</title>
    <style type="text/css">
        .content {
            text-align: center;
        }
    </style>    
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="10" border="0" width="500">
            <tbody>
                <tr>
                    <td>
                        <a href="/" id="siteLink" runat="server">
                            <img border="0" alt="Sir Speedy logo" src="/uploadedimages/SignalGraphics/Content/Global/logo.png" /></a>
                        <br />
                        <br />
                        <div class="content">
                            <h1>Page Not Found</h1>
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
