Imports System

Namespace LegacyDataService
    <Serializable()> _
    Public Class CustomerData
        Inherits Csla.ReadOnlyBase(Of CustomerData)

        Private _customerId As Integer
        Private _customerName As String
        Private _status As String

        Public Shared Function GetCustomer(ByVal customerId As Integer) As CustomerData
            Return Csla.DataPortal.Fetch(Of CustomerData)(New CustomerCriteria(customerId))
        End Function

        Public ReadOnly Property CustomerId() As Integer
            Get
                Return _customerId
            End Get
        End Property

        Public ReadOnly Property CustomerName() As String
            Get
                Return _customerName
            End Get
        End Property

        Public ReadOnly Property Status() As String
            Get
                Return _status
            End Get
        End Property

        Protected Overrides Sub AddAuthorizationRules()
            MyBase.AddAuthorizationRules()
            AuthorizationRules.AllowRead("CustomerId", "Admin")
            AuthorizationRules.AllowRead("CustomerName", "Admin")
            AuthorizationRules.AllowRead("Status", "Admin")
        End Sub

        Protected Overrides Function GetIdValue() As Object
            Return _customerId
        End Function

        Protected Overrides Sub DataPortal_Fetch(ByVal criteria As Object)
            Dim customerCriteria = CType(criteria, CustomerCriteria)
            Dim record = New LegacyCustomerDal().GetCustomerById(customerCriteria.CustomerId)

            _customerId = record.CustomerId
            _customerName = record.CustomerName
            _status = record.Status
        End Sub
    End Class
End Namespace
