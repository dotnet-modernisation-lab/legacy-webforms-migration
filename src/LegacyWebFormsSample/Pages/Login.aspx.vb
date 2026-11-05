Imports System
Imports System.Web
Imports System.Web.Security

Namespace LegacyWebFormsSample
    Public Class LoginPage
        Inherits System.Web.UI.Page

        Protected WithEvents MessageLabel As Global.System.Web.UI.WebControls.Label
        Protected WithEvents UserNameTextBox As Global.System.Web.UI.WebControls.TextBox
        Protected WithEvents LoginButton As Global.System.Web.UI.WebControls.Button

        Public Shared Function GetCurrentRole() As String
            If HttpContext.Current Is Nothing OrElse HttpContext.Current.Request Is Nothing Then
                Return "Guest"
            End If

            Dim authCookie = HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)
            If authCookie Is Nothing OrElse String.IsNullOrEmpty(authCookie.Value) Then
                Return "Guest"
            End If

            Try
                Dim ticket = FormsAuthentication.Decrypt(authCookie.Value)
                If ticket Is Nothing OrElse String.IsNullOrEmpty(ticket.UserData) Then
                    Return "User"
                End If

                Return ticket.UserData
            Catch
                Return "Guest"
            End Try
        End Function

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Request.IsAuthenticated Then
                Response.Redirect("~/Pages/Profile.aspx", False)
                Return
            End If

            If Not IsPostBack Then
                MessageLabel.Text = "Please sign in to continue."
            End If
        End Sub

        Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginButton.Click
            Dim userName = UserNameTextBox.Text.Trim()
            If String.IsNullOrEmpty(userName) Then
                MessageLabel.Text = "A user name is required."
                Return
            End If

            Dim userRole = If(userName.Equals("admin", StringComparison.OrdinalIgnoreCase), "Admin", "User")
            Dim ticket = New FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(20), True, userRole, FormsAuthentication.FormsCookiePath)
            Dim encryptedTicket = FormsAuthentication.Encrypt(ticket)
            Dim authCookie = New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            authCookie.HttpOnly = True
            authCookie.Expires = ticket.Expiration
            Response.Cookies.Add(authCookie)

            Session("LegacyUser") = userName
            Session("LegacyUserRole") = userRole
            Session("LastPage") = Request.Path

            Dim returnUrl = Request.QueryString("ReturnUrl")
            If String.IsNullOrEmpty(returnUrl) Then
                returnUrl = FormsAuthentication.DefaultUrl
            End If

            Response.Redirect(returnUrl, False)
        End Sub
    End Class
End Namespace
