Imports System.IO
Imports System.Text

Public Class Utility

    Public Shared Function ReadToSeparator(reader As TextReader, separator As String) As String
        Dim sb As New StringBuilder()
        While reader.Peek <> -1
            Dim ch As Char = ChrW(reader.Read())
            sb.Append(ch)
            If TailMatchesSeparator(sb, separator) Then
                sb.Remove(sb.Length - separator.Length, separator.Length)
                Exit While
            End If
        End While
        Return sb.ToString()
    End Function

    Public Shared Function TailMatchesSeparator(sb As StringBuilder, separator As String) As Boolean
        If sb.Length >= separator.Length Then
            Dim i As Integer = sb.Length - 1
            For j As Integer = separator.Length - 1 To 0 Step -1
                If sb(i) <> separator(j) Then
                    Return False
                End If
                i = i - 1
            Next
            Return True
        End If
        Return False
    End Function

End Class
