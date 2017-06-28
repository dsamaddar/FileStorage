
Partial Class Administration_frmRoleWiseMenuPermission
    Inherits System.Web.UI.Page

    Dim MenuData As New clsMenuDataAccess()
    Dim RoleData As New clsRoleDataAccess()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "RoleWiseMenu~") = 0 Then
            Response.Redirect("~\Login.aspx")
        End If

        If Not IsPostBack Then
            ShowRoleList()
            ShowMenuList(grdAdministrationMenu, "Administration")
            ShowMenuList(grdUploadFiles, "UploadFile")
            ShowMenuList(grdSearchFiles, "SearchFile")
            ShowMenuList(grdIssueFile, "IssueFile")
            ShowMenuList(grdReturnFiles, "ReturnFile")
        End If
    End Sub

    Protected Sub ShowMenuList(ByVal grd As GridView, ByVal MenuGroupID As String)
        grd.DataSource = MenuData.fnGetMenuListByGroup(MenuGroupID)
        grd.DataBind()
    End Sub

    Protected Sub ShowRoleList()
        drpRoleList.DataTextField = "RoleName"
        drpRoleList.DataValueField = "RoleID"
        drpRoleList.DataSource = RoleData.fnGetRoleList()
        drpRoleList.DataBind()

        Dim A As New ListItem()

        A.Text = "N\A"
        A.Value = "N\A"

        drpRoleList.Items.Insert(0, A)
    End Sub

    Protected Sub GetMenuPermission(ByVal MenuIDList As String)

        Dim chkSelectAdminMenu, chkSelectUploadFileMenu, chkSelectSearchFileMenu, chkSelectIssueFileMenu, chkSelectReturnFileMenu As New CheckBox()
        Dim lblAdminMenuID, lblUploadFileMenuID, lblSearchFileMenuID, lblIssueFileMenuID, lblReturnFileMenuID As New Label()

        Try
            For Each rw As GridViewRow In grdAdministrationMenu.Rows
                lblAdminMenuID = rw.FindControl("lblAdminMenuID")
                If MenuIDList.Contains(lblAdminMenuID.Text) Then
                    chkSelectAdminMenu = rw.FindControl("chkSelectAdminMenu")
                    chkSelectAdminMenu.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next

            For Each rw As GridViewRow In grdUploadFiles.Rows
                lblUploadFileMenuID = rw.FindControl("lblUploadFileMenuID")
                If MenuIDList.Contains(lblUploadFileMenuID.Text) Then
                    chkSelectUploadFileMenu = rw.FindControl("chkSelectUploadFileMenu")
                    chkSelectUploadFileMenu.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next

            For Each rw As GridViewRow In grdSearchFiles.Rows
                lblSearchFileMenuID = rw.FindControl("lblSearchFileMenuID")
                If MenuIDList.Contains(lblSearchFileMenuID.Text) Then
                    chkSelectSearchFileMenu = rw.FindControl("chkSelectSearchFileMenu")
                    chkSelectSearchFileMenu.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next

            For Each rw As GridViewRow In grdIssueFile.Rows
                lblIssueFileMenuID = rw.FindControl("lblIssueFileMenuID")
                If MenuIDList.Contains(lblIssueFileMenuID.Text) Then
                    chkSelectIssueFileMenu = rw.FindControl("chkSelectIssueFileMenu")
                    chkSelectIssueFileMenu.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next

            For Each rw As GridViewRow In grdReturnFiles.Rows
                lblReturnFileMenuID = rw.FindControl("lblReturnFileMenuID")
                If MenuIDList.Contains(lblReturnFileMenuID.Text) Then
                    chkSelectReturnFileMenu = rw.FindControl("chkSelectReturnFileMenu")
                    chkSelectReturnFileMenu.Checked = True
                    rw.ForeColor = Drawing.Color.Green
                    rw.Font.Bold = True
                End If
            Next
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If drpRoleList.SelectedValue = "N\A" Then
            MessageBox("Select Proper Role")
            Exit Sub
        End If

        Dim MenuIDList As String = ""

        Dim chkSelectAdminMenu, chkSelectUploadFileMenu, chkSelectSearchFileMenu, chkSelectIssueFileMenu, chkSelectReturnFileMenu As New CheckBox()
        Dim lblAdminMenuID, lblUploadFileMenuID, lblSearchFileMenuID, lblIssueFileMenuID, lblReturnFileMenuID As New Label()

        For Each rw As GridViewRow In grdAdministrationMenu.Rows
            chkSelectAdminMenu = rw.FindControl("chkSelectAdminMenu")

            If chkSelectAdminMenu.Checked = True Then
                lblAdminMenuID = rw.FindControl("lblAdminMenuID")
                MenuIDList += lblAdminMenuID.Text & "~"
            End If
        Next

        For Each rw As GridViewRow In grdUploadFiles.Rows
            chkSelectUploadFileMenu = rw.FindControl("chkSelectUploadFileMenu")

            If chkSelectUploadFileMenu.Checked = True Then
                lblUploadFileMenuID = rw.FindControl("lblUploadFileMenuID")
                MenuIDList += lblUploadFileMenuID.Text & "~"
            End If
        Next

        For Each rw As GridViewRow In grdSearchFiles.Rows
            chkSelectSearchFileMenu = rw.FindControl("chkSelectSearchFileMenu")

            If chkSelectSearchFileMenu.Checked = True Then
                lblSearchFileMenuID = rw.FindControl("lblSearchFileMenuID")
                MenuIDList += lblSearchFileMenuID.Text & "~"
            End If
        Next

        For Each rw As GridViewRow In grdIssueFile.Rows
            chkSelectIssueFileMenu = rw.FindControl("chkSelectIssueFileMenu")

            If chkSelectIssueFileMenu.Checked = True Then
                lblIssueFileMenuID = rw.FindControl("lblIssueFileMenuID")
                MenuIDList += lblIssueFileMenuID.Text & "~"
            End If
        Next

        For Each rw As GridViewRow In grdReturnFiles.Rows
            chkSelectReturnFileMenu = rw.FindControl("chkSelectReturnFileMenu")

            If chkSelectReturnFileMenu.Checked = True Then
                lblReturnFileMenuID = rw.FindControl("lblReturnFileMenuID")
                MenuIDList += lblReturnFileMenuID.Text & "~"
            End If
        Next

        Dim Role As New clsRole()

        Role.RoleID = drpRoleList.SelectedValue
        Role.MenuIDList = MenuIDList
        Role.LastUpdatedBy = Session("UserID")

        Dim Check As Integer = RoleData.fnUpdateRolePermission(Role)

        If Check = 1 Then
            MessageBox("Successfully Inserted.")
        Else
            MessageBox("Error Found.")
        End If

    End Sub

    Protected Sub ClearMenuSelection()

        Dim chkSelectAdminMenu, chkSelectUploadFileMenu, chkSelectSearchFileMenu, chkSelectIssueFileMenu, chkSelectReturnFileMenu As New CheckBox()

        For Each rw As GridViewRow In grdAdministrationMenu.Rows
            chkSelectAdminMenu = rw.FindControl("chkSelectAdminMenu")
            If chkSelectAdminMenu.Checked = True Then
                chkSelectAdminMenu.Checked = False
                rw.ForeColor = Drawing.Color.Black
                rw.Font.Bold = False
            End If
        Next

        For Each rw As GridViewRow In grdUploadFiles.Rows
            chkSelectUploadFileMenu = rw.FindControl("chkSelectUploadFileMenu")
            If chkSelectUploadFileMenu.Checked = True Then
                chkSelectUploadFileMenu.Checked = False
                rw.ForeColor = Drawing.Color.Black
                rw.Font.Bold = False
            End If
        Next

        For Each rw As GridViewRow In grdSearchFiles.Rows
            chkSelectSearchFileMenu = rw.FindControl("chkSelectSearchFileMenu")
            If chkSelectSearchFileMenu.Checked = True Then
                chkSelectSearchFileMenu.Checked = False
                rw.ForeColor = Drawing.Color.Black
                rw.Font.Bold = False
            End If
        Next

        For Each rw As GridViewRow In grdIssueFile.Rows
            chkSelectIssueFileMenu = rw.FindControl("chkSelectIssueFileMenu")
            If chkSelectIssueFileMenu.Checked = True Then
                chkSelectIssueFileMenu.Checked = False
                rw.ForeColor = Drawing.Color.Black
                rw.Font.Bold = False
            End If
        Next

        For Each rw As GridViewRow In grdReturnFiles.Rows
            chkSelectReturnFileMenu = rw.FindControl("chkSelectReturnFileMenu")
            If chkSelectReturnFileMenu.Checked = True Then
                chkSelectReturnFileMenu.Checked = False
                rw.ForeColor = Drawing.Color.Black
                rw.Font.Bold = False
            End If
        Next

    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub drpRoleList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpRoleList.SelectedIndexChanged
        ClearMenuSelection()
        If drpRoleList.SelectedValue <> "N\A" Then

            Dim MenuIDList As String = RoleData.fnGetRoleWiseMenuIDs(drpRoleList.SelectedValue)
            GetMenuPermission(MenuIDList)
        Else

            MessageBox("Select Role Properly.")
            Exit Sub
        End If

    End Sub

End Class
