<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Compatibility.aspx.vb" Inherits="LegacyWebFormsSample.LegacyWebFormsSample.CompatibilityPage" %>
<asp:Content ID="MainContentBlock" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Compatibility Review</h1>
    <div class="panel">
        <asp:Label ID="LegacySummary" runat="server" />
        <ul>
            <li>System.Web and Web Forms lifecycle</li>
            <li>ASP.NET server controls: GridView, Label, Button, TextBox</li>
            <li>CSLA ReadOnlyBase and DataPortal</li>
            <li>WCF service contract</li>
            <li>Portable .NET Framework code: HttpClient and basic string logic</li>
        </ul>
    </div>
</asp:Content>
