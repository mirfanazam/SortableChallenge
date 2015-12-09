Imports Newtonsoft.Json

Public Class Product

    Public product_name As String

    Public manufacturer As String

    Public family As String

    Public model As String

    <JsonProperty("announced-date")>
    Public announced_date As String

End Class
