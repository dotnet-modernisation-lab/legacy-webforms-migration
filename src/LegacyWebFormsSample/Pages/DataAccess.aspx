<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="DataAccess.aspx.vb" Inherits="LegacyWebFormsSample.LegacyWebFormsSample.DataAccessPage" %>
<asp:Content ID="MainContentBlock" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Customer Retrieval</h1>
    <div class="panel">
        <asp:Label ID="StatusLabel" runat="server" /><br /><br />
        <asp:GridView ID="CustomerGrid" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="CustomerId" HeaderText="Customer Id" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
