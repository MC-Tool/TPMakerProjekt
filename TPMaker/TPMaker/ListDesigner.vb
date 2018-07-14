Module ListDesigner
    Private Sub Init_Views(Target As ListView)
        Target.HotTracking = True
        Target.MultiSelect = False
        Target.FullRowSelect = True
        Target.HideSelection = True
        Target.View = View.Details
    End Sub
    Public Sub Ini_View_small(Target As ListView)

        Target.Items.Clear()
        Target.Columns.Clear()
        Target.View = View.LargeIcon
        Dim c As New ColumnHeader
        c.Text = ""
        c.Name = "Header"
        c.Width = Target.Width - 5
        Target.Columns.Add(c)
        Init_Views(Target)
    End Sub
End Module
