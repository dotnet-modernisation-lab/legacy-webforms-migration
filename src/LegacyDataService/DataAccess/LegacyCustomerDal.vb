Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.Serialization.Json
Imports System.Configuration

Namespace LegacyDataService
    Public Class LegacyCustomerDal
        Public Function ExecuteCustomerInfoSelectById(ByVal customerId As Integer) As CustomerRecord
            Dim customer = LoadCustomers().FirstOrDefault(Function(item) item.CustomerId = customerId)
            If customer Is Nothing Then
                Throw New KeyNotFoundException(String.Format("Customer {0} was not found in the fixture-backed legacy data store.", customerId))
            End If

            Return customer
        End Function

        Public Function GetCustomerById(ByVal customerId As Integer) As CustomerRecord
            Return ExecuteCustomerInfoSelectById(customerId)
        End Function

        Private Shared Function LoadCustomers() As List(Of CustomerRecord)
            Dim path = ResolveFixturePath()
            Dim serializer = New DataContractJsonSerializer(GetType(List(Of CustomerRecord)))

            Using stream = File.OpenRead(path)
                Dim result = CType(serializer.ReadObject(stream), List(Of CustomerRecord))
                Return result
            End Using
        End Function

        Private Shared Function ResolveFixturePath() As String
            Dim configuredPath = ConfigurationManager.AppSettings("LegacyCustomerFixturePath")
            If String.IsNullOrEmpty(configuredPath) Then
                Throw New ConfigurationErrorsException("The LegacyCustomerFixturePath appSetting is required.")
            End If

            Dim fixturePath = If(Path.IsPathRooted(configuredPath), configuredPath, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuredPath))
            If Not File.Exists(fixturePath) Then
                Throw New FileNotFoundException("Unable to locate the Customer fixture data file.", fixturePath)
            End If

            Return fixturePath
        End Function
    End Class
End Namespace
