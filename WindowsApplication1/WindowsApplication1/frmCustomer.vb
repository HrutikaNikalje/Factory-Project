Imports System.Data.OleDb
Imports System.Data
Public Class frmCustomer
    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\PROJECT\icecream.mdb;"
    Dim LastNo As Integer


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Update_record.Enabled = True
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from customer where CustomerName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
            cmd.Parameters("@find").Value = Trim(TextBox1.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                CustomerContact.Text = Trim(rdr.GetString(1))
                CustomerEmail.Text = Trim(rdr.GetString(2))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click

        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (CustomerId) FROM customer", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "customer")
            LastNo = ds.Tables("customer").Rows.Count + 1
            If LastNo >= 0 Then
                TextBox1.Text = LastNo
            Else
                TextBox1.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        CustomerName.Text = ""
        CustomerContact.Text = ""
        CustomerEmail.Text = ""
        Save.Enabled = True
    End Sub


    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(CustomerName.Text)) = 0 Then
            MessageBox.Show("Please enter customer name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerName.Focus()
            Exit Sub
        End If
        If Len(Trim(CustomerContact.Text)) = 0 Then
            MessageBox.Show("Please enter contact no", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerContact.Focus()
            Exit Sub
        End If
        If Len(Trim(CustomerEmail.Text)) = 0 Then
            MessageBox.Show("Please enter email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerEmail.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select CustomerName from customer where CustomerName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
            cmd.Parameters("@find").Value = TextBox1.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Customer Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into customer(CustomerId,CustomerName,CustomerContact,CustomerEmail) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerContact"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerEmail"))
                cmd.Parameters("@d1").Value = Trim(TextBox1.Text)
                cmd.Parameters("@d2").Value = Trim(CustomerName.Text)
                cmd.Parameters("@d3").Value = Trim(CustomerContact.Text)
                cmd.Parameters("@d4").Value = Trim(CustomerEmail.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = True
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CustomerContact_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CustomerContact.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub CustomerName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmCustomerDetail.Show()
        Me.Hide()
    End Sub


    Private Sub frmCustomer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
    Sub CustomerId()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (CustomerId) FROM customer", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "customer")
            LastNo = ds.Tables("customer").Rows.Count + 1
            If LastNo >= 0 Then
                TextBox1.Text = LastNo
            Else
                TextBox1.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CustomerId()
    End Sub

    Private Sub Update_record_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        frmCustomerUpdate.Show()
    End Sub
End Class