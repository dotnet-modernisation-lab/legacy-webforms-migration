Imports System

Namespace LegacyDataService
    <Serializable()> _
    Public Class CustomerCriteria
        Private ReadOnly _customerId As Integer

        Public Sub New(ByVal customerId As Integer)
            _customerId = customerId
        End Sub

        Public ReadOnly Property CustomerId() As Integer
            Get
                Return _customerId
            End Get
        End Property
    End Class
End Namespace
