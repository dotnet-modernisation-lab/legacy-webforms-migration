<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="LegacyWebFormsSample.LegacyWebFormsSample.LoginPage" %>
<asp:Content ID="MainContentBlock" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sign In</h1>
    <div class="panel">
        <asp:Label ID="MessageLabel" runat="server" /><br /><br />
        <asp:TextBox ID="UserNameTextBox" runat="server" Width="200px" />
        <asp:Button ID="LoginButton" runat="server" Text="Sign In" />
    </div>
</asp:Content>
