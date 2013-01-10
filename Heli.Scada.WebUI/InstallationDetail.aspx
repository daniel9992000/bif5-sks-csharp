<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstallationDetail.aspx.cs" Inherits="Heli.Scada.WebUI.InstallationDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <h4>Anlagenzustand</h4>
    <div runat="server" id="anlagenzustand">
        
    </div>

    <h4>Statistikauswahl</h4>
    <div runat="server" id="statistikauswahl">
        <asp:Button ID="daystat" runat="server" Text="Tagesstatistik" OnClick="daystat_Click" />
        <asp:Button ID="monthstat" runat="server" Text="Monatsstatistik" OnClick="monthstat_Click" />
        <asp:Button ID="yearstat" runat="server" Text="Jahresstatistik" OnClick="yearstat_Click" />
    </div>
    <h4>Statistikanzeige</h4>
    <div runat="server" id="statistikanzeige"> 

    </div>
        
        <asp:Button ID="btnback" Text="Zurück" OnClick="btnback_Click" runat="server" />
    </form>
  
</body>
</html>
