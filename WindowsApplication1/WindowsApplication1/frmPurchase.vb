
Imports System.Data.OleDb
Imports System.Data
Public Class frmPurchase
    Dim dtable As DataTable
    Dim con As OleDbConnection
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim cs As String = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\PROJECT\icecream.mdb;"
    Dim LastNo As Integer
    Dim cmd2 As OleDbCommand
    Dim rdr As OleDbDataReader
    Dim str As String
    Public sn, b, pdct, t, d As String

    Sub supplier()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (SupplierId) FROM supplier", cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            SupplierId.Items.Clear()
            For Each drow As DataRow In dtable.Rows
                SupplierId.Items.Add(drow(0).ToString())

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SupplierId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierId.SelectedIndexChanged
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from supplier where SupplierId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "SupplierId"))
            cmd.Parameters("@find").Value = Trim(SupplierId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                SupplierName.Text = Trim(rdr.GetString(1))
                SupplierContact.Text = Trim(rdr.GetString(2))
                SupplierGstNo.Text = Trim(rdr.GetString(3))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bill()
        supplier()
        pro()
        TextBox2.Text = DateTime.Now
    End Sub
    Sub bill()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (BillNo) FROM sale", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "sale")
            LastNo = ds.Tables("sale").Rows.Count + 1
            If LastNo >= 0 Then
                TextBox1.Text = LastNo
            Else
                TextBox1.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub pro()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (ProductId) FROM stock", cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            ProductId.Items.Clear()
            For Each drow As DataRow In dtable.Rows
                ProductId.Items.Add(drow(0).ToString())

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ProductId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductId.SelectedIndexChanged
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from stock where ProductId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "ProductId"))
            cmd.Parameters("@find").Value = Trim(ProductId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                product.Text = Trim(rdr.GetString(1))
                ProductPrice.Text = Trim(rdr.GetString(2))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Quantity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Quantity.SelectedIndexChanged
        Dim tot As Integer
        tot = Val(ProductPrice.Text) * Val(Quantity.Text)
        TextBox3.Text = Convert.ToString(tot)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Close()
        str = "Insert into [purchase]([SupplierId],[SupplierName],[SupplierContact],[SupplierGstNo],[ProductId],[product],[ProductPrice],[Quantity],[TotalPrice],[Date])values('" & SupplierId.Text & "','" & SupplierName.Text & "','" & SupplierContact.Text & "','" & SupplierGstNo.Text & "','" & ProductId.Text & "','" & product.Text & "','" & ProductPrice.Text & "','" & Quantity.Text & "','" & TextBox3.Text & "','" & TextBox2.Text & "')"
        Try
            con.Open()
            cmd = New OleDbCommand(str, con)
            If cmd.ExecuteNonQuery() > 0 Then
                MsgBox("Record Saved", MsgBoxStyle.Information)
                b = TextBox1.Text
                sn = SupplierName.Text
                pdct = product.Text
                t = TextBox3.Text
                d = TextBox2.Text

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub SupplierId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles SupplierId.KeyPress
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

    Private Sub ProductId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ProductId.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub product_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles product.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub ProductPrice_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ProductPrice.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        frmPurchaseDetail.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MDI.Show()
        Me.Hide()
    End Sub

    Private Sub frmPurchase_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
End Class