Public Class LOGIN
    Dim attempt As Integer = 1

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim Username, Password As String
        Username = TextBox3.Text
        Password = TextBox4.Text
        If Username = "admin" And Password = "123" Then
            MsgBox("LOGIN SUCCESSFULL")
            MDI.Show()
            Me.Hide()
        ElseIf attempt = 3 Then
            MsgBox("MAXIMUM NUMBER OF ATTEMPTS(3), PROGRAM WILL NOW CLOSE")
            Close()
        Else
            MsgBox("Username and password are incorrect, Please re-enter the correct username and password " & attempt & " of 3,")
            attempt = attempt + 1
            TextBox3.Text = " "
            TextBox4.Text = " "
            TextBox3.Focus()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        End
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub
End Class


