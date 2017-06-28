Imports Microsoft.VisualBasic
Imports System

Public Class clsUserWiseRole

    Dim _UserWiseRoleID, _UniqueUserID, _RoleID, _EntryBy As String

    Public Property UserWiseRoleID() As String
        Get
            Return _UserWiseRoleID
        End Get
        Set(ByVal value As String)
            _UserWiseRoleID = value
        End Set
    End Property

    Public Property UniqueUserID() As String
        Get
            Return _UniqueUserID
        End Get
        Set(ByVal value As String)
            _UniqueUserID = value
        End Set
    End Property

    Public Property RoleID() As String
        Get
            Return _RoleID
        End Get
        Set(ByVal value As String)
            _RoleID = value
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

End Class
