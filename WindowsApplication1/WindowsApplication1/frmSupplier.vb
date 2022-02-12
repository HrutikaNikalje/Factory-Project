Imports System.Data.OleDb
Imports System.Data
Public Class frmSupplier
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
            Dim ct As String = "select * from supplier where SupplierName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierName"))
            cmd.Parameters("@find").Value = Trim(SupplierId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                SupplierContact.Text = Trim(rdr.GetString(1))
                SupplierGstNo.Text = Trim(rdr.GetString(2))
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (SupplierId) FROM supplier", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "supplier")
            LastNo = ds.Tables("supplier").Rows.Count + 1
            If LastNo >= 0 Then
                SupplierId.Text = LastNo
            Else
                SupplierId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        SupplierName.Text = ""
        SupplierContact.Text = ""
        SupplierGstNo.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(SupplierName.Text)) = 0 Then
            MessageBox.Show("Please enter Supplier Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SupplierName.Focus()
            Exit Sub
        End If
        If Len(Trim(SupplierContact.Text)) = 0 Then
            MessageBox.Show("Please enter Supplier Contact", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SupplierContact.Focus()
            Exit Sub
        End If
        If Len(Trim(SupplierGstNo.Text)) = 0 Then
            MessageBox.Show("Please enter Supplier Gst No", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SupplierGstNo.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select SupplierName from supplier where SupplierName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierName"))
            cmd.Parameters("@find").Value = SupplierId.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Supplier Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SupplierId.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into supplier(SupplierId,SupplierName,SupplierContact,SupplierGstNo) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierName"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierContact"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierGstNo"))
                cmd.Parameters("@d1").Value = Trim(SupplierId.Text)
                cmd.Parameters("@d2").Value = Trim(SupplierName.Text)
                cmd.Parameters("@d3").Value = Trim(SupplierContact.Text)
                cmd.Parameters("@d4").Value = Trim(SupplierGstNo.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = True
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SupplierId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub SupplierName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles SupplierName.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub SupplierContact_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles SupplierContact.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub SupplierGstNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles SupplierGstNo.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmSupplierDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmSupplier_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
    Sub supp()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (SupplierId) FROM supplier", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "supplier")
            LastNo = ds.Tables("supplier").Rows.Count + 1
            If LastNo >= 0 Then
                SupplierId.Text = LastNo
            Else
                SupplierId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        supp()
    End Sub

    Private Sub Update_record_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        frmSupplierUpdate.Show()
    End Sub
End Class