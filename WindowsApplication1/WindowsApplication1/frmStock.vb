
Imports System.Data.OleDb
Imports System.Data
Public Class frmStock
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
            Dim ct As String = "select * from stock where product=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "product"))
            cmd.Parameters("@find").Value = Trim(product.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                ProductPrice.Text = Trim(rdr.GetString(1))
                ProductQuantity.Text = Trim(rdr.GetString(2))
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (ProductId) FROM stock", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "stock")
            LastNo = ds.Tables("stock").Rows.Count + 1
            If LastNo >= 0 Then
                ProductId.Text = LastNo
            Else
                ProductId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        product.Text = ""
        ProductPrice.Text = ""
        ProductQuantity.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(product.Text)) = 0 Then
            MessageBox.Show("Please enter product", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            product.Focus()
            Exit Sub
        End If
        If Len(Trim(ProductPrice.Text)) = 0 Then
            MessageBox.Show("Please enter product price", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ProductPrice.Focus()
            Exit Sub
        End If
        If Len(Trim(ProductQuantity.Text)) = 0 Then
            MessageBox.Show("Please enter product quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ProductQuantity.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select product from stock where product=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "product"))
            cmd.Parameters("@find").Value = ProductId.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Product Id Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ProductId.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into stock(ProductId,product,ProductPrice,ProductQuantity) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "ProductId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "product"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "ProductPrice"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "ProductQuantity"))
                cmd.Parameters("@d1").Value = Trim(ProductId.Text)
                cmd.Parameters("@d2").Value = Trim(product.Text)
                cmd.Parameters("@d3").Value = Trim(ProductPrice.Text)
                cmd.Parameters("@d4").Value = Trim(ProductQuantity.Text)
                cmd.ExecuteReader()
                MessageBox.Show("Successfully registered", "Id", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = True
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ProductId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmStockDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmStock_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
    End Sub
    Sub sto()
        Try
            Dim cn As New OleDbConnection(cs)
            cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (ProductId) FROM stock", cn)
            ds = New DataSet("ds")
            adp.Fill(ds, "stock")
            LastNo = ds.Tables("stock").Rows.Count + 1
            If LastNo >= 0 Then
                ProductId.Text = LastNo
            Else
                ProductId.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sto()
    End Sub

    Private Sub Update_record_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        frmStockUpdate.Show()
    End Sub
End Class