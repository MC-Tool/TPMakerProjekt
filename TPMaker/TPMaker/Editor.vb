Imports System.IO
Public Class Editor

    Public akt_zoom As Integer = 100

    Private Sub ResetToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Pic.Cursor = Cursors.Default
        If Not (Akt_bild() Is Nothing) Then

            Dim fs As FileStream
            fs = New FileStream(currentfilename, IO.FileMode.Open, IO.FileAccess.Read)
            Show_Bild(Image.FromStream(fs))
            fs.Close()
            original = Akt_bild()
            resetmenus()
            set_zoom100_checked()
        End If
    End Sub

    Dim m_PanStartPoint As New Point 'for Pan Function
    Dim DrawRectangle As Boolean = False 'for draw rectangle function
    Dim DrawRectangle_Mouse_X_Start As Integer 'for draw rectangle function
    Dim DrawRectangle_Mouse_Y_Start As Integer 'for draw rectangle function
    Dim DrawRectangle_Mouse_X_End As Integer 'for draw rectangle function
    Dim DrawRectangle_Mouse_Y_End As Integer 'for draw rectangle function
    Dim currentfilename As String = Nothing 'copy of current image filename
    Dim original As Image = Nothing 'original image used for zoom feature




    Private Sub resetmenus()
        Pic.Cursor = Cursors.Default
        CopyToolStripMenuItem1.Enabled = False
        PanToolStripMenuItem1.Enabled = True
        PanToolStripMenuItem1.BorderStyle = BorderStyle.None
        PrintToolStripMenuItem1.Enabled = False
        Stift.Enabled = True
        RotateToolStripMenuItem1.Enabled = True
        SelectToolStripMenuItem1.Enabled = True
        SelectToolStripMenuItem1.BorderStyle = BorderStyle.None
        Zoom.Enabled = True
    End Sub
    Private Sub set_zoom100_checked()
        Zoom200ToolStripMenuItem1.Checked = False
        Zoom100ToolStripMenuItem2.Checked = True
        Zoom75ToolStripMenuItem3.Checked = False
        Zoom50ToolStripMenuItem4.Checked = False
        Zoom25ToolStripMenuItem5.Checked = False
    End Sub
    Private Sub Pic_MouseDown(ByVal sender As Object,
                                  ByVal e As System.Windows.Forms.MouseEventArgs) Handles _
                                  Pic.MouseDown
        If Pic.Cursor = Cursors.Cross Then
            DrawRectangle = True
            DrawRectangle_Mouse_X_Start = e.Location.X
            DrawRectangle_Mouse_Y_Start = e.Location.Y
            DrawRectangle_Mouse_X_End = e.Location.X + 1
            DrawRectangle_Mouse_Y_End = e.Location.Y + 1
        End If
        If Pic.Cursor = Cursors.Hand Then

            m_PanStartPoint = New Point(e.X, e.Y)
        End If
    End Sub
    Private Sub Pic_MouseMove(ByVal sender As Object,
                                  ByVal e As System.Windows.Forms.MouseEventArgs) Handles _
                                  Pic.MouseMove


        If Pic.Cursor = Cursors.Cross Then
            DrawRectangle_Mouse_X_End = e.Location.X
            DrawRectangle_Mouse_Y_End = e.Location.Y
            Pic.Refresh()

        End If

        If Pic.Cursor = Cursors.Hand AndAlso e.Button = Windows.Forms.MouseButtons.Left Then

            Dim DeltaX As Integer = (m_PanStartPoint.X - e.X)
            Dim DeltaY As Integer = (m_PanStartPoint.Y - e.Y)

            Panel1.AutoScrollPosition =
                New Drawing.Point((DeltaX - Panel1.AutoScrollPosition.X),
                                (DeltaY - Panel1.AutoScrollPosition.Y))
        End If

        If Stift.BorderStyle = BorderStyle.Fixed3D AndAlso e.Button = Windows.Forms.MouseButtons.Left Then
            If ist_in_img(e.X, e.Y) = True Then

                Draw_with_stift(New Point(e.X, e.Y))
            End If
        End If

        If Not original Is Nothing And Not Akt_bild() Is Nothing Then
            If ist_in_img(e.X, e.Y) = True Then
                Dim bildposX As Integer = Get_Img_position_x(e.X)
                Dim bildposY As Integer = Get_Img_position_y(e.Y)
                Locationpixel.Text = bildposX & "; " & bildposY
                Locationpixel.ForeColor = CType(Akt_bild(), Bitmap).GetPixel(bildposX, bildposY)

            Else
                Locationpixel.Text = Get_Img_position_x(e.X) & "; " & Get_Img_position_y(e.Y)
                Locationpixel.ForeColor = Color.Black
            End If
        End If




    End Sub
    Private Function ist_in_img(X As Integer, Y As Integer) As Boolean
        Dim BX As Integer = (Pic.Width - Akt_bild.Width) / 2
        Dim By As Integer = (Pic.Height - Akt_bild.Height) / 2
        If X > BX And X < BX + Akt_bild.Width And Y > By And Y < By + Akt_bild.Height Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function Get_Img_position_x(X As Integer) As Integer
        Dim BX As Integer = (Pic.Width - Akt_bild.Width) / 2
        Return X - BX

    End Function
    Private Function Get_Img_position_y(y As Integer) As Integer
        Dim By As Integer = (Pic.Height - Akt_bild.Height) / 2
        Return y - By

    End Function
    Private Function CropBitmap(ByVal srcBitmap As Bitmap,
         ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer,
         ByVal cropHeight As Integer) As Bitmap


        Dim bmp As New Bitmap(cropWidth, cropHeight)

        Dim g As Graphics = Graphics.FromImage(bmp)

        g.DrawImage(srcBitmap, New Rectangle(0, 0, cropWidth, cropHeight),
                        cropX, cropY, cropWidth, cropHeight, GraphicsUnit.Pixel)

        g.Dispose()


        Return bmp

    End Function 'Copy
    Private Sub Set_größen()

        For x = 1 To 500
            ComboBox1.Items.Add(x & "px")
        Next
    End Sub

    Private Sub pic_Paint(ByVal sender As Object,
                              ByVal e As System.Windows.Forms.PaintEventArgs) Handles Pic.Paint
        If SelectToolStripMenuItem1.BorderStyle = BorderStyle.Fixed3D Or rectangle.BorderStyle = BorderStyle.Fixed3D And MouseButtons = Windows.Forms.MouseButtons.Left Then
            Try
                e.Graphics.DrawRectangle(Pens.Red, Get_selection_rectangle())
            Catch ex As Exception
            End Try
        End If
        If Stift.BorderStyle = BorderStyle.Fixed3D Then
            Dim g As Graphics = e.Graphics
            g.DrawEllipse(Pens.Black, pinsel_rectangle(New Point(DrawRectangle_Mouse_X_End, DrawRectangle_Mouse_Y_End)))
        End If

    End Sub
    Private Sub Pic_MouseUp(sender As Object, e As MouseEventArgs) Handles Pic.MouseUp
        If SelectToolStripMenuItem1.BorderStyle = BorderStyle.Fixed3D Then
            Dim newimg As Bitmap = New Bitmap(Akt_bild.Width, Akt_bild.Height)
            Using gr As Graphics = Graphics.FromImage(newimg)
                gr.DrawImage(Akt_bild, New Rectangle(0, 0, Akt_bild.Width, Akt_bild.Height))
                gr.DrawRectangle(Pens.Red, get_rectange_selection())
            End Using
            Show_Bild(newimg)
        End If
        If rectangle.BorderStyle = BorderStyle.Fixed3D Then
            Dim newimg As Bitmap = New Bitmap(Akt_bild.Width, Akt_bild.Height)
            Using gr As Graphics = Graphics.FromImage(newimg)
                gr.DrawImage(Akt_bild, New Rectangle(0, 0, Akt_bild.Width, Akt_bild.Height))
                Dim farbe As Color = AktFarbe.BackColor
                Dim alpha As Integer = CInt(Math.Round(2.55 * Get_Deckkraft(), 0))
                gr.FillRectangle(New SolidBrush(Color.FromArgb(alpha, farbe)), get_rectange_selection())
            End Using
            Show_Bild(newimg)
        End If

    End Sub
    Private Function get_rectange_selection() As Rectangle
        Dim aktrectangle As Rectangle = Get_selection_rectangle()
        Dim koords_D As Point = Korrds_korrigieren(Get_Img_position_x(aktrectangle.X), Get_Img_position_y(aktrectangle.Y), 2)
        Dim koord_C As Point = Korrds_korrigieren(Get_Img_position_x(aktrectangle.X) + aktrectangle.Width, Get_Img_position_y(aktrectangle.Y), 2)
        Dim koord_B As Point = Korrds_korrigieren(Get_Img_position_x(aktrectangle.X) + aktrectangle.Width, Get_Img_position_y(aktrectangle.Y) + aktrectangle.Height, 2)
        Dim koord_A As Point = Korrds_korrigieren(Get_Img_position_x(aktrectangle.X), Get_Img_position_y(aktrectangle.Y) + aktrectangle.Height, 2)
        Return New Rectangle(koords_D.X, koords_D.Y, koord_C.X - koords_D.X, koord_A.Y - koords_D.Y)
    End Function
    Private Function Get_selection_rectangle() As Rectangle
        Dim mousex As Integer = DrawRectangle_Mouse_X_End - DrawRectangle_Mouse_X_Start
        Dim mousey As Integer = DrawRectangle_Mouse_Y_End - DrawRectangle_Mouse_Y_Start
        Dim rect As Rectangle 'for draw rectangle function
        ' Up and Left
        If mousex < 0 AndAlso mousey < 0 Then
            rect = New Rectangle((New Point(DrawRectangle_Mouse_X_End, DrawRectangle_Mouse_Y_End)),
                                         New Size(System.Math.Abs(mousex), System.Math.Abs(mousey)))
        End If
        'Down and Right
        If mousex > 0 AndAlso mousey > 0 Then
            rect = New Rectangle((New Point(DrawRectangle_Mouse_X_Start, DrawRectangle_Mouse_Y_Start)),
                                                     New Size((mousex), (mousey)))
        End If
        'Up and Right
        If mousex < 0 AndAlso mousey > 0 Then
            rect = New Rectangle((New Point(DrawRectangle_Mouse_X_End, DrawRectangle_Mouse_Y_Start)),
                                         New Size(System.Math.Abs(mousex), mousey))
        End If
        'Down and Left
        If mousex > 0 AndAlso mousey < 0 Then
            rect = New Rectangle((New Point(DrawRectangle_Mouse_X_Start, DrawRectangle_Mouse_Y_End)),
                                         New Size(mousex, System.Math.Abs(mousey)))
        End If
        Return rect
    End Function
    Private Function pinsel_rectangle(koords As Point) As Rectangle
        Return New Rectangle(koords.X - ((get_pinsel_größe() * (akt_zoom / 100)) / 2), koords.Y - ((get_pinsel_größe() * (akt_zoom / 100)) / 2), get_pinsel_größe() * (akt_zoom / 100), get_pinsel_größe() * (akt_zoom / 100))
    End Function
    Private Sub Pic_MouseClick(sender As Object, e As MouseEventArgs) Handles Pic.MouseClick
        If Farbwahl.BorderStyle = BorderStyle.Fixed3D AndAlso e.Button = Windows.Forms.MouseButtons.Left Then
            If ist_in_img(e.X, e.Y) = True Then
                AktFarbe.BackColor = Akt_bild.GetPixel(Get_Img_position_x(e.X), Get_Img_position_y(e.Y))
            End If
        End If

        If Stift.BorderStyle = BorderStyle.Fixed3D Then
            Draw_with_stift(New Point(e.X, e.Y))
        End If
        If singlePixel.BorderStyle = BorderStyle.Fixed3D Then
            Dim mittelpunkt As Point = Korrds_korrigieren(Get_Img_position_x(e.X), Get_Img_position_y(e.Y), 0)
            Dim rect As Rectangle = New Rectangle(CInt(mittelpunkt.X), CInt(mittelpunkt.Y), 1 * (akt_zoom / 100), 1 * (akt_zoom / 100))
            Dim newimg As Bitmap = New Bitmap(Akt_bild.Width, Akt_bild.Height)
            Using gr As Graphics = Graphics.FromImage(newimg)
                gr.DrawImage(Akt_bild, New Rectangle(0, 0, Akt_bild.Width, Akt_bild.Height))
                gr.DrawRectangle(Pens.Red, rect)

            End Using
            Show_Bild(newimg)
        End If
    End Sub
    Private Sub Draw_with_stift(e As Point)
        If ist_in_img(e.X, e.Y) = True Then
            Dim bildposX As Integer = Get_Img_position_x(e.X)
            Dim bildposY As Integer = Get_Img_position_y(e.Y)

            Dim oImage As New Bitmap(Akt_bild.Width, Akt_bild.Height)
            Dim korrigierter_punkt As Point
            Dim mittelpunkt As Point
            If Not get_pinsel_größe() Mod 2 = 0 Then
                korrigierter_punkt = Korrds_korrigieren(Get_Img_position_x(e.X), Get_Img_position_y(e.Y), 0)
                mittelpunkt = New Point(korrigierter_punkt.X + ((akt_zoom / 100) / 2), korrigierter_punkt.Y + ((akt_zoom / 100) / 2))
            Else
                korrigierter_punkt = Korrds_korrigieren(Get_Img_position_x(e.X), Get_Img_position_y(e.Y), 2)
                mittelpunkt = korrigierter_punkt
            End If



            Using gr As Graphics = Graphics.FromImage(oImage)
                gr.DrawImage(Akt_bild, New Rectangle(0, 0, Akt_bild.Width, Akt_bild.Height))
                oImage.SetPixel(korrigierter_punkt.X, korrigierter_punkt.Y, Color.YellowGreen)
                oImage.SetPixel(mittelpunkt.X, mittelpunkt.Y, Color.Red)
                Dim farbe As Color = AktFarbe.BackColor
                Dim alpha As Integer = CInt(Math.Round(2.55 * Get_Deckkraft(), 0))
                gr.FillRectangle(New SolidBrush(Color.FromArgb(alpha, farbe)), pinsel_rectangle(mittelpunkt))
                '  gr.DrawRectangle(Pens.Red, pinsel_rectangle(mittelpunkt))


            End Using

            Show_Bild(oImage)
        End If
    End Sub

    Private Function Korrds_korrigieren(x As Integer, y As Integer, Startpos As Integer) As Point
        If x < 0 Then
            x = 0
        End If
        If y < 0 Then
            y = 0
        End If
        Dim pixel_X As Integer = CInt(Math.Floor(x / (akt_zoom / 100)))
        Dim anfang_x As Single = pixel_X * (akt_zoom / 100)
        Dim ende_x As Single = (akt_zoom / 100)
        Dim pixel_y As Integer = CInt(Math.Floor(y / (akt_zoom / 100)))
        Dim anfang_Y As Single = pixel_y * (akt_zoom / 100)
        Dim ende_Y As Single = (akt_zoom / 100)

        If Startpos = 0 Then
            Return New Point(anfang_x, anfang_Y)
        ElseIf Startpos = 1 Then
            Return New Point(anfang_x + ende_x, anfang_Y + ende_Y)
        ElseIf Startpos = 2 Then
            Dim pixelpos_X As Integer = CInt(Math.Round(x / (akt_zoom / 100), 0))
            Dim pixelpos_Y As Integer = CInt(Math.Round(y / (akt_zoom / 100), 0))
            If pixel_X = pixelpos_X And pixel_y = pixelpos_Y Then
                Return New Point(anfang_x, anfang_Y)
            ElseIf pixel_X < pixelpos_X And pixel_y = pixelpos_Y Then
                Return New Point(anfang_x + ende_x, anfang_Y)
            ElseIf pixel_X < pixelpos_X And pixel_y < pixelpos_Y Then
                Return New Point(anfang_x + ende_x, anfang_Y + ende_Y)
            ElseIf pixel_X = pixelpos_X And pixel_y < pixelpos_Y Then
                Return New Point(anfang_x, anfang_Y + ende_Y)
            Else
                Debug.Print("No Point")
            End If
        End If
    End Function


    Private Sub RotateToolStripMenuItem1_Click(ByVal sender As System.Object,
                                                   ByVal e As System.EventArgs) Handles _
                                                   RotateToolStripMenuItem1.Click
        If Not (Akt_bild() Is Nothing) Then
            Dim img As Image = Akt_bild()
            img.RotateFlip(RotateFlipType.Rotate90FlipNone)
            Show_Bild(img)
        End If
    End Sub


    Public Sub ZoomImage(ByRef ZoomValue As Int32)
        If ZoomValue > 2000 Or ZoomValue < 10 Then Exit Sub
        If original Is Nothing Then
            Exit Sub
        End If
        If Not Akt_bild() Is Nothing Then
            Reset_original(original.Width, original.Height)
        End If

        Dim newwidth As Integer = original.Width * (ZoomValue) / 100
        Dim newheight As Integer = original.Height * (ZoomValue / 100)
        Show_Bild(Neu_skalieren(newwidth, newheight))
        ' Pic.Image = Neu_skalieren(newwidth, newheight)
        akt_zoom = ZoomValue
        Zoomprozent.Text = ZoomValue & "%"
        TrackBar1.Maximum = 2000
        TrackBar1.Value = ZoomValue

    End Sub
    Private Sub Reset_original(breite As Integer, höhe As Integer)
        Dim oImage As New Bitmap(breite, höhe)
        Using g As Graphics = Graphics.FromImage(oImage)
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(Akt_bild, New Rectangle(0, 0, breite, höhe))

        End Using

        original = oImage

    End Sub
    Private Function Neu_skalieren(breite As Integer, höhe As Integer) As Bitmap
        Dim oImage As New Bitmap(breite, höhe)
        Using g As Graphics = Graphics.FromImage(oImage)
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(original, New Rectangle(0, 0, breite, höhe))
            '  Debug.Print(breite & "  " & höhe)
        End Using

        Return oImage
    End Function
    Private Bildindex As Integer = 0
    Private Bilder As New List(Of Bitmap)
    Private Function Akt_bild() As Bitmap
        Return Bilder(Bildindex)
    End Function
    Private Sub Pic_go_Back()
        If Bildindex > 0 Then
            Bildindex -= 1
            Pic.Image = (Design_Bild(Bilder(Bildindex)))
            Pic_forward.Image = My.Resources.icons8_Forward_48px
            Pic_forward.Cursor = Cursors.Hand
        End If
        If Bildindex = 0 Then
            Pic_Back.Image = My.Resources.icons8_Back_64px
            Pic_Back.Cursor = Cursors.No
        End If
    End Sub
    Private Sub Pic_go_Vor()
        If Bildindex < Bilder.Count - 1 Then
            Bildindex += 1
            Pic.Image = (Design_Bild(Bilder(Bildindex)))
            Pic_Back.Image = My.Resources.icons8_Back_48px
            Pic_Back.Cursor = Cursors.Hand
        End If
        If Bildindex = Bilder.Count - 1 Then
            Pic_forward.Image = My.Resources.Forward_64px
            Pic_forward.Cursor = Cursors.No
        End If
    End Sub
    Private Function Design_Bild(bild As Bitmap) As Bitmap
        Dim oImage As New Bitmap(bild.Width + 2, bild.Height + 2)
        Using g As Graphics = Graphics.FromImage(oImage)
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(My.Resources.transparent, New Rectangle(1, 1, bild.Width + 1, bild.Height + 1))
            g.DrawImage(bild, New Rectangle(1, 1, bild.Width + 1, bild.Height + 1))
            g.DrawRectangle(Pens.Black, New Rectangle(0, 0, bild.Width + 2, bild.Height + 2))
        End Using
        Return oImage
    End Function
    Private Function Show_Bild(bild As Bitmap)
        For y = Bildindex To Bilder.Count - 1
            If Not Bildindex = Bilder.Count - 1 Then
                Bilder.RemoveAt(y)
            End If
        Next
        Bilder.Add(bild)
        Bildindex = Bilder.Count - 1
        Pic_Back.Image = My.Resources.icons8_Back_48px
        Pic_Back.Cursor = Cursors.Hand
        Pic_forward.Image = My.Resources.Forward_64px
        Pic_forward.Cursor = Cursors.No
        Pic.Image = Design_Bild(bild)
    End Function




    Private Sub Zoom200ToolStripMenuItem1_Click(ByVal sender As System.Object,
                                                    ByVal e As System.EventArgs) Handles _
                                                    Zoom200ToolStripMenuItem1.Click
        ZoomImage(InputBox("zoom"))
    End Sub

    Private Sub Zoom100ToolStripMenuItem2_Click(ByVal sender As System.Object,
                                                ByVal e As System.EventArgs) Handles _
                                                Zoom100ToolStripMenuItem2.Click
        ZoomImage(100)
    End Sub

    Private Sub Zoom75ToolStripMenuItem3_Click(ByVal sender As System.Object,
                                               ByVal e As System.EventArgs) Handles _
                                               Zoom75ToolStripMenuItem3.Click
        ZoomImage(75)
    End Sub

    Private Sub Zoom50ToolStripMenuItem4_Click(ByVal sender As System.Object,
                                               ByVal e As System.EventArgs) Handles _
                                               Zoom50ToolStripMenuItem4.Click
        ZoomImage(50)
    End Sub

    Private Sub Zoom25ToolStripMenuItem5_Click(ByVal sender As System.Object,
                                               ByVal e As System.EventArgs) Handles _
                                               Zoom25ToolStripMenuItem5.Click
        ZoomImage(25)
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(ByVal sender As System.Object,
                                              ByVal e As System.EventArgs) Handles _
                                              PrintToolStripMenuItem1.Click
        If DrawRectangle Then
            PrintDocument1.Print()
        End If
    End Sub

    'Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object,
    '                                     ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _
    '                                     PrintDocument1.PrintPage
    '    Dim oldimage As Bitmap = Akt_bild()
    '    Dim adjustedimage As Bitmap
    '    adjustedimage = CropBitmap(oldimage, rect.X, rect.Y, rect.Width, rect.Height)
    '    Try
    '        Clipboard.SetImage(adjustedimage)
    '    Catch ex As Exception
    '    End Try
    '    e.Graphics.DrawImage(Clipboard.GetImage, 0, 0)
    'End Sub

    Private Sub LoadToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem1.Click
        Dim fs As FileStream
        Dim OpenFileDialog1 As New OpenFileDialog
        OpenFileDialog1.Filter = "JPG files (*.jpg)|*.jpg|" & "BMP Files (*.bmp)|*.bmp|" _
        & "TIF Files (*.tif)|*.tif|" & "PNG Files (*.png)|*.png|" & "ALL Files (*.*)|*.*"
        OpenFileDialog1.Title = "Select an Image File"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            currentfilename = OpenFileDialog1.FileName
            If currentfilename.ToUpper.EndsWith(".JPG") Or
            currentfilename.ToUpper.EndsWith(".BMP") Or
            currentfilename.ToUpper.EndsWith(".TIF") Or
            currentfilename.ToUpper.EndsWith(".PNG") Then

                fs = New FileStream(currentfilename, IO.FileMode.Open, IO.FileAccess.Read)
                Show_Bild(Image.FromStream(fs))
                fs.Close()
                original = Akt_bild()

                resetmenus()
                set_zoom100_checked()
            End If
        End If
    End Sub
    Public Sub Load_Pic(file As String)
        Me.Show()


        Dim myStream As System.IO.FileStream = New System.IO.FileStream(file, IO.FileMode.Open)
        Dim myImage As Bitmap = Image.FromStream(myStream)
        myStream.Close()
        original = myImage
        Show_Bild(original)
        resetmenus()
        set_zoom100_checked()
        skalwith.Text = original.Width.ToString
        skalhigh.Text = original.Height.ToString

    End Sub
    Private Sub set_transparent_bild()

    End Sub

    'Private Sub CopyToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem1.Click

    '    If DrawRectangle Then
    '        Dim oldimage As Bitmap = Akt_bild()
    '        Dim adjustedimage As Bitmap
    '        adjustedimage = CropBitmap(oldimage, rect.X, rect.Y, rect.Width, rect.Height)
    '        Try
    '            Clipboard.SetImage(adjustedimage)
    '        Catch ex As Exception
    '        End Try
    '    End If

    'End Sub

    Private Sub PanToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles PanToolStripMenuItem1.Click
        reset_werkzeuge()
        If PanToolStripMenuItem1.BorderStyle = FormBorderStyle.Fixed3D Then
            PanToolStripMenuItem1.BorderStyle = FormBorderStyle.None
            Pic.Cursor = Cursors.Default
        Else
            PanToolStripMenuItem1.BorderStyle = FormBorderStyle.Fixed3D
            SelectToolStripMenuItem1.BorderStyle = FormBorderStyle.None
            CopyToolStripMenuItem1.Enabled = False
            PrintToolStripMenuItem1.Enabled = False
            DrawRectangle = False
            Pic.Invalidate()
            Pic.Cursor = Cursors.Hand
        End If
    End Sub
    Private Function reset_werkzeuge()
        Dim Werkzeuge() = New PictureBox() {PanToolStripMenuItem1, SelectToolStripMenuItem1, singlePixel, Stift, Farbwahl, rectangle}
        For Each w In Werkzeuge
            w.BorderStyle = BorderStyle.None
        Next
    End Function
    Private Sub SelectToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles SelectToolStripMenuItem1.Click
        reset_werkzeuge()
        If SelectToolStripMenuItem1.BorderStyle = FormBorderStyle.Fixed3D Then
            SelectToolStripMenuItem1.BorderStyle = FormBorderStyle.None
            CopyToolStripMenuItem1.Enabled = False
            PrintToolStripMenuItem1.Enabled = False
            DrawRectangle = False
            Pic.Invalidate()
            Pic.Cursor = Cursors.Default
        Else
            SelectToolStripMenuItem1.BorderStyle = FormBorderStyle.Fixed3D
            PanToolStripMenuItem1.BorderStyle = FormBorderStyle.None
            CopyToolStripMenuItem1.Enabled = True
            PrintToolStripMenuItem1.Enabled = True
            Pic.Cursor = Cursors.Cross
        End If
    End Sub



    Private Sub Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Set_größen()
        AktFarbe.BackColor = My.Settings.Farbe
        ComboBox1.Text = My.Settings.Stiftgröße & "px"
        Prozentdeckkreft.Width = CInt(Math.Round(My.Settings.Stiftalpha * (Panel3.Width / 100), 0))

    End Sub

    Private Sub AktFarbe_Click(sender As Object, e As EventArgs) Handles AktFarbe.Click, Button2.Click
        Dim f As New ColorDialog
        f.Color = AktFarbe.BackColor

        f.FullOpen = True
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            AktFarbe.BackColor = f.Color
        End If
    End Sub

    Private Sub Stift_Click(sender As Object, e As EventArgs) Handles Stift.Click
        reset_werkzeuge()
        If Stift.BorderStyle = BorderStyle.None Then
            Stift.BorderStyle = BorderStyle.Fixed3D
            Pic.Cursor = Cursors.Cross

        Else
            Stift.BorderStyle = BorderStyle.None
            Pic.Cursor = Cursors.Default
        End If
        Pic.Invalidate()
    End Sub

    Private Sub Zoom_Click(sender As Object, e As EventArgs) Handles Zoom.Click

    End Sub

    Private Sub Pic_MouseWheel(sender As Object, e As MouseEventArgs) Handles Pic.MouseWheel
        If Windows.Forms.Keys.Shift Then

            If e.Delta > -1 Then
                If akt_zoom < 2000 And akt_zoom >= 100 Then
                    ZoomImage(akt_zoom + 100)
                ElseIf akt_zoom <= 100 Then
                    ZoomImage(akt_zoom + 10)
                End If
            Else
                If akt_zoom > 100 Then
                    ZoomImage(akt_zoom - 100)


                ElseIf akt_zoom <= 100 And akt_zoom > 10 Then
                    ZoomImage(akt_zoom - 10)
                End If
            End If
            End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        If TrackBar1.Value > 0 Then
            ZoomImage(TrackBar1.Value)
        End If
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub



    Private Sub Prozentgröße_SizeChanged(sender As Object, e As EventArgs) Handles Prozentgröße.SizeChanged
        ComboBox1.SelectedItem = Prozentgröße.Width & "px"
    End Sub
    Private Function get_pinsel_größe() As Integer
        Return CInt(ComboBox1.SelectedItem.ToString.Remove((ComboBox1.SelectedItem.ToString.Length - 2)))
    End Function
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Prozentgröße.Width = get_pinsel_größe()
    End Sub

    Private Sub Farbwahl_Click(sender As Object, e As EventArgs) Handles Farbwahl.Click, Button3.Click
        reset_werkzeuge()
        If Farbwahl.BorderStyle = BorderStyle.None Then
            Farbwahl.BorderStyle = BorderStyle.Fixed3D
            Pic.Cursor = New Cursor(My.Resources.pipette1.Handle)
        Else
            Farbwahl.BorderStyle = BorderStyle.None
            Pic.Cursor = Cursors.Default
        End If
        Pic.Invalidate()
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint


    End Sub

    Private Sub Panel3_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel3.MouseMove, Panel2.MouseMove, Prozentdeckkreft.MouseMove, Prozentgröße.MouseMove
        Dim bar As Panel = CType(sender, Panel)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim mousepos = Math.Min(Math.Max(e.X, 0), bar.ClientSize.Width)
            Dim ziel As Panel
            Select Case bar.Name
                Case Panel2.Name
                    ziel = Prozentgröße
                Case Prozentgröße.Name
                    ziel = Prozentgröße
                Case Panel3.Name
                    ziel = Prozentdeckkreft
                Case Prozentdeckkreft.Name
                    ziel = Prozentdeckkreft
            End Select
            If e.X > 0 Then
                ziel.Width = mousepos
            End If
        End If
    End Sub

    Private Function Get_Deckkraft() As Integer
        Dim ein_prozent = Panel3.Width / 100
        Return CInt(Math.Round(Prozentdeckkreft.Width / ein_prozent, 0))
    End Function

    Private Sub Prozentdeckkreft_Paint(sender As Object, e As PaintEventArgs) Handles Prozentdeckkreft.Paint

    End Sub

    Private Sub Prozentdeckkreft_SizeChanged(sender As Object, e As EventArgs) Handles Prozentdeckkreft.SizeChanged
        Prozent_alpha.Text = Get_Deckkraft() & "%"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If Prozentgröße.Width < Panel2.Width Then
            Prozentgröße.Width += 1
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If Prozentgröße.Width > 0 Then
            Prozentgröße.Width -= 1
        End If
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If Prozentdeckkreft.Width > 0 Then
            Prozentdeckkreft.Width -= 1
        End If
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        If Prozentdeckkreft.Width < Panel3.Width Then
            Prozentdeckkreft.Width += 1
        End If
    End Sub

    Private Sub StiftTP_Click(sender As Object, e As EventArgs) Handles StiftTP.Click

    End Sub

    Private Sub singlePixel_Click(sender As Object, e As EventArgs) Handles singlePixel.Click
        reset_werkzeuge()
        If singlePixel.BorderStyle = BorderStyle.None Then
            singlePixel.BorderStyle = BorderStyle.Fixed3D
            Pic.Cursor = New Cursor(My.Resources.select_pixel.Handle)
        Else
            singlePixel.BorderStyle = BorderStyle.None
            Pic.Cursor = Cursors.Default
        End If
        Pic.Invalidate()
    End Sub

    Private Sub Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Stiftgröße = get_pinsel_größe()
        My.Settings.Stiftalpha = Get_Deckkraft()
        My.Settings.Farbe = AktFarbe.BackColor
    End Sub

    Private Sub skalwith_TextChanged(sender As Object, e As EventArgs) Handles skalwith.TextChanged

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim zahl As Integer = CInt(skalwith.Text)
        Dim zahl2 As Integer = CInt(skalhigh.Text)

        If zahl <= 0 Or zahl2 <= 0 Then
            MsgBox("Ein Bild muss mehr als 1px Groß sein", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            Reset_original(zahl, zahl2)
            ZoomImage(100)
        End If


    End Sub

    Private Sub AktFarbe_BackColorChanged(sender As Object, e As EventArgs) Handles AktFarbe.BackColorChanged
        rgbcolor.Text = "Rot: " & AktFarbe.BackColor.R & "  Grün: " & AktFarbe.BackColor.G & "  Blau: " & AktFarbe.BackColor.B
    End Sub

    Private Sub Pic_Back_Click(sender As Object, e As EventArgs) Handles Pic_Back.Click
        Pic_go_Back()
    End Sub

    Private Sub Pic_forward_Click(sender As Object, e As EventArgs) Handles Pic_forward.Click
        Pic_go_Vor()
    End Sub

    Private Sub Pic_Click(sender As Object, e As EventArgs) Handles Pic.Click

    End Sub

    Private Sub rectangle_Click(sender As Object, e As EventArgs) Handles rectangle.Click
        reset_werkzeuge()
        If rectangle.BorderStyle = BorderStyle.None Then
            rectangle.BorderStyle = BorderStyle.Fixed3D
            Pic.Cursor = Cursors.Cross
        Else
            rectangle.BorderStyle = BorderStyle.None
            Pic.Cursor = Cursors.Default
        End If
        Pic.Invalidate()
    End Sub
End Class

