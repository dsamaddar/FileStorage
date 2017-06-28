
Partial Class frmManageAgreements
    Inherits System.Web.UI.Page

    Dim AgreementData As New clsAgreements()

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            Dim Agreements As New clsAgreements()

            If Trim(txtAgreementNo.Text) = "" Then
                MessageBox("Provide Agreement No")
                Exit Sub
            End If

            If Trim(txtClientId.Text) = "" Then
                MessageBox("Provide Client Id")
                Exit Sub
            End If

            If Trim(txtClientName.Text) = "" Then
                MessageBox("Provide Client Name")
                Exit Sub
            End If

            Agreements.ClientID = txtClientId.Text
            Agreements.AgreementNo = txtAgreementNo.Text
            Agreements.ClientName = txtClientName.Text
            Agreements.EntryBy = Session("UserID")

            Dim Result As clsResult = Agreements.fnInsertAgreements(Agreements)

            MessageBox(Result.Message)
            If Result.Success = True Then
                ClearForm()
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub SearchAgreements(ByVal SearchText As String)
        grdAgreements.DataSource = AgreementData.fnSearchAgreements(SearchText)
        grdAgreements.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub ClearForm()
        txtAgreementNo.Text = ""
        txtClientId.Text = ""
        txtClientName.Text = ""
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            btnUpdate.Enabled = False
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchAgreements(txtSearchText.Text)
    End Sub
End Class
