Imports System.ServiceModel
Imports System.Runtime.Serialization

Namespace LegacyDataService
    <ServiceContract()> _
    Public Interface ILegacyCustomerService
        <OperationContract()> _
        <FaultContract(GetType(CustomerNotFoundFault))> _
        Function GetCustomer(ByVal request As CustomerRequest) As CustomerRecord
    End Interface

    <DataContract()> _
    Public Class CustomerRequest
        <DataMember()> Public Property CustomerId As Integer
        <DataMember()> Public Property ApplicationName As String
        <DataMember()> Public Property ApplicationSecret As String
        <DataMember()> Public Property UserName As String
        <DataMember()> Public Property UserRole As String
    End Class

    <DataContract()> _
    Public Class CustomerNotFoundFault
        <DataMember()> Public Property CustomerId As Integer
        <DataMember()> Public Property Message As String
    End Class

End Namespace
