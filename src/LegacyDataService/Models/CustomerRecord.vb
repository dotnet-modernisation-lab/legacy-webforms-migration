Imports System.Runtime.Serialization

Namespace LegacyDataService
    <DataContract()> _
    Public Class CustomerRecord
        <DataMember()> Public Property CustomerId As Integer
        <DataMember()> Public Property CustomerName As String
        <DataMember()> Public Property Status As String
    End Class
End Namespace
