Imports System.IO

Module Ersteller_List
    Private Handlerlist As New List(Of EventHandler)
    Private Buttonandhandlerlist As New List(Of EventHandler)
    Private Buttonlist As New List(Of Button)
    Private Listviewlist As New List(Of ListView)
    Private pack_importer_handler As New List(Of EventHandler)
    Private pack_importer_button As New List(Of Button)
    Private importer_handler As New List(Of EventHandler)
    Private importer_button As New List(Of Button)

    Private Function get_back_string(akt As String) As Integer
        Dim Buchstabe As String, i As Integer
        Dim abschnitt As Integer
        For i = Len(akt) To 1 Step -1
            abschnitt += 1
            Buchstabe = Mid$(akt, i, 1)
            If Buchstabe = "/" Then
                Return abschnitt
            End If
        Next i
    End Function
    Private Function Get_icon_bild(datei) As Image

        Select Case IO.Path.GetFileName(datei)
            Case Else
                Return MyFunctions.get_image(datei)
        End Select
    End Function
    Private Function Get_anzeige_bild(datei) As Image
        Select Case IO.Path.GetFileName(datei)
            Case Else
                Return MyFunctions.get_image(datei)
        End Select
    End Function
    Public Async Sub Show_Liste(Box As ListView, Path As Label, grundpfad As String, funktionpanel As Panel, Edit_texture_button As Button, ersetzen_button As Button, titel As Label, ersetzen_ext_button As Button, edit_ext_button As Button, auflösung_button As Button, entfernen_button As Button, anzeige As PictureBox, Pack_importer As Button, import As Button, suche As TextBox)

        '###########clear
        Do
            'import 1 button
            Dim z As Integer = 0
            Dim pack_importer_handler_to_remove As New List(Of EventHandler)
            Dim pack_importer_button_to_remove As New List(Of Button)
            For Each i In pack_importer_handler
                If pack_importer_button(z).Name = Pack_importer.Name Then
                    pack_importer_handler_to_remove.Add(i)
                    pack_importer_button_to_remove.Add(pack_importer_button(z))
                End If
                z += 1
            Next
            Dim z2 As Integer = 0
            For Each i In pack_importer_handler_to_remove
                RemoveHandler pack_importer_button_to_remove(z2).Click, i
                z2 += 1
            Next
            'import 2
            Dim z3 As Integer = 0
            Dim importer_handler_to_remove As New List(Of EventHandler)
            Dim importer_button_to_remove As New List(Of Button)
            For Each i In importer_handler
                If importer_button(z3).Name = import.Name Then
                    importer_handler_to_remove.Add(i)
                    importer_button_to_remove.Add(importer_button(z3))
                End If
                z3 += 1
            Next
            Dim z4 As Integer = 0
            For Each i In importer_handler_to_remove
                RemoveHandler importer_button_to_remove(z4).Click, i
                z4 += 1
            Next


            Dim Handlerlist_clear As New List(Of EventHandler)
            Dim Listviewlist_clear As New List(Of ListView)

            'Item aktivate
            For x = 0 To Handlerlist.Count - 1
                If Listviewlist(x).Name = Box.Name Then
                    Handlerlist_clear.Add(Handlerlist(x))
                    Listviewlist_clear.Add(Listviewlist(x))
                End If
            Next
            Dim zähler1 As Integer = 0
            For Each item In Handlerlist_clear
                RemoveHandler Box.ItemActivate, item
                Handlerlist.Remove(item)
                Listviewlist.Remove(Listviewlist_clear(zähler1))
                zähler1 += 1
            Next



            Box.Items.Clear()
            funktionpanel.Visible = False
        Loop While 6 = 8
        '################

        Dim l As New ImageList
        l.ImageSize = New System.Drawing.Size(60, 60)

        '############################## back

        If Not Path.Text = "/" Then
            l.Images.Add("image", My.Resources.Circled_Left_52px_1)
            Box.Items.Add("Zurück")
            Box.Items(0).ImageIndex = 0
            Dim addh As EventHandler = Sub(sender, e)
                                           For Each index In Box.SelectedIndices
                                               If index = 0 Then
                                                   Dim akttext As String = Path.Text
                                                   Dim toremove As Integer = get_back_string(Path.Text)
                                                   If akttext.Length = toremove Then
                                                       Path.Text = "/"
                                                   Else
                                                       Path.Text = akttext.Remove(akttext.Length - toremove, toremove)
                                                   End If
                                                   Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                               End If
                                           Next
                                       End Sub
            AddHandler Box.ItemActivate, addh
            Handlerlist.Add(addh)
            Listviewlist.Add(Box)

        End If
        '#######################
        'rest
        If Path.Text.Contains(".import") Then
            search_Texture.Show()
            Dim search_result As List(Of Array) = Await System.Threading.Tasks.Task.Run(Function() search_Texture.Get_texture_out_of_pack(Path.Text, grundpfad))
            search_Texture.Close()
            For Each item In search_result
                'Auflistung aller Items in anderen Pack mit selbem verzeichnis
                Dim bildpfad As String = item(0)
                If IO.Path.GetExtension(bildpfad) = (".png") Then
                    l.Images.Add("image", Get_icon_bild(bildpfad))
                ElseIf IO.Path.GetExtension(bildpfad) = (".ogg") Then
                    l.Images.Add("image", My.Resources.icons8_Music_40px)
                Else
                    l.Images.Add("image", infos.unbekannt_Ico)
                End If
                Debug.Print("add " & bildpfad)
                Box.Items.Add(IO.Path.GetFileName(item(0)))
                Dim aktindex_auswahl As Integer = Box.Items.Count - 1
                Box.Items(aktindex_auswahl).ImageIndex = aktindex_auswahl
                Dim addhint As EventHandler = Sub(sender, e)
                                                  For Each index In Box.SelectedIndices
                                                      If index = aktindex_auswahl Then


                                                          If MsgBox("Willst du wirklich '" & IO.Path.GetFileName(bildpfad) & "'in das Projekt impotieren?", MsgBoxStyle.Question + vbYesNo) = MsgBoxResult.Yes Then
                                                              Try
                                                                  IO.File.Copy(item(0), Ersteller.Wpfad & grundpfad & IO.Path.GetDirectoryName(Path.Text) & "/" & IO.Path.GetFileName(bildpfad), True)
                                                                  Dim akttext As String = Path.Text
                                                                  Dim toremove As Integer = get_back_string(Path.Text)
                                                                  If akttext.Length = toremove Then
                                                                      Path.Text = "/"
                                                                  Else
                                                                      Path.Text = akttext.Remove(akttext.Length - toremove, toremove)
                                                                  End If
                                                                  Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                              Catch ex As Exception
                                                                  MsgBox("Fehler beim impotieren des Bildes! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                                              End Try
                                                              '  Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige)
                                                          End If
                                                      End If
                                                  Next
                                              End Sub
                AddHandler Box.ItemActivate, addhint
                Handlerlist.Add(addhint)
                Listviewlist.Add(Box)
            Next
            Box.SmallImageList = l
        ElseIf Path.Text.Contains(".") Then


            'TPPackimporter
            Dim endung As String = IO.Path.GetExtension(Path.Text)
            l.Images.Add("image", My.Resources.icons8_Add_50px_1)
            Box.Items.Add("Weiteres Pack zur Auswahl impotieren")
            Dim aktindex As Integer = Box.Items.Count - 1
            Box.Items(aktindex).ImageIndex = aktindex
            Dim addh As EventHandler = Sub(sender, e)
                                           For Each index In Box.SelectedIndices
                                               If index = 1 Then
                                                   Version.Akt_Version = Ersteller.WVersion
                                                   If Not TPImporter.Get_Pack("Weiteres Pack zur Auswahl impotieren...") = MsgBoxResult.Ok Then Exit Sub
                                                   Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                               End If
                                           Next
                                       End Sub
            AddHandler Box.ItemActivate, addh
            Handlerlist.Add(addh)
            Listviewlist.Add(Box)
            'alle anderen impotierten packs
            search_Texture.Show()
            Dim searchversion As String = Ersteller.WVersion
            Dim search_result As List(Of Array) = Await System.Threading.Tasks.Task.Run(Function() search_Texture.Get_texture_out_of_imports(IO.Path.GetFileName(Path.Text), searchversion))
            search_Texture.Close()
            For Each item In search_result
                Dim bildpfad As String = item(0)
                If endung = (".png") Then
                    l.Images.Add("image", Get_icon_bild(bildpfad))
                ElseIf endung = (".ogg") Then
                    l.Images.Add("image", My.Resources.icons8_Music_40px)
                Else
                    l.Images.Add("image", My.Resources.File_64px)
                End If

                Box.Items.Add(IO.Path.GetFileName(Path.Text) & "(" & IO.Path.GetFileName(item(1)) & ")")
                Dim aktindex_auswahl As Integer = Box.Items.Count - 1
                Box.Items(aktindex_auswahl).ImageIndex = aktindex_auswahl
                Dim addhint As EventHandler = Sub(sender, e)
                                                  For Each index In Box.SelectedIndices
                                                      If index = aktindex_auswahl Then
                                                          If MsgBox("Willst du wirklich '" & IO.Path.GetFileName(Path.Text) & "' durch diese Textur aus dem Pack '" & IO.Path.GetFileName(item(1)) & "' ersetzen?", MsgBoxStyle.Question + vbYesNo) = MsgBoxResult.Yes Then
                                                              Try
                                                                  IO.File.Copy(item(0), Ersteller.Wpfad & grundpfad & Path.Text, True)
                                                                  Dim akttext As String = Path.Text
                                                                  Dim toremove As Integer = get_back_string(Path.Text)
                                                                  If akttext.Length = toremove Then
                                                                      Path.Text = "/"
                                                                  Else
                                                                      Path.Text = akttext.Remove(akttext.Length - toremove, toremove)
                                                                  End If
                                                                  Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                              Catch ex As Exception
                                                                  MsgBox("Fehler beim ersetzen des Bildes! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                                              End Try
                                                              '  Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige)
                                                          End If
                                                      End If
                                                  Next
                                              End Sub
                AddHandler Box.ItemActivate, addhint
                Handlerlist.Add(addhint)
                Listviewlist.Add(Box)
            Next
            Box.SmallImageList = l

        Else
            '########### file importer buttons
            Dim pack_importer_h As EventHandler = Sub()
                                                      Version.Akt_Version = Ersteller.WVersion
                                                      If Not TPImporter.Get_Pack("Wähle ein Pack aus...") = MsgBoxResult.Ok Then Exit Sub
                                                      Dim setzpack As String = TPImporter.Akt_Pack
                                                      If Path.Text = "/" Then
                                                          Path.Text += setzpack.Remove(0, infos.Pfad().Length).Replace("\", "~").Replace("/", "~") & ".import"
                                                      Else
                                                          Path.Text += "/" & setzpack.Remove(0, infos.Pfad().Length).Replace("\", "~").Replace("/", "~") & ".import"
                                                      End If
                                                      Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                  End Sub
            AddHandler Pack_importer.Click, pack_importer_h
            pack_importer_handler.Add(pack_importer_h)
            pack_importer_button.Add(Pack_importer)
            Dim importer_h As EventHandler = Sub()
                                                 Dim f As New OpenFileDialog()
                                                 f.Title = "wähle ein Datei zum impotieren..."
                                                 f.Filter = "Texturen|*.png|Sounds|*.ogg|Alle Dateien|*.*"
                                                 If Not f.ShowDialog = MsgBoxResult.Cancel Then
                                                     Dim to_import As String = f.FileName
                                                     Dim ziel As String = Ersteller.Wpfad & grundpfad & Path.Text & "\" & IO.Path.GetFileName(to_import)
                                                     If File.Exists(ziel) Then
                                                         If Not MsgBox("Die Datei exestiert bereits! Möchtest du sie ersetzen?", MsgBoxStyle.Information + vbYesNo) = MsgBoxResult.Yes Then Exit Sub
                                                     End If
                                                     Try
                                                         File.Copy(to_import, ziel, True)
                                                     Catch ex As Exception
                                                         MsgBox("Fehler beim impotieren! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                                                     End Try

                                                     Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                 End If

                                             End Sub
            AddHandler import.Click, importer_h
            importer_handler.Add(importer_h)
            importer_button.Add(import)
            '###################
            If Not My.Computer.FileSystem.DirectoryExists(Ersteller.Wpfad & grundpfad & Path.Text) Then
                MkDir(Ersteller.Wpfad & grundpfad & Path.Text)
            End If
            For Each item In get_items(grundpfad & Path.Text, suche)

                Dim aktname As String = IO.Path.GetFileNameWithoutExtension(item(0))
                Select Case item(1)
                    Case Nothing
                        '############## Ordner
                        Select Case IO.Path.GetFileName(item(0))
                            Case Else
                                Box.Items.Add(aktname)
                                l.Images.Add("image", My.Resources.Open_48px_1)
                                Dim aktindex As Integer = Box.Items.Count - 1
                                Box.Items(aktindex).ImageIndex = aktindex
                                Dim addh As EventHandler = Sub(sender, e)
                                                               For Each index In Box.SelectedIndices
                                                                   If index = aktindex Then
                                                                       If Path.Text = "/" Then
                                                                           Path.Text += aktname
                                                                       Else
                                                                           Path.Text += "/" & aktname
                                                                       End If
                                                                       Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                   End If
                                                               Next
                                                           End Sub
                                AddHandler Box.ItemActivate, addh
                                Handlerlist.Add(addh)
                                Listviewlist.Add(Box)
                        End Select
                    '#####################
                    Case ".png"
                        '############## Bilder


                        'Additem mit click event
                        Box.Items.Add(aktname)
                        Dim aktpfad As String = (Ersteller.Wpfad & grundpfad & Path.Text & "/" & aktname & ".png")
                        l.Images.Add("image", Get_icon_bild(aktpfad))
                        Dim aktindex As Integer = Box.Items.Count - 1
                        Box.Items(aktindex).ImageIndex = aktindex
                        Dim addh As EventHandler = Sub(sender, e)


                                                       For Each index In Box.SelectedIndices
                                                           If aktindex = index Then
                                                               'button handler remove
                                                               Dim Buttonandhandlerlist_clear As New List(Of EventHandler)
                                                               Dim Buttonlist_clear As New List(Of Button)
                                                               'Debug.Print(Buttonlist.Count)
                                                               For x = 0 To Buttonandhandlerlist.Count - 1
                                                                   If Buttonlist(x).Name = Edit_texture_button.Name Or Buttonlist(x).Name = ersetzen_button.Name Or Buttonlist(x).Name = ersetzen_ext_button.Name Or Buttonlist(x).Name = auflösung_button.Name Or Buttonlist(x).Name = entfernen_button.Name Or Buttonlist(x).Name = edit_ext_button.Name Then
                                                                       Buttonandhandlerlist_clear.Add(Buttonandhandlerlist(x))
                                                                       Buttonlist_clear.Add(Buttonlist(x))
                                                                       '  Debug.Print("zum removen hinzugefügt: " & Buttonlist(x).Name)
                                                                   End If
                                                               Next
                                                               Dim zähler2 As Integer = 0
                                                               For Each i In Buttonandhandlerlist_clear
                                                                   RemoveHandler Buttonlist_clear(zähler2).Click, i
                                                                   Buttonandhandlerlist.Remove(i)
                                                                   Buttonlist.Remove(Buttonlist_clear(zähler2))
                                                                   zähler2 += 1
                                                               Next
                                                               '  Debug.Print(Buttonlist.Count)

                                                               'Design
                                                               Edit_texture_button.Image = My.Resources.icons8_Edit_50px
                                                               Edit_texture_button.Text = "Textur Bearbeiten"
                                                               edit_ext_button.Image = My.Resources.icons8_Edit_Property_48px
                                                               edit_ext_button.Text = "Textur in anderem Programm  Bearbeiten"
                                                               ersetzen_button.Image = My.Resources.icons8_Replace_48px
                                                               ersetzen_button.Text = "Textur ersetzen mit anderem Pack"
                                                               ersetzen_ext_button.Image = My.Resources.icons8_Find_and_Replace_40px
                                                               ersetzen_ext_button.Text = "Ersetzen duch Textur auf dem Computer"
                                                               auflösung_button.Image = My.Resources.Image_50px
                                                               auflösung_button.Text = "Textur Auflösung " & vbNewLine & " ändern"
                                                               entfernen_button.Image = My.Resources.Delete_40px_1
                                                               entfernen_button.Text = "Textur aus dem Pack entfernen"
                                                               Edit_texture_button.Visible = True
                                                               edit_ext_button.Visible = True
                                                               ersetzen_button.Visible = True
                                                               ersetzen_ext_button.Visible = True
                                                               auflösung_button.Visible = True
                                                               entfernen_button.Visible = True

                                                               Dim Edit_Img As EventHandler = Nothing
                                                               Dim Edit_Img_ext As EventHandler = Nothing
                                                               Dim ersetzen As EventHandler = Nothing
                                                               Dim ersetzen_file As EventHandler = Nothing
                                                               Dim auflösung As EventHandler = Nothing
                                                               Dim entfernen As EventHandler = Nothing

                                                               'setze Handler mit Filenameunterscheidung

                                                               Select Case IO.Path.GetFileName(item(0))

                                                                   Case Else
                                                               End Select

                                                               If Edit_Img = Nothing Then Edit_Img = Sub() Process.Start(aktpfad)
                                                               If Edit_Img_ext = Nothing Then Edit_Img_ext = Sub() Open_bild_in_ext_programm(edit_ext_button, aktpfad)
                                                               If entfernen = Nothing Then entfernen = Sub()
                                                                                                           file_entfernen(aktpfad)
                                                                                                           Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                       End Sub
                                                               If ersetzen_file = Nothing Then ersetzen_file = Sub()
                                                                                                                   ersetzen_file_sub("Texturen|*.png", aktpfad)
                                                                                                                   Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                               End Sub
                                                               If auflösung = Nothing Then auflösung = Sub() MsgBox(aktpfad)
                                                               If ersetzen = Nothing Then ersetzen = Sub()
                                                                                                         If Path.Text = "/" Then
                                                                                                             Path.Text += aktname & ".png"
                                                                                                         Else
                                                                                                             Path.Text += "/" & aktname & ".png"
                                                                                                         End If
                                                                                                         Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                     End Sub



                                                               'clearaktbuttonhandler
                                                               Dim bindex As Integer = 0
                                                               For Each h As EventHandler In Buttonandhandlerlist
                                                                   RemoveHandler Buttonlist(bindex).Click, h
                                                                   bindex += 1
                                                               Next

                                                               'setnewhandler
                                                               'AddHandler Edit_texture_button.Click, Edit_Img
                                                               Buttonandhandlerlist.Add(Edit_Img)
                                                               Buttonlist.Add(Edit_texture_button)

                                                               'AddHandler edit_ext_button.Click, Edit_Img_ext
                                                               Buttonandhandlerlist.Add(Edit_Img_ext)
                                                               Buttonlist.Add(edit_ext_button)

                                                               'AddHandler ersetzen_button.Click, ersetzen
                                                               Buttonandhandlerlist.Add(ersetzen)
                                                               Buttonlist.Add(ersetzen_button)

                                                               'AddHandler ersetzen_ext_button.Click, ersetzen_file
                                                               Buttonandhandlerlist.Add(ersetzen_file)
                                                               Buttonlist.Add(ersetzen_ext_button)

                                                               'AddHandler auflösung_button.Click, auflösung
                                                               Buttonandhandlerlist.Add(auflösung)
                                                               Buttonlist.Add(auflösung_button)

                                                               'AddHandler entfernen_button.Click, entfernen
                                                               Buttonandhandlerlist.Add(entfernen)
                                                               Buttonlist.Add(entfernen_button)

                                                               For x = 0 To Buttonandhandlerlist.Count - 1
                                                                   AddHandler Buttonlist(x).Click, Buttonandhandlerlist(x)
                                                               Next

                                                               titel.Text = "Minecraft Texture: " & aktname
                                                               anzeige.Image = Get_anzeige_bild(aktpfad)
                                                               funktionpanel.Visible = True
                                                               anzeige.Visible = True
                                                           End If
                                                       Next
                                                   End Sub
                        AddHandler Box.ItemActivate, addh
                        Handlerlist.Add(addh)
                        Listviewlist.Add(Box)
                        '#####################
                    Case ".ogg"
                        '############## Sound


                        'Additem mit click event
                        Box.Items.Add(aktname)
                        Dim aktpfad As String = (Ersteller.Wpfad & grundpfad & Path.Text & "/" & aktname & ".ogg")
                        l.Images.Add("image", My.Resources.icons8_Music_40px)
                        Dim aktindex As Integer = Box.Items.Count - 1
                        Box.Items(aktindex).ImageIndex = aktindex
                        Dim addh As EventHandler = Sub(sender, e)


                                                       For Each index In Box.SelectedIndices
                                                           If aktindex = index Then
                                                               'button handler remove
                                                               Dim Buttonandhandlerlist_clear As New List(Of EventHandler)
                                                               Dim Buttonlist_clear As New List(Of Button)
                                                               'Debug.Print(Buttonlist.Count)
                                                               For x = 0 To Buttonandhandlerlist.Count - 1
                                                                   If Buttonlist(x).Name = Edit_texture_button.Name Or Buttonlist(x).Name = ersetzen_button.Name Or Buttonlist(x).Name = ersetzen_ext_button.Name Or Buttonlist(x).Name = auflösung_button.Name Or Buttonlist(x).Name = entfernen_button.Name Or Buttonlist(x).Name = edit_ext_button.Name Then
                                                                       Buttonandhandlerlist_clear.Add(Buttonandhandlerlist(x))
                                                                       Buttonlist_clear.Add(Buttonlist(x))
                                                                       '  Debug.Print("zum removen hinzugefügt: " & Buttonlist(x).Name)
                                                                   End If
                                                               Next
                                                               Dim zähler2 As Integer = 0
                                                               For Each i In Buttonandhandlerlist_clear
                                                                   RemoveHandler Buttonlist_clear(zähler2).Click, i
                                                                   Buttonandhandlerlist.Remove(i)
                                                                   Buttonlist.Remove(Buttonlist_clear(zähler2))
                                                                   zähler2 += 1
                                                               Next
                                                               'Design
                                                               Edit_texture_button.Image = My.Resources.icons8_Play_48px
                                                               Edit_texture_button.Text = "Sound Abspielen"
                                                               edit_ext_button.Image = My.Resources.icons8_Edit_Property_48px
                                                               edit_ext_button.Text = "Sound in anderem Programm  Bearbeiten"
                                                               ersetzen_button.Image = My.Resources.icons8_Replace_48px
                                                               ersetzen_button.Text = "Sound ersetzen mit anderem Pack"
                                                               ersetzen_ext_button.Image = My.Resources.icons8_Find_and_Replace_40px
                                                               ersetzen_ext_button.Text = "Ersetzen duch Sound auf dem Computer"
                                                               'auflösung_button.Image = My.Resources.Image_50px
                                                               'auflösung_button.Text = "Textur Auflösung " & vbNewLine & " ändern"
                                                               entfernen_button.Image = My.Resources.Delete_40px_1
                                                               entfernen_button.Text = "Sound aus dem Pack entfernen"
                                                               Edit_texture_button.Visible = True
                                                               edit_ext_button.Visible = True
                                                               ersetzen_button.Visible = True
                                                               ersetzen_ext_button.Visible = True
                                                               auflösung_button.Visible = False
                                                               entfernen_button.Visible = True

                                                               Dim Edit_Img As EventHandler = Nothing
                                                               Dim Edit_Img_ext As EventHandler = Nothing
                                                               Dim ersetzen As EventHandler = Nothing
                                                               Dim ersetzen_file As EventHandler = Nothing
                                                               Dim auflösung As EventHandler = Nothing
                                                               Dim entfernen As EventHandler = Nothing

                                                               'setze Handler mit Filenameunterscheidung

                                                               Select Case IO.Path.GetFileName(item(0))

                                                                   Case Else
                                                               End Select

                                                               If Edit_Img = Nothing Then Edit_Img = Sub() Soundplayer.Play_Sound(aktpfad)
                                                               If Edit_Img_ext = Nothing Then Edit_Img_ext = Sub() Open_ogg_in_ext_programm(edit_ext_button, aktpfad)
                                                               If entfernen = Nothing Then entfernen = Sub()
                                                                                                           file_entfernen(aktpfad)
                                                                                                           Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                       End Sub
                                                               If ersetzen_file = Nothing Then ersetzen_file = Sub()
                                                                                                                   ersetzen_file_sub("Sounds|*.ogg", aktpfad)
                                                                                                                   Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                               End Sub
                                                               If auflösung = Nothing Then auflösung = Sub() MsgBox(aktpfad)
                                                               If ersetzen = Nothing Then ersetzen = Sub()
                                                                                                         If Path.Text = "/" Then
                                                                                                             Path.Text += aktname & ".ogg"
                                                                                                         Else
                                                                                                             Path.Text += "/" & aktname & ".ogg"
                                                                                                         End If
                                                                                                         Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                     End Sub



                                                               'clearaktbuttonhandler
                                                               Dim bindex As Integer = 0
                                                               For Each h As EventHandler In Buttonandhandlerlist
                                                                   RemoveHandler Buttonlist(bindex).Click, h
                                                                   bindex += 1
                                                               Next

                                                               'setnewhandler
                                                               'AddHandler Edit_texture_button.Click, Edit_Img
                                                               Buttonandhandlerlist.Add(Edit_Img)
                                                               Buttonlist.Add(Edit_texture_button)

                                                               'AddHandler edit_ext_button.Click, Edit_Img_ext
                                                               Buttonandhandlerlist.Add(Edit_Img_ext)
                                                               Buttonlist.Add(edit_ext_button)

                                                               'AddHandler ersetzen_button.Click, ersetzen
                                                               Buttonandhandlerlist.Add(ersetzen)
                                                               Buttonlist.Add(ersetzen_button)

                                                               'AddHandler ersetzen_ext_button.Click, ersetzen_file
                                                               Buttonandhandlerlist.Add(ersetzen_file)
                                                               Buttonlist.Add(ersetzen_ext_button)

                                                               'AddHandler auflösung_button.Click, auflösung
                                                               Buttonandhandlerlist.Add(auflösung)
                                                               Buttonlist.Add(auflösung_button)

                                                               'AddHandler entfernen_button.Click, entfernen
                                                               Buttonandhandlerlist.Add(entfernen)
                                                               Buttonlist.Add(entfernen_button)

                                                               For x = 0 To Buttonandhandlerlist.Count - 1
                                                                   AddHandler Buttonlist(x).Click, Buttonandhandlerlist(x)
                                                               Next
                                                               anzeige.Visible = False
                                                               titel.Text = "Sound: " & aktname
                                                               funktionpanel.Visible = True

                                                           End If
                                                       Next
                                                   End Sub
                        AddHandler Box.ItemActivate, addh
                        Handlerlist.Add(addh)
                        Listviewlist.Add(Box)
                        '#####################
                    Case Else
                        '############## Sound


                        'Additem mit click event
                        Dim filename As String = IO.Path.GetFileName(item(0))
                        Box.Items.Add(filename)
                        Dim aktpfad As String = (Ersteller.Wpfad & grundpfad & Path.Text & "/" & filename)
                        l.Images.Add("image", My.Resources.File_64px)
                        Dim aktindex As Integer = Box.Items.Count - 1
                        Box.Items(aktindex).ImageIndex = aktindex
                        Dim addh As EventHandler = Sub(sender, e)


                                                       For Each index In Box.SelectedIndices
                                                           If aktindex = index Then
                                                               'button handler remove
                                                               Dim Buttonandhandlerlist_clear As New List(Of EventHandler)
                                                               Dim Buttonlist_clear As New List(Of Button)
                                                               'Debug.Print(Buttonlist.Count)
                                                               For x = 0 To Buttonandhandlerlist.Count - 1
                                                                   If Buttonlist(x).Name = Edit_texture_button.Name Or Buttonlist(x).Name = ersetzen_button.Name Or Buttonlist(x).Name = ersetzen_ext_button.Name Or Buttonlist(x).Name = auflösung_button.Name Or Buttonlist(x).Name = entfernen_button.Name Or Buttonlist(x).Name = edit_ext_button.Name Then
                                                                       Buttonandhandlerlist_clear.Add(Buttonandhandlerlist(x))
                                                                       Buttonlist_clear.Add(Buttonlist(x))
                                                                       '  Debug.Print("zum removen hinzugefügt: " & Buttonlist(x).Name)
                                                                   End If
                                                               Next
                                                               Dim zähler2 As Integer = 0
                                                               For Each i In Buttonandhandlerlist_clear
                                                                   RemoveHandler Buttonlist_clear(zähler2).Click, i
                                                                   Buttonandhandlerlist.Remove(i)
                                                                   Buttonlist.Remove(Buttonlist_clear(zähler2))
                                                                   zähler2 += 1
                                                               Next
                                                               'Design
                                                               Edit_texture_button.Image = My.Resources.Open_48px_1
                                                               Edit_texture_button.Text = "Datei öffnen"

                                                               ersetzen_button.Image = My.Resources.icons8_Replace_48px
                                                               ersetzen_button.Text = "Datei ersetzen mit anderem Pack"
                                                               ersetzen_ext_button.Image = My.Resources.icons8_Find_and_Replace_40px
                                                               ersetzen_ext_button.Text = "Ersetzen duch Datei auf dem Computer"
                                                               'auflösung_button.Image = My.Resources.Image_50px
                                                               'auflösung_button.Text = "Textur Auflösung " & vbNewLine & " ändern"
                                                               entfernen_button.Image = My.Resources.Delete_40px_1
                                                               entfernen_button.Text = "Datei aus dem Pack entfernen"
                                                               Edit_texture_button.Visible = True
                                                               edit_ext_button.Visible = False
                                                               ersetzen_button.Visible = True
                                                               ersetzen_ext_button.Visible = True
                                                               auflösung_button.Visible = False
                                                               entfernen_button.Visible = True

                                                               Dim Edit_Img As EventHandler = Nothing

                                                               Dim ersetzen As EventHandler = Nothing
                                                               Dim ersetzen_file As EventHandler = Nothing

                                                               Dim entfernen As EventHandler = Nothing

                                                               'setze Handler mit Filenameunterscheidung

                                                               Select Case IO.Path.GetFileName(item(0))

                                                                   Case Else
                                                               End Select

                                                               If Edit_Img = Nothing Then Edit_Img = Sub() Process.Start(aktpfad)

                                                               If entfernen = Nothing Then entfernen = Sub()
                                                                                                           file_entfernen(aktpfad)
                                                                                                           Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                       End Sub
                                                               If ersetzen_file = Nothing Then ersetzen_file = Sub()
                                                                                                                   ersetzen_file_sub("Datei|*" & IO.Path.GetExtension(aktpfad), aktpfad)
                                                                                                                   Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                               End Sub

                                                               If ersetzen = Nothing Then ersetzen = Sub()
                                                                                                         If Path.Text = "/" Then
                                                                                                             Path.Text += filename
                                                                                                         Else
                                                                                                             Path.Text += "/" & filename
                                                                                                         End If
                                                                                                         Show_Liste(Box, Path, grundpfad, funktionpanel, Edit_texture_button, ersetzen_button, titel, ersetzen_ext_button, edit_ext_button, auflösung_button, entfernen_button, anzeige, Pack_importer, import, suche)
                                                                                                     End Sub



                                                               'clearaktbuttonhandler
                                                               Dim bindex As Integer = 0
                                                               For Each h As EventHandler In Buttonandhandlerlist
                                                                   RemoveHandler Buttonlist(bindex).Click, h
                                                                   bindex += 1
                                                               Next

                                                               'setnewhandler
                                                               'AddHandler Edit_texture_button.Click, Edit_Img
                                                               Buttonandhandlerlist.Add(Edit_Img)
                                                               Buttonlist.Add(Edit_texture_button)



                                                               'AddHandler ersetzen_button.Click, ersetzen
                                                               Buttonandhandlerlist.Add(ersetzen)
                                                               Buttonlist.Add(ersetzen_button)

                                                               'AddHandler ersetzen_ext_button.Click, ersetzen_file
                                                               Buttonandhandlerlist.Add(ersetzen_file)
                                                               Buttonlist.Add(ersetzen_ext_button)


                                                               'AddHandler entfernen_button.Click, entfernen
                                                               Buttonandhandlerlist.Add(entfernen)
                                                               Buttonlist.Add(entfernen_button)

                                                               For x = 0 To Buttonandhandlerlist.Count - 1
                                                                   AddHandler Buttonlist(x).Click, Buttonandhandlerlist(x)
                                                               Next

                                                               titel.Text = "Datei: " & filename
                                                               anzeige.Visible = False
                                                               funktionpanel.Visible = True

                                                           End If
                                                       Next
                                                   End Sub
                        AddHandler Box.ItemActivate, addh
                        Handlerlist.Add(addh)
                        Listviewlist.Add(Box)
                        '#####################
                End Select
                Box.SmallImageList = l
            Next
        End If
    End Sub

    Private Sub ersetzen_file_sub(filter As String, datei As String)
        Dim f As New OpenFileDialog()
        f.Filter = filter
        f.Title = "Datei zum ersetzen wählen..."
        If Not f.ShowDialog = MsgBoxResult.Cancel Then
            Try
                IO.File.Copy(f.FileName, datei, True)
            Catch ex As Exception
                MsgBox("Fehler beim ersetzen der Datei! Fehler: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    Private Sub file_entfernen(file As String)
        If MsgBox("Möchtest du wirklich '" & IO.Path.GetFileName(file) & "' entfernen?", MsgBoxStyle.Question + vbYesNo) = MsgBoxResult.Yes Then
            Try
                IO.File.Delete(file)
            Catch ex As Exception
                MsgBox("Fehler beim löschen der Datei! Fehler: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    Private Sub Open_bild_in_ext_programm(senderb As Button, pfad As String)
        If My.Settings.Bildprogramm Is Nothing Then
            My.Settings.Bildprogramm = New Specialized.StringCollection
        End If

        Dim c As New ContextMenuStrip
        Dim item1 As New ToolStripMenuItem()
        item1.Text = "Programm hinzufügen..."
        item1.Image = My.Resources.Settings_48px_1
        AddHandler item1.Click, Sub()
                                    Dim f As New OpenFileDialog()
                                    f.Filter = "Programm|*.exe"
                                    f.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                                    If Not f.ShowDialog = DialogResult.Cancel Then
                                        My.Settings.Bildprogramm.Add(f.FileName)
                                    End If
                                End Sub
        c.Items.Add(item1)
        For Each item In My.Settings.Bildprogramm
            Dim additem As New ToolStripMenuItem()
            additem.Text = "Öffnen mit " & IO.Path.GetFileNameWithoutExtension(item)
            additem.Image = get_programm_icon(item)
            AddHandler additem.Click, Sub()
                                          Dim startInfo As New ProcessStartInfo(item)
                                          startInfo.WindowStyle = ProcessWindowStyle.Normal
                                          startInfo.Arguments = pfad
                                          Process.Start(startInfo)
                                      End Sub
            c.Items.Add(additem)
        Next
        c.Show(senderb, New Point(200, 74))
    End Sub
    Private Sub Open_ogg_in_ext_programm(senderb As Button, pfad As String)
        If My.Settings.Tonprogramme Is Nothing Then
            My.Settings.Tonprogramme = New Specialized.StringCollection
        End If

        Dim c As New ContextMenuStrip
        Dim item1 As New ToolStripMenuItem()
        item1.Text = "Programm hinzufügen..."
        item1.Image = My.Resources.Settings_48px_1
        AddHandler item1.Click, Sub()
                                    Dim f As New OpenFileDialog()
                                    f.Filter = "Programm|*.exe"
                                    f.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                                    If Not f.ShowDialog = DialogResult.Cancel Then
                                        My.Settings.Tonprogramme.Add(f.FileName)
                                    End If
                                End Sub
        c.Items.Add(item1)
        For Each item In My.Settings.Tonprogramme
            Dim additem As New ToolStripMenuItem()
            additem.Text = "Öffnen mit " & IO.Path.GetFileNameWithoutExtension(item)
            additem.Image = get_programm_icon(item)
            AddHandler additem.Click, Sub()
                                          Dim startInfo As New ProcessStartInfo(item)
                                          startInfo.WindowStyle = ProcessWindowStyle.Normal
                                          startInfo.Arguments = pfad.Replace("/", "\")
                                          Process.Start(startInfo)
                                      End Sub
            c.Items.Add(additem)
        Next
        c.Show(senderb, New Point(200, 74))
    End Sub
    Private Function get_programm_icon(pfad As String) As Image
        Try
            Dim to_return As Image = Icon.ExtractAssociatedIcon(pfad).ToBitmap()
            Return to_return
        Catch ex As Exception
            Return My.Resources.Computer_40px
        End Try
    End Function

    Private Function get_items(addpfad As String, suche As TextBox) As List(Of Array)
        If Not My.Computer.FileSystem.DirectoryExists(Ersteller.Wpfad & addpfad) Then
            Return Nothing
        End If
        Dim l As New List(Of Array)
        For Each item In (IO.Directory.GetDirectories(Ersteller.Wpfad & addpfad))
            If suche.Text = infos.such_text Then
                l.Add(New String() {item, Nothing})
            Else
                If IO.Path.GetFileName(item).Contains(suche.Text) Then
                    l.Add(New String() {item, Nothing})
                End If
            End If

        Next
        For Each Datei As String In My.Computer.FileSystem.GetFiles(Ersteller.Wpfad & addpfad, FileIO.SearchOption.SearchTopLevelOnly)
            If suche.Text = infos.such_text Then
                l.Add(New String() {(Datei), IO.Path.GetExtension(Datei)})
            Else
                If IO.Path.GetFileName(Datei).Contains(suche.Text) Then
                    l.Add(New String() {(Datei), IO.Path.GetExtension(Datei)})
                End If
            End If

        Next
        Return l
    End Function
End Module
