﻿Imports R2CoreGUI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCLoadAllocationManipulation
    Inherits UCLoadAllocation

    'UserControl overrides dispose to clean up the component list.
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
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.PnlOutter = New System.Windows.Forms.Panel()
        Me.PnlInner = New System.Windows.Forms.Panel()
        Me.UcButtonNewnEstelamIdRemain = New R2CoreGUI.UCButton()
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite = New R2CoreGUI.UCPersianTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.UcButtonLoadAllocationCancelling = New R2CoreGUI.UCButton()
        Me.UcButtonLoadAllocationRegistering = New R2CoreGUI.UCButton()
        Me.UcButtonNew = New R2CoreGUI.UCButton()
        Me.UcPersianTextBoxLoadAllocationStatus = New R2CoreGUI.UCPersianTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.UcNumberLoadAllocationId = New R2CoreGUI.UCNumber()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PnlHolder = New System.Windows.Forms.Panel()
        Me.UcViewerNSSLoadCapacitorLoadDataEntry = New R2CoreTransportationAndLoadNotification.UCViewerNSSLoadCapacitorLoadDataEntry()
        Me.UcViewerNSSTurnDataEntry = New R2CoreTransportationAndLoadNotification.UCViewerNSSTurnDataEntry()
        Me.PnlMain.SuspendLayout()
        Me.PnlOutter.SuspendLayout()
        Me.PnlInner.SuspendLayout()
        Me.PnlHolder.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.Controls.Add(Me.PnlOutter)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Location = New System.Drawing.Point(5, 5)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(951, 218)
        Me.PnlMain.TabIndex = 0
        '
        'PnlOutter
        '
        Me.PnlOutter.BackColor = System.Drawing.Color.Black
        Me.PnlOutter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlOutter.Controls.Add(Me.PnlInner)
        Me.PnlOutter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlOutter.Location = New System.Drawing.Point(0, 0)
        Me.PnlOutter.Name = "PnlOutter"
        Me.PnlOutter.Padding = New System.Windows.Forms.Padding(2)
        Me.PnlOutter.Size = New System.Drawing.Size(951, 218)
        Me.PnlOutter.TabIndex = 0
        '
        'PnlInner
        '
        Me.PnlInner.BackColor = System.Drawing.Color.SeaShell
        Me.PnlInner.Controls.Add(Me.UcButtonNewnEstelamIdRemain)
        Me.PnlInner.Controls.Add(Me.UcPersianTextBoxLoadAllocationDateTimeComposite)
        Me.PnlInner.Controls.Add(Me.Label4)
        Me.PnlInner.Controls.Add(Me.UcButtonLoadAllocationCancelling)
        Me.PnlInner.Controls.Add(Me.UcButtonLoadAllocationRegistering)
        Me.PnlInner.Controls.Add(Me.UcButtonNew)
        Me.PnlInner.Controls.Add(Me.UcPersianTextBoxLoadAllocationStatus)
        Me.PnlInner.Controls.Add(Me.Label3)
        Me.PnlInner.Controls.Add(Me.UcNumberLoadAllocationId)
        Me.PnlInner.Controls.Add(Me.Label2)
        Me.PnlInner.Controls.Add(Me.Label1)
        Me.PnlInner.Controls.Add(Me.PnlHolder)
        Me.PnlInner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlInner.Location = New System.Drawing.Point(2, 2)
        Me.PnlInner.Name = "PnlInner"
        Me.PnlInner.Size = New System.Drawing.Size(945, 212)
        Me.PnlInner.TabIndex = 0
        '
        'UcButtonNewnEstelamIdRemain
        '
        Me.UcButtonNewnEstelamIdRemain.BackColor = System.Drawing.Color.Transparent
        Me.UcButtonNewnEstelamIdRemain.Location = New System.Drawing.Point(57, 1)
        Me.UcButtonNewnEstelamIdRemain.Name = "UcButtonNewnEstelamIdRemain"
        Me.UcButtonNewnEstelamIdRemain.Padding = New System.Windows.Forms.Padding(1)
        Me.UcButtonNewnEstelamIdRemain.Size = New System.Drawing.Size(87, 30)
        Me.UcButtonNewnEstelamIdRemain.TabIndex = 18
        Me.UcButtonNewnEstelamIdRemain.UCBackColor = System.Drawing.Color.MidnightBlue
        Me.UcButtonNewnEstelamIdRemain.UCBackColorDisable = System.Drawing.Color.Gray
        Me.UcButtonNewnEstelamIdRemain.UCEnable = True
        Me.UcButtonNewnEstelamIdRemain.UCFont = New System.Drawing.Font("B Homa", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcButtonNewnEstelamIdRemain.UCForeColor = System.Drawing.Color.White
        Me.UcButtonNewnEstelamIdRemain.UCValue = "جدید-حفظ کدبار"
        '
        'UcPersianTextBoxLoadAllocationDateTimeComposite
        '
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.BackColor = System.Drawing.Color.Transparent
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.Location = New System.Drawing.Point(571, 4)
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.MaxCharacterReached = CType(50, Short)
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.Name = "UcPersianTextBoxLoadAllocationDateTimeComposite"
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.Size = New System.Drawing.Size(139, 25)
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.TabIndex = 6
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCBackColor = System.Drawing.Color.Gainsboro
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCBorder = True
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCBorderColor = System.Drawing.Color.DarkGray
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCEnable = False
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCFont = New System.Drawing.Font("IRMehr", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCForeColor = System.Drawing.Color.Black
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCMultiLine = False
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCOnlyDigit = R2Core.R2Enums.OnlyDigit.Any
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.UcPersianTextBoxLoadAllocationDateTimeComposite.UCValue = "1397/08/88 88:88:88"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label4.Location = New System.Drawing.Point(479, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 23)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "وضعیت تخصیص"
        '
        'UcButtonLoadAllocationCancelling
        '
        Me.UcButtonLoadAllocationCancelling.BackColor = System.Drawing.Color.Transparent
        Me.UcButtonLoadAllocationCancelling.Location = New System.Drawing.Point(210, 1)
        Me.UcButtonLoadAllocationCancelling.Name = "UcButtonLoadAllocationCancelling"
        Me.UcButtonLoadAllocationCancelling.Padding = New System.Windows.Forms.Padding(1)
        Me.UcButtonLoadAllocationCancelling.Size = New System.Drawing.Size(95, 30)
        Me.UcButtonLoadAllocationCancelling.TabIndex = 16
        Me.UcButtonLoadAllocationCancelling.UCBackColor = System.Drawing.Color.OrangeRed
        Me.UcButtonLoadAllocationCancelling.UCBackColorDisable = System.Drawing.Color.Gray
        Me.UcButtonLoadAllocationCancelling.UCEnable = True
        Me.UcButtonLoadAllocationCancelling.UCFont = New System.Drawing.Font("B Homa", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcButtonLoadAllocationCancelling.UCForeColor = System.Drawing.Color.White
        Me.UcButtonLoadAllocationCancelling.UCValue = "کنسلی تخصیص بار"
        '
        'UcButtonLoadAllocationRegistering
        '
        Me.UcButtonLoadAllocationRegistering.BackColor = System.Drawing.Color.Transparent
        Me.UcButtonLoadAllocationRegistering.Location = New System.Drawing.Point(142, 1)
        Me.UcButtonLoadAllocationRegistering.Name = "UcButtonLoadAllocationRegistering"
        Me.UcButtonLoadAllocationRegistering.Padding = New System.Windows.Forms.Padding(1)
        Me.UcButtonLoadAllocationRegistering.Size = New System.Drawing.Size(69, 30)
        Me.UcButtonLoadAllocationRegistering.TabIndex = 7
        Me.UcButtonLoadAllocationRegistering.UCBackColor = System.Drawing.Color.ForestGreen
        Me.UcButtonLoadAllocationRegistering.UCBackColorDisable = System.Drawing.Color.Gray
        Me.UcButtonLoadAllocationRegistering.UCEnable = True
        Me.UcButtonLoadAllocationRegistering.UCFont = New System.Drawing.Font("B Homa", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcButtonLoadAllocationRegistering.UCForeColor = System.Drawing.Color.White
        Me.UcButtonLoadAllocationRegistering.UCValue = "تخصیص بار"
        '
        'UcButtonNew
        '
        Me.UcButtonNew.BackColor = System.Drawing.Color.Transparent
        Me.UcButtonNew.Location = New System.Drawing.Point(7, 1)
        Me.UcButtonNew.Name = "UcButtonNew"
        Me.UcButtonNew.Padding = New System.Windows.Forms.Padding(1)
        Me.UcButtonNew.Size = New System.Drawing.Size(52, 30)
        Me.UcButtonNew.TabIndex = 15
        Me.UcButtonNew.UCBackColor = System.Drawing.Color.MidnightBlue
        Me.UcButtonNew.UCBackColorDisable = System.Drawing.Color.Gray
        Me.UcButtonNew.UCEnable = True
        Me.UcButtonNew.UCFont = New System.Drawing.Font("B Homa", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcButtonNew.UCForeColor = System.Drawing.Color.White
        Me.UcButtonNew.UCValue = "جدید"
        '
        'UcPersianTextBoxLoadAllocationStatus
        '
        Me.UcPersianTextBoxLoadAllocationStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcPersianTextBoxLoadAllocationStatus.BackColor = System.Drawing.Color.Transparent
        Me.UcPersianTextBoxLoadAllocationStatus.Location = New System.Drawing.Point(394, 4)
        Me.UcPersianTextBoxLoadAllocationStatus.MaxCharacterReached = CType(50, Short)
        Me.UcPersianTextBoxLoadAllocationStatus.Name = "UcPersianTextBoxLoadAllocationStatus"
        Me.UcPersianTextBoxLoadAllocationStatus.Size = New System.Drawing.Size(82, 25)
        Me.UcPersianTextBoxLoadAllocationStatus.TabIndex = 12
        Me.UcPersianTextBoxLoadAllocationStatus.UCBackColor = System.Drawing.Color.Gainsboro
        Me.UcPersianTextBoxLoadAllocationStatus.UCBorder = True
        Me.UcPersianTextBoxLoadAllocationStatus.UCBorderColor = System.Drawing.Color.DarkGray
        Me.UcPersianTextBoxLoadAllocationStatus.UCEnable = False
        Me.UcPersianTextBoxLoadAllocationStatus.UCFont = New System.Drawing.Font("IRMehr", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcPersianTextBoxLoadAllocationStatus.UCForeColor = System.Drawing.Color.Black
        Me.UcPersianTextBoxLoadAllocationStatus.UCMultiLine = False
        Me.UcPersianTextBoxLoadAllocationStatus.UCOnlyDigit = R2Core.R2Enums.OnlyDigit.Any
        Me.UcPersianTextBoxLoadAllocationStatus.UCTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.UcPersianTextBoxLoadAllocationStatus.UCValue = "کنسل شده"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("B Homa", 9.0!)
        Me.Label3.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label3.Location = New System.Drawing.Point(713, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 22)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "زمان ثبت"
        '
        'UcNumberLoadAllocationId
        '
        Me.UcNumberLoadAllocationId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcNumberLoadAllocationId.Font = New System.Drawing.Font("Alborz Titr", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.UcNumberLoadAllocationId.Location = New System.Drawing.Point(766, 4)
        Me.UcNumberLoadAllocationId.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UcNumberLoadAllocationId.Name = "UcNumberLoadAllocationId"
        Me.UcNumberLoadAllocationId.Size = New System.Drawing.Size(65, 25)
        Me.UcNumberLoadAllocationId.TabIndex = 4
        Me.UcNumberLoadAllocationId.UCAllowedMaxNumber = CType(9223372036854775807, Long)
        Me.UcNumberLoadAllocationId.UCAllowedMinNumber = CType(-922337203685477580, Long)
        Me.UcNumberLoadAllocationId.UCBackColor = System.Drawing.Color.White
        Me.UcNumberLoadAllocationId.UCBackColorDisable = System.Drawing.Color.Gainsboro
        Me.UcNumberLoadAllocationId.UCBackColorInvalidEntryException = System.Drawing.Color.Gold
        Me.UcNumberLoadAllocationId.UCBorder = True
        Me.UcNumberLoadAllocationId.UCBorderColor = System.Drawing.Color.DarkGray
        Me.UcNumberLoadAllocationId.UCEnable = False
        Me.UcNumberLoadAllocationId.UCFont = New System.Drawing.Font("Alborz Titr", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.UcNumberLoadAllocationId.UCForeColor = System.Drawing.Color.Black
        Me.UcNumberLoadAllocationId.UCMultiLine = False
        Me.UcNumberLoadAllocationId.UCValue = CType(8888888, Long)
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("B Homa", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label2.Location = New System.Drawing.Point(830, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 22)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "کد"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("B Homa", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label1.Location = New System.Drawing.Point(859, -1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 29)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "تخصیص بار"
        '
        'PnlHolder
        '
        Me.PnlHolder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlHolder.Controls.Add(Me.UcViewerNSSLoadCapacitorLoadDataEntry)
        Me.PnlHolder.Controls.Add(Me.UcViewerNSSTurnDataEntry)
        Me.PnlHolder.Location = New System.Drawing.Point(3, 16)
        Me.PnlHolder.Name = "PnlHolder"
        Me.PnlHolder.Size = New System.Drawing.Size(939, 193)
        Me.PnlHolder.TabIndex = 0
        '
        'UcViewerNSSLoadCapacitorLoadDataEntry
        '
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.BackColor = System.Drawing.Color.Transparent
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.Location = New System.Drawing.Point(408, 126)
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.Name = "UcViewerNSSLoadCapacitorLoadDataEntry"
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.Size = New System.Drawing.Size(524, 62)
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.TabIndex = 1
        Me.UcViewerNSSLoadCapacitorLoadDataEntry.UCNSSCurrent = Nothing
        '
        'UcViewerNSSTurnDataEntry
        '
        Me.UcViewerNSSTurnDataEntry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcViewerNSSTurnDataEntry.BackColor = System.Drawing.Color.Transparent
        Me.UcViewerNSSTurnDataEntry.Location = New System.Drawing.Point(7, 14)
        Me.UcViewerNSSTurnDataEntry.Name = "UcViewerNSSTurnDataEntry"
        Me.UcViewerNSSTurnDataEntry.Size = New System.Drawing.Size(925, 111)
        Me.UcViewerNSSTurnDataEntry.TabIndex = 0
        Me.UcViewerNSSTurnDataEntry.UCNSSCurrent = Nothing
        '
        'UCLoadAllocationManipulation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PnlMain)
        Me.Name = "UCLoadAllocationManipulation"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(961, 228)
        Me.PnlMain.ResumeLayout(false)
        Me.PnlOutter.ResumeLayout(false)
        Me.PnlInner.ResumeLayout(false)
        Me.PnlInner.PerformLayout
        Me.PnlHolder.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents PnlOutter As System.Windows.Forms.Panel
    Friend WithEvents PnlInner As System.Windows.Forms.Panel
    Friend WithEvents PnlHolder As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UcNumberLoadAllocationId As R2CoreGUI.UCNumber
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents UcPersianTextBoxLoadAllocationDateTimeComposite As R2CoreGUI.UCPersianTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents UcPersianTextBoxLoadAllocationStatus As R2CoreGUI.UCPersianTextBox
    Friend WithEvents UcButtonNew As R2CoreGUI.UCButton
    Friend WithEvents UcButtonLoadAllocationRegistering As R2CoreGUI.UCButton
    Friend WithEvents UcButtonLoadAllocationCancelling As R2CoreGUI.UCButton
    Friend WithEvents UcViewerNSSLoadCapacitorLoadDataEntry As UCViewerNSSLoadCapacitorLoadDataEntry
    Friend WithEvents UcViewerNSSTurnDataEntry As UCViewerNSSTurnDataEntry
    Friend WithEvents UcButtonNewnEstelamIdRemain As UCButton
End Class
