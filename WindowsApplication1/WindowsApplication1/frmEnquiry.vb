
Imports System.Data.OleDb
Imports System.Data
Public Class frmEnquiry
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
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from enquiry where CustomerName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
            cmd.Parameters("@find").Value = Trim(CustomerName.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                CustomerEmail.Text = Trim(rdr.GetString(1))
                EnquiryFor.Text = Trim(rdr.GetString(2))
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (EnquiryId) FROM enquiry", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "enquiry")
            LastNo = ds.Tables("enquiry").Rows.Count + 1
            If LastNo >= 0 Then
                EnquiryId.Text = LastNo
            Else
                EnquiryId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        CustomerName.Text = ""
        CustomerEmail.Text = ""
        EnquiryFor.Text = ""
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(CustomerName.Text)) = 0 Then
            MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerName.Focus()
            Exit Sub
        End If
        If Len(Trim(CustomerEmail.Text)) = 0 Then
            MessageBox.Show("Please enter email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerEmail.Focus()
            Exit Sub
        End If
        If Len(Trim(EnquiryFor.Text)) = 0 Then
            MessageBox.Show("Please enter enquiry", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            EnquiryFor.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select CustomerName from enquiry where CustomerName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
            cmd.Parameters("@find").Value = EnquiryId.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Enquiry Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                EnquiryId.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into enquiry(EnquiryId,CustomerName,CustomerEmail,EnquiryFor) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "EnquiryId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerEmail"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "EnquiryFor"))
                cmd.Parameters("@d1").Value = Trim(EnquiryId.Text)
                cmd.Parameters("@d2").Value = Trim(CustomerName.Text)
                cmd.Parameters("@d3").Value = Trim(CustomerEmail.Text)
                cmd.Parameters("@d4").Value = Trim(EnquiryFor.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CustomerName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CustomerName.KeyPress
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
        frmEnquiryDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmEnquiry_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmCustomer.Show()
        Me.Hide()
    End Sub
    Sub Enq()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (EnquiryId) FROM enquiry", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "enquiry")
            LastNo = ds.Tables("enquiry").Rows.Count + 1
            If LastNo >= 0 Then
                EnquiryId.Text = LastNo
            Else
                EnquiryId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmEnquiry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Enq()
    End Sub

    Private Sub Update_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        frmEnquiryUpdate.Show()
    End Sub
End Class
