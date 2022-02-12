
Imports System.Data.OleDb
Imports System.Data
Public Class frmEnquiryUpdate
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (EnquiryId) FROM enquiry", cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            EnquiryId.Items.Clear()
            For Each drow As DataRow In dtable.Rows
                EnquiryId.Items.Add(drow(0).ToString())

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnquiryId.SelectedIndexChanged
        Try
            Update_record.Enabled = True
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from enquiry where EnquiryId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "EnquiryId"))
            cmd.Parameters("@find").Value = Trim(EnquiryId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                CustomerName.Text = Trim(rdr.GetString(1))
                CustomerEmail.Text = Trim(rdr.GetString(2))
                EnquiryFor.Text = Trim(rdr.GetString(3))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        EnquiryId.Text = ""
        CustomerName.Text = ""
        CustomerEmail.Text = ""
        EnquiryFor.Text = ""
    End Sub

    Private Sub Update_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        Try
            If EnquiryId.Text = "" Then
                MessageBox.Show("Please select EnquiryId", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New OleDbConnection(cs)
            con.Open()
            Dim cb As String = "update enquiry set CustomerName='" & CustomerName.Text & "',CustomerEmail='" & CustomerEmail.Text & "',EnquiryFor='" & EnquiryFor.Text & "' where EnquiryId='" & EnquiryId.Text & "'"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            MessageBox.Show("Successfully updated", "enquiry details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Update_record.Enabled = False
            fillcombo()
            con.Close()
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

    Private Sub frmEnquiryUpdate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmEnquiry.Show()
        Me.Hide()
    End Sub

    Private Sub frmEnquiryUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcombo()
    End Sub
End Class