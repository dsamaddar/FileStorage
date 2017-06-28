<%@ Page Language="VB" MasterPageFile="~/FileStorage.master" AutoEventWireup="false"
    Theme="CommonSkin" CodeFile="frmUploadDocuments.aspx.vb" Inherits="frmUploadDocuments"
    Title=".:File Storage:." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="pnlAgreements" runat="server" SkinID="pnlInner">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="3">
                                <div class="widget-title">
                                    Agreement Selection<asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="InputTxtBox" Placeholder="Search by Agreement No\Client Name"
                                    Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="styled-button-1" Text="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div>
                                    <asp:GridView ID="grdAgreements" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:TemplateField HeaderText="AgreementID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAgreementID" runat="server" Text='<%# Bind("AgreementID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AgreementNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AgreementNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ClientName">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ClientName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ClientID">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ClientID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EntryBy" HeaderText="EntryBy" />
                                            <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlUploadFile" runat="server" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4">
                                <div class="widget-title">
                                    Upload Documents</div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdFldAgreementID" runat="server" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label" width="150px">
                                Reference No
                            </td>
                            <td>
                                <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="InputTxtBox" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label" width="150px">
                                File Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileName" runat="server" CssClass="InputTxtBox" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                                Category
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFileCategory" runat="server" CssClass="InputTxtBox" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                                Custodian
                            </td>
                            <td>
                                <asp:DropDownList ID="drpCustodianDept" runat="server" CssClass="InputTxtBox" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                                Responsible
                            </td>
                            <td>
                                <asp:DropDownList ID="drpResponsiblePerson" runat="server" CssClass="InputTxtBox"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td class="label">
                                Location
                            </td>
                            <td>
                                <asp:DropDownList ID="drpCabinetLocation" runat="server" CssClass="InputTxtBox" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                                Remarks
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="InputTxtBox" Height="50px"
                                    TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td class="label">
                                Effective Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="InputTxtBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtEffectiveDate_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtEffectiveDate" Format="dd-MMM-yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                                Attachment
                            </td>
                            <td>
                                <asp:FileUpload ID="flUpDoc" runat="server" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td class="label">
                            </td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" CssClass="styled-button-1" Text="Upload Document" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlUploadedDocuments" runat="server" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <div class="widget-title">
                                    Uploaded Documents</div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="max-height: 200px; max-width: 100%; overflow: auto">
                                    <asp:GridView ID="grdUploadedDocuments" runat="server" CssClass="mGrid" 
                                        AutoGenerateColumns="False" EmptyDataText="No File Found">
                                        <Columns>
                                            <asp:BoundField DataField="FileStorageID" HeaderText="FileStorageID" Visible="False" />
                                            <asp:BoundField DataField="FileName" HeaderText="File" />
                                            <asp:TemplateField HeaderText="Attachment">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hpDocument" runat="server" CssClass="linkbtn" NavigateUrl='<%# ConfigurationManager.AppSettings("OutputStorageFiles")+ Eval("Attachment") %>'
                                                        Target="_blank">View</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Custodian" HeaderText="Custodian" />
                                            <asp:BoundField DataField="Responsible" HeaderText="Responsible" />
                                            <asp:BoundField DataField="CabinetLocation" HeaderText="CabinetLocation" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="Server">
</asp:Content>
