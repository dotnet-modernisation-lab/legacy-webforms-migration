<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="AjaxToolkit.aspx.vb" Inherits="LegacyWebFormsSample.LegacyWebFormsSample.AjaxToolkitPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="MainContentBlock" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h1>AjaxControlToolkit Example</h1>
    <div class="panel">
        <p>This legacy page demonstrates a CalendarExtender attached to an ASP.NET TextBox.</p>
        <asp:Label ID="DateLabel" runat="server" AssociatedControlID="DateTextBox" Text="Select a date:" />
        <asp:TextBox ID="DateTextBox" runat="server" />
        <asp:Button ID="CalendarButton" runat="server" Text="Open calendar" CausesValidation="false" />
        <ajaxToolkit:CalendarExtender ID="DateCalendarExtender" runat="server"
            TargetControlID="DateTextBox" PopupButtonID="CalendarButton" Format="yyyy-MM-dd"
            CssClass="ajax__calendar" />
        <br /><br />
        <asp:Button ID="SubmitButton" runat="server" Text="Display date" />
        <asp:Label ID="ResultLabel" runat="server" />
    </div>
</asp:Content>
