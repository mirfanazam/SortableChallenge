Imports System.IO
Imports Newtonsoft.Json

Module Module1

    Sub Main(ByVal args As String())

        Console.WriteLine("Starting...")

        'Check if parameters are specified
        If args.Length < 3 Then
            Console.WriteLine("Insufficient command line parameters - Example: productfile listingfile outputfile")
            End
        End If

        'Verify if files exist and paths are valid
        Try
            If Not File.Exists(args(0)) Then
                Console.WriteLine("Product file not recognized.")
                End
            End If
            If Not File.Exists(args(1)) Then
                Console.WriteLine("Listing file not recognized.")
                End
            End If
        Catch ex As Exception
            Console.WriteLine("Unrecognized files. Please verify paths.")
            End
        End Try

        Dim ProductFileName As String = args(0) '"C:\Users\mirfanazam\Downloads\challenge_data_20110429\products.txt"
        Dim ListingFileName As String = args(1) '"C:\Users\mirfanazam\Downloads\challenge_data_20110429\listings.txt"
        Dim OutputFileName As String = args(2) '"C:\Users\mirfanazam\Downloads\challenge_data_20110429\output.txt"

        'Set the characters that seperate each JSON object in the files
        Dim separator As String = vbLf
        Dim objListings As New List(Of Listing)
        Dim objProducts As New List(Of Product)

        Try
            'If output file exsits then delete it
            If File.Exists(OutputFileName) Then
                File.Delete(OutputFileName)
            End If
        Catch ex As Exception

        End Try

        Try
            'Fetch Listings
            Console.WriteLine("Reading listing file...")
            objListings = ReadListingFile(ListingFileName, separator)
            Console.Write(objListings.Count & " listings" & vbCrLf)
        Catch ex As Exception
            Console.WriteLine("Unrecognized listing file.")
        End Try


        Try
            'Fetch products
            Console.WriteLine("Reading product file...")
            objProducts = ReadProductFile(ProductFileName, separator)
            Console.Write(objProducts.Count & " products " & vbCrLf)
        Catch ex As Exception
            Console.WriteLine("Unrecognized product file.")
        End Try

        Dim objResult As New List(Of ResultListing)

        'Iterate through all products
        For Each product In objProducts

            'Get listings matching model name
            Dim result = objListings.Where(Function(listing, index) (listing.title.Contains(product.model))).ToList()

            'Add matched listings to result list
            Dim r As New ResultListing
            r.product_name = product.product_name
            r.listings = result

            File.AppendAllText(OutputFileName, JsonConvert.SerializeObject(r))

            Console.Write(".")

        Next

        Console.WriteLine(vbCrLf & "Complete....Output file saved at " & OutputFileName)

    End Sub

    Function ReadListingFile(fileName As String, separator As String) As List(Of Listing)
        Dim objects As New List(Of Listing)
        Using sr As New StreamReader(fileName)
            Dim json As String
            Do
                json = Utility.ReadToSeparator(sr, separator)
                If json.Length > 0 Then
                    objects.Add(JsonConvert.DeserializeObject(Of Listing)(json))
                End If
            Loop Until json.Length = 0
        End Using
        Return objects
    End Function

    Function ReadProductFile(fileName As String, separator As String) As List(Of Product)
        Dim objects As New List(Of Product)
        Using sr As New StreamReader(fileName)
            Dim json As String
            Do
                json = Utility.ReadToSeparator(sr, separator)
                If json.Length > 0 Then
                    objects.Add(JsonConvert.DeserializeObject(Of Product)(json))
                End If
            Loop Until json.Length = 0
        End Using
        Return objects
    End Function

End Module
