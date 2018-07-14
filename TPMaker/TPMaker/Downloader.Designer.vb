<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Downloader
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
        Me.path = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.prozent = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.transfer = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'path
        '
        Me.path.Location = New System.Drawing.Point(12, 25)
        Me.path.Name = "path"
        Me.path.Size = New System.Drawing.Size(422, 16)
        Me.path.TabIndex = 7
        Me.path.Text = "---"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Downloade von Server:"
        '
        'prozent
        '
        Me.prozent.AutoSize = True
        Me.prozent.Location = New System.Drawing.Point(388, 99)
        Me.prozent.Name = "prozent"
        Me.prozent.Size = New System.Drawing.Size(16, 13)
        Me.prozent.TabIndex = 5
        Me.prozent.Text = "---"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 63)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(414, 28)
        Me.ProgressBar1.TabIndex = 4
        '
        'transfer
        '
        Me.transfer.Location = New System.Drawing.Point(12, 46)
        Me.transfer.Name = "transfer"
        Me.transfer.Size = New System.Drawing.Size(422, 16)
        Me.transfer.TabIndex = 8
        Me.transfer.Text = "---"
        '
        'Downloader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 118)
        Me.Controls.Add(Me.transfer)
        Me.Controls.Add(Me.path)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.prozent)
        Me.Controls.Add(Me.ProgressBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Downloader"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Downloader"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents path As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents prozent As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents transfer As Label
End Class
