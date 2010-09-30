Module AsteriskProxy
    Public Class UserAgentLoginEvent
        Inherits Asterisk.NET.Manager.Action.LoginAction
        Public Agent As String
    End Class
    Dim manager As Asterisk.NET.Manager.ManagerConnection
    WithEvents managerEvt As Asterisk.NET.Manager.ManagerConnection
    Sub dam_UserEvents(ByVal sender As Object, ByVal userEvent As Object) Handles managerEvt.UserEvents

        If (TypeOf userEvent Is UserAgentLoginEvent) Then
            Console.WriteLine("User Event - AgentLogin:" + "\n\tAgent\t\t")
            Console.WriteLine(CType(userEvent, UserAgentLoginEvent).Agent)
        Else
            Console.WriteLine("User Event:" + "\n\tUserEventName\t\t" + userEvent.UserEventName)
            '  foreach (System.Collections.Generic.KeyValuePair<string, string> pair in e.Attributes)
            '{
            'Console.WriteLine(String.Format("\t{0}\t{1}", pair.Key, pair.Value));
            '}
        End If
    End Sub


    Const DEV_HOST As String = "192.168.7.144"
    Const ASTERISK_PORT As Integer = 5038
    Const ASTERISK_HOST As String = "192.168.7.199"
    Const ASTERISK_LOGINNAME As String = "phonesuite"
    Const ASTERISK_LOGINPWD As String = "5d4aba078"

    Const ORIGINATE_CONTEXT As String = "from-internal"
    Const ORIGINATE_CHANNEL As String = "IAX2/100"
    Const ORIGINATE_EXTRA_CHANNEL As String = "SIP/101"
    Const ORIGINATE_EXTRA_EXTEN As String = "101"
    Const ORIGINATE_EXTEN As String = "101"
    Const ORIGINATE_CALLERID As String = "Asterisk.NET"
    Const ORIGINATE_TIMEOUT As Integer = 15000
    Sub Main()
        manager = New Asterisk.NET.Manager.ManagerConnection(ASTERISK_HOST, ASTERISK_PORT, ASTERISK_LOGINNAME, ASTERISK_LOGINPWD)
        'Register user event class
        manager.RegisterUserEventClass(GetType(UserAgentLoginEvent))
        Try

            manager.Login()         ' Login only (fast)
            Console.WriteLine("Asterisk version : " + manager.Version)
        Catch ex As System.Exception
            Console.WriteLine(ex)
            Console.ReadLine()
            manager.Logoff()
        End Try
    End Sub

End Module
