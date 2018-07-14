Imports System.ComponentModel
Imports System.IO

Public Class Ersteller
    Private Sub Ersteller_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Create_Icon_List()
        Load_Global_settings()
        ListDesigner.Ini_View_small(ListView1)
        ListDesigner.Ini_View_small(ListView2)
        ListDesigner.Ini_View_small(ListView3)
        ListDesigner.Ini_View_small(ListView4)
        Ersteller_List.Show_Liste(ListView1, Pfad_Texturen, "\assets\minecraft\textures\", Panle_texturen, Button1, Button4, titel_Texturen, Button3, Button2, Button6, Button5, PictureBox1, Button25, Button26, TextBox1)
        Ersteller_List.Show_Liste(ListView2, Label5, "\assets\minecraft\font\", Panel1, Button12, Button10, Label4, Button9, Button11, Button7, Button8, PictureBox2, Button30, Button29, TextBox3)
        Ersteller_List.Show_Liste(ListView3, Label7, "\assets\minecraft\mcpatcher\", Panel2, Button18, Button16, Label6, Button15, Button17, Button13, Button14, PictureBox3, Button32, Button31, TextBox4)
        Ersteller_List.Show_Liste(ListView4, Label9, "\assets\minecraft\sounds\", Panel3, Button24, Button22, Label8, Button21, Button23, Button19, Button20, PictureBox4, Button28, Button27, TextBox2)
        Ini_suche(New TextBox() {TextBox1, TextBox2, TextBox3, TextBox4})
    End Sub
    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            Ersteller_List.Show_Liste(ListView3, Label7, "\assets\minecraft\mcpatcher\", Panel2, Button18, Button16, Label6, Button15, Button17, Button13, Button14, PictureBox3, Button32, Button31, TextBox4)
        End If
    End Sub
    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Ersteller_List.Show_Liste(ListView2, Label5, "\assets\minecraft\font\", Panel1, Button12, Button10, Label4, Button9, Button11, Button7, Button8, PictureBox2, Button30, Button29, TextBox3)
        End If
    End Sub
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Ersteller_List.Show_Liste(ListView4, Label9, "\assets\minecraft\sounds\", Panel3, Button24, Button22, Label8, Button21, Button23, Button19, Button20, PictureBox4, Button28, Button27, TextBox2)
        End If
    End Sub
    Private Sub TextBox1KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Ersteller_List.Show_Liste(ListView1, Pfad_Texturen, "\assets\minecraft\textures\", Panle_texturen, Button1, Button4, titel_Texturen, Button3, Button2, Button6, Button5, PictureBox1, Button25, Button26, TextBox1)
        End If
    End Sub
    Private Sub Ini_suche(tbox As Array)
        For Each b As TextBox In tbox
            b.Text = infos.such_text
        Next
    End Sub

    Public Wpfad As String
    Public WVersion As String
    Private Sub Create_Icon_List()
        Dim components = New Container()
        Dim myImages As New ImageList(components)
        myImages.Images.Clear()


        myImages.ImageSize = New Size(16, 16)
        myImages.Images.Add(My.Resources.Computer_40px)

        MetroTabControl1.ImageList = myImages
        MetroTabControl1.TabPages.Item(0).ImageIndex = 0
    End Sub

    Private Sub Load_Global_settings()
        title.Text = "Texturepack " & IO.Path.GetFileName(Wpfad)
        tpname.Text = IO.Path.GetFileName(Wpfad)
        tpversion.Text = WVersion
        Dim bild As String = Get_anzeige_bild()
        If Not bild = Nothing Then
            tpcbild.Image = Image.FromFile(bild)
        End If
        tpbeschreibung.Text = Get_description()
    End Sub
    Private Function Get_anzeige_bild()
        Try
            If Not IO.Directory.Exists(infos.Pfad() & Pfad_Temp) Then MkDir(infos.Pfad() & Pfad_Temp)
            Dim format As String = Now.ToString("yyyyMMddHHmmssfff")
            IO.File.Copy(Wpfad & "/pack.png", infos.Pfad() & Pfad_Temp & "\" & format)
            Return (infos.Pfad() & Pfad_Temp & "\" & format)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function Get_description() As String
        Try

            Dim l As New List(Of String)
            Using sr As StreamReader = File.OpenText(Wpfad & "/pack.mcmeta")
                Do While sr.Peek() >= 0
                    l.Add(sr.ReadLine())
                Loop
            End Using

            Dim s As String = l(3)
            Dim split() As String = s.Split(":")
            Dim result = split(1).ToString.Replace(Chr(34), "").Replace(" ", "")
            Return result
        Catch ex As Exception

        End Try
    End Function

    Private Sub save_desc_Click(sender As Object, e As EventArgs) Handles save_desc.Click
        save_mcdata()
    End Sub

    Private Sub save_mcdata()
        If tpbeschreibung.Text = Nothing Then
            MsgBox("Bitte fülle das Beschreibungs-Feld aus!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Using sw As StreamWriter = File.CreateText(Wpfad & "/pack.mcmeta")
            sw.WriteLine("{")
            sw.WriteLine("  {")
            sw.WriteLine("  " & Chr(34) & "pack" & Chr(34) & ": {")
            sw.WriteLine("    " & Chr(34) & "description" & Chr(34) & ": " & Chr(34) & "" & tpbeschreibung.Text & "" & Chr(34) & "")
            sw.WriteLine("  }")
            sw.WriteLine("}")
        End Using
        tpbeschreibung.Text = Get_description()
    End Sub

    Private Sub edit_img_Click(sender As Object, e As EventArgs) Handles edit_img.Click
        Dim f As New OpenFileDialog()
        f.Filter = "Bild|*.png"
        If Not f.ShowDialog = DialogResult.Cancel Then
            Try
                IO.File.Copy(f.FileName, Wpfad & "/pack.png", True)
                tpcbild.Image = Image.FromFile(Wpfad & "/pack.png")
            Catch ex As Exception
                MsgBox("Fehler beim setzen des Bildes! Fehler: " & ex.Message, MsgBoxStyle.Critical)

            End Try
        End If
    End Sub

    Private Sub Ersteller_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

    End Sub

    Private Sub suche_Click(sender As Object, e As EventArgs) Handles TextBox1.Click, TextBox2.Click, TextBox3.Click, TextBox4.Click, TextBox1.LostFocus, TextBox2.LostFocus, TextBox3.LostFocus, TextBox4.LostFocus
        Dim boxt As TextBox = CType(sender, TextBox)
        If boxt.Text = infos.such_text Then
            boxt.Text = ""
        ElseIf boxt.Text = "" Then
            boxt.Text = infos.such_text
        End If
    End Sub


End Class