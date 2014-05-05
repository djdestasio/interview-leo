'Leo Vensel 05/03/2014
'TrueFit Missing Number Programming Test
'See TrueFit\MissingNumber.doc for details


Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.String
Imports System.Security.Permissions


Module Module1
    Sub Main()

        'getFile
        Dim filePath As String
        Console.WriteLine("Please enter your file path:")
        filePath = Console.ReadLine()

        Try
            Using MyReader As New Microsoft.VisualBasic.
                            FileIO.TextFieldParser(
                              filePath)

                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")

                Dim currentRow As String()
                Dim compArray(10) As Integer
                Dim i As Integer
                Dim length As Integer

                'write console header
                Console.WriteLine("Missing Numbers")

                'write file header
                Dim sResultsPath As String = My.Settings.resultsPath
                Dim sResultsFile As String = My.Settings.resultsFile

                Dim f2 As New FileIOPermission(FileIOPermissionAccess.Write, sResultsPath)
                f2.AddPathList(FileIOPermissionAccess.Write Or FileIOPermissionAccess.Read, sResultsPath & "\" & sResultsFile)

                If System.IO.Directory.Exists(sResultsPath) Then
                    My.Computer.FileSystem.WriteAllText(sResultsPath & "\" & sResultsFile,
                                       vbCrLf & Date.Now & vbCrLf & "Missing Numbers" & vbCrLf, True)
                Else
                    System.IO.Directory.CreateDirectory(sResultsPath)
                    System.IO.File.Create(sResultsPath & "\" & sResultsFile)
                    My.Computer.FileSystem.WriteAllText(sResultsFile,
                    vbCrLf & Date.Now & vbCrLf & "Missing Numbers" & vbCrLf, True)

                End If

                'read rows
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        length = currentRow.Length - 1
                        ReDim compArray(length)
                        Dim currentField As String
                        i = 0
                        For Each currentField In currentRow
                            'populate the comparison array
                            compArray(i) = CInt(currentField)
                            i = i + 1
                        Next
                    Catch ex As Microsoft.VisualBasic.
                                FileIO.MalformedLineException
                        Console.WriteLine(ex.Message)
                    End Try

                    'loop and compare
                    i = 0
                    For Each num In compArray
                        If i + 1 <= compArray.GetUpperBound(0) Then
                            Array.Sort(compArray)

                            If compArray(i) + 1 <> compArray(i + 1) Then
                                'write missing numbers to results file
                                My.Computer.FileSystem.WriteAllText(sResultsPath & "\" & sResultsFile,
                                    compArray(i) + 1 & vbCrLf, True)

                                Console.WriteLine(compArray(i) + 1)
                            End If
                            i = i + 1
                        End If
                    Next

                End While
            End Using

            'error catching
        Catch dirNotFound As System.IO.DirectoryNotFoundException
            Console.WriteLine(dirNotFound.Message)
        Catch fileNotFound As System.IO.FileNotFoundException
            Console.WriteLine(fileNotFound.Message)
        Catch pathTooLong As System.IO.PathTooLongException
            Console.WriteLine(pathTooLong.Message)
        Catch ioEx As System.IO.IOException
            Console.WriteLine(ioEx.Message)
        Catch security As System.Security.SecurityException
            Console.WriteLine(security.Message)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        'end program
        Console.WriteLine("Press any key to end program.")
        Console.ReadLine()

    End Sub
End Module
