Module infos
    Public versionen() = New String() {"1.8", "1.9", "1.10", "1.11", "1.12"}
    Public Function Pfad() As String
        If IO.Directory.Exists("D:\Prog Projekte\TPMaker\Programm") Then
            Return "D:\Prog Projekte\TPMaker\Programm"
        Else
            If Not IO.Directory.Exists("C:\TPMaker") Then MkDir("C:\TPMaker")
            Return "C:\TPMaker"
        End If
    End Function
    Public Standart_Pack_Ico As Image = My.Resources.pack
    Public unbekannt_Ico As Image = My.Resources.Settings_48px_1
    Public Pfad_Impotiert As String = "\Packs\"
    Public Pfad_Projekte As String = "\Projekts\"
    Public Pfad_Output As String = "\Out\"
    Public Pfad_Temp As String = "\Temp\"
    Public Pfad_Standart As String = "\Standart\"
    Public Pfad_Download As String = "\Download\"
    Public such_text As String = "Suche..."
End Module
