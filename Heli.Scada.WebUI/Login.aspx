<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Heli.Scada.WebUI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id ="login" style="margin:auto;">
        <h2 style="text-align:center;">Heli.Scada - Login</h2>
        <table style="margin:auto;">
            <tr>
                <td><label>Benutzername: </label></td>
                <td><input type="text" required="required" id="txtbenutzername" runat="server"  /></td>
            </tr>
            <tr>
                <td><label>Passwort: </label></td>
                <td><input runat="server" required="required" type="password" id="txtpasswort" /></td>
            </tr>
            <tr>
                <td><asp:Button OnClick="btnlogin_Click" ID="btnlogin" Text="Login" runat="server"/></td>
            </tr>
        </table>
        <p runat="server" id="loginresponse" style="margin:auto; text-align:center;"></p>
    </div>
    </form>
</body>
</html>
