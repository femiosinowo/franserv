<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SiteError.aspx.cs" Inherits="SiteError" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title>Signal Graphics | Site Error</title>  
    <style type="text/css">
        .content{
            text-align:center;
        }
    </style>     
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <table cellspacing="10" border="0" width="500">
                <tbody>
                    <tr>
                        <td>
                            <a href="/">
                                <img border="0" alt="Sir Speedy logo" src="/uploadedimages/SignalGraphics/Content/Global/logo.png" /></a>
                            <br />
                            <br />
                            <h1>Site Error</h1>
                            <p></p>
                            <p>We’re sorry, but the page you are looking for is no longer available or the link is not currently functioning. Please visit our <a id="siteMapLink" runat="server" href="/site-map/">site map</a> and try again.</p>
                            <p></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <input type="hidden" id="hddnCenterId" runat="server" class="hddnCenterId" value="" />
        </div>
    </form>
</body>
</html>


