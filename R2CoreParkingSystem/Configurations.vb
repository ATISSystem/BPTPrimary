
Imports R2Core.GeneralConfiguration

Namespace GeneralConfiguration
    Public MustInherit Class R2CoreParkingSystemGeneralConfigurations
        Inherits R2CoreGeneralConfigurations

        Public Shared ReadOnly Property TerafficAndTerafficCard As Int64 = 41
        Public Shared ReadOnly Property IndigenousCars As Int64 = 89


    End Class

End Namespace

Namespace ConfigurationOfDevices
    Public MustInherit Class R2CoreParkingSystemConfigurationsOfDevices
        Inherits R2Core.ConfigurationOfDevices.R2CoreConfigurationsOfDevices
        Public Shared ReadOnly Property LPRCamera As Int64 = 1


    End Class



End Namespace



Namespace ConfigurationManagement

    Public MustInherit Class R2CoreParkingSystemConfigurations
        Inherits R2Core.ConfigurationManagement.R2CoreConfigurations

        Public Shared ReadOnly Property PersonChargeMessage As Int64 = 12
        Public Shared ReadOnly Property Camera1 As Int64 = 13
        Public Shared ReadOnly Property Camera2 As Int64 = 14
        Public Shared ReadOnly Property MoneyWalletCharge As Int64 = 15
        Public Shared ReadOnly Property ImageProcessing As Int64 = 16
        Public Shared ReadOnly Property EnterExitAccountingCanPrint As Int64 = 17
        Public Shared ReadOnly Property SepasSystem As Int64 = 18
        Public Shared ReadOnly Property TerafficCardsTypeSupport As Int64 = 19
        Public Shared ReadOnly Property SaveCarPicture As Int64 = 20
        Public Shared ReadOnly Property TrafficCardEERequestStatusWithMaabarMatch As Int64 = 23
        Public Shared ReadOnly Property TariffsMblghPaye As Int64 = 28
        Public Shared ReadOnly Property TariffsMblghDelay As Int64 = 29
        Public Shared ReadOnly Property TariffsMblghApproved As Int64 = 30
        Public Shared ReadOnly Property ChargeActiveOnThisLocation As Int64 = 39
        Public Shared ReadOnly Property LPRIsActive As Int64 = 40
        'Public Shared ReadOnly Property TerafficAndTerafficCard As Int64 = 41
        Public Shared ReadOnly Property FrmcEnterExitSetting As Int64 = 48
        'Public Shared ReadOnly Property IndigenousCars As Int64 = 89
        Public Shared ReadOnly Property EntryExitAllownSMS As Int64 = 90



    End Class


End Namespace


