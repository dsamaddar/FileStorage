<%@ Page Language="VB" MasterPageFile="~/FileStorage.master" AutoEventWireup="false"
    Theme="CommonSkin" CodeFile="frmCabinetLocation.aspx.vb" Inherits="frmCabinetLocation"
    Title=".:File Storage:Cabinet Location:." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" runat="Server">
    <table style="width: 100%;">
        <tr align="center">
            <td style="margin-left: 40px">
                <asp:Panel ID="pnlCabinetLocation" SkinID="pnlInner" runat="server" Width="100%">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3">
                                <div class="widget-title">
                                    Manage Cabinet Location
                                </div>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 20px;">
                            </td>
                            <td style="width: 100px;">
                                <asp:RequiredFieldValidator ID="reqFldFloor" runat="server" ControlToValidate="txtFloor"
                                    Display="None" ErrorMessage="Required: Floor No" ValidationGroup="Cabinet"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="reqFldFloor_valcalout" runat="server" CloseImageUrl="~/Sources/img/valClose.png"
                                    CssClass="customCalloutStyle" Enabled="True" TargetControlID="reqFldFloor" WarningIconImageUrl="~/Sources/img/Valwarning.png">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                            <td style="width: 350px">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 20px;">
                                &nbsp;
                            </td>
                            <td style="width: 150px;">
                                <asp:RequiredFieldValidator ID="reqFldBranchCode" runat="server" ControlToValidate="txtBranchCode"
                                    Display="None" ErrorMessage="Required: Branch Code" ValidationGroup="Cabinet"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="reqFldBranchCode_ValidatorCalloutExtender" runat="server"
                                    CloseImageUrl="~/Sources/img/valClose.png" CssClass="customCalloutStyle" Enabled="True"
                                    TargetControlID="reqFldBranchCode" WarningIconImageUrl="~/Sources/img/Valwarning.png">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                            <td style="width: 350px">
                                <asp:RequiredFieldValidator ID="reqFldCabinetNo" runat="server" ControlToValidate="txtCabinetNo"
                                    Display="None" ErrorMessage="Required: Cabinet No" ValidationGroup="Cabinet"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="reqFldCabinetNo_valcalout" runat="server" CloseImageUrl="~/Sources/img/valClose.png"
                                    CssClass="customCalloutStyle" Enabled="True" TargetControlID="reqFldCabinetNo"
                                    WarningIconImageUrl="~/Sources/img/Valwarning.png">
                                </cc1:ValidatorCalloutExtender>
                                &nbsp;<asp:RequiredFieldValidator ID="reqFldShelfNo" runat="server" ControlToValidate="txtShelfNo"
                                    Display="None" ErrorMessage="Required: Shelf No" ValidationGroup="Cabinet"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="reqFldShelfNo_ValidatorCalloutExtender" runat="server"
                                    CloseImageUrl="~/Sources/img/valClose.png" CssClass="customCalloutStyle" Enabled="True"
                                    TargetControlID="reqFldShelfNo" WarningIconImageUrl="~/Sources/img/Valwarning.png">
                                </cc1:ValidatorCalloutExtender>
                                &nbsp;<asp:RequiredFieldValidator ID="reqFldFolderNo" runat="server" ControlToValidate="txtFolderNo"
                                    Display="None" ErrorMessage="Required: Folder No" ValidationGroup="Cabinet"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="reqFldFolderNo_ValidatorCalloutExtender" runat="server"
                                    CloseImageUrl="~/Sources/img/valClose.png" CssClass="customCalloutStyle" Enabled="True"
                                    TargetControlID="reqFldFolderNo" WarningIconImageUrl="~/Sources/img/Valwarning.png">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                                Cabinet Name
                            </td>
                            <td class="label">
                                <asp:TextBox ID="txtBranchCode" Placeholder="Branch" runat="server" CssClass="InputTxtBox"
                                    Width="50px"></asp:TextBox>
                                &nbsp;
                                <asp:TextBox ID="txtFloor" runat="server" Placeholder="Floor" CssClass="InputTxtBox"
                                    Width="50px"></asp:TextBox>
                                &nbsp;
                                <asp:TextBox ID="txtCabinetNo" runat="server" Placeholder="Cabinet" CssClass="InputTxtBox"
                                    Width="50px"></asp:TextBox>
                                &nbsp;
                                <asp:TextBox ID="txtShelfNo" runat="server" Placeholder="Shelf" CssClass="InputTxtBox"
                                    Width="50px"></asp:TextBox>
                                &nbsp;
                                <asp:TextBox ID="txtFolderNo" runat="server" Placeholder="Folder" CssClass="InputTxtBox"
                                    Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                                Branch
                            </td>
                            <td>
                                <asp:DropDownList ID="drpBranch" runat="server" CssClass="InputTxtBox" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                                Active
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsActive" runat="server" CssClass="chkText" Text="(Check If Active)" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdFldCabinetLocationID" runat="server" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CssClass="styled-button-1" Text="Insert"
                                    ValidationGroup="Cabinet" />
                                &nbsp;<asp:Button ID="btnUpdate" runat="server" CssClass="styled-button-1" Text="Update"
                                    ValidationGroup="Cabinet" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr align="center">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:Panel ID="pnlCabinetLocations" runat="server" SkinID="pnlInner">
                    <div>
                        <asp:GridView ID="grdCabinetLocations" runat="server" AutoGenerateColumns="False"
                            CssClass="mGrid">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:TemplateField HeaderText="CabinetLocationID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCabinetLocationID" runat="server" Text='<%# Bind("CabinetLocationID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BranchID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchID" runat="server" Text='<%# Bind("BranchID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                <asp:TemplateField HeaderText="BranchCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchCode" runat="server" Text='<%# Bind("BranchCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FloorNo" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFloorNo" runat="server" Text='<%# Bind("FloorNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CabinetNo" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCabinetNo" runat="server" Text='<%# Bind("CabinetNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ShelfNo" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShelfNo" runat="server" Text='<%# Bind("ShelfNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FolderNo" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolderNo" runat="server" Text='<%# Bind("FolderNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CabinetLocation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCabinetLocation" runat="server" Text='<%# Bind("CabinetLocation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CabinetLocationID" HeaderText="CabinetLocationID" Visible="False" />
                                <asp:BoundField DataField="EntryBy" HeaderText="EntryBy" />
                                <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptPlaceHolder" runat="Server">
</asp:Content>
