Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Printing

Public Class frmBill
    Dim WithEvents mPrintDocument As New PrintDocument
    Dim mPrintBitMap As Bitmap

    Private Sub frmBill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label7.Text = My.Forms.frmSales.b
        Label8.Text = My.Forms.frmSales.cn
        Label9.Text = My.Forms.frmSales.pdct
        Label10.Text = My.Forms.frmSales.t
        Label13.Text = My.Forms.frmSales.d
        Label11.Text = "We received Rs " & " " & Label10.Text & " " & "from" & " " & Label8.Text & " for " & " " & Label9.Text
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Text = "PRINT RECEIPT"
        Dim Preview As New PrintPreviewDialog
        Dim pd As New System.Drawing.Printing.PrintDocument
        pd.DefaultPageSettings.Landscape = True
        AddHandler pd.PrintPage, AddressOf OnPrintPage
        Preview.Document = pd
        Preview.ShowDialog()
    End Sub

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Using bmp As Bitmap = New Bitmap(Me.Width, Me.Height)
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
            Dim ratio As Single = CSng(bmp.Width / bmp.Height)
            If ratio > e.MarginBounds.Width / e.MarginBounds.Height Then
                e.Graphics.DrawImage(bmp, e.MarginBounds.Left, CInt(e.MarginBounds.Top + (e.MarginBounds.Height / 2) - ((e.MarginBounds.Width / ratio) / 2)), e.MarginBounds.Width, CInt(e.MarginBounds.Width / ratio))
            Else
                e.Graphics.DrawImage(bmp, CInt(e.MarginBounds.Left + (e.MarginBounds.Width / 2) - (e.MarginBounds.Height * ratio / 2)), e.MarginBounds.Top, CInt(e.MarginBounds.Height * ratio), e.MarginBounds.Height)
            End If
        End Using

    End Sub
    Private Sub PrintDocument1_BeginPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        MsgBox("BEGIN PRINT CALLED")
    End Sub

    Private Sub m_PrintDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles mPrintDocument.PrintPage
        Dim Width As Integer = e.MarginBounds.X + (e.MarginBounds.Width - mPrintBitMap.Width) \ 2
        Dim Height As Integer = e.MarginBounds.Y + (e.MarginBounds.Height - mPrintBitMap.Height) \ 2
        e.Graphics.DrawImage(mPrintBitMap, Width, Height)
        e.HasMorePages = False
    End Sub

    Private Sub PrintDocument1_EndPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        MsgBox("END PRINT CALLED")
    End Sub

    Private Sub PrintDocument1_QueryPageSettings(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.QueryPageSettingsEventArgs) Handles PrintDocument1.QueryPageSettings
        MsgBox("QUERY PAGE PRINT CALLED")
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        MsgBox("PRINT PAGE CALLED")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MDI.Show()
        Me.Hide()
    End Sub

    Private Sub frmBill_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.Show()
        Me.Hide()
    End Sub
End Class