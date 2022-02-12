
Imports System.Data.OleDb
Imports System.Data
Public Class frmSalesDetail
    Private Const ConnectionString As String = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\PROJECT\icecream.mdb;"

    Private Sub frmStockDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = GetData()
    End Sub
    Private ReadOnly Property Connection() As OleDbConnection
        Get
            Dim ConnectionToFetch As New OleDbConnection(ConnectionString)
            ConnectionToFetch.Open()
            Return ConnectionToFetch
        End Get
    End Property
    Public Function GetData() As DataView
        Dim SelectQry = "SELECT (BillNo) as [BILL NO],(CustomerId) as [CUSTOMER ID],(CustomerName) as [CUSTOMER NAME],(CustomerContact) as [CUSTOMER CONTACT],(CustomerEmail) as [CUSTOMER EMAIL],(ProductId) as [PRODUCT ID],(product) as [PRODUCT NAME],(ProductPrice) as [PRODUCT PRICE],(Quantity) as [QUANTITY],(TotalPrice) as[TOTAL PRICE],(Date) as [DATE] FROM sale"
        Dim SampleSource As New DataSet
        Dim TableView As DataView
        Try
            Dim SampleDataAdapter = New OleDbDataAdapter()
            Dim SampleCommand = New OleDbCommand()
            SampleCommand.CommandText = SelectQry
            SampleCommand.Connection = Connection
            SampleDataAdapter.SelectCommand = SampleCommand
            SampleDataAdapter.Fill(SampleSource)
            TableView = SampleSource.Tables(0).DefaultView
        Catch ex As Exception
            Throw ex
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return TableView
    End Function

    Private Sub frmSalesDetail_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmSales.Show()
        Me.Hide()
    End Sub
End Class