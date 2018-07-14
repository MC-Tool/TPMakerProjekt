Public Class Version
    Private Sub Version_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListDesigner.Ini_View_small(ListView1)

        INI_Bild(ListView1)
        Dim zähler As Integer = 0
        For Each item In infos.versionen
            ListView1.Items.Add(item)
            ListView1.Items(zähler).ImageIndex = 0

            zähler += 1
        Next

    End Sub

    Private Sub Context(item As String, p As Point)
        Dim c As New ContextMenuStrip
        Dim item1 As New ToolStripMenuItem
        item1.Text = "Kompatible Versionen bekommen"
        item1.Name = "item1"
        item1.Image = My.Resources.Settings_48px_1
        c.Items.Add(item1)
        AddHandler item1.Click, Sub() MsgBox(Supportet_versions(item))
        c.Show(ListView1, p)
    End Sub
    Public Function Get_Version() As DialogResult
        Return Me.ShowDialog()
    End Function
    Private Function Supportet_versions(version As String) As String
        Dim to_return As String
        For x = 1 To 10
            to_return += version & "." & x & "  ;"
        Next
        Return to_return
    End Function
    Private Function INI_Bild(target As ListView)
        Dim l As New ImageList
        l.ImageSize = New System.Drawing.Size(30, 30)
        l.Images.Add("image", My.Resources.Settings_48px_1)
        target.SmallImageList = l
    End Function
    Public Akt_Version As String

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If ListView1.GetItemAt(e.X, e.Y) IsNot Nothing Then
                ListView1.GetItemAt(e.X, e.Y).Selected = True
                Dim aktname As String = (ListView1.GetItemAt(e.X, e.Y).Text)
                Context(aktname, New Point(e.X, e.Y))
            End If
        End If
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If ListView1.GetItemAt(e.X, e.Y) IsNot Nothing Then
                ListView1.GetItemAt(e.X, e.Y).Selected = True
                Dim aktname As String = (ListView1.GetItemAt(e.X, e.Y).Text)
                Akt_Version = aktname
                Me.Close()
                Me.DialogResult = DialogResult.OK
            End If
        End If
    End Sub
End Class