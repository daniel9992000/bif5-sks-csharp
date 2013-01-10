<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstallationList.aspx.cs" Inherits="Heli.Scada.WebUI.Anlagenauswahl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Ihre Anlagen:</h2>
        <div runat="server" id="divtable">
           
        </div>
        <p runat="server" id="Serverresponse"></p>
    </div>
        <asp:Button ID="btnlogout" Text="Logout" runat="server" OnClick="btnlogout_Click" />
    </form>
</body>
</html>
