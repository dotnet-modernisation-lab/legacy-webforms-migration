Imports System
Imports System.Collections.Generic

Namespace LegacyWebFormsSample
    Public Class DataAccessPage
        Inherits System.Web.UI.Page

        Protected WithEvents StatusLabel As Global.System.Web.UI.WebControls.Label
        Protected WithEvents CustomerGrid As Global.System.Web.UI.WebControls.GridView

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Request.IsAuthenticated Then
                Response.Redirect("~/Pages/Login.aspx?ReturnUrl=" & Server.UrlEncode(Request.RawUrl), True)
                Return
            End If

            Dim userRoleValue = If(Session("LegacyUserRole"), LoginPage.GetCurrentRole())
            Dim userRole As String = Convert.ToString(userRoleValue)
            If Not String.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase) Then
                StatusLabel.Text = "Access denied. Only Admin users can access customer data."
                CustomerGrid.Visible = False
                Return
            End If

            If Not IsPostBack Then
                Dim requestedCustomerId = 1042
                Dim customerIdText = Request.QueryString("customerId")
                If Not String.IsNullOrEmpty(customerIdText) Then
                    If Not Integer.TryParse(customerIdText, requestedCustomerId) OrElse requestedCustomerId <= 0 Then
                        StatusLabel.Text = "Customer ID must be a positive number."
                        CustomerGrid.Visible = False
                        Return
                    End If
                End If

                Try
                    Dim customer = CustomerInfo.GetCustomer(requestedCustomerId)
                    If customer Is Nothing OrElse customer.CustomerId = 0 Then
                        Throw New InvalidOperationException("Customer data was not authorized by CSLA.")
                    End If

                    Dim list = New List(Of CustomerInfo) From {customer}
                    CustomerGrid.DataSource = list
                    CustomerGrid.DataBind()
                    StatusLabel.Text = "Loaded customer " & customer.CustomerId & " from the legacy CSLA/WCF/DAL path."
                Catch ex As Exception
                    StatusLabel.Text = "Customer retrieval failed: " & ex.Message
                End Try
            End If
        End Sub
    End Class
End Namespace
