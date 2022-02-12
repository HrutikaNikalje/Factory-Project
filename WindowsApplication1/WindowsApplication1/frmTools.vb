Imports System.Data.OleDb
Imports System.Data
Public Class frmTools
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
            Dim ct As String = "select * from Tools where Name=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "Name"))
            cmd.Parameters("@find").Value = Trim(ToolsId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                Expenses.Text = Trim(rdr.GetString(1))
                Quantity.Text = Trim(rdr.GetString(2))
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (ToolsId) FROM tools", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "tools")
            LastNo = ds.Tables("tools").Rows.Count + 1
            If LastNo >= 0 Then
                ToolsId.Text = LastNo
            Else
                ToolsId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        txtName.Text = ""
        Expenses.Text = ""
        Quantity.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Expenses_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Expenses.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(txtName.Text)) = 0 Then
            MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Exit Sub
        End If
        If Len(Trim(Expenses.Text)) = 0 Then
            MessageBox.Show("Please enter expenses", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Expenses.Focus()
            Exit Sub
        End If
        If Len(Trim(Quantity.Text)) = 0 Then
            MessageBox.Show("Please enter quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Quantity.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select Name from Tools where Name=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "Name"))
            cmd.Parameters("@find").Value = ToolsId.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Tools Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ToolsId.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into Tools(ToolsId,Name,Expenses,Quantity) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "ToolsId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "Name"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "Expenses"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "Quantity"))
                cmd.Parameters("@d1").Value = Trim(ToolsId.Text)
                cmd.Parameters("@d2").Value = Trim(txtName.Text)
                cmd.Parameters("@d3").Value = Trim(Expenses.Text)
                cmd.Parameters("@d4").Value = Trim(Quantity.Text)
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
        frmToolsDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmTools_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
    Sub tool()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (ToolsId) FROM tools", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "tools")
            LastNo = ds.Tables("tools").Rows.Count + 1
            If LastNo >= 0 Then
                ToolsId.Text = LastNo
            Else
                ToolsId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tool()
    End Sub

    Private Sub Update_record_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        frmToolsUpdate.Show()
    End Sub
End Class