Imports System.ComponentModel

Public Class Projekte
    Private Sub Projekte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListDesigner.Ini_View_small(ListView1)

    End Sub
    Public Projekt As String
    Public P_Version As String
    Dim loadhandler As EventHandler
    Public Function Get_Projekt() As DialogResult
        Dim h As EventHandler = Sub() Change_Anzeige()
        AddHandler Me.Shown, h
        loadhandler = h
        Return Me.ShowDialog
    End Function
    Private Function INI_Bild(target As ListView)
        Dim l As New ImageList
        l.ImageSize = New System.Drawing.Size(30, 30)
        l.Images.Add("image", My.Resources.Settings_48px_1)
        ListView1.SmallImageList = l
    End Function

    Private Sub Show_versions()
        ListView1.Items.Clear()
        INI_Bild(ListView1)
        For Each item In (IO.Directory.GetDirectories(infos.Pfad() & infos.Pfad_Projekte))
            If String.IsNullOrEmpty(get_suche(search)) = False Then
                If IO.Path.GetFileName(item).Contains(get_suche(search)) Then
                    ListView1.Items.Add(IO.Path.GetFileName(item))
                    ListView1.Items(ListView1.Items.Count - 1).ImageIndex = 0
                End If
            Else
                ListView1.Items.Add(IO.Path.GetFileName(item))
                ListView1.Items(ListView1.Items.Count - 1).ImageIndex = 0
            End If

        Next
    End Sub
    Private Sub Show_Projekts(version As String)
        ListView1.Items.Clear()
        Dim l As New List(Of String)
        For Each item In (IO.Directory.GetDirectories(infos.Pfad() & infos.Pfad_Projekte & version))
            If String.IsNullOrEmpty(get_suche(search)) = False Then
                If IO.Path.GetFileName(item).Contains(get_suche(search)) Then
                    l.Add(IO.Path.GetFileName(item))

                End If
            Else
                l.Add(IO.Path.GetFileName(item))

            End If

        Next
        If Not l.Count = 0 Then Create_Einträge(l.ToArray, infos.Pfad() & infos.Pfad_Projekte & version)
    End Sub
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

            l.Images.Add("image", TPImporter.Get_icon(pfad & "\" & item))

            ListView1.Items(zähler).ImageIndex = zähler
            zähler += 1
        Next
        ListView1.SmallImageList = l
    End Sub

    Private Sub Projekte_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        RemoveHandler Me.Shown, loadhandler
    End Sub

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        search.Text = ""
    End Sub

    Private Sub search_LostFocus(sender As Object, e As EventArgs) Handles search.LostFocus
        If search.Text = "" Then
            search.Text = "Suche..."
        End If
    End Sub
    Private Sub Change_Anzeige()
        Select Case Import_pfad.Text
            Case "/"
                Show_versions()
            Case Else
                Show_Projekts(Import_pfad.Text.Remove(0, 1))
        End Select
    End Sub

    Private Sub search_KeyDown(sender As Object, e As KeyEventArgs) Handles search.KeyDown
        If e.KeyCode = Keys.Enter Then
            Change_Anzeige()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ListView1_ItemActivate(sender As Object, e As EventArgs) Handles ListView1.ItemActivate

    End Sub
    Private Sub Context(Punkt As Point, pfad As String)
        Dim c As New ContextMenuStrip
        Dim item1 As New ToolStripMenuItem
        item1.Text = "Projekt Löschen"
        item1.Image = My.Resources.Delete_40px_1
        AddHandler item1.Click, Sub()
                                    If MsgBox("Willst du das Projekt wirklich löschen? ", MsgBoxStyle.Question + vbYesNo) = MsgBoxResult.Yes Then
                                        My.Computer.FileSystem.DeleteDirectory(pfad, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                                        Change_Anzeige()
                                    End If

                                End Sub
        c.Items.Add(item1)

        Dim item2 As New ToolStripMenuItem
        item2.Text = "Projekt umbennenen"
        item2.Image = My.Resources.Rename_48px
        AddHandler item2.Click, Sub()
                                    Dim neu As String = InputBox("Neuer Name:", "Projekt umbennenen", IO.Path.GetFileName(pfad))
                                    If Not neu = Nothing Then
                                        Try
                                            My.Computer.FileSystem.RenameDirectory(pfad, neu)
                                        Catch ex As Exception
                                            MsgBox("Fehler beim umbennenen. Fehler:  " & ex.Message, MsgBoxStyle.Exclamation)
                                        End Try

                                    End If
                                    Change_Anzeige()


                                End Sub
        c.Items.Add(item2)

        c.Show(ListView1, Punkt)
    End Sub
    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If ListView1.GetItemAt(e.X, e.Y) IsNot Nothing Then
                ListView1.GetItemAt(e.X, e.Y).Selected = True
                Dim aktname As String = (ListView1.GetItemAt(e.X, e.Y).Text)
                Dim Index As Integer = ListView1.GetItemAt(e.X, e.Y).Index
                If Import_pfad.Text = "/" Then
                    Import_pfad.Text = "/" & aktname
                    Change_Anzeige()
                Else
                    If Index = 0 Then
                        Import_pfad.Text = "/"
                        Change_Anzeige()
                    Else
                        Projekt = Import_pfad.Text & "/" & aktname
                        P_Version = Import_pfad.Text.Remove(0, 1)
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    End If
                End If
            End If
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            If ListView1.GetItemAt(e.X, e.Y) IsNot Nothing Then
                ListView1.GetItemAt(e.X, e.Y).Selected = True
                Dim aktname As String = (ListView1.GetItemAt(e.X, e.Y).Text)
                Dim Index As Integer = ListView1.GetItemAt(e.X, e.Y).Index
                If Import_pfad.Text = "/" Then

                Else
                    If Index = 0 Then

                    Else
                        Context(New Point(e.X, e.Y), infos.Pfad() & infos.Pfad_Projekte & Import_pfad.Text & "/" & aktname)
                    End If
                End If
            End If
        End If
    End Sub
End Class