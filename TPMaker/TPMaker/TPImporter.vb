Imports System.IO
Public Class TPImporter
    Private Sub TPImporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListDesigner.Ini_View_small(ListView1)
        If Import_pfad.Text = "/" Then Standart_auflistung() Else Change_Anzeige()
    End Sub
    Private Sub add_to_favorit(pfad As String)
        If My.Settings.Fav Is Nothing Then
            My.Settings.Fav = New Specialized.StringCollection
        End If
        Dim inp As String = pfad.Remove(0, infos.Pfad().Length)
        If Not My.Settings.Fav.Contains(inp) Then
            My.Settings.Fav.Add(inp)
            MsgBox(inp & " wurde zur Favoriten Liste hinzugefügt", MsgBoxStyle.Information)
        Else
            My.Settings.Fav.Remove(inp)
            MsgBox(inp & " wurde von der Favoriten Liste gelöscht", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub Edit_favorites(alt As String, neu As String)
        If My.Settings.Fav Is Nothing Then
            My.Settings.Fav = New Specialized.StringCollection
        End If
        For Each item In My.Settings.Fav
            If item = alt Then
                My.Settings.Fav.Remove(alt)
                If Not neu = Nothing Then My.Settings.Fav.Add(neu)
                Exit For
            End If
        Next
    End Sub
    Private Sub Standart_auflistung()
        ListView1.Items.Clear()
        INI_Bild(ListView1)
        ListView1.Items.Add(cat(0).ToString)
        ListView1.Items(0).ImageIndex = 0
        ListView1.Items.Add(cat(1).ToString)
        ListView1.Items(1).ImageIndex = 1
        ListView1.Items.Add(cat(2).ToString)
        ListView1.Items(2).ImageIndex = 2
        ListView1.Items.Add(cat(3).ToString)
        ListView1.Items(3).ImageIndex = 3
        ListView1.Items.Add(cat(4).ToString)
        ListView1.Items(4).ImageIndex = 4
    End Sub
    Public cat() = New String() {"Bereits Hinzugefügte Packs", "Empfohlene Packs von Server", "Pack aus den Favoriten", "Pack von Computer wählen", "Standart Minecraft Texturen"}
    Private Function INI_Bild(target As ListView)
        Dim l As New ImageList
        l.ImageSize = New System.Drawing.Size(30, 30)
        l.Images.Add("image", My.Resources.Computer_40px)
        l.Images.Add("image", My.Resources.Server_52px)
        l.Images.Add("image", My.Resources.icons8_Star_Filled_48px)
        l.Images.Add("image", My.Resources.Download_48px)
        l.Images.Add("image", My.Resources.images)
        ListView1.SmallImageList = l
    End Function
    Public Akt_Pack As String
    Public Function Get_Pack(Txt As String) As DialogResult
        Me.Text = Txt
        Import_pfad.Text = "/"
        Return Me.ShowDialog()

    End Function
    Public Function Import(Txt As String) As DialogResult
        Me.Text = Txt
        Import_pfad.Text = "/" & cat(3)
        Return Me.ShowDialog()

    End Function
    Private Sub Impotieren()
        ListView1.Items.Clear()
        Dim l As New ImageList
        l.ImageSize = New System.Drawing.Size(30, 30)
        'back
        l.Images.Add("image", My.Resources.Circled_Left_52px_1)
        ListView1.Items.Add("Zurück")
        ListView1.Items(0).ImageIndex = 0
        'rest
        Dim zähler As Integer = 1
        l.Images.Add("image", My.Resources.ZIP_48px_1)
        l.Images.Add("image", My.Resources.Open_48px_1)
        For Each item In New String() {"Texture Pack aus zip Datei", "Ordner"}
            ListView1.Items.Add(item)
            ListView1.Items(zähler).ImageIndex = zähler
            zähler += 1
        Next
        ListView1.SmallImageList = l
    End Sub
    Public Function Get_icon(pfad As String) As Image
        If File.Exists(pfad & "\pack.png") Then
            Return MyFunctions.get_image(pfad & "\pack.png")
        Else
            Return infos.Standart_Pack_Ico
        End If
    End Function
    Private Sub Create_Einträge(Items As Array, pfad As String)
        ListView1.Items.Clear()
        Dim l As New ImageList
        l.ImageSize = New System.Drawing.Size(30, 30)
        'back
        l.Images.Add("image", My.Resources.Circled_Left_52px_1)
        ListView1.Items.Add("Zurück")
        ListView1.Items(0).ImageIndex = 0
        'rest
        Dim zähler As Integer = 1
        For Each item In Items
            Dim s_pfad As String = (item)
            ListView1.Items.Add(s_pfad)

            l.Images.Add("image", Get_icon(pfad & "\" & item))

            ListView1.Items(zähler).ImageIndex = zähler
            zähler += 1
        Next
        ListView1.SmallImageList = l
    End Sub

    Private Sub Change_Anzeige()
        Select Case Import_pfad.Text.ToString.Remove(0, 1)
            Case Nothing
                Standart_auflistung()
            Case cat(0).ToString
                'Impotieren
                Debug.Print((infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version))
                If Directory.Exists(infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version) Then
                    Dim liste As New List(Of String)
                    For Each item In (IO.Directory.GetDirectories(infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version))
                        Debug.Print(String.IsNullOrEmpty(get_suche(search)))
                        If String.IsNullOrEmpty(get_suche(search)) = False Then
                            If IO.Path.GetFileName(item).Contains(get_suche(search)) Then
                                liste.Add(IO.Path.GetFileName(item))
                            End If
                        Else
                            liste.Add(IO.Path.GetFileName(item))
                        End If

                    Next
                    Create_Einträge(liste.ToArray, infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version)

                Else
                    Import_pfad.Text = "/"
                    MsgBox("Keine Packs vorhanden!", MsgBoxStyle.Information)
                End If

            Case cat(1).ToString
                'Server
                Dim response As List(Of String) = Server_Handler.Get_Packs(Version.Akt_Version, get_suche(search))
                If response.Count = 0 Then
                    Import_pfad.Text = "/"
                    MsgBox("Keine Packs vorhanden!", MsgBoxStyle.Information)
                Else
                    Create_Einträge(response.ToArray, "")
                End If
            Case cat(2).ToString
                'Favoriten
                Dim fav_array As New List(Of String)
                If My.Settings.Fav Is Nothing Then
                    My.Settings.Fav = New Specialized.StringCollection
                End If
                For Each i In My.Settings.Fav
                    If Directory.Exists(infos.Pfad() & i) Then
                        If String.IsNullOrEmpty(get_suche(search)) = False Then
                            If i.Contains(get_suche(search)) Then
                                fav_array.Add(i)
                            End If
                        Else
                            fav_array.Add(i)
                        End If
                    End If
                Next
                If Not fav_array.Count = 0 Then
                    Create_Einträge(fav_array.ToArray, infos.Pfad())

                Else
                    MsgBox("Keine Favoriten vorhanden!", MsgBoxStyle.Information)
                    Import_pfad.Text = "/"
                End If
            Case cat(3).ToString
                'Impotieren
                Impotieren()
            Case cat(4).ToString
                'Standart Minecraft
                If Directory.Exists(infos.Pfad() & infos.Pfad_Standart & "/" & Version.Akt_Version) Then
                    Dim liste As New List(Of String)
                    For Each item In (IO.Directory.GetDirectories(infos.Pfad() & infos.Pfad_Standart & "/" & Version.Akt_Version))
                        If String.IsNullOrEmpty(get_suche(search)) = False Then
                            If IO.Path.GetFileName(item).Contains(get_suche(search)) Then
                                liste.Add(IO.Path.GetFileName(item))
                            End If
                        Else
                            liste.Add(IO.Path.GetFileName(item))
                        End If
                    Next
                    Create_Einträge(liste.ToArray, infos.Pfad() & infos.Pfad_Standart & Version.Akt_Version)
                Else
                    Import_pfad.Text = "/"
                    MsgBox("Keine Packs vorhanden!", MsgBoxStyle.Information)
                End If

        End Select
    End Sub

    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        '################ Ausführen
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If ListView1.GetItemAt(e.X, e.Y) IsNot Nothing Then
                ListView1.GetItemAt(e.X, e.Y).Selected = True
                Dim aktname As String = (ListView1.GetItemAt(e.X, e.Y).Text)
                Dim Index As Integer = ListView1.GetItemAt(e.X, e.Y).Index
                If Import_pfad.Text = "/" Then
                    Import_pfad.Text = "/" & cat(Index)
                    Change_Anzeige()
                Else
                    If Index = 0 Then
                        Import_pfad.Text = "/"
                        Change_Anzeige()
                    Else
                        Select Case Import_pfad.Text.ToString.Remove(0, 1)

                            Case cat(0).ToString
                                'Importet
                                Akt_Pack = infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version & "/" & aktname
                                Me.DialogResult = DialogResult.OK
                                Me.Close()
                            Case cat(1).ToString
                                'server
                                Downloader.Run(aktname & ".zip", Version.Akt_Version, infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version & "/" & aktname)
                            Case cat(2).ToString
                                'favorieten
                                Akt_Pack = infos.Pfad() & aktname
                                Me.DialogResult = DialogResult.OK
                                Me.Close()
                            Case cat(3).ToString
                                'Import
                                Select Case Index
                                    Case 1
                                        'zip
                                        Import_zip()
                                    Case 2
                                        'Ornder
                                        Import_Ordner()
                                    Case cat(4).ToString
                                End Select
                            Case cat(4).ToString
                                'Standart Minecraft
                                Akt_Pack = infos.Pfad() & infos.Pfad_Standart & Version.Akt_Version & "/" & aktname
                                Me.DialogResult = DialogResult.OK
                                Me.Close()
                        End Select
                    End If
                End If
            End If
        End If
        '########## Context
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If ListView1.GetItemAt(e.X, e.Y) IsNot Nothing Then
                ListView1.GetItemAt(e.X, e.Y).Selected = True
                Dim aktname As String = (ListView1.GetItemAt(e.X, e.Y).Text)
                Dim Index As Integer = ListView1.GetItemAt(e.X, e.Y).Index
                If Import_pfad.Text = "/" Then

                Else
                    If Index = 0 Then

                    Else
                        Select Case Import_pfad.Text.ToString.Remove(0, 1)

                            Case cat(0).ToString
                                'Importet
                                Dim s As String = infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version & "/" & aktname
                                Importet_Context(New Point(e.X, e.Y), s)
                            Case cat(1).ToString
                                'server

                            Case cat(2).ToString
                                'favorieten
                                Fav_context(New Point(e.X, e.Y), aktname)
                            Case cat(3).ToString
                                'Import
                                Select Case Index
                                    Case 1
                                        'zip

                                    Case 2
                                        'Ornder

                                    Case cat(4).ToString
                                End Select
                            Case cat(4).ToString
                                'Standart Minecraft

                        End Select
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub Pfad_context()
        Dim c As New ContextMenuStrip
        Dim counter As Integer = 0
        For Each item In cat
            Dim item1 As New ToolStripMenuItem
            item1.Text = cat(counter)
            item1.Name = "Item" & counter
            item1.Image = My.Resources.Open_48px_1

            c.Items.Add(item1)
            counter += 1
        Next
        AddHandler c.ItemClicked, Sub(sender, e)
                                      Dim akt As String = (e.ClickedItem.Text)
                                      For Each i In cat
                                          If i = akt Then
                                              Import_pfad.Text = "/" & i
                                              Change_Anzeige()
                                          End If
                                      Next
                                  End Sub
        c.Show(Import_pfad, Import_pfad.Location)

    End Sub
    Private Sub Fav_context(Punkt As Point, Item As String)
        Dim c As New ContextMenuStrip


        Dim item1 As New ToolStripMenuItem
        item1.Text = "Aus Favoriten löschen"
        item1.Name = "Item"
        item1.Image = My.Resources.Delete_40px_1
        AddHandler item1.Click, Sub(sender, e)
                                    My.Settings.Fav.Remove(Item)
                                    Change_Anzeige()
                                End Sub
        c.Items.Add(item1)

        c.Show(ListView1, Punkt)

    End Sub
    Private Sub Importet_Context(Punkt As Point, pfad As String)
        Dim c As New ContextMenuStrip
        Dim item1 As New ToolStripMenuItem
        item1.Text = "Pack Löschen"
        item1.Image = My.Resources.Delete_40px_1
        AddHandler item1.Click, Sub()
                                    If MsgBox("Willst du das Pack wirklich löschen? ", MsgBoxStyle.Question + vbYesNo) = MsgBoxResult.Yes Then
                                        My.Computer.FileSystem.DeleteDirectory(pfad, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                                        Edit_favorites(pfad.Remove(0, infos.Pfad().Length), Nothing)
                                        Change_Anzeige()
                                    End If

                                End Sub
        c.Items.Add(item1)

        Dim item2 As New ToolStripMenuItem
        item2.Text = "Pack umbennenen"
        item2.Image = My.Resources.Rename_48px
        AddHandler item2.Click, Sub()
                                    Dim neu As String = InputBox("Neuer Name:", "Pack umbennenen", IO.Path.GetFileName(pfad))
                                    If Not neu = Nothing Then
                                        Try
                                            My.Computer.FileSystem.RenameDirectory(pfad, neu)
                                        Catch ex As Exception
                                            MsgBox("Fehler beim umbennenen. Fehler:  " & ex.Message, MsgBoxStyle.Exclamation)
                                        End Try

                                    End If
                                    Edit_favorites(pfad.Remove(0, infos.Pfad().Length), infos.Pfad_Impotiert & Version.Akt_Version & "\" & neu)
                                    Change_Anzeige()


                                End Sub
        c.Items.Add(item2)

        Dim item3 As New ToolStripMenuItem
        item3.Text = "Pack zu Favoriten hinzufügen/löschen"
        item3.Image = My.Resources.icons8_Star_Filled_48px
        AddHandler item3.Click, Sub()
                                    If MsgBox("Willst du das Pack wirklich in die Favoriten aufnehmen? ", MsgBoxStyle.Question + vbYesNo) = MsgBoxResult.Yes Then
                                        add_to_favorit(pfad)
                                    End If




                                End Sub
        c.Items.Add(item3)
        c.Show(ListView1, Punkt)
    End Sub
    Private Sub Import_zip()
        Dim f As New OpenFileDialog
        f.Filter = "Zip Archiv|*.zip"
        If f.ShowDialog() = MsgBoxResult.Cancel Then Exit Sub

        ZIP.Extract_pack(f.FileName)
    End Sub
    Private Sub Import_Ordner()
        Dim i As New FolderBrowserDialog
        i.Description = "Wähle einen Texturenpack Ordner aus"
        If Not i.ShowDialog() = DialogResult.Cancel Then
            Dim Ordner As String = i.SelectedPath
            Dim Ziel As String = infos.Pfad() & infos.Pfad_Impotiert & Version.Akt_Version & "/" & IO.Path.GetFileName(i.SelectedPath)
            Dim Import As Boolean = True
            If Directory.Exists(Ziel) Then
                If MsgBox("Dieses Pack wurde schon impotiert! Willst du es überschreiben?", MsgBoxStyle.Question) = MsgBoxResult.No Then
                    Import = False
                End If
            End If
            If MyFunctions.is_Tp_Pack(Ordner) = False Then
                MsgBox("Der Ordner kann nicht als Texturepack gelesen werden!", MsgBoxStyle.Exclamation)
                Import = False
            End If
            If Import = True Then
                Try
                    My.Computer.FileSystem.CopyDirectory(Ordner, Ziel, True)
                    MsgBox("Pack wurde Impotiert!  Du findest es in der Liste " & cat(0), MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Fehler beim Kopieren des Ordners! Fehler: " & ex.Message, MsgBoxStyle.Critical)
                End Try

            End If
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles search.TextChanged

    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles search.LostFocus
        If search.Text = "" Then
            search.Text = "Suche..."
        End If
    End Sub

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        search.Text = ""
    End Sub

    Private Sub search_MouseDown(sender As Object, e As MouseEventArgs) Handles search.MouseDown

    End Sub

    Private Sub search_KeyDown(sender As Object, e As KeyEventArgs) Handles search.KeyDown
        If e.KeyCode = Keys.Enter Then
            Change_Anzeige()
        End If
    End Sub

    Private Sub Import_pfad_Click(sender As Object, e As EventArgs) Handles Import_pfad.Click
        Pfad_context()
    End Sub
End Class