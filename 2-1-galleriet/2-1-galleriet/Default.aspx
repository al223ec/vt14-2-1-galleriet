<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_2_1_galleriet.Default" ViewStateMode="Disabled" Trace="true"%>
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
                <asp:Image ID="MainImage" runat="server" visible="false"/>
            </div>
            <%-- ThumbnailPanel --%>
            <asp:Panel ID="ThumbnailPanel" CssClass="ThumbnailDiv" runat="server">
                <asp:Repeater ID="Repeater" runat="server" ItemType="_2_1_galleriet.Model.GalleryImage" SelectMethod="Repeater_GetData" OnItemDataBound="Repeater_ItemDataBound">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink" runat="server"><asp:Image ID="Image" runat="server" ImageUrl='<%# Item.ThumbImgPath%>'/> </asp:HyperLink>
                </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>

            <asp:Panel ID="UpLoadPanel" runat="server">
                <asp:FileUpload ID="FileUploader" runat="server" /> <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click"/>
            </asp:Panel>
            <asp:Panel ID="SuccessFullUploadPanel" runat="server" Visible="false">
                <p class="intro">
                    <asp:Literal ID="OutputLiteral" runat="server"></asp:Literal>
                </p>
            </asp:Panel>
            
            <%-- Footer --%>
            <p class="footer">
                Anton Ledström
            </p>
        </asp:Panel>
    </form>
</body>
</html>
