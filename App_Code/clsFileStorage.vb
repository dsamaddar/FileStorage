Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsFileStorage

    Public con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FileStorageConStr").ConnectionString)

    Dim _FileStorageID, _AgreementID, _ReferenceNo, _FileName, _Attachment, _FileCategoryID, _CustodianID, _ResponsibleID, _CabinetLocationID, _Remarks, _
    _LastIssuedBy, _EntryBy As String

    Public Property FileStorageID() As String
        Get
            Return _FileStorageID
        End Get
        Set(ByVal value As String)
            _FileStorageID = value
        End Set
    End Property

    Public Property AgreementID() As String
        Get
            Return _AgreementID
        End Get
        Set(ByVal value As String)
            _AgreementID = value
        End Set
    End Property

    Public Property ReferenceNo() As String
        Get
            Return _ReferenceNo
        End Get
        Set(ByVal value As String)
            _ReferenceNo = value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Public Property Attachment() As String
        Get
            Return _Attachment
        End Get
        Set(ByVal value As String)
            _Attachment = value
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

    Public Property CustodianID() As String
        Get
            Return _CustodianID
        End Get
        Set(ByVal value As String)
            _CustodianID = value
        End Set
    End Property

    Public Property ResponsibleID() As String
        Get
            Return _ResponsibleID
        End Get
        Set(ByVal value As String)
            _ResponsibleID = value
        End Set
    End Property

    Public Property CabinetLocationID() As String
        Get
            Return _CabinetLocationID
        End Get
        Set(ByVal value As String)
            _CabinetLocationID = value
        End Set
    End Property

    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Public Property LastIssuedBy() As String
        Get
            Return _LastIssuedBy
        End Get
        Set(ByVal value As String)
            _LastIssuedBy = value
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

    Dim _IsIssued, _IsActive As Boolean

    Public Property IsIssued() As Boolean
        Get
            Return _IsIssued
        End Get
        Set(ByVal value As Boolean)
            _IsIssued = value
        End Set
    End Property

    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property

    Dim _EffectiveDate, _LastIssuedOn, _EntryDate As DateTime

    Public Property EffectiveDate() As String
        Get
            Return _EffectiveDate
        End Get
        Set(ByVal value As String)
            _EffectiveDate = value
        End Set
    End Property

    Public Property LastIssuedOn() As DateTime
        Get
            Return _LastIssuedOn
        End Get
        Set(ByVal value As DateTime)
            _LastIssuedOn = value
        End Set
    End Property

    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property

    Public Function fnInsertFileStorage(ByVal Storage As clsFileStorage) As clsResult
        Dim Result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spInsertFileStorage", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AgreementID", Storage.AgreementID)
            cmd.Parameters.AddWithValue("@ReferenceNo", Storage.ReferenceNo)
            cmd.Parameters.AddWithValue("@FileName", Storage.FileName)
            cmd.Parameters.AddWithValue("@Attachment", Storage.Attachment)
            cmd.Parameters.AddWithValue("@FileCategoryID", Storage.FileCategoryID)
            cmd.Parameters.AddWithValue("@CustodianID", Storage.CustodianID)
            cmd.Parameters.AddWithValue("@ResponsibleID", Storage.ResponsibleID)
            cmd.Parameters.AddWithValue("@CabinetLocationID", Storage.CabinetLocationID)
            cmd.Parameters.AddWithValue("@Remarks", Storage.Remarks)
            cmd.Parameters.AddWithValue("@EffectiveDate", Storage.EffectiveDate)
            cmd.Parameters.AddWithValue("@IsActive", Storage.IsActive)
            cmd.Parameters.AddWithValue("@EntryBy", Storage.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Result.Success = True
            Result.Message = "File: Added Successfully."
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

    Public Function fnGetUploadedDocsByAgrID(ByVal AgreementID As String) As DataSet
        Dim sp As String = "spGetUploadedDocsByAgrID"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@AgreementID", AgreementID)
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

    Public Function fnGetUploadedDocsByAgrIDForEmp(ByVal EmployeeID As String, ByVal AgreementID As String) As DataSet
        Dim sp As String = "spGetUploadedDocsByAgrIDForEmp"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                cmd.Parameters.AddWithValue("@AgreementID", AgreementID)
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

    Public Function fnSearchStorageFiles(ByVal SearchText As String) As DataSet
        Dim sp As String = "spSearchStorageFiles"
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

    Public Function fnSearchStorageFilesForEmp(ByVal EmployeeID As String, ByVal SearchText As String) As DataSet
        Dim sp As String = "spSearchStorageFilesForEmp"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
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
