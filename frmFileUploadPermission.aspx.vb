
Partial Class frmFileUploadPermission
    Inherits System.Web.UI.Page

    Dim FileCategory As New clsFileCategory()
    Dim FileStoragePerm As New clsFileStoragePermission()
    Dim DeptData As New clsDepartmentDataAccess()

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lblFileCategoryID As New Label
        Dim chkCanUpload, chkCanView As New CheckBox
        Dim Result As New clsResult()

        Try
            Dim CanUploadList As String = ""
            Dim CanViewList As String = ""

            For Each rw As GridViewRow In grdFileCategoryPermission.Rows
                chkCanUpload = rw.FindControl("chkCanUpload")

                If chkCanUpload.Checked = True Then
                    lblFileCategoryID = rw.FindControl("lblFileCategoryID")
                    CanUploadList += lblFileCategoryID.Text & "~"
                End If
            Next

            For Each rw As GridViewRow In grdFileCategoryPermission.Rows
                chkCanView = rw.FindControl("chkCanView")

                If chkCanView.Checked = True Then
                    lblFileCategoryID = rw.FindControl("lblFileCategoryID")
                    CanViewList += lblFileCategoryID.Text & "~"
                End If
            Next

            If CanUploadList <> "" Then
                Result = FileStoragePerm.fnBulkInsertCanUploadFileStoPerm(drpDepartments.SelectedValue, CanUploadList, Session("UserID"))

                If Result.Success = True Then
                    MessageBox(Result.Message)
                End If
            End If

            If CanViewList <> "" Then
                Result = FileStoragePerm.fnBulkInsertCanViewFileStoPerm(drpDepartments.SelectedValue, CanViewList, Session("UserID"))

                If Result.Success = True Then
                    MessageBox(Result.Message)
                End If
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim MenuIDs As String

            MenuIDs = Session("PermittedMenus")

            If InStr(MenuIDs, "flupPerm~") = 0 Then
                Response.Redirect("~\Login.aspx")
            End If

            GetFileCategoryLists()
            GetDeptList()
        End If
    End Sub

    Protected Sub GetDeptList()
        drpDepartments.DataTextField = "DeptName"
        drpDepartments.DataValueField = "DepartmentID"
        drpDepartments.DataSource = DeptData.fnGetDeptList()
        drpDepartments.DataBind()
    End Sub

    Protected Sub GetFileCategoryLists()
        grdFileCategoryPermission.DataSource = FileCategory.fnListFileCategories()
        grdFileCategoryPermission.DataBind()
    End Sub

    Protected Sub drpDepartments_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpDepartments.SelectedIndexChanged
        Dim lblFileCategoryID As New Label
        Dim chkCanUpload, chkCanView As New CheckBox

        Try
            ClearGrid()

            Dim CanUploadList As String = FileStoragePerm.fnGetCanUploadListByDept(drpDepartments.SelectedValue)
            Dim CanViewList As String = FileStoragePerm.fnGetCanViewListByDept(drpDepartments.SelectedValue)

            For Each rw As GridViewRow In grdFileCategoryPermission.Rows
                lblFileCategoryID = rw.FindControl("lblFileCategoryID")
                If CanUploadList.Contains(lblFileCategoryID.Text) Then
                    chkCanUpload = rw.FindControl("chkCanUpload")
                    chkCanUpload.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next

            For Each rw As GridViewRow In grdFileCategoryPermission.Rows
                lblFileCategoryID = rw.FindControl("lblFileCategoryID")
                If CanViewList.Contains(lblFileCategoryID.Text) Then
                    chkCanView = rw.FindControl("chkCanView")
                    chkCanView.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next

        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ClearGrid()
        Dim chkCanUpload, chkCanView As New CheckBox

        For Each rw As GridViewRow In grdFileCategoryPermission.Rows
            chkCanUpload = rw.FindControl("chkCanUpload")
            If chkCanUpload.Checked = True Then
                chkCanUpload.Checked = False
            End If
        Next

        For Each rw As GridViewRow In grdFileCategoryPermission.Rows
            chkCanView = rw.FindControl("chkCanView")
            If chkCanView.Checked = True Then
                chkCanView.Checked = False
            End If
        Next
    End Sub

End Class
