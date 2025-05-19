﻿Imports R2CoreGUI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmcCapacitorLoadsTransportCompaniesRegisteredLoadsReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmcCapacitorLoadsTransportCompaniesRegisteredLoadsReport))
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport = New System.Windows.Forms.Panel()
        Me.UcSearcherSoftwareUser = New R2CoreGUI.UCSearcherSoftwareUser()
        Me.UcSearcherLoadTargets = New R2CoreTransportationAndLoadNotification.UCSearcherLoadTargets()
        Me.UcSearcherTransportCompanies = New R2CoreTransportationAndLoadNotification.UCSearcherTransportCompanies()
        Me.UcDateTimeHolder = New R2CoreGUI.UCDateTimeHolder()
        Me.PnlSoftwareUsers = New System.Windows.Forms.Panel()
        Me.RBSpecialSoftwareUser = New System.Windows.Forms.RadioButton()
        Me.RBAllSoftwareUser = New System.Windows.Forms.RadioButton()
        Me.PnlTCs = New System.Windows.Forms.Panel()
        Me.RBSpecialCompany = New System.Windows.Forms.RadioButton()
        Me.RBAllCompany = New System.Windows.Forms.RadioButton()
        Me.ChkLoadTargetCity = New System.Windows.Forms.CheckBox()
        Me.PnlAnnouncementHallSelection = New System.Windows.Forms.Panel()
        Me.UcAnnouncementHallSelection = New R2CoreTransportationAndLoadNotification.UCAnnouncementHallSelection()
        Me.RBAllAnnouncementHall = New System.Windows.Forms.RadioButton()
        Me.RBSpecialAnnouncementHall = New System.Windows.Forms.RadioButton()
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.SuspendLayout()
        Me.PnlSoftwareUsers.SuspendLayout()
        Me.PnlTCs.SuspendLayout()
        Me.PnlAnnouncementHallSelection.SuspendLayout()
        Me.SuspendLayout()
        '
        '_FrmMessageDialog
        '
        Me._FrmMessageDialog.Location = New System.Drawing.Point(-1000, -1000)
        '
        'PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport
        '
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.BackColor = System.Drawing.Color.Transparent
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.UcSearcherSoftwareUser)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.UcSearcherLoadTargets)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.UcSearcherTransportCompanies)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.UcDateTimeHolder)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.PnlSoftwareUsers)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.PnlTCs)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.ChkLoadTargetCity)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Controls.Add(Me.PnlAnnouncementHallSelection)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Location = New System.Drawing.Point(5, 50)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Name = "PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport"
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.Size = New System.Drawing.Size(995, 512)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.TabIndex = 209
        '
        'UcSearcherSoftwareUser
        '
        Me.UcSearcherSoftwareUser.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.UcSearcherSoftwareUser.BackColor = System.Drawing.Color.Transparent
        Me.UcSearcherSoftwareUser.Location = New System.Drawing.Point(548, 218)
        Me.UcSearcherSoftwareUser.Name = "UcSearcherSoftwareUser"
        Me.UcSearcherSoftwareUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.UcSearcherSoftwareUser.Size = New System.Drawing.Size(229, 31)
        Me.UcSearcherSoftwareUser.TabIndex = 21
        Me.UcSearcherSoftwareUser.UCBackColor = System.Drawing.Color.Pink
        Me.UcSearcherSoftwareUser.UCFillFirstTime = False
        Me.UcSearcherSoftwareUser.UCFontList = New System.Drawing.Font("IRMehr", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcSearcherSoftwareUser.UCFontSearch = New System.Drawing.Font("IRMehr", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcSearcherSoftwareUser.UCForeColor = System.Drawing.Color.Black
        Me.UcSearcherSoftwareUser.UCIcon = Nothing
        Me.UcSearcherSoftwareUser.UCMaximizeHight = CType(200, Long)
        Me.UcSearcherSoftwareUser.UCMinimizeHight = CType(31, Long)
        Me.UcSearcherSoftwareUser.UCMode = R2CoreGUI.UCSearcherAdvance.UCModeType.DropDown
        Me.UcSearcherSoftwareUser.UCShowDomainIcon = False
        '
        'UcSearcherLoadTargets
        '
        Me.UcSearcherLoadTargets.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.UcSearcherLoadTargets.BackColor = System.Drawing.Color.Transparent
        Me.UcSearcherLoadTargets.Location = New System.Drawing.Point(548, 181)
        Me.UcSearcherLoadTargets.Name = "UcSearcherLoadTargets"
        Me.UcSearcherLoadTargets.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.UcSearcherLoadTargets.Size = New System.Drawing.Size(229, 31)
        Me.UcSearcherLoadTargets.TabIndex = 20
        Me.UcSearcherLoadTargets.UCBackColor = System.Drawing.Color.White
        Me.UcSearcherLoadTargets.UCFillFirstTime = False
        Me.UcSearcherLoadTargets.UCFontList = New System.Drawing.Font("IRMehr", 8.25!)
        Me.UcSearcherLoadTargets.UCFontSearch = New System.Drawing.Font("IRMehr", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcSearcherLoadTargets.UCForeColor = System.Drawing.Color.Black
        Me.UcSearcherLoadTargets.UCIcon = Nothing
        Me.UcSearcherLoadTargets.UCMaximizeHight = CType(120, Long)
        Me.UcSearcherLoadTargets.UCMinimizeHight = CType(31, Long)
        Me.UcSearcherLoadTargets.UCMode = R2CoreGUI.UCSearcherAdvance.UCModeType.DropDown
        Me.UcSearcherLoadTargets.UCShowDomainIcon = False
        '
        'UcSearcherTransportCompanies
        '
        Me.UcSearcherTransportCompanies.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.UcSearcherTransportCompanies.BackColor = System.Drawing.Color.Transparent
        Me.UcSearcherTransportCompanies.Location = New System.Drawing.Point(548, 144)
        Me.UcSearcherTransportCompanies.Name = "UcSearcherTransportCompanies"
        Me.UcSearcherTransportCompanies.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.UcSearcherTransportCompanies.Size = New System.Drawing.Size(231, 31)
        Me.UcSearcherTransportCompanies.TabIndex = 24
        Me.UcSearcherTransportCompanies.UCBackColor = System.Drawing.Color.Red
        Me.UcSearcherTransportCompanies.UCFillFirstTime = False
        Me.UcSearcherTransportCompanies.UCFontList = New System.Drawing.Font("IRMehr", 8.25!)
        Me.UcSearcherTransportCompanies.UCFontSearch = New System.Drawing.Font("IRMehr", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcSearcherTransportCompanies.UCForeColor = System.Drawing.Color.Black
        Me.UcSearcherTransportCompanies.UCIcon = CType(resources.GetObject("UcSearcherTransportCompanies.UCIcon"), System.Drawing.Image)
        Me.UcSearcherTransportCompanies.UCMaximizeHight = CType(200, Long)
        Me.UcSearcherTransportCompanies.UCMinimizeHight = CType(31, Long)
        Me.UcSearcherTransportCompanies.UCMode = R2CoreGUI.UCSearcherAdvance.UCModeType.DropDown
        Me.UcSearcherTransportCompanies.UCShowDomainIcon = False
        '
        'UcDateTimeHolder
        '
        Me.UcDateTimeHolder.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.UcDateTimeHolder.BackColor = System.Drawing.Color.Transparent
        Me.UcDateTimeHolder.Location = New System.Drawing.Point(393, 252)
        Me.UcDateTimeHolder.Name = "UcDateTimeHolder"
        Me.UcDateTimeHolder.Padding = New System.Windows.Forms.Padding(3)
        Me.UcDateTimeHolder.Size = New System.Drawing.Size(209, 186)
        Me.UcDateTimeHolder.TabIndex = 9
        Me.UcDateTimeHolder.UCDisableTimeSetting = False
        Me.UcDateTimeHolder.UCTime1 = "00:00:00"
        Me.UcDateTimeHolder.UCTime2 = "23:59:59"
        Me.UcDateTimeHolder.UCViewTitle = False
        '
        'PnlSoftwareUsers
        '
        Me.PnlSoftwareUsers.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PnlSoftwareUsers.Controls.Add(Me.RBSpecialSoftwareUser)
        Me.PnlSoftwareUsers.Controls.Add(Me.RBAllSoftwareUser)
        Me.PnlSoftwareUsers.Location = New System.Drawing.Point(785, 217)
        Me.PnlSoftwareUsers.Name = "PnlSoftwareUsers"
        Me.PnlSoftwareUsers.Size = New System.Drawing.Size(175, 27)
        Me.PnlSoftwareUsers.TabIndex = 23
        '
        'RBSpecialSoftwareUser
        '
        Me.RBSpecialSoftwareUser.AutoSize = True
        Me.RBSpecialSoftwareUser.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RBSpecialSoftwareUser.Location = New System.Drawing.Point(11, 0)
        Me.RBSpecialSoftwareUser.Name = "RBSpecialSoftwareUser"
        Me.RBSpecialSoftwareUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RBSpecialSoftwareUser.Size = New System.Drawing.Size(47, 27)
        Me.RBSpecialSoftwareUser.TabIndex = 13
        Me.RBSpecialSoftwareUser.Text = "کاربر"
        Me.RBSpecialSoftwareUser.UseVisualStyleBackColor = True
        '
        'RBAllSoftwareUser
        '
        Me.RBAllSoftwareUser.AutoSize = True
        Me.RBAllSoftwareUser.Checked = True
        Me.RBAllSoftwareUser.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RBAllSoftwareUser.Location = New System.Drawing.Point(85, 0)
        Me.RBAllSoftwareUser.Name = "RBAllSoftwareUser"
        Me.RBAllSoftwareUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RBAllSoftwareUser.Size = New System.Drawing.Size(81, 27)
        Me.RBAllSoftwareUser.TabIndex = 11
        Me.RBAllSoftwareUser.TabStop = True
        Me.RBAllSoftwareUser.Text = "همه کاربران"
        Me.RBAllSoftwareUser.UseVisualStyleBackColor = True
        '
        'PnlTCs
        '
        Me.PnlTCs.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PnlTCs.Controls.Add(Me.RBSpecialCompany)
        Me.PnlTCs.Controls.Add(Me.RBAllCompany)
        Me.PnlTCs.Location = New System.Drawing.Point(782, 144)
        Me.PnlTCs.Name = "PnlTCs"
        Me.PnlTCs.Size = New System.Drawing.Size(175, 27)
        Me.PnlTCs.TabIndex = 22
        '
        'RBSpecialCompany
        '
        Me.RBSpecialCompany.AutoSize = True
        Me.RBSpecialCompany.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RBSpecialCompany.Location = New System.Drawing.Point(3, 0)
        Me.RBSpecialCompany.Name = "RBSpecialCompany"
        Me.RBSpecialCompany.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RBSpecialCompany.Size = New System.Drawing.Size(57, 27)
        Me.RBSpecialCompany.TabIndex = 13
        Me.RBSpecialCompany.Text = "شرکت"
        Me.RBSpecialCompany.UseVisualStyleBackColor = True
        '
        'RBAllCompany
        '
        Me.RBAllCompany.AutoSize = True
        Me.RBAllCompany.Checked = True
        Me.RBAllCompany.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RBAllCompany.Location = New System.Drawing.Point(76, 0)
        Me.RBAllCompany.Name = "RBAllCompany"
        Me.RBAllCompany.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RBAllCompany.Size = New System.Drawing.Size(93, 27)
        Me.RBAllCompany.TabIndex = 11
        Me.RBAllCompany.TabStop = True
        Me.RBAllCompany.Text = "همه شرکت ها"
        Me.RBAllCompany.UseVisualStyleBackColor = True
        '
        'ChkLoadTargetCity
        '
        Me.ChkLoadTargetCity.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ChkLoadTargetCity.AutoSize = True
        Me.ChkLoadTargetCity.Font = New System.Drawing.Font("B Homa", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.ChkLoadTargetCity.Location = New System.Drawing.Point(878, 178)
        Me.ChkLoadTargetCity.Name = "ChkLoadTargetCity"
        Me.ChkLoadTargetCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkLoadTargetCity.Size = New System.Drawing.Size(82, 33)
        Me.ChkLoadTargetCity.TabIndex = 19
        Me.ChkLoadTargetCity.Text = "مقصد بار"
        Me.ChkLoadTargetCity.UseVisualStyleBackColor = True
        '
        'PnlAnnouncementHallSelection
        '
        Me.PnlAnnouncementHallSelection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlAnnouncementHallSelection.Controls.Add(Me.UcAnnouncementHallSelection)
        Me.PnlAnnouncementHallSelection.Controls.Add(Me.RBAllAnnouncementHall)
        Me.PnlAnnouncementHallSelection.Controls.Add(Me.RBSpecialAnnouncementHall)
        Me.PnlAnnouncementHallSelection.Location = New System.Drawing.Point(3, 10)
        Me.PnlAnnouncementHallSelection.Name = "PnlAnnouncementHallSelection"
        Me.PnlAnnouncementHallSelection.Size = New System.Drawing.Size(989, 126)
        Me.PnlAnnouncementHallSelection.TabIndex = 16
        '
        'UcAnnouncementHallSelection
        '
        Me.UcAnnouncementHallSelection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcAnnouncementHallSelection.BackColor = System.Drawing.Color.Transparent
        Me.UcAnnouncementHallSelection.Location = New System.Drawing.Point(26, 39)
        Me.UcAnnouncementHallSelection.Name = "UcAnnouncementHallSelection"
        Me.UcAnnouncementHallSelection.Size = New System.Drawing.Size(934, 80)
        Me.UcAnnouncementHallSelection.TabIndex = 21
        '
        'RBAllAnnouncementHall
        '
        Me.RBAllAnnouncementHall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RBAllAnnouncementHall.AutoSize = True
        Me.RBAllAnnouncementHall.Checked = True
        Me.RBAllAnnouncementHall.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RBAllAnnouncementHall.Location = New System.Drawing.Point(886, 3)
        Me.RBAllAnnouncementHall.Name = "RBAllAnnouncementHall"
        Me.RBAllAnnouncementHall.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RBAllAnnouncementHall.Size = New System.Drawing.Size(71, 27)
        Me.RBAllAnnouncementHall.TabIndex = 10
        Me.RBAllAnnouncementHall.TabStop = True
        Me.RBAllAnnouncementHall.Text = "همه بارها"
        Me.RBAllAnnouncementHall.UseVisualStyleBackColor = True
        '
        'RBSpecialAnnouncementHall
        '
        Me.RBSpecialAnnouncementHall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RBSpecialAnnouncementHall.AutoSize = True
        Me.RBSpecialAnnouncementHall.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.RBSpecialAnnouncementHall.Location = New System.Drawing.Point(700, 3)
        Me.RBSpecialAnnouncementHall.Name = "RBSpecialAnnouncementHall"
        Me.RBSpecialAnnouncementHall.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RBSpecialAnnouncementHall.Size = New System.Drawing.Size(148, 27)
        Me.RBSpecialAnnouncementHall.TabIndex = 15
        Me.RBSpecialAnnouncementHall.Text = "بار مرتبط با زیرگروه اعلام بار"
        Me.RBSpecialAnnouncementHall.UseVisualStyleBackColor = True
        '
        'FrmcCapacitorLoadsTransportCompaniesRegisteredLoadsReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 600)
        Me.Controls.Add(Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "FrmcCapacitorLoadsTransportCompaniesRegisteredLoadsReport"
        Me.Text = "FrmcCapacitorLoadsTransportCompaniesRegisteredLoads"
        Me.Controls.SetChildIndex(Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport, 0)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.ResumeLayout(False)
        Me.PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport.PerformLayout()
        Me.PnlSoftwareUsers.ResumeLayout(False)
        Me.PnlSoftwareUsers.PerformLayout()
        Me.PnlTCs.ResumeLayout(False)
        Me.PnlTCs.PerformLayout()
        Me.PnlAnnouncementHallSelection.ResumeLayout(False)
        Me.PnlAnnouncementHallSelection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlCapacitorLoadsTransportCompaniesRegisteredLoadsReport As System.Windows.Forms.Panel
    Friend WithEvents UcDateTimeHolder As UCDateTimeHolder
    Friend WithEvents RBSpecialCompany As System.Windows.Forms.RadioButton
    Friend WithEvents RBAllCompany As System.Windows.Forms.RadioButton
    Friend WithEvents RBAllAnnouncementHall As System.Windows.Forms.RadioButton
    Friend WithEvents PnlAnnouncementHallSelection As System.Windows.Forms.Panel
    Friend WithEvents RBSpecialAnnouncementHall As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLoadTargetCity As Windows.Forms.CheckBox
    Friend WithEvents UcSearcherLoadTargets As R2CoreTransportationAndLoadNotification.UCSearcherLoadTargets
    Friend WithEvents UcAnnouncementHallSelection As R2CoreTransportationAndLoadNotification.UCAnnouncementHallSelection
    Friend WithEvents PnlSoftwareUsers As Windows.Forms.Panel
    Friend WithEvents RBSpecialSoftwareUser As Windows.Forms.RadioButton
    Friend WithEvents RBAllSoftwareUser As Windows.Forms.RadioButton
    Friend WithEvents PnlTCs As Windows.Forms.Panel
    Friend WithEvents UcSearcherSoftwareUser As UCSearcherSoftwareUser
    Friend WithEvents UcSearcherTransportCompanies As R2CoreTransportationAndLoadNotification.UCSearcherTransportCompanies
End Class
