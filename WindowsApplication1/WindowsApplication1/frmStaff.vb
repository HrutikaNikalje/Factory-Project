Imports System.Data.OleDb
Imports System.Data
Public Class frmStaff
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
            Dim ct As String = "select * from staff where EmployeeId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "EmployeeId"))
            cmd.Parameters("@find").Value = Trim(EmployeeId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                EmployeeName.Text = Trim(rdr.GetString(1))
                EmployeeContact.Text = Trim(rdr.GetString(2))
                Salary.Text = Trim(rdr.GetString(3))
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (EmployeeId) FROM staff", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "staff")
            LastNo = ds.Tables("staff").Rows.Count + 1
            If LastNo >= 0 Then
                EmployeeId.Text = LastNo
            Else
                EmployeeId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        EmployeeName.Text = ""
        EmployeeContact.Text = ""
        Salary.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(EmployeeName.Text)) = 0 Then
            MessageBox.Show("Please enter employee name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            EmployeeName.Focus()
            Exit Sub
        End If
        If Len(Trim(EmployeeContact.Text)) = 0 Then
            MessageBox.Show("Please enter employee contact", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            EmployeeContact.Focus()
            Exit Sub
        End If
        If Len(Trim(Salary.Text)) = 0 Then
            MessageBox.Show("Please enter salary", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Salary.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select EmployeeId from staff where EmployeeName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "EmployeeName"))
            cmd.Parameters("@find").Value = EmployeeId.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Employee Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                EmployeeId.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into staff(EmployeeId,EmployeeName,EmployeeContact,Salary) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "EmployeeId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "EmployeeName"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "EmployeeContact"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "Salary"))
                cmd.Parameters("@d1").Value = Trim(EmployeeId.Text)
                cmd.Parameters("@d2").Value = Trim(EmployeeName.Text)
                cmd.Parameters("@d3").Value = Trim(EmployeeContact.Text)
                cmd.Parameters("@d4").Value = Trim(Salary.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = True
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EmployeeId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub EmployeeName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles EmployeeName.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub EmployeeContact_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles EmployeeContact.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Salary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Salary.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmStaffDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmStaff_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
    Sub sta()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (EmployeeId) FROM staff", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "staff")
            LastNo = ds.Tables("staff").Rows.Count + 1
            If LastNo >= 0 Then
                EmployeeId.Text = LastNo
            Else
                EmployeeId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmStaff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sta()
    End Sub

    Private Sub Update_record_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        frmStaffUpdate.Show()
    End Sub
End Class