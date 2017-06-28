Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System


Public Class clsDepartmentDataAccess

    Public con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FileStorageConStr").ConnectionString)
    Public con2 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NorthWind").ConnectionString)
    Public conHRM As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("HRMConnectionString").ConnectionString)

#Region " Insert Department "
    Public Function fnInsertDepartment(ByVal DepartmentInfo As clsDepartmentInfo) As Integer
        Try
            Dim cmd As SqlCommand = New SqlCommand("spInsertDepartment", con)
            con.Open()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@DeptName", DepartmentInfo.DeptName)
            cmd.Parameters.AddWithValue("@IsActive", DepartmentInfo.IsActive)
            cmd.Parameters.AddWithValue("@EntryBy", DepartmentInfo.EntryBy)

            cmd.ExecuteNonQuery()
            con.Close()
            Return 1
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return 0
        End Try
    End Function
#End Region

#Region " Update Dept Info By ID "
    Public Function fnUpdateDeptInfoByID(ByVal DepartmentInfo As clsDepartmentInfo) As Integer
        Try
            Dim cmd As SqlCommand = New SqlCommand("spUpdateDeptInfoByID", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@DepartmentID", DepartmentInfo.DepartmentID)
            cmd.Parameters.AddWithValue("@DeptName", DepartmentInfo.DeptName)
            cmd.Parameters.AddWithValue("@IsActive", DepartmentInfo.IsActive)
            cmd.Parameters.AddWithValue("@EntryBy", DepartmentInfo.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Return 1
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return 0
        End Try
    End Function
#End Region

#Region " Get Dept List "

    Public Function fnGetDeptList() As DataSet

        Dim sp As String = "spGetDeptList"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
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

#End Region

#Region " Get Department Details "

    Public Function fnGetDepartmentDetails() As DataSet

        Dim sp As String = "spGetDepartmentDetails"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
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

#End Region

End Class
