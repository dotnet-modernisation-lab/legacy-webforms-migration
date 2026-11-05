Namespace LegacyWebFormsSample
    Public Class ProfilePage
        Inherits System.Web.UI.Page

        Protected WithEvents SessionLabel As Global.System.Web.UI.WebControls.Label

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Request.IsAuthenticated Then
                Response.Redirect("~/Pages/Login.aspx?ReturnUrl=" & Server.UrlEncode(Request.RawUrl), True)
                Return
            End If

            Dim userName = If(Session("LegacyUser"), User.Identity.Name)
            Dim userRole = If(Session("LegacyUserRole"), LoginPage.GetCurrentRole())
            SessionLabel.Text = "Current session user: " & userName.ToString() & ". Role: " & userRole.ToString() & ". Last page: " & If(Session("LastPage"), "/Pages/Default.aspx").ToString()
        End Sub
    End Class
End Namespace
