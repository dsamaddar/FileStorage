Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsCabinetLocation

    Public con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FileStorageConStr").ConnectionString)

    Dim _CabinetLocationID, _BranchID, _BranchCode, _FloorNo, _CabinetNo, _ShelfNo, _
    _FolderNo, _CabinetLocation, _EntryBy As String

    Public Property CabinetLocationID() As String
        Get
            Return _CabinetLocationID
        End Get
        Set(ByVal value As String)
            _CabinetLocationID = value
        End Set
    End Property

    Public Property BranchID() As String
        Get
            Return _BranchID
        End Get
        Set(ByVal value As String)
            _BranchID = value
        End Set
    End Property

    Public Property BranchCode() As String
        Get
            Return _BranchCode
        End Get
        Set(ByVal value As String)
            _BranchCode = value
        End Set
    End Property

    Public Property FloorNo() As String
        Get
            Return _FloorNo
        End Get
        Set(ByVal value As String)
            _FloorNo = value
        End Set
    End Property

    Public Property CabinetNo() As String
        Get
            Return _CabinetNo
        End Get
        Set(ByVal value As String)
            _CabinetNo = value
        End Set
    End Property

    Public Property ShelfNo() As String
        Get
            Return _ShelfNo
        End Get
        Set(ByVal value As String)
            _ShelfNo = value
        End Set
    End Property

    Public Property FolderNo() As String
        Get
            Return _FolderNo
        End Get
        Set(ByVal value As String)
            _FolderNo = value
        End Set
    End Property

    Public Property CabinetLocation() As String
        Get
            Return _CabinetLocation
        End Get
        Set(ByVal value As String)
            _CabinetLocation = value
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

    Dim _IsActive As Boolean

    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
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

    Public Function fnInsertCabinetLocation(ByVal Cabinet As clsCabinetLocation) As clsResult
        Dim Result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spInsertCabinetLocation", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@BranchID", Cabinet.BranchID)
            cmd.Parameters.AddWithValue("@BranchCode", Cabinet.BranchCode)
            cmd.Parameters.AddWithValue("@FloorNo", Cabinet.FloorNo)
            cmd.Parameters.AddWithValue("@CabinetNo", Cabinet.CabinetNo)
            cmd.Parameters.AddWithValue("@ShelfNo", Cabinet.ShelfNo)
            cmd.Parameters.AddWithValue("@FolderNo", Cabinet.FolderNo)
            cmd.Parameters.AddWithValue("@EntryBy", Cabinet.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Result.Success = True
            Result.Message = "Cabinet: Added Successfully."
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

    Public Function fnUpdateCabinetLocation(ByVal Cabinet As clsCabinetLocation) As clsResult
        Dim Result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spUpdateCabinetLocation", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@CabinetLocationID", Cabinet.CabinetLocationID)
            cmd.Parameters.AddWithValue("@BranchID", Cabinet.BranchID)
            cmd.Parameters.AddWithValue("@BranchCode", Cabinet.BranchCode)
            cmd.Parameters.AddWithValue("@FloorNo", Cabinet.FloorNo)
            cmd.Parameters.AddWithValue("@CabinetNo", Cabinet.CabinetNo)
            cmd.Parameters.AddWithValue("@ShelfNo", Cabinet.ShelfNo)
            cmd.Parameters.AddWithValue("@FolderNo", Cabinet.FolderNo)
            cmd.Parameters.AddWithValue("@EntryBy", Cabinet.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Result.Success = True
            Result.Message = "Cabinet: Updated Successfully."
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

    Public Function fnListCabinetLocations() As DataSet

        Dim sp As String = "spListCabinetLocations"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
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
