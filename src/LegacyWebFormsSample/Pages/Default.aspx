<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="LegacyWebFormsSample.LegacyWebFormsSample.DefaultPage" %>
<asp:Content ID="MainContentBlock" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Legacy Web Forms Migration Sample</h1>
    <div class="panel">
        <p><asp:Label ID="SummaryLabel" runat="server" /></p>
        <ul>
            <li>ASP.NET Web Forms</li>
            <li>System.Web</li>
            <li>CSLA ReadOnlyBase</li>
            <li>WCF service boundary</li>
            <li>Fixture-backed DAL</li>
        </ul>
    </div>
</asp:Content>
