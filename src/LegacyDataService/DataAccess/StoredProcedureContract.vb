Namespace LegacyDataService
    Public Class StoredProcedureContract
        Public Const CustomerInfoSelectById As String = "dbo.CustomerInfo_SelectById"

        Public Shared ReadOnly Property CustomerIdParameterName() As String
            Get
                Return "@CustomerId"
            End Get
        End Property
    End Class
End Namespace
