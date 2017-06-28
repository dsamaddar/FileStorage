Imports System.Net
Imports System.IO
Imports System.Web.UI
Imports System.Data

Partial Class frmUploadDocuments
    Inherits System.Web.UI.Page

    Dim FileStorageData As New clsFileStorage()
    Dim AgreementData As New clsAgreements()
    Dim FileCategoryData As New clsFileCategory()
    Dim DepartmentData As New clsDepartmentDataAccess()
    Dim EmpData As New clsEmployeeInfoDataAccess()
    Dim CabLocData As New clsCabinetLocation()

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchAgreements(txtSearchText.Text)
    End Sub

    Protected Sub SearchAgreements(ByVal SearchText As String)

        NewSearch()

        grdAgreements.DataSource = AgreementData.fnSearchAgreements(SearchText)
        grdAgreements.DataBind()
    End Sub

    Protected Sub NewSearch()
        If grdAgreements.SelectedIndex <> -1 Then
            grdAgreements.SelectedIndex = -1
        End If

        grdUploadedDocuments.DataSource = ""
        grdUploadedDocuments.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub ListFileCategoriesForEmp(ByVal EmployeeID As String)
        Try
            drpFileCategory.DataTextField = "FileCategory"
            drpFileCategory.DataValueField = "FileCategoryID"
            drpFileCategory.DataSource = FileCategoryData.fnListFileCategoriesForEmp(EmployeeID)
            drpFileCategory.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ListDepartments()
        Try
            drpCustodianDept.DataTextField = "DeptName"
            drpCustodianDept.DataValueField = "DepartmentID"
            drpCustodianDept.DataSource = DepartmentData.fnGetDeptList()
            drpCustodianDept.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub GetUploadedDocumentsByAgrID(ByVal EmployeeID As String, ByVal AgreementID As String)
        grdUploadedDocuments.DataSource = FileStorageData.fnGetUploadedDocsByAgrIDForEmp(EmployeeID, AgreementID)
        grdUploadedDocuments.DataBind()
    End Sub

    Protected Sub ListEmployees()
        Try
            drpResponsiblePerson.DataTextField = "EmployeeName"
            drpResponsiblePerson.DataValueField = "EmployeeID"
            drpResponsiblePerson.DataSource = EmpData.fnGetActiveEmpList()
            drpResponsiblePerson.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ListCabinetLocation()
        Try
            drpCabinetLocation.DataTextField = "CabinetLocation"
            drpCabinetLocation.DataValueField = "CabinetLocationID"
            drpCabinetLocation.DataSource = CabLocData.fnListCabinetLocations()
            drpCabinetLocation.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "UpFile~") = 0 Then
            Response.Redirect("~\Login.aspx")
        End If

        If Not IsPostBack Then
            ListFileCategoriesForEmp(Session("EmployeeID"))
            ListEmployees()
            ListDepartments()
            ListCabinetLocation()
            btnUpload.Enabled = False
        End If
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        Dim FileStorage As New clsFileStorage()
        Dim Result As New clsResult()

        Dim folder As String = ""
        Dim Title As String = ""
        Dim DocExt As String = ""
        Dim DocFullName As String = ""
        Dim DocPrefix As String = ""
        Dim FileSize As Integer = 0
        Dim DocFileName As String = ""

        Try
            If hdFldAgreementID.Value = "" Then
                MessageBox("Select an Agreement first.")
                Exit Sub
            End If

            FileStorage.AgreementID = hdFldAgreementID.Value
            FileStorage.ReferenceNo = txtReferenceNo.Text
            FileStorage.FileName = txtFileName.Text
            FileStorage.FileCategoryID = drpFileCategory.SelectedValue
            FileStorage.CustodianID = drpCustodianDept.SelectedValue
            FileStorage.ResponsibleID = drpResponsiblePerson.SelectedValue
            FileStorage.CabinetLocationID = drpCabinetLocation.SelectedValue
            FileStorage.Remarks = txtRemarks.Text
            FileStorage.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text)

            FileStorage.EntryBy = Session("UserID")

            If flUpDoc.HasFile Then

                folder = ConfigurationManager.AppSettings("InputStorageFiles")

                Title = txtFileName.Text

                Title = Replace(Title, ".", "")

                FileSize = flUpDoc.PostedFile.ContentLength()
                If FileSize > 6500000 Then
                    MessageBox("File size should be within 6MB")
                    Exit Sub
                End If

                DocPrefix = Title.Replace(" ", "")

                DocExt = System.IO.Path.GetExtension(flUpDoc.FileName)
                DocFileName = "FS_" & DateTime.Now.ToString("ddMMyyHHmmss") & DocExt
                DocFullName = folder & DocFileName
                flUpDoc.SaveAs(DocFullName)

                '' Uploading A file stream
                'Dim fs As System.IO.Stream = flUpDoc.PostedFile.InputStream
                'Dim br As New System.IO.BinaryReader(fs)
                'Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Integer))
                'UploadFile(DocFileName, bytes)

                FileStorage.Attachment = DocFileName

                Result = FileStorage.fnInsertFileStorage(FileStorage)

                MessageBox(Result.Message)

                If Result.Success = True Then
                    ClearForm()
                End If

            Else
                MessageBox("Select A Document To Upload.")
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ClearForm()
        txtEffectiveDate.Text = ""
        txtFileName.Text = ""
        txtReferenceNo.Text = ""
        txtRemarks.Text = ""

        drpCabinetLocation.SelectedIndex = -1
        drpCustodianDept.SelectedIndex = -1
        drpFileCategory.SelectedIndex = -1
        drpResponsiblePerson.SelectedIndex = -1
    End Sub

    Protected Sub UploadFile(ByVal FileName As String, ByVal filebyte As Byte())
        Try
            Dim webClient As WebClient = New WebClient()
            Dim FileSavePath As String = Server.MapPath("~\Attachments\") & FileName
            File.WriteAllBytes(FileSavePath, filebyte)
            webClient.UploadFile("http://192.168.0.241/HRMAttachments/Upload.aspx", FileSavePath)
            webClient.Dispose()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub grdAgreements_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAgreements.SelectedIndexChanged
        Dim lblAgreementID As New Label

        lblAgreementID = grdAgreements.SelectedRow.FindControl("lblAgreementID")
        hdFldAgreementID.Value = lblAgreementID.Text

        GetUploadedDocumentsByAgrID(Session("EmployeeID"), lblAgreementID.Text)
        btnUpload.Enabled = True
    End Sub

End Class
