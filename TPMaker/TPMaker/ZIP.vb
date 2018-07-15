Imports Ionic.Zip
Public Class ZIP
    Public Function Get_Inhalt(Zip As String) As List(Of String)
        Try
            Dim l As New List(Of String)
            Using zip1 As ZipFile = ZipFile.Read(Zip)

                Dim e As ZipEntry

                For Each e In zip1
                    l.Add(e.FileName)
                Next
            End Using
            Return l
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Private handler_shown As EventHandler
    Public Sub Extract_pack(source As String)
        Dim name As String = IO.Path.GetFileNameWithoutExtension(source)
        If My.Computer.FileSystem.DirectoryExists(infos.Pfad() & infos.Pfad_Impotiert & "/" & Version.Akt_Version & "/" & name) Then
            If Not MsgBox("Dieses Pack ist bereits impotiert! Soll es überschrieben werden?", MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If

        If MyFunctions.is_Zip_Tp_Pack(source) = False Then
            MsgBox("Das ausgewählte ZIP archiv ist nicht im richtigen Texturepack Format", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim h1 As EventHandler = Sub() extract(source, infos.Pfad() & infos.Pfad_Impotiert & "/" & Version.Akt_Version)
        AddHandler Me.Shown, h1
        handler_shown = h1
        Me.Text = "Entpacke " & name
        Me.ShowDialog()


    End Sub

    Private Sub extract(ZipToUnpack As String, TargetDir As String)
        Try
            Using zip1 As ZipFile = ZipFile.Read(ZipToUnpack)
                AddHandler zip1.ExtractProgress, AddressOf MyextractProgress
                Dim e As ZipEntry

                For Each e In zip1
                    e.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently)

                Next
            End Using

            Me.Close()
            MsgBox("Datei wurde Impotiert und erfolgreich zu Version: " & Version.Akt_Version & " hinzugefügt. Du findest es in der Liste " & TPImporter.cat(0), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Fehler beim entpacken! Fehler: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Async Sub MyextractProgress(sender As Object, e As ExtractProgressEventArgs)
        If Me.Visible = False Then Exit Sub

        If e.BytesTransferred > 0 AndAlso e.TotalBytesToTransfer > 0 Then



            Dim Prozent As Integer = "" & CInt(e.BytesTransferred * 100 \ e.TotalBytesToTransfer)
            Me.prozent.Text = Prozent & "%"
            Me.path.Text = e.CurrentEntry.FileName
            ProgressBar1.Value = Prozent
            Me.Refresh()
        End If

    End Sub

    Private Sub ZIP_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler Me.Shown, handler_shown
    End Sub
End Class