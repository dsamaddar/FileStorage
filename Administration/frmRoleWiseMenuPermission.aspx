<%@ Page Language="VB" Theme="CommonSkin" MasterPageFile="~/FileStorage.master"
    AutoEventWireup="false" CodeFile="frmRoleWiseMenuPermission.aspx.vb" Inherits="Administration_frmRoleWiseMenuPermission"
    Title=".:File Storage:Role Wise Menu Permission:." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" runat="Server">
    <table style="width: 100%;">
        <tr align="center">
            <td>
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:Panel ID="pnlAvailableProfile" runat="server" Width="100%" 
                    SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4">
                                <div class="widgettitle">
                                    Role Wise Menu Permission</div>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 20px">
                            </td>
                            <td style="width: 150px">
                            </td>
                            <td style="width: 230px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                                Select Role
                            </td>
                            <td>
                                <asp:DropDownList ID="drpRoleList" runat="server" CssClass="InputTxtBox" Width="200px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="styled-button-1" Text="Save Changes" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
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
        <tr align="center">
            <td>
                <asp:Panel ID="pnlMenuPermission" runat="server" Width="100%" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td class="label">
                                Administration
                            </td>
                            <td class="label">
                                Uplod Files</td>
                            <td class="label">
                                Search Files</td>
                            <td class="label">
                                Issue Files</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="grdAdministrationMenu" runat="server" AutoGenerateColumns="False"
                                    CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectAdminMenu" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdminMenuID" runat="server" Text='<%# Bind("MenuID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("MenuName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:GridView ID="grdUploadFiles" runat="server" AutoGenerateColumns="False" 
                                    CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectUploadFileMenu" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUploadFileMenuID" runat="server" Text='<%# Bind("MenuID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("MenuName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:GridView ID="grdSearchFiles" runat="server" AutoGenerateColumns="False" 
                                    CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectSearchFileMenu" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSearchFileMenuID" runat="server" Text='<%# Bind("MenuID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("MenuName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:GridView ID="grdIssueFile" runat="server" 
                                    AutoGenerateColumns="False" CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectIssueFileMenu" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueFileMenuID" runat="server" Text='<%# Bind("MenuID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("MenuName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="label">
                                Return Files</td>
                            <td valign="top">
                            </td>
                            <td valign="top">
                            </td>
                            <td valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="grdReturnFiles" runat="server" AutoGenerateColumns="False" 
                                    CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectReturnFileMenu" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReturnFileMenuID" runat="server" Text='<%# Bind("MenuID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MenuName">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("MenuName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                            </td>
                            <td valign="top">
                            </td>
                            <td valign="top">
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
