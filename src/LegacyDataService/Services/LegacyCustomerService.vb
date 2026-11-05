Imports System
Imports System.Collections.Generic
Imports System.Security
Imports System.Configuration
Imports System.Security.Principal
Imports System.ServiceModel
Imports System.Threading

Namespace LegacyDataService
    Public Class LegacyCustomerService
        Implements ILegacyCustomerService

        Public Function GetCustomer(ByVal request As CustomerRequest) As CustomerRecord Implements ILegacyCustomerService.GetCustomer
            ValidateTrustedSubsystem(request)

            Dim customerId = request.CustomerId
            If customerId <= 0 Then
                Throw New FaultException(Of CustomerNotFoundFault)(New CustomerNotFoundFault With {
                    .CustomerId = customerId,
                    .Message = "The customer ID must be a positive number."
                }, "The customer ID must be a positive number.")
            End If

            Thread.CurrentPrincipal = New GenericPrincipal(
                New GenericIdentity(request.UserName),
                New String() {request.UserRole})

            Dim customer As CustomerData
            Try
                customer = CustomerData.GetCustomer(customerId)
            Catch ex As KeyNotFoundException
                Throw New FaultException(Of CustomerNotFoundFault)(New CustomerNotFoundFault With {
                    .CustomerId = customerId,
                    .Message = ex.Message
                }, ex.Message)
            End Try
            Dim upgradeStatus = LegacySharedAssembly.UpgradeMarker.GetUpgradeStatus()

            Dim status = customer.Status
            If String.IsNullOrEmpty(status) Then
                status = upgradeStatus
            End If

            Return New CustomerRecord With {
                .CustomerId = customer.CustomerId,
                .CustomerName = customer.CustomerName,
                .Status = status
            }
        End Function

        Private Shared Sub ValidateTrustedSubsystem(ByVal request As CustomerRequest)
            If request Is Nothing Then
                Throw New SecurityException("A trusted-subsystem request is required.")
            End If

            Dim expectedName = ConfigurationManager.AppSettings("LegacyTrustedApplicationName")
            Dim expectedSecret = ConfigurationManager.AppSettings("LegacyTrustedApplicationSecret")
            If String.IsNullOrEmpty(expectedName) OrElse String.IsNullOrEmpty(expectedSecret) OrElse
                Not String.Equals(request.ApplicationName, expectedName, StringComparison.Ordinal) OrElse
                Not String.Equals(request.ApplicationSecret, expectedSecret, StringComparison.Ordinal) Then
                Throw New SecurityException("The trusted application could not be authenticated.")
            End If

            If String.IsNullOrWhiteSpace(request.UserName) OrElse
                String.IsNullOrWhiteSpace(request.UserRole) Then
                Throw New SecurityException("A website user identity and role are required.")
            End If
        End Sub

    End Class
End Namespace
