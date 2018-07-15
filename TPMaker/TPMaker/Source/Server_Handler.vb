Module Server_Handler
    Public Const P As String = "edxX7dvAkk6NpVa5"
    Public Const Host As String = "ftp://ftp_ahkw2sdd@sidezockinglp.lima-ftp.de"
    Public Const Benutzer As String = "ftp_ahkw2sdd"
    Public Const Port As String = "21"
    Public Const Pfad_Packs As String = "/Packs/"

    Public Function Get_Packs(Version As String, suche As String) As List(Of String)

        Dim l As New List(Of String)
        Try
            '##########################################Ordner Auslesen
            Debug.Print("anfrage an " & (Host & Pfad_Packs & Version))
            Dim request As Net.FtpWebRequest = Net.FtpWebRequest.Create(Host & Pfad_Packs & Version)

            request.Method = Net.WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New Net.NetworkCredential(Benutzer, P)
            Dim response As Net.FtpWebResponse = request.GetResponse()
            Using myReader As New IO.StreamReader(response.GetResponseStream())
                Do While Not myReader.EndOfStream = True
                    Dim file As String = myReader.ReadLine()
                    If file = "." Or file = ".." Then
                    Else
                        If String.IsNullOrEmpty(suche) = False Then
                            If IO.Path.GetFileNameWithoutExtension(file).Contains(suche) Then
                                l.Add(IO.Path.GetFileNameWithoutExtension(file))
                            End If
                        Else
                            l.Add(IO.Path.GetFileNameWithoutExtension(file))
                        End If

                    End If
                Loop
            End Using


            '#####################################################################

        Catch ex As Exception
            MsgBox("Fehler beim Verbinden zum Server! Fehler: " + ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try
        Return l
    End Function

End Module
