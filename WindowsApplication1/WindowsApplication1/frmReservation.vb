
Imports System.Data.OleDb
Imports System.Data
Public Class frmReservation
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
            adp.SelectCommand = New OleDbCommand("SELECT distinct (CustomerName) FROM reservation", cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            CustomerName.Items.Clear()
            For Each drow As DataRow In dtable.Rows
                CustomerName.Items.Add(drow(0).ToString())

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerName.SelectedIndexChanged
        Try
            Update_record.Enabled = True
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select * from reservation where CustomerName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
            cmd.Parameters("@find").Value = Trim(CustomerName.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                CustomerContact.Text = Trim(rdr.GetString(1))
                ReservedSeat.Text = Trim(rdr.GetString(2))
                Occasion.Text = Trim(rdr.GetString(3))
            End If
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        CustomerName.Text = ""
        CustomerContact.Text = ""
        ReservedSeat.Text = ""
        Occasion.Text = ""
        Save.Enabled = True
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(CustomerName.Text)) = 0 Then
            MessageBox.Show("Please enter CustomerName", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerName.Focus()
            Exit Sub
        End If
        If Len(Trim(CustomerContact.Text)) = 0 Then
            MessageBox.Show("Please enter customer contact", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CustomerContact.Focus()
            Exit Sub
        End If
        If Len(Trim(ReservedSeat.Text)) = 0 Then
            MessageBox.Show("Please enter reserved seat", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ReservedSeat.Focus()
            Exit Sub
        End If
        If Len(Trim(Occasion.Text)) = 0 Then
            MessageBox.Show("Please enter occasion", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Occasion.Focus()
            Exit Sub
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select CustomerName from reservation where CustomerName=@find"
            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
            cmd.Parameters("@find").Value = CustomerName.Text
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Customer Name Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CustomerName.Text = ""
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()
                Dim cb As String = "insert into reservation(CustomerName,CustomerContact,ReservedSeat,Occasion) VALUES(@d1,@d2,@d3,@d4)"
                cmd = New OleDbCommand(cb)
                cmd.Connection = con
                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerName"))
                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "CustomerContact"))
                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "ReservedSeat"))
                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "Occasion"))
                cmd.Parameters("@d1").Value = Trim(CustomerName.Text)
                cmd.Parameters("@d2").Value = Trim(CustomerContact.Text)
                cmd.Parameters("@d3").Value = Trim(ReservedSeat.Text)
                cmd.Parameters("@d4").Value = Trim(Occasion.Text)
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
            If CustomerName.Text = "" Then
                MessageBox.Show("Please select CustomerName", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New OleDbConnection(cs)
            con.Open()
            Dim cb As String = "update reservation set CustomerContact='" & CustomerContact.Text & "',ReservedSeat='" & ReservedSeat.Text & "',Occasion='" & Occasion.Text & "' where CustomerName='" & CustomerName.Text & "'"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            MessageBox.Show("Successfully updated", "reservation details", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub CustomerContact_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CustomerContact.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ReservedSeat_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ReservedSeat.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Occasion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Occasion.KeyPress
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
        frmReservationDetail.Show()
        Me.Hide()
    End Sub

    Private Sub frmReservation_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub

    Private Sub frmReservation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcombo()
    End Sub
End Class