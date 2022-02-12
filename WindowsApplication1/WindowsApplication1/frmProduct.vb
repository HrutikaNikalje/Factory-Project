
Imports System.Data.OleDb
Imports System.Data
Public Class frmProduct
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (ProductId) FROM product", cn)
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


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductId.SelectedIndexChanged
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from product where ProductId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "ProductId"))
            cmd.Parameters("@find").Value = Trim(ProductId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                Product.Text = Trim(rdr.GetString(1))
                BatchNo.Text = Trim(rdr.GetString(2))
                ManufacturingDate.Text = Trim(rdr.GetString(3))
                BestBefore.Text = Trim(rdr.GetString(3))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        ProductId.Text = ""
        Product.Text = ""
        BatchNo.Text = ""
        ManufacturingDate.Text = ""
        BestBefore.Text = ""
        Save.Enabled = True

    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(ProductId.Text)) = 0 Then
            MessageBox.Show("Please enter Product Id", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ProductId.Focus()
            Exit Sub
        End If
        If Len(Trim(Product.Text)) = 0 Then
            MessageBox.Show("Please enter product", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Product.Focus()
            Exit Sub
        End If
        If Len(Trim(BatchNo.Text)) = 0 Then
            MessageBox.Show("Please enter batch no", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BatchNo.Focus()
            Exit Sub
        End If
        If Len(Trim(ManufacturingDate.Text)) = 0 Then
            MessageBox.Show("Please enter manufacturing date", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ManufacturingDate.Focus()
            Exit Sub
        End If
        If Len(Trim(BestBefore.Text)) = 0 Then
            MessageBox.Show("Please enter best before", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BestBefore.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select ProductId from product where ProductId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "ProductId"))
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
                Dim cb As String = "insert into product(ProductId,Product,BatchNo,ManufacturingDate,BestBefore) VALUES(@d1,@d2,@d3,@d4,@d5)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "ProductId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "Product"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "BatchNo"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "ManufacturingDate"))
                cmd.Parameters.Add(New OleDbParameter("@d5", System.Data.OleDb.OleDbType.VarChar, 30, "BestBefore"))
                cmd.Parameters("@d1").Value = Trim(ProductId.Text)
                cmd.Parameters("@d2").Value = Trim(Product.Text)
                cmd.Parameters("@d3").Value = Trim(BatchNo.Text)
                cmd.Parameters("@d4").Value = Trim(ManufacturingDate.Text)
                cmd.Parameters("@d5").Value = Trim(BestBefore.Text)
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

    Private Sub ProductId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ProductId.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Product_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Product.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub BatchNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles BatchNo.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmProduct_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        frmProductDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcombo()
    End Sub
End Class