Namespace LegacyWebFormsSample
    Public Class DefaultPage
        Inherits System.Web.UI.Page

        Protected WithEvents SummaryLabel As Global.System.Web.UI.WebControls.Label

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SummaryLabel.Text = "This application demonstrates a classic Visual Basic .NET Web Forms baseline for migration assessment."
        End Sub
    End Class
End Namespace
