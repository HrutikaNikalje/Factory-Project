
Imports System.Data.OleDb
Imports System.Data
Public Class frmExpenseDetail
    Private Const ConnectionString As String = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\PROJECT\icecream.mdb;"

    Private Sub frmExpenseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        Dim SelectQry = "SELECT (MonthYear) as [MONTH & YEAR],(ProfitGained) as [PROFIT GAINED],(SupplierPayment) as [SUPPLIER PAYMENT],(CashCollection) as [CASH COLLECTION] FROM expense"
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

    Private Sub frmExpenseDetail_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmExpense.Show()
        Me.Hide()
    End Sub
End Class