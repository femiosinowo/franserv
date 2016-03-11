<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testScribd.aspx.cs" Inherits="testScribd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src='http://www.scribd.com/javascripts/scribd_api.js'></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div id='embedded_doc'><a href='http://www.scribd.com'>Scribd</a></div>

<script type='text/javascript'>
    var scribd_doc = scribd.Document.getDoc(2659, 'key-6vhdaqehjkeob');

    var onDocReady = function (e) {
        // scribd_doc.api.setPage(3);
    }

    scribd_doc.addParam('jsapi_version', 2);
    scribd_doc.addEventListener('docReady', onDocReady);
    scribd_doc.write('embedded_doc');
</script>
    </form>
</body>
</html>
