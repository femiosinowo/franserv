<%@ Page Language="C#" AutoEventWireup="true" CodeFile="URLRedirects.aspx.cs" Inherits="URLRedirects" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <h2>Helper page to add 301 re-directs to the system.</h2>
        <br />
        Before you start please copy the latest excel files on the Author Server (10.0.0.164) on the foolowing locations:<br />
        C:\\Temp\\SGCenterList_URLRedirects.xlsx
        <br />
        <br />
        <asp:Label ID="lblStatus" runat="server" ></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnAddRedirects" runat="server" Text="Add 301 Re-directs from Excel" OnClick="btnAddRedirects_Click" />
    </div>
    </form>
</body>
</html>
