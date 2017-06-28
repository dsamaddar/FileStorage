
Partial Class FileStorage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.UserAgent.IndexOf("AppleWebKit") > 0 Then
            Request.Browser.Adapters.Clear()
        End If

        If Not IsPostBack Then
            lblEmpName.Text = "Welcome " + Session("EmployeeName") + " ! "

            Dim mnu As New Menu
            Dim MenuIDs As String

            mnu = Me.FindControl("mnuChaser")
            MenuIDs = Session("PermittedMenus")
            'Administration(Menu)
            mnu.Items(0).Enabled = IIf(InStr(MenuIDs, "Administration~"), True, False)
            mnu.Items(0).ChildItems(0).Enabled = IIf(InStr(MenuIDs, "MngFileCat~"), True, False)
            mnu.Items(0).ChildItems(1).Enabled = IIf(InStr(MenuIDs, "MngCabLoc~"), True, False)
            mnu.Items(0).ChildItems(2).Enabled = IIf(InStr(MenuIDs, "MngAgr~"), True, False)
            mnu.Items(0).ChildItems(3).Enabled = IIf(InStr(MenuIDs, "RoleMng~"), True, False)
            mnu.Items(0).ChildItems(4).Enabled = IIf(InStr(MenuIDs, "RoleWiseMnu~"), True, False)
            mnu.Items(0).ChildItems(5).Enabled = IIf(InStr(MenuIDs, "UsrWiseRole~"), True, False)
            mnu.Items(0).ChildItems(6).Enabled = IIf(InStr(MenuIDs, "flupPerm~"), True, False)
            ' Administration Menu Ends

            ' Top Level other Menu
            mnu.Items(1).Enabled = IIf(InStr(MenuIDs, "UpFile~"), True, False)
            mnu.Items(2).Enabled = IIf(InStr(MenuIDs, "SrcFile~"), True, False)
            mnu.Items(3).Enabled = IIf(InStr(MenuIDs, "IssueFile~"), True, False)
            mnu.Items(4).Enabled = IIf(InStr(MenuIDs, "ReturnFile~"), True, False)
        End If
    End Sub

End Class

