﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockUpdate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GetDetails = New System.Windows.Forms.Button
        Me.NewRecord = New System.Windows.Forms.Button
        Me.Update_record = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.ProductQuantity = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.product = New System.Windows.Forms.TextBox
        Me.ProductId = New System.Windows.Forms.ComboBox
        Me.ProductPrice = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GetDetails
        '
        Me.GetDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GetDetails.Location = New System.Drawing.Point(451, 411)
        Me.GetDetails.Name = "GetDetails"
        Me.GetDetails.Size = New System.Drawing.Size(123, 37)
        Me.GetDetails.TabIndex = 22
        Me.GetDetails.Text = "GET DATA"
        Me.GetDetails.UseVisualStyleBackColor = True
        '
        'NewRecord
        '
        Me.NewRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewRecord.Location = New System.Drawing.Point(73, 411)
        Me.NewRecord.Name = "NewRecord"
        Me.NewRecord.Size = New System.Drawing.Size(123, 37)
        Me.NewRecord.TabIndex = 18
        Me.NewRecord.Text = "NEW"
        Me.NewRecord.UseVisualStyleBackColor = True
        '
        'Update_record
        '
        Me.Update_record.Enabled = False
        Me.Update_record.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_record.Location = New System.Drawing.Point(264, 411)
        Me.Update_record.Name = "Update_record"
        Me.Update_record.Size = New System.Drawing.Size(123, 37)
        Me.Update_record.TabIndex = 21
        Me.Update_record.Text = "UPDATE"
        Me.Update_record.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Crimson
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.GetDetails)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.NewRecord)
        Me.GroupBox1.Controls.Add(Me.Update_record)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ProductQuantity)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.product)
        Me.GroupBox1.Controls.Add(Me.ProductId)
        Me.GroupBox1.Controls.Add(Me.ProductPrice)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(133, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(611, 502)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(191, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(238, 31)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "UPDATE STOCK"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Crimson
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(110, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 24)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "PRODUCT ID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Crimson
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 324)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(217, 24)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "PRODUCT QUANTITY"
        '
        'ProductQuantity
        '
        Me.ProductQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProductQuantity.Location = New System.Drawing.Point(311, 324)
        Me.ProductQuantity.Name = "ProductQuantity"
        Me.ProductQuantity.Size = New System.Drawing.Size(263, 29)
        Me.ProductQuantity.TabIndex = 30
        Me.ProductQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Crimson
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(69, 202)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(174, 24)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "PRODUCT NAME"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Crimson
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(69, 264)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(174, 24)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "PRODUCT PRICE"
        '
        'product
        '
        Me.product.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.product.Location = New System.Drawing.Point(309, 199)
        Me.product.Name = "product"
        Me.product.Size = New System.Drawing.Size(267, 29)
        Me.product.TabIndex = 28
        Me.product.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ProductId
        '
        Me.ProductId.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProductId.FormattingEnabled = True
        Me.ProductId.Location = New System.Drawing.Point(304, 135)
        Me.ProductId.Name = "ProductId"
        Me.ProductId.Size = New System.Drawing.Size(272, 32)
        Me.ProductId.TabIndex = 27
        '
        'ProductPrice
        '
        Me.ProductPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProductPrice.Location = New System.Drawing.Point(310, 258)
        Me.ProductPrice.Name = "ProductPrice"
        Me.ProductPrice.Size = New System.Drawing.Size(266, 29)
        Me.ProductPrice.TabIndex = 29
        Me.ProductPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmStockUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.berries_colorful_ice_cream_fruit_wallpaper_preview1
        Me.ClientSize = New System.Drawing.Size(1090, 586)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmStockUpdate"
        Me.Text = "frmStockUpdate"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GetDetails As System.Windows.Forms.Button
    Friend WithEvents NewRecord As System.Windows.Forms.Button
    Friend WithEvents Update_record As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ProductQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents product As System.Windows.Forms.TextBox
    Friend WithEvents ProductId As System.Windows.Forms.ComboBox
    Friend WithEvents ProductPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
