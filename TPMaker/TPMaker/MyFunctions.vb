Module MyFunctions
    Public Function get_image(file As String) As Image
        Try
            If Not IO.Directory.Exists(infos.Pfad() & Pfad_Temp) Then MkDir(infos.Pfad() & Pfad_Temp)
            Dim format As String = Now.ToString("MMddHHmmssffff")
            Dim filename As String = Get_datei_primär(infos.Pfad() & Pfad_Temp, format)
            IO.File.Copy(file, infos.Pfad() & Pfad_Temp & filename)
            Return Image.FromFile(infos.Pfad() & Pfad_Temp & filename)
        Catch ex As Exception
            Return infos.Standart_Pack_Ico
        End Try
    End Function
    Private Function Get_datei_primär(searchpfad As String, format As String) As String
        Dim zähler As Integer = 0

        Try
            For Each Datei As String In My.Computer.FileSystem.GetFiles(searchpfad, FileIO.SearchOption.SearchAllSubDirectories)

                Do While My.Computer.FileSystem.FileExists(IO.Path.GetDirectoryName(Datei) & "/" & format & zähler)
                    zähler += 1
                Loop

            Next
        Catch ex As Exception

        End Try
        Return format & zähler
    End Function

    Public Function is_Tp_Pack(Ordner As String) As Boolean
        If Not IO.Directory.Exists(Ordner) Then Return False
        If Not IO.Directory.Exists(Ordner & "/assets") Then Return False
        If Not IO.File.Exists(Ordner & "/pack.mcmeta") Then Return False
        Return True
    End Function
    Public Function is_Zip_Tp_Pack(file As String) As Boolean
        If Not IO.File.Exists(file) Then Return False
        Dim l As List(Of String) = ZIP.Get_Inhalt(file)

        If l.Contains("assets/") Then Return False
        If l.Contains("pack.mcmeta") Then Return False
        Return True
    End Function
    Public Function get_suche(search As TextBox) As String
        If search.Text = Nothing Or search.Text = "Suche..." Then
            Return Nothing
        Else
            Return search.Text
        End If
    End Function
End Module
