
Imports System.Data.OleDb
Imports System.Data

Public Class frmOrder
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
            adp.SelectCommand = New OleDbCommand("select distinct (OrderId) from order", cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            OrderId.Items.Clear()
            For Each drow As DataRow In dtable.Rows
                OrderId.Items.Add(drow(0).ToString())
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderId.SelectedIndexChanged
        Try
            Delete.Enabled = True
            Update_record.Enabled = True
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from order where OrderId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "OrderId"))
            cmd.Parameters("@find").Value = Trim(OrderId.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                OrderedItem.Text = Trim(rdr.GetString(1))
                Quantity.Text = Trim(rdr.GetString(2))
                PayableAmount.Text = Trim(rdr.GetString(3))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        OrderId.Text = ""
        OrderedItem.Text = ""
        Quantity.Text = ""
        PayableAmount.Text = ""
        Save.Enabled = True
        Update_record.Enabled = False
        Delete.Enabled = False

    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(OrderId.Text)) = 0 Then
            MessageBox.Show("Please enter Order id", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            OrderId.Focus()
            Exit Sub
        End If
        If Len(Trim(OrderedItem.Text)) = 0 Then
            MessageBox.Show("Please enter Ordered Item", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            OrderedItem.Focus()
            Exit Sub
        End If
        If Len(Trim(Quantity.Text)) = 0 Then
            MessageBox.Show("Please enter Quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Quantity.Focus()
            Exit Sub
        End If
        If Len(Trim(PayableAmount.Text)) = 0 Then
            MessageBox.Show("Please enter Payable Amount", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            PayableAmount.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select OrderId from order where OrderId=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "OrderId"))
            cmd.Parameters("@find").Value = OrderId.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("OrderId Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                OrderId.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into order(OrderId,OrderedItem,Quantity,PayableAmount) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "OrderId"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "OrderedItem"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "Quantity"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "PayableAmount"))
                cmd.Parameters("@d1").Value = Trim(OrderId.Text)
                cmd.Parameters("@d2").Value = Trim(OrderedItem.Text)
                cmd.Parameters("@d3").Value = Trim(Quantity.Text)
                cmd.Parameters("@d4").Value = Trim(PayableAmount.Text)
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

    Private Sub Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete.Click
        Try
            If OrderId.Text = "" Then
                MessageBox.Show("Please select OrderId", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If OrderId.Items.Count > 0 Then
                If MsgBox("Do you really want to delete this record?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    delete_records()
                    Delete.Enabled = False
                    Update_record.Enabled = False
                    fillcombo()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub delete_records()
        Try
            Dim RowsAffected As Integer = 0
            con = New OleDbConnection(cs)
            con.Open()
            Dim cq As String = "delete from order where OrderId=@DELETE1;"
            cmd = New OleDbCommand(cq)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@DELETE1", System.Data.OleDb.OleDbType.VarChar, 30, "OrderId"))
            cmd.Parameters("@DELETE1").Value = Trim(OrderId.Text)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then
                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MsgBox("Sorry no record deleted")
                OrderId.Text = ""
                OrderedItem.Text = ""
                Quantity.Text = ""
                PayableAmount.Text = ""
                cmd.ExecuteReader()
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Close()
            End If
            OrderId.Text = ""
            OrderedItem.Text = ""
            Quantity.Text = ""
            PayableAmount.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Update_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        Try
            If OrderId.Text = "" Then
                MessageBox.Show("Please select OrderId", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New OleDbConnection(cs)
            con.Open()
            Dim cb As String = "update order set OrderedItem='" & OrderedItem.Text & "',Quantity='" & Quantity.Text & "',PayableAmount='" & PayableAmount.Text & "' where OrderId='" & OrderId.Text & "'"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            MessageBox.Show("Successfully updated", "order details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Update_record.Enabled = False
            fillcombo()
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OrderId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles OrderId.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub OrederedItem_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles OrderedItem.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub PayableAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PayableAmount.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub
End Class
