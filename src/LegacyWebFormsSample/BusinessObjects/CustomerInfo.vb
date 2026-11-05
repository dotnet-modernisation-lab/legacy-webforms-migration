Imports System
Imports System.Configuration
Imports System.Security
Imports System.ServiceModel
Imports System.Web
Imports LegacyDataService.LegacyDataService

Namespace LegacyWebFormsSample
    <Serializable()>
    Public Class CustomerInfo
        Inherits Csla.ReadOnlyBase(Of CustomerInfo)

        Private _customerId As Integer
        Private _customerName As String
        Private _status As String

        Public Shared Function GetCustomer(ByVal customerId As Integer) As CustomerInfo
            Dim criteria = New CustomerCriteria(customerId)
            Return Csla.DataPortal.Fetch(Of CustomerInfo)(criteria)
        End Function

        Public Shared Function IsAuthenticatedUser() As Boolean
            Dim currentUser = HttpContext.Current
            If currentUser Is Nothing OrElse currentUser.User Is Nothing OrElse currentUser.User.Identity Is Nothing Then
                Return False
            End If

            Return currentUser.User.Identity.IsAuthenticated
        End Function

        Public Shared Function IsAuthorizedUser() As Boolean
            Dim currentContext = HttpContext.Current
            If Not IsAuthenticatedUser() Then
                Return False
            End If

            Dim role = LoginPage.GetCurrentRole()
            If Not String.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase) Then
                Return False
            End If

            Return True
        End Function

        Public ReadOnly Property CustomerId() As Integer
            Get
                If Not CanReadProperty("CustomerId") Then
                    Return 0
                End If

                Return _customerId
            End Get
        End Property

        Public ReadOnly Property CustomerName() As String
            Get
                If Not CanReadProperty("CustomerName") Then
                    Return Nothing
                End If

                Return _customerName
            End Get
        End Property

        Public ReadOnly Property Status() As String
            Get
                If Not CanReadProperty("Status") Then
                    Return Nothing
                End If

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
            If Not IsAuthorizedUser() Then
                Throw New SecurityException("An authenticated Admin user is required before loading customer data through CSLA.")
            End If

            Dim customerCriteria = CType(criteria, CustomerCriteria)
            Dim record = WcfClientFacade.GetCustomer(customerCriteria.CustomerId)

            _customerId = record.CustomerId
            _customerName = record.CustomerName
            _status = record.Status
        End Sub
    End Class

    Public NotInheritable Class WcfClientFacade
        Private Sub New()
        End Sub

        Public Shared Function GetCustomer(ByVal customerId As Integer) As CustomerRecord
            Dim serviceUrl = ConfigurationManager.AppSettings("LegacyCustomerServiceUrl")
            If String.IsNullOrEmpty(serviceUrl) Then
                Throw New ConfigurationErrorsException("The LegacyCustomerServiceUrl appSetting is required.")
            End If

            Dim currentContext = HttpContext.Current
            If currentContext Is Nothing OrElse currentContext.User Is Nothing OrElse
                currentContext.User.Identity Is Nothing Then
                Throw New SecurityException("An authenticated Admin user is required before calling the customer service.")
            End If

            If Not CustomerInfo.IsAuthorizedUser() Then
                Throw New SecurityException("An authenticated Admin user is required before calling the customer service.")
            End If

            Dim binding = New BasicHttpBinding()
            Dim factory = New ChannelFactory(Of ILegacyCustomerService)(binding, New EndpointAddress(serviceUrl))
            Dim channel = factory.CreateChannel()

            Try
                Dim result = channel.GetCustomer(New CustomerRequest With {
                    .CustomerId = customerId,
                    .ApplicationName = ConfigurationManager.AppSettings("LegacyTrustedApplicationName"),
                    .ApplicationSecret = ConfigurationManager.AppSettings("LegacyTrustedApplicationSecret"),
                    .UserName = currentContext.User.Identity.Name,
                    .UserRole = LoginPage.GetCurrentRole()
                })

                CType(channel, IClientChannel).Close()
                factory.Close()
                Return result
            Catch
                CType(channel, IClientChannel).Abort()
                factory.Abort()
                Throw
            End Try
        End Function
    End Class
End Namespace
