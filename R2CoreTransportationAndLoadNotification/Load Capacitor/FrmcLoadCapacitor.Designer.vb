﻿Imports R2CoreGUI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmcLoadCapacitor
    Inherits FrmcGeneral 

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
        Me.PnlLoadCapacitorLoadManipulation = New System.Windows.Forms.Panel()
        Me.UcLoadCapacitorLoadManipulation = New R2CoreTransportationAndLoadNotification.UCLoadCapacitorLoadManipulation()
        Me.PnlLoadCapacitor = New BlueActivity.Controls.AlphaGradientPanel()
        Me.ColorWithAlpha1 = New BlueActivity.Controls.ColorWithAlpha()
        Me.ColorWithAlpha2 = New BlueActivity.Controls.ColorWithAlpha()
        Me.UcucLoadCapacitorLoadCollectionAdvance = New R2CoreTransportationAndLoadNotification.UCUCLoadCapacitorLoadCollectionAdvance()
        Me.PnlLoadCapacitorLoadManipulation.SuspendLayout()
        Me.PnlLoadCapacitor.SuspendLayout()
        Me.SuspendLayout()
        '
        '_FrmMessageDialog
        '
        Me._FrmMessageDialog.Location = New System.Drawing.Point(-1000, -1000)
        '
        'PnlLoadCapacitorLoadManipulation
        '
        Me.PnlLoadCapacitorLoadManipulation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlLoadCapacitorLoadManipulation.BackColor = System.Drawing.Color.Transparent
        Me.PnlLoadCapacitorLoadManipulation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlLoadCapacitorLoadManipulation.Controls.Add(Me.UcLoadCapacitorLoadManipulation)
        Me.PnlLoadCapacitorLoadManipulation.Location = New System.Drawing.Point(5, 50)
        Me.PnlLoadCapacitorLoadManipulation.Name = "PnlLoadCapacitorLoadManipulation"
        Me.PnlLoadCapacitorLoadManipulation.Size = New System.Drawing.Size(995, 512)
        Me.PnlLoadCapacitorLoadManipulation.TabIndex = 201
        '
        'UcLoadCapacitorLoadManipulation
        '
        Me.UcLoadCapacitorLoadManipulation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcLoadCapacitorLoadManipulation.BackColor = System.Drawing.Color.Transparent
        Me.UcLoadCapacitorLoadManipulation.DisableUCSearcherLoadSources = False
        Me.UcLoadCapacitorLoadManipulation.Location = New System.Drawing.Point(2, -2)
        Me.UcLoadCapacitorLoadManipulation.Name = "UcLoadCapacitorLoadManipulation"
        Me.UcLoadCapacitorLoadManipulation.Padding = New System.Windows.Forms.Padding(5)
        Me.UcLoadCapacitorLoadManipulation.Size = New System.Drawing.Size(990, 509)
        Me.UcLoadCapacitorLoadManipulation.TabIndex = 2
        Me.UcLoadCapacitorLoadManipulation.UCNSSCurrent = Nothing
        '
        'PnlLoadCapacitor
        '
        Me.PnlLoadCapacitor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlLoadCapacitor.BackColor = System.Drawing.Color.Transparent
        Me.PnlLoadCapacitor.Border = True
        Me.PnlLoadCapacitor.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.PnlLoadCapacitor.Colors.Add(Me.ColorWithAlpha1)
        Me.PnlLoadCapacitor.Colors.Add(Me.ColorWithAlpha2)
        Me.PnlLoadCapacitor.ContentPadding = New System.Windows.Forms.Padding(0)
        Me.PnlLoadCapacitor.Controls.Add(Me.UcucLoadCapacitorLoadCollectionAdvance)
        Me.PnlLoadCapacitor.CornerRadius = 20
        Me.PnlLoadCapacitor.Corners = CType((((BlueActivity.Controls.Corner.TopLeft Or BlueActivity.Controls.Corner.TopRight) _
            Or BlueActivity.Controls.Corner.BottomLeft) _
            Or BlueActivity.Controls.Corner.BottomRight), BlueActivity.Controls.Corner)
        Me.PnlLoadCapacitor.Gradient = True
        Me.PnlLoadCapacitor.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.PnlLoadCapacitor.GradientOffset = 1.0!
        Me.PnlLoadCapacitor.GradientSize = New System.Drawing.Size(0, 0)
        Me.PnlLoadCapacitor.GradientWrapMode = System.Drawing.Drawing2D.WrapMode.Tile
        Me.PnlLoadCapacitor.Grayscale = False
        Me.PnlLoadCapacitor.Image = Nothing
        Me.PnlLoadCapacitor.ImageAlpha = 75
        Me.PnlLoadCapacitor.ImagePadding = New System.Windows.Forms.Padding(5)
        Me.PnlLoadCapacitor.ImagePosition = BlueActivity.Controls.ImagePosition.BottomRight
        Me.PnlLoadCapacitor.ImageSize = New System.Drawing.Size(48, 48)
        Me.PnlLoadCapacitor.Location = New System.Drawing.Point(5, 50)
        Me.PnlLoadCapacitor.Name = "PnlLoadCapacitor"
        Me.PnlLoadCapacitor.Rounded = True
        Me.PnlLoadCapacitor.Size = New System.Drawing.Size(995, 512)
        Me.PnlLoadCapacitor.TabIndex = 202
        '
        'ColorWithAlpha1
        '
        Me.ColorWithAlpha1.Alpha = 255
        Me.ColorWithAlpha1.Color = System.Drawing.Color.Transparent
        Me.ColorWithAlpha1.Parent = Me.PnlLoadCapacitor
        '
        'ColorWithAlpha2
        '
        Me.ColorWithAlpha2.Alpha = 255
        Me.ColorWithAlpha2.Color = System.Drawing.Color.Transparent
        Me.ColorWithAlpha2.Parent = Me.PnlLoadCapacitor
        '
        'UcucLoadCapacitorLoadCollectionAdvance
        '
        Me.UcucLoadCapacitorLoadCollectionAdvance.BackColor = System.Drawing.Color.Transparent
        Me.UcucLoadCapacitorLoadCollectionAdvance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcucLoadCapacitorLoadCollectionAdvance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcucLoadCapacitorLoadCollectionAdvance.Location = New System.Drawing.Point(0, 0)
        Me.UcucLoadCapacitorLoadCollectionAdvance.Name = "UcucLoadCapacitorLoadCollectionAdvance"
        Me.UcucLoadCapacitorLoadCollectionAdvance.Size = New System.Drawing.Size(995, 512)
        Me.UcucLoadCapacitorLoadCollectionAdvance.TabIndex = 1
        Me.UcucLoadCapacitorLoadCollectionAdvance.UCTimerInterval = CType(1, Long)
        Me.UcucLoadCapacitorLoadCollectionAdvance.UCViewnCarNumZero = False
        '
        'FrmcLoadCapacitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 600)
        Me.Controls.Add(Me.PnlLoadCapacitor)
        Me.Controls.Add(Me.PnlLoadCapacitorLoadManipulation)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "FrmcLoadCapacitor"
        Me.Text = "FrmcLoadCapacitor"
        Me.Controls.SetChildIndex(Me.PnlLoadCapacitorLoadManipulation, 0)
        Me.Controls.SetChildIndex(Me.PnlLoadCapacitor, 0)
        Me.PnlLoadCapacitorLoadManipulation.ResumeLayout(False)
        Me.PnlLoadCapacitor.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents PnlLoadCapacitorLoadManipulation As System.Windows.Forms.Panel
    Friend WithEvents UcucLoadCapacitorLoadCollectionAdvance As UCUCLoadCapacitorLoadCollectionAdvance
    Friend WithEvents UcLoadCapacitorLoadManipulation As UCLoadCapacitorLoadManipulation
    Friend WithEvents PnlLoadCapacitor As BlueActivity.Controls.AlphaGradientPanel
    Friend WithEvents ColorWithAlpha1 As BlueActivity.Controls.ColorWithAlpha
    Friend WithEvents ColorWithAlpha2 As BlueActivity.Controls.ColorWithAlpha
End Class
