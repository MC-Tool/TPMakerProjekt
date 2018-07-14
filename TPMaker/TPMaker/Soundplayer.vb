Imports System.Threading
Imports Un4seen.Bass
Public Class Soundplayer

    Public Sub Play_Sound(file As String)
        Dim stream As Integer
        Dim filelänge As Integer
        Dim f As New Soundplayer
        Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, Me.Handle)
        stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_AUTOFREE Or BASSFlag.BASS_STREAM_PRESCAN)
        filelänge = Bass.BASS_ChannelGetLength(stream)
        f.TrackBar1.Maximum = filelänge / 100000
        f.Label4.Text = "Sound: " & IO.Path.GetFileName(file)
        f.Text = "Anhören " & IO.Path.GetFileName(file)
        f.Label5.Text = "Länge: " & SecToTime(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)))
        AddHandler f.Button1.Click, Sub()
                                        Try
                                            If f.TrackBar1.Value = 0 Then stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_AUTOFREE Or BASSFlag.BASS_STREAM_PRESCAN)
                                            Bass.BASS_ChannelPlay(stream, False)
                                            f.Timer1.Start()
                                        Catch ex As Exception
                                            MsgBox("Fehler beim starten! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                        End Try

                                    End Sub
        AddHandler f.Timer1.Tick, Sub()
                                      Try
                                          If Bass.BASS_ChannelGetPosition(stream) = Bass.BASS_ChannelGetLength(stream) Then
                                              f.ProgressBar1.Value = 0
                                              f.ProgressBar2.Value = 0
                                          End If
                                          Dim Spectrum As New Un4seen.Bass.Misc.Visuals
                                          f.TrackBar1.Value = Bass.BASS_ChannelGetPosition(stream) / 100000

                                          Dim level As Integer = Bass.BASS_ChannelGetLevel(stream)
                                          Dim left As Integer = Utils.LowWord32(level) ' the left level
                                          Dim right As Integer = Utils.HighWord32(level) ' the right level
                                          If Not left > ProgressBar1.Maximum And Not right > ProgressBar2.Maximum Then
                                              f.ProgressBar1.Value = left
                                              f.ProgressBar2.Value = right

                                          Else
                                              f.ProgressBar1.Value = 0
                                              f.ProgressBar2.Value = 0
                                          End If
                                          Dim zeit As Double = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream))
                                          If zeit >= 0 Then
                                              f.Label3.Text = SecToTime(zeit)
                                          Else
                                              f.Label3.Text = "00:00"
                                          End If
                                          f.PictureBox1.Image = Spectrum.CreateSpectrumLine(stream, f.PictureBox1.Width, f.PictureBox1.Height, Color.Lime, Color.Red, Color.White, 8, 5, False, True, True)
                                      Catch
                                      End Try
                                  End Sub

        AddHandler f.TrackBar1.Scroll, Sub()
                                           Try
                                               Bass.BASS_ChannelSetPosition(stream, f.TrackBar1.Value * 100000)
                                               Dim zeit As Double = Bass.BASS_ChannelBytes2Seconds(stream, f.TrackBar1.Value * 100000)
                                               f.Label3.Text = SecToTime(zeit)
                                           Catch ex As Exception
                                               MsgBox("Fehler beim verändern der Position! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                           End Try

                                       End Sub

        AddHandler f.Button3.Click, Sub()
                                        Bass.BASS_ChannelPause(stream)
                                        f.ProgressBar1.Value = 0
                                        f.ProgressBar2.Value = 0
                                        f.Timer1.Stop()
                                    End Sub
        AddHandler f.Button4.Click, Sub()
                                        Try
                                            Dim Spectrum As New Un4seen.Bass.Misc.Visuals
                                            f.Timer1.Stop()
                                            Bass.BASS_ChannelStop(stream)
                                            f.TrackBar1.Value = Bass.BASS_ChannelGetPosition(stream) / 100000
                                            Dim zeit As Double = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream))
                                            If zeit >= 0 Then
                                                f.Label3.Text = SecToTime(zeit)
                                            Else
                                                f.Label3.Text = "00:00"
                                            End If
                                            f.PictureBox1.Image = Spectrum.CreateSpectrumLine(stream, f.PictureBox1.Width, f.PictureBox1.Height, Color.Lime, Color.Red, Color.White, 8, 5, False, True, True)
                                            f.ProgressBar1.Value = 0
                                            f.ProgressBar2.Value = 0
                                        Catch ex As Exception
                                            MsgBox("Fehler beim stoppen! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                        End Try
                                    End Sub
        AddHandler f.FormClosing, Sub(sender, e)
                                      Dim status As BASSActive = Bass.BASS_ChannelIsActive(stream)
                                      If status = BASSActive.BASS_ACTIVE_PLAYING Then
                                          Try
                                              f.Timer1.Stop()
                                              Bass.BASS_ChannelStop(stream)
                                          Catch ex As Exception
                                              MsgBox("Fehler beim stoppen! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                          End Try
                                      End If
                                  End Sub
        f.Show()
                                    End Sub
    Private Sub Soundplayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub





    Public Function SecToTime(ByVal Seconds As Long, Optional ByRef rHour As Long = 0, Optional ByRef rMinute As Long = 0, Optional ByRef rSecond As Long = 0) As String
        rHour = (Seconds \ 3600)
        rMinute = (Seconds - (rHour * 3600)) \ 60
        rSecond = (Seconds - (rHour * 3600) - (rMinute * 60))
        SecToTime = Format(rHour, "00") & ":" & Format(rMinute, "00") & ":" & Format(rSecond, "00")
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim c As New ContextMenuStrip
        For x = 0 To 100 Step 10
            Dim item As New ToolStripMenuItem
            item.Text = x & "%"
            Dim v As Single = x / 100
            Dim aktlaut As Single = Math.Round(Bass.BASS_GetVolume(), 2)
            If aktlaut = v Then item.Image = My.Resources.Ok_48px
            AddHandler item.Click, Sub()
                                       Try

                                           Bass.BASS_SetVolume(v)

                                       Catch ex As Exception
                                           MsgBox("Fehler beim setzen! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                       End Try
                                   End Sub
            c.Items.Add(item)
        Next
        c.Show(Button2, Button2.Location)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class