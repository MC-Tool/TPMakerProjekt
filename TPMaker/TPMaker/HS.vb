Public Class Hs

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub



    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel1_MouseEnter(sender As Object, e As EventArgs) Handles Panel1.MouseEnter
        Panel1.BackgroundImage = My.Resources.NeuErstellen2
        Panel1.BorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Panel1_MouseLeave(sender As Object, e As EventArgs) Handles Panel1.MouseLeave
        Panel1.BackgroundImage = My.Resources.NeuErstellen1
        Panel1.BorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Panel2_MouseEnter(sender As Object, e As EventArgs) Handles Panel2.MouseEnter
        sender.BackgroundImage = My.Resources.impotieren2
        sender.BorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Panel2_MouseLeave(sender As Object, e As EventArgs) Handles Panel2.MouseLeave
        sender.BackgroundImage = My.Resources.impotieren1
        sender.BorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Panel3_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Panel3_MouseEnter(sender As Object, e As EventArgs) Handles Panel3.MouseEnter
        sender.BackgroundImage = My.Resources.projektladen2
        sender.BorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Panel3_MouseLeave(sender As Object, e As EventArgs) Handles Panel3.MouseLeave
        sender.BackgroundImage = My.Resources.projektladen1
        sender.BorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Hs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Panel4_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel4.MouseDown

    End Sub

    Private Sub Panel4_MouseEnter(sender As Object, e As EventArgs) Handles Panel4.MouseEnter
        sender.BackgroundImage = My.Resources.veröffentlichen2
        sender.BorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Panel4_MouseLeave(sender As Object, e As EventArgs) Handles Panel4.MouseLeave
        sender.BackgroundImage = My.Resources.veröffentlichen1
        sender.BorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Panel5_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel5.MouseDown

    End Sub

    Private Sub Panel5_MouseEnter(sender As Object, e As EventArgs) Handles Panel5.MouseEnter
        sender.BackgroundImage = My.Resources.favorieten2
        sender.BorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Panel5_MouseLeave(sender As Object, e As EventArgs) Handles Panel5.MouseLeave
        sender.BackgroundImage = My.Resources.favorieten1
        sender.BorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Panel6_MouseEnter(sender As Object, e As EventArgs) Handles Panel6.MouseEnter
        sender.BackgroundImage = My.Resources.support2
        sender.BorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub Panel6_MouseLeave(sender As Object, e As EventArgs) Handles Panel6.MouseLeave
        sender.BackgroundImage = My.Resources.support1
        sender.BorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        If Not Version.Get_Version() = MsgBoxResult.Ok Then Exit Sub
        If Not TPImporter.Get_Pack("Wählen ein Pack Haupt-Texturepack aus...") = MsgBoxResult.Ok Then Exit Sub

        Dim ans As String
        Do
            ans = InputBox("Name des Projektes")
            If ans = Nothing Then Exit Sub
            If IO.Directory.Exists(infos.Pfad() & infos.Pfad_Projekte & Version.Akt_Version & "/" & ans) Then MsgBox("Ein Projekt mit diesem Namen gibt es bereits", MsgBoxStyle.Exclamation)
        Loop While IO.Directory.Exists(infos.Pfad() & infos.Pfad_Projekte & Version.Akt_Version & "/" & ans)

        Try
            My.Computer.FileSystem.CopyDirectory(TPImporter.Akt_Pack, infos.Pfad() & infos.Pfad_Projekte & Version.Akt_Version & "/" & ans, True)
            Ersteller.Wpfad = infos.Pfad() & infos.Pfad_Projekte & Version.Akt_Version & "/" & ans
            Ersteller.WVersion = Version.Akt_Version
            Me.Hide()
            Ersteller.Show()
        Catch ex As Exception
            MsgBox("Fehler beim erstellen des Projektes! Fehler: " & ex.Message, MsgBoxStyle.Critical)
        End Try


    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        If Not Version.Get_Version() = MsgBoxResult.Ok Then Exit Sub
        TPImporter.Import("Texturpack impotieren...")
    End Sub

    Private Sub Panel3_Click(sender As Object, e As EventArgs) Handles Panel3.Click
        If Projekte.Get_Projekt() = DialogResult.OK Then

            Ersteller.Wpfad = infos.Pfad() & infos.Pfad_Projekte & Projekte.Projekt
            Ersteller.WVersion = Projekte.P_Version
            Me.Hide()
            Ersteller.Show()
        End If
    End Sub

    Private Sub Panel1_ClientSizeChanged(sender As Object, e As EventArgs) Handles Panel1.ClientSizeChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Editor.Load_Pic("D:\Prog Projekte\TPMaker\Tests\SolrflareDefaultEdit 1.12\assets\minecraft\textures\items\diamond_sword.png")
    End Sub
End Class
