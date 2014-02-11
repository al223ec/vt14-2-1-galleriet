<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_2_1_galleriet.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/style.css" rel="stylesheet" />
    <title>Anton Ledström, 2-1 - Galleriet</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="MainPanel" runat="server" DefaultButton="">

            <%-- Header --%>
            <p class="heading">2-1 - Galleriet</p>
            <h1>Bildgalleriet </h1>
            <%-- Validering --%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error" />
            <%-- Huvudbild --%>
            <div>
                <img src="Content/img/Koala.jpg" alt="KOALA" />
            </div>
            <%-- ThumbnailPanel --%>
            <asp:Panel ID="ThumbnailPanel" runat="server">
            </asp:Panel>

            <%-- Footer --%>
            <p class="footer">
                Anton Ledström
            </p>
        </asp:Panel>
    </form>
</body>
</html>
