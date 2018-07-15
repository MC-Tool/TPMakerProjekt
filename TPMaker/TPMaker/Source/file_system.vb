Module file_system
    Public Function Datein(Pfad As String) As List(Of String)
        Dim l As New List(Of String)
        For Each item In IO.Directory.GetFiles(infos.Pfad() & Pfad)
            l.Add(item)
        Next
        Return l
    End Function


End Module
