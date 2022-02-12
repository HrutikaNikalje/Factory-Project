Public Class MDI
    Private Sub TOOLSANDEQUIPMENTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLSANDEQUIPMENTToolStripMenuItem.Click
        frmTools.Show()
        Me.Hide()
    End Sub

    Private Sub MARKETINGDETAILSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MARKETINGDETAILSToolStripMenuItem.Click
        frmMarketing.Show()
        Me.Hide()
    End Sub

    Private Sub STOCKDETAILSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmStock.Show()
        Me.Hide()
    End Sub

    Private Sub STAFFDETAILSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STAFFDETAILSToolStripMenuItem.Click
        frmStaff.Show()
        Me.Hide()
    End Sub

    Private Sub CUSTOMERENQUIRYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CUSTOMERENQUIRYToolStripMenuItem.Click
        frmEnquiry.Show()
        Me.Hide()
    End Sub

    Private Sub CUSTOMERREGISTRATIONToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CUSTOMERREGISTRATIONToolStripMenuItem.Click
        frmCustomer.Show()
        Me.Hide()
    End Sub

    Private Sub PRERESERVATIONSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRERESERVATIONSToolStripMenuItem.Click
        frmReservation.Show()
        Me.Hide()
    End Sub

    Private Sub HOMEDELIVERYToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HOMEDELIVERYToolStripMenuItem1.Click
        frmHome.Show()
        Me.Hide()
    End Sub

    Private Sub EXPENSEDETAILSToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPENSEDETAILSToolStripMenuItem1.Click
        frmExpense.Show()
        Me.Hide()
    End Sub

    Private Sub SALESORDERToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmSales.Show()
        Me.Hide()
    End Sub

    Private Sub FRANCHISEDETAILSToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FRANCHISEDETAILSToolStripMenuItem1.Click
        frmFranchise.Show()
        Me.Hide()
    End Sub

    Private Sub LOGOUTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LOGOUTToolStripMenuItem.Click
        LOGIN.Show()
        Me.Hide()
    End Sub

    Private Sub MDI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        LOGIN.Show()
        Me.Hide()
    End Sub

    Private Sub STOCKToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKToolStripMenuItem1.Click
        frmStock.Show()
        Me.Hide()
    End Sub

    Private Sub PURCHASEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASEToolStripMenuItem.Click
        frmPurchase.Show()
        Me.Hide()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        frmProduct.Show()
    End Sub

    Private Sub SUPPLIERToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SUPPLIERToolStripMenuItem2.Click
        frmSupplier.Show()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        frmSales.Show()
        Me.Hide()
    End Sub

    Private Sub CUSTOMERToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CUSTOMERToolStripMenuItem.Click
        CUSTOMER.Show()
    End Sub

    Private Sub TOOLSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLSToolStripMenuItem.Click
        TOOLS.Show()
    End Sub

    Private Sub MARKETINGToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MARKETINGToolStripMenuItem.Click
        MARKETING.Show()
    End Sub

    Private Sub SUPPLIERToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SUPPLIERToolStripMenuItem1.Click
        SUPPLIER.Show()
    End Sub

    Private Sub STOCKToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKToolStripMenuItem.Click
        STOCK.Show()
    End Sub

    Private Sub STAFFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STAFFToolStripMenuItem.Click
        STAFF.Show()
    End Sub

    Private Sub FRANCHISEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FRANCHISEToolStripMenuItem.Click
        FRANCHISE.Show()
    End Sub

    Private Sub SALESToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALESToolStripMenuItem.Click
        SALES.Show()
    End Sub

    Private Sub PURCHASEToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASEToolStripMenuItem1.Click
        PURCHASE.Show()
    End Sub

    Private Sub EXPENSEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPENSEToolStripMenuItem.Click
        EXPENSE.Show()
    End Sub

    Private Sub ENQUIRYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENQUIRYToolStripMenuItem.Click
        ENQUIRY.Show()
    End Sub

    Private Sub HOMEDELIVERYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HOMEDELIVERYToolStripMenuItem.Click
        HOME.Show()
    End Sub

    Private Sub PRODUCTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRODUCTToolStripMenuItem.Click
        PRODUCT.Show()
    End Sub

    Private Sub PRERESERVATIONToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRERESERVATIONToolStripMenuItem.Click
        RESERVATION.Show()
    End Sub
End Class