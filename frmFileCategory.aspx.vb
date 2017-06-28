
Partial Class frmFileCategory
    Inherits System.Web.UI.Page

    Dim FileCategoryData As New clsFileCategory()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ListFileCategories()

            btnAddCategory.Enabled = True
            btnUpdateCategory.Enabled = False
        End If
    End Sub

    Protected Sub btnAddCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click

        If Trim(txtFileCategory.Text) = "" Then
            MessageBox("Provide File Category")
            Exit Sub
        End If

        Try
            Dim FileCategory As New clsFileCategory()
            Dim Result As New clsResult()

            FileCategory.FileCategory = txtFileCategory.Text

            If chkIsActive.Checked = True Then
                FileCategory.IsActive = True
            Else
                FileCategory.IsActive = False
            End If

            FileCategory.EntryBy = Session("UserID")

            Result = FileCategory.fnInsertFileCategory(FileCategory)

            MessageBox(Result.Message)
            If Result.Success = True Then
                ListFileCategories()
                ClearForm()
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
       
    End Sub

    Protected Sub ClearForm()
        hdFldFileCategoryID.Value = ""
        txtFileCategory.Text = ""
        chkIsActive.Checked = False
        btnAddCategory.Enabled = True
        btnUpdateCategory.Enabled = False
        If grdFileCategories.Rows.Count > 0 Then
            grdFileCategories.SelectedIndex = -1
        End If
    End Sub

    Protected Sub ListFileCategories()
        grdFileCategories.DataSource = FileCategoryData.fnListFileCategories()
        grdFileCategories.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub grdFileCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFileCategories.SelectedIndexChanged
        Dim lblFileCategoryID, lblFileCategory, lblIsActive As New Label

        lblFileCategoryID = grdFileCategories.SelectedRow.FindControl("lblFileCategoryID")
        lblFileCategory = grdFileCategories.SelectedRow.FindControl("lblFileCategory")
        lblIsActive = grdFileCategories.SelectedRow.FindControl("lblIsActive")

        hdFldFileCategoryID.Value = lblFileCategoryID.Text
        txtFileCategory.Text = lblFileCategory.Text

        If lblIsActive.Text = "YES" Then
            chkIsActive.Checked = True
        Else
            chkIsActive.Checked = False
        End If

        btnAddCategory.Enabled = False
        btnUpdateCategory.Enabled = True

    End Sub

    Protected Sub btnUpdateCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateCategory.Click
        If Trim(txtFileCategory.Text) = "" Then
            MessageBox("Provide File Category")
            Exit Sub
        End If

        Try
            Dim FileCategory As New clsFileCategory()
            Dim Result As New clsResult()

            FileCategory.FileCategoryID = hdFldFileCategoryID.Value
            FileCategory.FileCategory = txtFileCategory.Text

            If chkIsActive.Checked = True Then
                FileCategory.IsActive = True
            Else
                FileCategory.IsActive = False
            End If

            FileCategory.EntryBy = Session("UserID")

            Result = FileCategory.fnUpdateFileCategory(FileCategory)

            MessageBox(Result.Message)
            If Result.Success = True Then
                ListFileCategories()
                ClearForm()
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

End Class
