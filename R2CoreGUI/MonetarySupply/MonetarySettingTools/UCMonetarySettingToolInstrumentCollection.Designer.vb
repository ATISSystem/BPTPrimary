﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCMonetarySettingToolInstrumentCollection
    Inherits UCGeneral

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim R2CoreStandardMonetarySettingToolStructure1 As R2Core.MonetarySettingTools.R2CoreStandardMonetarySettingToolStructure = New R2Core.MonetarySettingTools.R2CoreStandardMonetarySettingToolStructure()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.UcButtonSpecial = New R2CoreGUI.UCButtonSpecial()
        Me.UcAmount = New R2CoreGUI.UCMoney()
        Me.UcucMonetarySettingToolCollection = New R2CoreGUI.UCUCMonetarySettingToolCollection()
        Me.PnlMonetarySettingToolInstrumentHolder = New System.Windows.Forms.Panel()
        Me.PnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.Controls.Add(Me.UcButtonSpecial)
        Me.PnlMain.Controls.Add(Me.UcAmount)
        Me.PnlMain.Controls.Add(Me.UcucMonetarySettingToolCollection)
        Me.PnlMain.Controls.Add(Me.PnlMonetarySettingToolInstrumentHolder)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Location = New System.Drawing.Point(0, 0)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(421, 230)
        Me.PnlMain.TabIndex = 0
        '
        'UcButtonSpecial
        '
        Me.UcButtonSpecial.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.UcButtonSpecial.BackColor = System.Drawing.Color.Black
        Me.UcButtonSpecial.Location = New System.Drawing.Point(147, 142)
        Me.UcButtonSpecial.Name = "UcButtonSpecial"
        Me.UcButtonSpecial.Padding = New System.Windows.Forms.Padding(2)
        Me.UcButtonSpecial.Size = New System.Drawing.Size(125, 47)
        Me.UcButtonSpecial.TabIndex = 6
        Me.UcButtonSpecial.UCBackColor = System.Drawing.Color.White
        Me.UcButtonSpecial.UCBackColorDisable = System.Drawing.Color.White
        Me.UcButtonSpecial.UCEnable = True
        Me.UcButtonSpecial.UCFont = New System.Drawing.Font("B Homa", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcButtonSpecial.UCForeColor = System.Drawing.Color.Black
        Me.UcButtonSpecial.UCValue = "تایید"
        '
        'UcAmount
        '
        Me.UcAmount.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.UcAmount.Location = New System.Drawing.Point(281, 155)
        Me.UcAmount.Name = "UcAmount"
        Me.UcAmount.Size = New System.Drawing.Size(103, 32)
        Me.UcAmount.TabIndex = 5
        Me.UcAmount.UCBackColor = System.Drawing.Color.White
        Me.UcAmount.UCBorder = False
        Me.UcAmount.UCBorderColor = System.Drawing.Color.Red
        Me.UcAmount.UCFont = New System.Drawing.Font("Alborz Titr", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.UcAmount.UCForeColor = System.Drawing.Color.Black
        Me.UcAmount.UCTextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.UcAmount.UCValue = ""
        '
        'UcucMonetarySettingToolCollection
        '
        Me.UcucMonetarySettingToolCollection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcucMonetarySettingToolCollection.BackColor = System.Drawing.Color.Transparent
        Me.UcucMonetarySettingToolCollection.Location = New System.Drawing.Point(0, 195)
        Me.UcucMonetarySettingToolCollection.Name = "UcucMonetarySettingToolCollection"
        Me.UcucMonetarySettingToolCollection.Size = New System.Drawing.Size(420, 33)
        Me.UcucMonetarySettingToolCollection.TabIndex = 0
        R2CoreStandardMonetarySettingToolStructure1.Active = True
        R2CoreStandardMonetarySettingToolStructure1.AssemblyDll = "R2CoreGUI.Dll"
        R2CoreStandardMonetarySettingToolStructure1.AssemblyPath = "R2CoreGUI.UCMonetarySettingToolUserPadInstrument"
        R2CoreStandardMonetarySettingToolStructure1.Deleted = False
        R2CoreStandardMonetarySettingToolStructure1.MSTColor = System.Drawing.Color.Blue
        R2CoreStandardMonetarySettingToolStructure1.MSTId = CType(1, Long)
        R2CoreStandardMonetarySettingToolStructure1.MSTName = "UserPad"
        R2CoreStandardMonetarySettingToolStructure1.MSTTitle = "پد کاربر"
        R2CoreStandardMonetarySettingToolStructure1.OCode = "1"
        R2CoreStandardMonetarySettingToolStructure1.OName = "UserPad"
        R2CoreStandardMonetarySettingToolStructure1.ViewFlag = True
        Me.UcucMonetarySettingToolCollection.UCCurrentNSS = R2CoreStandardMonetarySettingToolStructure1
        Me.UcucMonetarySettingToolCollection.UCDefaultMSTId = CType(1, Long)
        Me.UcucMonetarySettingToolCollection.UCViewBorder = True
        '
        'PnlMonetarySettingToolInstrumentHolder
        '
        Me.PnlMonetarySettingToolInstrumentHolder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMonetarySettingToolInstrumentHolder.Location = New System.Drawing.Point(0, 0)
        Me.PnlMonetarySettingToolInstrumentHolder.Name = "PnlMonetarySettingToolInstrumentHolder"
        Me.PnlMonetarySettingToolInstrumentHolder.Size = New System.Drawing.Size(420, 198)
        Me.PnlMonetarySettingToolInstrumentHolder.TabIndex = 1
        '
        'UCMonetarySettingToolInstrumentCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PnlMain)
        Me.Name = "UCMonetarySettingToolInstrumentCollection"
        Me.Size = New System.Drawing.Size(421, 230)
        Me.PnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlMain As Panel
    Friend WithEvents PnlMonetarySettingToolInstrumentHolder As Panel
    Friend WithEvents UcucMonetarySettingToolCollection As UCUCMonetarySettingToolCollection
    Friend WithEvents UcAmount As UCMoney
    Friend WithEvents UcButtonSpecial As UCButtonSpecial
End Class
