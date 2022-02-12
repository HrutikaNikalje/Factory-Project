Imports System.Data.OleDb
Imports System.Data
Public Class frmExpense
    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\PROJECT\icecream.mdb;"

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        MonthYear.Text = ""
        ProfitGained.Text = ""
        SupplierPayment.Text = ""
        CashCollection.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(MonthYear.Text)) = 0 Then
            MessageBox.Show("Please enter Month & Year", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MonthYear.Focus()
            Exit Sub
        End If
        If Len(Trim(ProfitGained.Text)) = 0 Then
            MessageBox.Show("Please enter profit gained", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ProfitGained.Focus()
            Exit Sub
        End If
        If Len(Trim(SupplierPayment.Text)) = 0 Then
            MessageBox.Show("Please enter Supplier Payment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SupplierPayment.Focus()
            Exit Sub
        End If
        If Len(Trim(CashCollection.Text)) = 0 Then
            MessageBox.Show("Please enter Cash Collection", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CashCollection.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select MonthYear from expense where MonthYear=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "MonthYear"))
            cmd.Parameters("@find").Value = MonthYear.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Month & Year Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MonthYear.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into expense(MonthYear,ProfitGained,SupplierPayment,CashCollection) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "MonthYear"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "ProfitGained"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierPayment"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "CashCollection"))
                cmd.Parameters("@d1").Value = Trim(MonthYear.Text)
                cmd.Parameters("@d2").Value = Trim(ProfitGained.Text)
                cmd.Parameters("@d3").Value = Trim(SupplierPayment.Text)
                cmd.Parameters("@d4").Value = Trim(CashCollection.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = True
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmExpenseDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmExpense_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
End Class