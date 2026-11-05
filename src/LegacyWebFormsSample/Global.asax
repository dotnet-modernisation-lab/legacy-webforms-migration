<%@ Application Language="VB" %>
<script runat="server">
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Application("AppName") = "Legacy Web Forms Migration Sample"
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        If HttpContext.Current Is Nothing OrElse HttpContext.Current.User Is Nothing OrElse
            Not HttpContext.Current.Request.IsAuthenticated Then
            Return
        End If

        Dim identity = TryCast(HttpContext.Current.User.Identity, System.Web.Security.FormsIdentity)
        If identity Is Nothing Then
            Return
        End If

        Dim role = identity.Ticket.UserData
        If String.IsNullOrEmpty(role) Then
            role = "User"
        End If

        HttpContext.Current.User = New System.Security.Principal.GenericPrincipal(identity, New String() {role})
        System.Threading.Thread.CurrentPrincipal = HttpContext.Current.User
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Session("LegacyUser") = "Guest"
        Session("LastPage") = "/Pages/Default.aspx"
    End Sub
</script>
