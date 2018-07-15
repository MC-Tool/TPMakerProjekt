Public Class Hs

#Region "Declarations"
    Dim i As Integer = 0
#End Region

    Private Sub NewPack_Click(sender As Object, e As EventArgs) Handles NewPack.Click
        ActionController.CreateNewProject()
    End Sub

    Private Sub NeedHelp_Click(sender As Object, e As EventArgs) Handles NeedHelp.Click
        'ToDo: NeedHelp Action in ActionController
    End Sub

    Private Sub ImportPack_Click(sender As Object, e As EventArgs) Handles ImportPack.Click
        ActionController.ImportTexturePack()
    End Sub

    Private Sub ImportProject_Click(sender As Object, e As EventArgs) Handles ImportProject.Click
        ActionController.LoadProject()
    End Sub

    Private Sub Share_Click(sender As Object, e As EventArgs) Handles Share.Click
        'ToDo: Share ACtion in ActionController
    End Sub

    Private Sub Hs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
    End Sub

    Private Sub Hs_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        startAnimation.Start()
    End Sub

    Private Sub startAnimation_Tick(sender As Object, e As EventArgs) Handles startAnimation.Tick
        If Not i > 5 Then
            i += 1
        Else
            startAnimation.Stop()
        End If

        Select Case i
            Case 1
                NewPack.Visible = True
            Case 2
                ImportPack.Visible = True
            Case 3
                ImportProject.Visible = True
            Case 4
                Share.Visible = True
            Case 5
                NeedHelp.Visible = True
        End Select
    End Sub

#Region "Mouse Stuff"

    Private Sub NewPack_MouseEnter(sender As Object, e As EventArgs) Handles NewPack.MouseEnter
        AnimationController.MouseEnter(NewPack)
    End Sub

    Private Sub NewPack_MouseLeave(sender As Object, e As EventArgs) Handles NewPack.MouseLeave
        AnimationController.MouseLeave(NewPack)
    End Sub

    Private Sub ImportPack_MouseEnter(sender As Object, e As EventArgs) Handles ImportPack.MouseEnter
        AnimationController.MouseEnter(ImportPack)
    End Sub

    Private Sub ImportPack_MouseEnter_MouseLeave(sender As Object, e As EventArgs) Handles ImportPack.MouseLeave
        AnimationController.MouseLeave(ImportPack)
    End Sub

    Private Sub ImportProject_MouseEnter(sender As Object, e As EventArgs) Handles ImportProject.MouseEnter
        AnimationController.MouseEnter(ImportProject)
    End Sub

    Private Sub ImportProject_MouseLeave(sender As Object, e As EventArgs) Handles ImportProject.MouseLeave
        AnimationController.MouseLeave(ImportProject)
    End Sub

    Private Sub Share_MouseEnter(sender As Object, e As EventArgs) Handles Share.MouseEnter
        AnimationController.MouseEnter(Share)
    End Sub

    Private Sub Share_MouseLeave(sender As Object, e As EventArgs) Handles Share.MouseLeave
        AnimationController.MouseLeave(Share)
    End Sub

    Private Sub NeedHelp_MouseEnter(sender As Object, e As EventArgs) Handles NeedHelp.MouseEnter
        AnimationController.MouseEnter(NeedHelp)
    End Sub

    Private Sub NeedHelp_MouseLeave(sender As Object, e As EventArgs) Handles NeedHelp.MouseLeave
        AnimationController.MouseLeave(NeedHelp)
    End Sub

#End Region

    Public Class ActionController

        Public Shared Sub CreateNewProject()
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
                Hs.Hide()
                Ersteller.Show()
            Catch ex As Exception
                MsgBox("Fehler beim erstellen des Projektes! Fehler: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        End Sub

        Public Shared Sub ImportTexturePack()
            If Not Version.Get_Version() = MsgBoxResult.Ok Then Exit Sub
            TPImporter.Import("Texturpack impotieren...")
        End Sub

        Public Shared Sub LoadTexture(Optional path As String = "D:\Prog Projekte\TPMaker\Tests\SolrflareDefaultEdit 1.12\assets\minecraft\textures\items\diamond_sword.png")
            Editor.Load_Pic(path)
        End Sub

        Public Shared Sub LoadProject()
            If Projekte.Get_Projekt() = DialogResult.OK Then

                Ersteller.Wpfad = infos.Pfad() & infos.Pfad_Projekte & Projekte.Projekt
                Ersteller.WVersion = Projekte.P_Version
                Hs.Hide()
                Ersteller.Show()
            End If
        End Sub

    End Class

    Public Class AnimationController

        Public Shared Sub MouseEnter(ByVal obj As Object)
            obj.BackColor = Color.FromArgb(0, 0, 0)
        End Sub

        Public Shared Sub MouseLeave(ByVal obj As Object)
            obj.BackColor = Color.FromArgb(100, 0, 0, 0)
        End Sub

    End Class

End Class
