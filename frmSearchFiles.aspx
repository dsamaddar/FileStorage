<%@ Page Language="VB" MasterPageFile="~/FileStorage.master" AutoEventWireup="false"
    Theme="CommonSkin" CodeFile="frmSearchFiles.aspx.vb" Inherits="frmSearchFiles"
    Title=".:File Storage:Search Files:." %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="pnlSearchBox" runat="server" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3">
                                <div class="widget-title">
                                    Search Files</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchBox" placeholder="Search by Agreement No/Client/Client Id"
                                    runat="server" CssClass="InputTxtBox" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearchFile" runat="server" Text="Search File" CssClass="styled-button-1" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlUploadedDocuments" runat="server" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <div class="widget-title">
                                    &nbsp;Search Result</div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="max-height: 200px; max-width: 100%; overflow: auto">
                                    <asp:GridView ID="grdUploadedDocuments" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                        EmptyDataText="No File Found">
                                        <Columns>
                                            <asp:BoundField DataField="FileStorageID" HeaderText="FileStorageID" Visible="False" />
                                            <asp:BoundField DataField="AgreementNo" HeaderText="AgreementNo" />
                                            <asp:BoundField DataField="ReferenceNo" HeaderText="ReferenceNo" />
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
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="Server">
</asp:Content>
