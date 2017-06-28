Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsAgreements

    Public con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FileStorageConStr").ConnectionString)

    Dim _AgreementID, _AgreementNo, _ClientID, _ClientName, _EntryBy As String

    Public Property AgreementID() As String
        Get
            Return _AgreementID
        End Get
        Set(ByVal value As String)
            _AgreementID = value
        End Set
    End Property

    Public Property AgreementNo() As String
        Get
            Return _AgreementNo
        End Get
        Set(ByVal value As String)
            _AgreementNo = value
        End Set
    End Property

    Public Property ClientID() As String
        Get
            Return _ClientID
        End Get
        Set(ByVal value As String)
            _ClientID = value
        End Set
    End Property

    Public Property ClientName() As String
        Get
            Return _ClientName
        End Get
        Set(ByVal value As String)
            _ClientName = value
        End Set
    End Property

    Public Property EntryBy() As String
        Get
            Return _EntryBy
        End Get
        Set(ByVal value As String)
            _EntryBy = value
        End Set
    End Property

    Dim _EntryDate As DateTime

    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property

    Public Function fnInsertAgreements(ByVal Agr As clsAgreements) As clsResult
        Dim Result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spInsertAgreements", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgreementNo", Agr.AgreementNo)
            cmd.Parameters.AddWithValue("@ClientID", Agr.ClientID)
            cmd.Parameters.AddWithValue("@ClientName", Agr.ClientName)
            cmd.Parameters.AddWithValue("@EntryBy", Agr.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Result.Success = True
            Result.Message = "Agreement: Added Successfully."
            Return Result
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Result.Success = False
            Result.Message = "Error Found: " & ex.Message
            Return Result
        End Try
    End Function

    Public Function fnUpdateAgreements(ByVal Agr As clsAgreements) As clsResult
        Dim Result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spUpdateAgreements", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgreementID", Agr.AgreementID)
            cmd.Parameters.AddWithValue("@AgreementNo", Agr.AgreementNo)
            cmd.Parameters.AddWithValue("@ClientID", Agr.ClientID)
            cmd.Parameters.AddWithValue("@ClientName", Agr.ClientName)
            cmd.Parameters.AddWithValue("@EntryBy", Agr.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Result.Success = True
            Result.Message = "Agreement: Updated Successfully."
            Return Result
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Result.Success = False
            Result.Message = "Error Found: " & ex.Message
            Return Result
        End Try
    End Function

    Public Function fnSearchAgreements(ByVal SearchText As String) As DataSet

        Dim sp As String = "spSearchAgreements"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@SearchText", SearchText)
                da.SelectCommand = cmd
                da.Fill(ds)
                con.Close()
                Return ds
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

End Class
