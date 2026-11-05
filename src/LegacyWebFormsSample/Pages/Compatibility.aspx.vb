Namespace LegacyWebFormsSample
    Public Class CompatibilityPage
        Inherits System.Web.UI.Page

        Protected WithEvents LegacySummary As Global.System.Web.UI.WebControls.Label

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim client = New System.Net.Http.HttpClient()
            Dim status = "Legacy and portable code coexist in this sample: System.Web + Web Forms + CSLA + WCF; HttpClient demonstrates a more portable component."
            LegacySummary.Text = status
            client.Dispose()
        End Sub
    End Class
End Namespace
