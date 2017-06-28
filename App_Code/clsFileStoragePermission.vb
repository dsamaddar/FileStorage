Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsFileStoragePermission

    Public con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FileStorageConStr").ConnectionString)

    Dim _FileStoragePermID, _DepartmentID, _FileCategoryID, _CanUpload, _CanView, _EntryBy, _CanUploadList, _CanViewList As String

    Public Property FileStoragePermID() As String
        Get
            Return _FileStoragePermID
        End Get
        Set(ByVal value As String)
            _FileStoragePermID = value
        End Set
    End Property

    Public Property DepartmentID() As String
        Get
            Return _DepartmentID
        End Get
        Set(ByVal value As String)
            _DepartmentID = value
        End Set
    End Property

    Public Property FileCategoryID() As String
        Get
            Return _FileCategoryID
        End Get
        Set(ByVal value As String)
            _FileCategoryID = value
        End Set
    End Property

    Public Property CanUpload() As String
        Get
            Return _CanUpload
        End Get
        Set(ByVal value As String)
            _CanUpload = value
        End Set
    End Property

    Public Property CanView() As String
        Get
            Return _CanView
        End Get
        Set(ByVal value As String)
            _CanView = value
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

    Public Property CanUploadList() As String
        Get
            Return _CanUploadList
        End Get
        Set(ByVal value As String)
            _CanUploadList = value
        End Set
    End Property

    Public Property CanViewList() As String
        Get
            Return _CanViewList
        End Get
        Set(ByVal value As String)
            _CanViewList = value
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

    Public Property EntryDate() As String
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As String)
            _EntryDate = value
        End Set
    End Property

    Public Function fnGetCanUploadListByDept(ByVal DepartmentID As String) As String
        Dim sp As String = "spGetCanUploadListByDept"
        Dim dr As SqlDataReader
        Dim CanUploadList As String = ""
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("DepartmentID", DepartmentID)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    CanUploadList = dr.Item("CanUploadList")
                End While
                con.Close()
                Return CanUploadList
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

    Public Function fnGetCanViewListByDept(ByVal DepartmentID As String) As String
        Dim sp As String = "spGetCanViewListByDept"
        Dim dr As SqlDataReader
        Dim CanViewList As String = ""
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("DepartmentID", DepartmentID)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    CanViewList = dr.Item("CanViewList")
                End While
                con.Close()
                Return CanViewList
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

    Public Function fnBulkInsertCanUploadFileStoPerm(ByVal DepartmentID As String, ByVal CanUploadList As String, ByVal EntryBy As String) As clsResult
        Dim Result As New clsResult()
        Dim sp As String = "spBulkInsertCanUploadFileStoPerm"
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("DepartmentID", DepartmentID)
                cmd.Parameters.AddWithValue("CanUploadList", CanUploadList)
                cmd.Parameters.AddWithValue("EntryBy", EntryBy)
                cmd.ExecuteNonQuery()
                con.Close()
                Result.Success = True
                Result.Message = "Upload Permission: Added Successfully."
                Return Result
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function


    Public Function fnBulkInsertCanViewFileStoPerm(ByVal DepartmentID As String, ByVal CanViewList As String, ByVal EntryBy As String) As clsResult
        Dim Result As New clsResult()
        Dim sp As String = "spBulkInsertCanViewFileStoPerm"
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("DepartmentID", DepartmentID)
                cmd.Parameters.AddWithValue("CanViewList", CanViewList)
                cmd.Parameters.AddWithValue("EntryBy", EntryBy)
                cmd.ExecuteNonQuery()
                con.Close()
                Result.Success = True
                Result.Message = "View Permission: Added Successfully."
                Return Result
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

End Class
