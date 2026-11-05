Imports System

Namespace LegacyWebFormsSample
    Public Class AjaxToolkitPage
        Inherits System.Web.UI.Page

        Protected WithEvents DateTextBox As Global.System.Web.UI.WebControls.TextBox
        Protected WithEvents SubmitButton As Global.System.Web.UI.WebControls.Button
        Protected WithEvents ResultLabel As Global.System.Web.UI.WebControls.Label

        Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitButton.Click
            Dim selectedDate As DateTime
            If DateTime.TryParse(DateTextBox.Text, selectedDate) Then
                ResultLabel.Text = " Selected date: " & selectedDate.ToString("yyyy-MM-dd")
            Else
                ResultLabel.Text = " Enter a valid date."
            End If
        End Sub
    End Class
End Namespace
