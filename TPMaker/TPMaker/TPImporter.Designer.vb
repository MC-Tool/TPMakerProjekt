<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TPImporter
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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Import_pfad = New System.Windows.Forms.Label()
        Me.search = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(-2, 28)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(395, 401)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Import_pfad
        '
        Me.Import_pfad.AutoSize = True
        Me.Import_pfad.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Import_pfad.Location = New System.Drawing.Point(12, 5)
        Me.Import_pfad.Name = "Import_pfad"
        Me.Import_pfad.Size = New System.Drawing.Size(12, 13)
        Me.Import_pfad.TabIndex = 1
        Me.Import_pfad.Text = "/"
        '
        'search
        '
        Me.search.Location = New System.Drawing.Point(209, 4)
        Me.search.Name = "search"
        Me.search.Size = New System.Drawing.Size(179, 20)
        Me.search.TabIndex = 2
        Me.search.Text = "Suche..."
        '
        'TPImporter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 427)
        Me.Controls.Add(Me.search)
        Me.Controls.Add(Me.Import_pfad)
        Me.Controls.Add(Me.ListView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "TPImporter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TPImporter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents Import_pfad As Label
    Friend WithEvents search As TextBox
End Class
