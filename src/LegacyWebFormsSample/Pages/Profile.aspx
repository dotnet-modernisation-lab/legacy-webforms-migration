<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Profile.aspx.vb" Inherits="LegacyWebFormsSample.LegacyWebFormsSample.ProfilePage" %>
<asp:Content ID="MainContentBlock" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Session Profile</h1>
    <div class="panel">
        <asp:Label ID="SessionLabel" runat="server" />
    </div>
</asp:Content>
