<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTransaction.aspx.cs"
    Inherits="JCB.UI.PrintTransaction" %>

<%@ Register Src="Controls/PrintControl.ascx" TagName="PrintControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href='Print.css' rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:PrintControl ID="PrintControl1" runat="server" />
  <script>        window.print();</script> 
    </form>
</body>
</html>
