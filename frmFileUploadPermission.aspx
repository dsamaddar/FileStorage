<%@ Page Language="VB" MasterPageFile="~/FileStorage.master" AutoEventWireup="false"
    Theme="CommonSkin" CodeFile="frmFileUploadPermission.aspx.vb" Inherits="frmFileUploadPermission"
    Title=".:File Storage:Upload Permission:." %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="pnlFileUploadPermission" runat="server" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3">
                                <div class="widget-title">
                                    File Upload Permission</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 33%">
                            </td>
                            <td style="width: 33%">
                            </td>
                            <td style="width: 33%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpDepartments" runat="server" CssClass="InputTxtBox" 
                                    Width="200px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="styled-button-1" Text="Save" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
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
                <asp:Panel ID="pnlFileCategoryPermissions" runat="server" SkinID="pnlInner">
                    <div>
                        <asp:GridView ID="grdFileCategoryPermission" runat="server" AutoGenerateColumns="False"
                            CssClass="mGrid">
                            <Columns>
                                <asp:TemplateField HeaderText="FileCategoryID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileCategoryID" runat="server" Text='<%# Bind("FileCategoryID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FileCategory">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileCategory" runat="server" Text='<%# Bind("FileCategory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CanUpload">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCanUpload" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CanView">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCanView" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
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
