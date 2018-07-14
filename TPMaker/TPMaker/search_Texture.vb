Public Class search_Texture
    Private Sub search_Texture_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function Get_texture_out_of_pack(path As String, grundpfad As String) As List(Of Array)

        Dim packpfad As String = infos.Pfad() & IO.Path.GetFileNameWithoutExtension(path).Replace("~", "/")
        Dim searchpath As String = grundpfad & IO.Path.GetDirectoryName(path)
        Dim l As New List(Of Array)

        For Each Datei As String In My.Computer.FileSystem.GetFiles(packpfad & searchpath, FileIO.SearchOption.SearchAllSubDirectories)
            Me.Text = "durchsuche Packs nach " & searchpath

            If Not IO.Directory.Exists(Datei) Then
                l.Add(New String() {Datei, IO.Path.GetExtension(Datei)})
            End If
        Next

        Return l

    End Function
    Public Function Get_texture_out_of_imports(search As String, version As String) As List(Of Array)

        Dim Packs As List(Of String) = Get_imports(version)
        Dim l As New List(Of Array)
        For Each Pack In Packs
            For Each Datei As String In My.Computer.FileSystem.GetFiles(Pack, FileIO.SearchOption.SearchAllSubDirectories)
                Me.Text = "durchsuche Packs nach " & search
                path.Text = IO.Path.GetDirectoryName(Datei)


                If IO.Path.GetFileName(Datei) = search Then
                    l.Add(New String() {Datei, Pack})
                End If
            Next
        Next
        Return l

    End Function
    Private Function Get_imports(version As String) As List(Of String)
        Dim l As New List(Of String)
        If My.Computer.FileSystem.DirectoryExists(infos.Pfad() & infos.Pfad_Impotiert & version) Then
            For Each item In (IO.Directory.GetDirectories(infos.Pfad() & infos.Pfad_Impotiert & version))
                l.Add(item)
            Next
        End If
        If My.Computer.FileSystem.DirectoryExists(infos.Pfad() & infos.Pfad_Standart & version) Then
            For Each item In (IO.Directory.GetDirectories(infos.Pfad() & infos.Pfad_Standart & version))
                l.Add(item)
            Next
        End If
        Return l
    End Function
End Class