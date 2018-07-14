<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Soundplayer
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Trackbar1 = New MetroSuite.MetroTrackbar()
        Me.ProgressBar1 = New MetroSuite.MetroProgressbar()
        Me.ProgressBar2 = New MetroSuite.MetroProgressbar()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.ProgressBar2)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.Trackbar1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(267, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(485, 197)
        Me.Panel1.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(20, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "00:00"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(261, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "R:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(261, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "L:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(17, 32)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(449, 117)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button2.Image = Global.TPMaker.My.Resources.Resources.icons8_Speaker_40px
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.Location = New System.Drawing.Point(134, 79)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(116, 61)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Lautstärke"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.Control
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button4.Image = Global.TPMaker.My.Resources.Resources.icons8_Stop_50px
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button4.Location = New System.Drawing.Point(12, 79)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(116, 61)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "Stop"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.Control
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button3.Image = Global.TPMaker.My.Resources.Resources.icons8_Pause_48px
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button3.Location = New System.Drawing.Point(134, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(116, 61)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Pause"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button1.Image = Global.TPMaker.My.Resources.Resources.icons8_Play_48px
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 61)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Abspielen"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(14, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(247, 28)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Sound: "
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(14, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(236, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Länge:"
        '
        'Trackbar1
        '
        Me.Trackbar1.BackColor = System.Drawing.Color.Transparent
        Me.Trackbar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Trackbar1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer))
        Me.Trackbar1.GradientColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(163, Byte), Integer))
        Me.Trackbar1.HoverColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Trackbar1.LeftColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Trackbar1.Location = New System.Drawing.Point(16, 158)
        Me.Trackbar1.Name = "Trackbar1"
        Me.Trackbar1.RegionColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Trackbar1.RightColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.Trackbar1.Size = New System.Drawing.Size(449, 20)
        Me.Trackbar1.SliderColor = System.Drawing.Color.White
        Me.Trackbar1.TabIndex = 12
        Me.Trackbar1.Value = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ProgressBar1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer))
        Me.ProgressBar1.DefaultColor = System.Drawing.Color.White
        Me.ProgressBar1.GradientColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(163, Byte), Integer))
        Me.ProgressBar1.Location = New System.Drawing.Point(281, 18)
        Me.ProgressBar1.Maximum = 32768
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ProgressBar1.RoundingArc = 11
        Me.ProgressBar1.Size = New System.Drawing.Size(184, 11)
        Me.ProgressBar1.TabIndex = 13
        Me.ProgressBar1.Text = "ProgressBar1"
        Me.ProgressBar1.Value = 0
        '
        'ProgressBar2
        '
        Me.ProgressBar2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ProgressBar2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer))
        Me.ProgressBar2.DefaultColor = System.Drawing.Color.White
        Me.ProgressBar2.GradientColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(163, Byte), Integer))
        Me.ProgressBar2.Location = New System.Drawing.Point(281, 5)
        Me.ProgressBar2.Maximum = 32768
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ProgressBar2.RoundingArc = 11
        Me.ProgressBar2.Size = New System.Drawing.Size(184, 11)
        Me.ProgressBar2.TabIndex = 14
        Me.ProgressBar2.Text = "ProgressBar2"
        Me.ProgressBar2.Value = 0
        '
        'Soundplayer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(759, 213)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Soundplayer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Soundplayer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ProgressBar1 As MetroSuite.MetroProgressbar
    Friend WithEvents Trackbar1 As MetroSuite.MetroTrackbar
    Friend WithEvents ProgressBar2 As MetroSuite.MetroProgressbar
End Class
