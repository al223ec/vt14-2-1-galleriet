<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_2_1_galleriet.Default" ViewStateMode="Disabled" Trace="true" %>

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
            <%-- Successfullpanel --%>
            <asp:Panel ID="SuccessFullUploadPanel" runat="server" Visible="false">
                <p class="intro">
                    <asp:Literal ID="OutputLiteral" runat="server"></asp:Literal>
                    <asp:Button ID="CloseUploadButton" runat="server" Text="Close" CausesValidation="false" />
                </p>
            </asp:Panel>

            <%-- Validering summary --%>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="error" />
            <%-- Huvudbild --%>
            <div>
                <asp:Image ID="MainImage" runat="server" Visible="false" />
            </div>
            <%-- ThumbnailPanel --%>
            <asp:Panel ID="ThumbnailPanel" CssClass="ThumbnailDiv" runat="server">
                <asp:Repeater ID="Repeater" runat="server" ItemType="_2_1_galleriet.Model.GalleryImage" SelectMethod="Repeater_GetData" OnItemDataBound="Repeater_ItemDataBound">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink" runat="server">
                            <asp:Image ID="Image" runat="server" ImageUrl='<%# Item.ThumbImgPath%>' />
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <%-- Uploadpanel --%>
            <asp:Panel ID="UpLoadPanel" runat="server">
                <asp:FileUpload ID="FileUploader" runat="server" />
                <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click"/>
                <%-- Validering, tomt fält och REGEX --%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Var god välj en bild" ControlToValidate="FileUploader" CssClass="error"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="Fel filändelse" ControlToValidate="FileUploader" ValidationExpression="^.*\.(jpg|gif|png)$" CssClass="error" ></asp:RegularExpressionValidator>
            </asp:Panel>

            <%-- Footer --%>
            <p class="footer">
                Anton Ledström
            </p>
        </asp:Panel>
    </form>
</body>
</html>
