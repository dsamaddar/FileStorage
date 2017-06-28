
Partial Class frmCabinetLocation
    Inherits System.Web.UI.Page

    Dim Branch As New clsULCBranchDataAccess()
    Dim CabinetData As New clsCabinetLocation()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetBranchList()
            GetCabinetLocations()
            btnUpdate.Enabled = False
        End If
    End Sub

    Protected Sub GetBranchList()
        drpBranch.DataTextField = "ULCBranchName"
        drpBranch.DataValueField = "ULCBranchID"
        drpBranch.DataSource = Branch.fnGetULCBranch()
        drpBranch.DataBind()
    End Sub

    Protected Sub GetCabinetLocations()
        grdCabinetLocations.DataSource = CabinetData.fnListCabinetLocations()
        grdCabinetLocations.DataBind()
    End Sub

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            If Trim(txtBranchCode.Text) = "" Then
                MessageBox("Provide Branch Code")
                Exit Sub
            End If

            If Trim(txtCabinetNo.Text) = "" Then
                MessageBox("Provide Cabinet No")
                Exit Sub
            End If

            If Trim(txtFloor.Text) = "" Then
                MessageBox("Provide Floor No")
                Exit Sub
            End If

            If Trim(txtShelfNo.Text) = "" Then
                MessageBox("Provide Shelf No")
                Exit Sub
            End If

            If Trim(txtFolderNo.Text) = "" Then
                MessageBox("Provide Folder No")
                Exit Sub
            End If

            Dim Cabinet As New clsCabinetLocation()

            Cabinet.BranchID = drpBranch.SelectedValue
            Cabinet.BranchCode = txtBranchCode.Text
            Cabinet.FloorNo = txtFloor.Text
            Cabinet.CabinetNo = txtCabinetNo.Text
            Cabinet.ShelfNo = txtShelfNo.Text
            Cabinet.FolderNo = txtFolderNo.Text

            If chkIsActive.Checked = True Then
                Cabinet.IsActive = True
            Else
                Cabinet.IsActive = False
            End If

            Cabinet.EntryBy = Session("UserID")

            Dim result As clsResult = Cabinet.fnInsertCabinetLocation(Cabinet)
            MessageBox(result.Message)

            If result.Success = True Then
                GetCabinetLocations()
                ClearForm()
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ClearForm()
        txtBranchCode.Text = ""
        txtCabinetNo.Text = ""
        txtFloor.Text = ""
        txtFolderNo.Text = ""
        txtShelfNo.Text = ""
        chkIsActive.Checked = False
        hdFldCabinetLocationID.Value = ""
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If Trim(txtBranchCode.Text) = "" Then
                MessageBox("Provide Branch Code")
                Exit Sub
            End If

            If Trim(txtCabinetNo.Text) = "" Then
                MessageBox("Provide Cabinet No")
                Exit Sub
            End If

            If Trim(txtFloor.Text) = "" Then
                MessageBox("Provide Floor No")
                Exit Sub
            End If

            If Trim(txtShelfNo.Text) = "" Then
                MessageBox("Provide Shelf No")
                Exit Sub
            End If

            If Trim(txtFolderNo.Text) = "" Then
                MessageBox("Provide Folder No")
                Exit Sub
            End If

            Dim Cabinet As New clsCabinetLocation()

            Cabinet.CabinetLocationID = hdFldCabinetLocationID.Value
            Cabinet.BranchID = drpBranch.SelectedValue
            Cabinet.BranchCode = txtBranchCode.Text
            Cabinet.FloorNo = txtFloor.Text
            Cabinet.CabinetNo = txtCabinetNo.Text
            Cabinet.ShelfNo = txtShelfNo.Text
            Cabinet.FolderNo = txtFolderNo.Text

            If chkIsActive.Checked = True Then
                Cabinet.IsActive = True
            Else
                Cabinet.IsActive = False
            End If

            Cabinet.EntryBy = Session("UserID")

            Dim result As clsResult = Cabinet.fnUpdateCabinetLocation(Cabinet)
            MessageBox(result.Message)

            If result.Success = True Then
                GetCabinetLocations()
                ClearForm()
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub grdCabinetLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCabinetLocations.SelectedIndexChanged
        Dim lblCabinetLocationID, lblBranchID, lblBranchCode, lblFloorNo, lblCabinetNo, lblShelfNo, lblFolderNo, lblIsActive As New Label

        lblCabinetLocationID = grdCabinetLocations.SelectedRow.FindControl("lblCabinetLocationID")
        lblBranchID = grdCabinetLocations.SelectedRow.FindControl("lblBranchID")
        lblBranchCode = grdCabinetLocations.SelectedRow.FindControl("lblBranchCode")
        lblFloorNo = grdCabinetLocations.SelectedRow.FindControl("lblFloorNo")
        lblCabinetNo = grdCabinetLocations.SelectedRow.FindControl("lblCabinetNo")
        lblShelfNo = grdCabinetLocations.SelectedRow.FindControl("lblShelfNo")
        lblFolderNo = grdCabinetLocations.SelectedRow.FindControl("lblFolderNo")
        lblIsActive = grdCabinetLocations.SelectedRow.FindControl("lblIsActive")

        hdFldCabinetLocationID.Value = lblCabinetLocationID.Text
        txtBranchCode.Text = lblBranchCode.Text
        txtCabinetNo.Text = lblCabinetNo.Text
        txtFloor.Text = lblFloorNo.Text
        txtFolderNo.Text = lblFolderNo.Text
        txtShelfNo.Text = lblShelfNo.Text
        drpBranch.SelectedValue = lblBranchID.Text

        If lblIsActive.Text = "YES" Then
            chkIsActive.Checked = True
        Else
            chkIsActive.Checked = False
        End If

        btnInsert.Enabled = False
        btnUpdate.Enabled = True

    End Sub

End Class
