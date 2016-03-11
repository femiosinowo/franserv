<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlickrPhotoDetail.aspx.cs" Title="Flickr Photo Images"
    Inherits="AdminTool_Templates_FlickrPhotoDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Flickr Photo Set Images:</h2>
    </div>
        <asp:Literal ID="photoSetImages" runat="server"></asp:Literal>
    </form>
</body>
</html>
