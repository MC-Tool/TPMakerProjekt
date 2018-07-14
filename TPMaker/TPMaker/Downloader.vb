Imports System.ComponentModel
Imports System.Net

Public Class Downloader
    Private Sub Downloader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    WithEvents wc As New System.Net.WebClient
    Private handler_shown As EventHandler
    Private handler_finish As AsyncCompletedEventHandler
    Public Sub Run(Dateiname As String, Version As String, Ziel As String)
        If wc.IsBusy = True Then
            MsgBox("Download bereits im gange! ", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim Z As String = infos.Pfad() & infos.Pfad_Download & Version
        If Not IO.Directory.Exists(Z) Then MkDir(Z)
        Dim h1 As EventHandler = Sub() Downloade(Dateiname, Version, Z & "/" & Dateiname)
        Me.Text = "Downloade " & Dateiname
        AddHandler Me.Shown, h1
        Dim H2 As AsyncCompletedEventHandler = Sub()
                                                   Me.Hide()
                                                   Me.Close()
                                                   ZIP.Extract_pack(Z & "/" & Dateiname)

                                               End Sub
        AddHandler wc.DownloadFileCompleted, H2
        handler_shown = h1
        handler_finish = H2
        Me.ShowDialog()
    End Sub
    Private Sub Downloade(Dateiname As String, Version As String, Ziel As String)
        Try
            ' wc.Credentials = New Net.NetworkCredential(Server_Handler.Benutzer, Server_Handler.P)
            Debug.Print("Downloade" & Server_Handler.Host & Server_Handler.Pfad_Packs & Version & "/" & Dateiname)
            wc.DownloadFileAsync(New Uri("https://sidezockinglp.lima-city.ch/Packs/1.8/" & Dateiname), Ziel)
            Me.path.Text = Dateiname
        Catch ex As Exception
            MsgBox("Fehler beim Download aufgetreten! Fehler: " & ex.Message, MsgBoxStyle.Critical)
            Me.Close()
        End Try

    End Sub

    Private Sub wc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        If e.BytesReceived > 0 Then

            Dim Prozent As Integer = e.ProgressPercentage
            Me.prozent.Text = e.ProgressPercentage & "%"
            Me.transfer.Text = "Empfangen: " & Math.Round(e.BytesReceived / 1000000) & " MB von " & Math.Round(e.TotalBytesToReceive / 1000000) & " MB"
            ProgressBar1.Value = e.ProgressPercentage
            Me.Refresh()
        End If
    End Sub

    Private Sub Downloader_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        wc.CancelAsync()
        wc.Dispose()
        RemoveHandler Me.Shown, handler_shown
        RemoveHandler wc.DownloadFileCompleted, handler_finish
    End Sub
End Class