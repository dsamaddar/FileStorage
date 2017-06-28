
Partial Class frmSearchFiles
    Inherits System.Web.UI.Page

    Dim FileStorageData As New clsFileStorage()

    Protected Sub btnSearchFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchFile.Click

        If Trim(txtSearchBox.Text) = "" Then
            MessageBox("Search File with a text.")
            Exit Sub
        End If
        SearchStorageFilesForEmp(Session("EmployeeID"), txtSearchBox.Text)
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub SearchStorageFilesForEmp(ByVal EmployeeID As String, ByVal SearchText As String)
        grdUploadedDocuments.DataSource = FileStorageData.fnSearchStorageFilesForEmp(EmployeeID, SearchText)
        grdUploadedDocuments.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim MenuIDs As String

            MenuIDs = Session("PermittedMenus")

            If InStr(MenuIDs, "SrcFile~") = 0 Then
                Response.Redirect("~\Login.aspx")
            End If
        End If
    End Sub

End Class
