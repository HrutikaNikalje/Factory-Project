Imports System.Data.OleDb
Imports System.Data
Public Class frmMarketing
    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\PROJECT\icecream.mdb;"
    Sub fillcombo()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (MarketingType) FROM marketing", cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            MarketingType.Items.Clear()
            For Each drow As DataRow In dtable.Rows
                MarketingType.Items.Add(drow(0).ToString())

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarketingType.SelectedIndexChanged
        Try
            Update_record.Enabled = True
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from marketing where MarketingType=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "MarketingType"))
            cmd.Parameters("@find").Value = Trim(MarketingType.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                MarketingExpense.Text = Trim(rdr.GetString(1))
                MarketingCompany.Text = Trim(rdr.GetString(2))
                CompanyContact.Text = Trim(rdr.GetString(3))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        MarketingType.Text = ""
        MarketingExpense.Text = ""
        MarketingCompany.Text = ""
        CompanyContact.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(MarketingType.Text)) = 0 Then
            MessageBox.Show("Please enter Marketing Type", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MarketingType.Focus()
            Exit Sub
        End If
        If Len(Trim(MarketingExpense.Text)) = 0 Then
            MessageBox.Show("Please enter marketing expense", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MarketingExpense.Focus()
            Exit Sub
        End If
        If Len(Trim(MarketingCompany.Text)) = 0 Then
            MessageBox.Show("Please enter marketing company", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MarketingCompany.Focus()
            Exit Sub
        End If
        If Len(Trim(CompanyContact.Text)) = 0 Then
            MessageBox.Show("Please enter company contact", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CompanyContact.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select MarketingType from marketing where MarketingType=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "MarketingType"))
            cmd.Parameters("@find").Value = MarketingType.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Marketing Type Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MarketingType.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into marketing(MarketingType,MarketingExpense,MarketingCompany,CompanyContact) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "MarketingType"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "MarketinExpense"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "MarketingCompany"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "CompanyContact"))
                cmd.Parameters("@d1").Value = Trim(MarketingType.Text)
                cmd.Parameters("@d2").Value = Trim(MarketingExpense.Text)
                cmd.Parameters("@d3").Value = Trim(MarketingCompany.Text)
                cmd.Parameters("@d4").Value = Trim(CompanyContact.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = True
                fillcombo()
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Update_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        Try
            If MarketingType.Text = "" Then
                MessageBox.Show("Please select MarketingType", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New OleDbConnection(cs)
            con.Open()
            Dim cb As String = "update marketing set MarketingExpense='" & MarketingExpense.Text & "',MarketingCompany='" & MarketingCompany.Text & "',CompanyContact='" & CompanyContact.Text & "' where MarketingType='" & MarketingType.Text & "'"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            MessageBox.Show("Successfully updated", "marketing details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            fillcombo()
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MarketingType_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MarketingType.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub MarketingExpenses_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MarketingExpense.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub MarketingCompany_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MarketingCompany.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub CompanyContact_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CompanyContact.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmMarketingDetail.Show()
        Me.Hide()
    End Sub

    
    Private Sub frmMarketing_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub

    Private Sub frmMarketing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcombo()
    End Sub
End Class
