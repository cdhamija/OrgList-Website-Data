<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="ManageOrganizations.ascx.cs" Inherits="SAI.Modules.SAIOrganizationList.DynamicData.FieldTemplates.ManageOrganizations" %>

<%--<asp:Literal runat="server" ID="Literal1" Text="<%# FieldValueString %>" />--%>
<div>
    <asp:Button ID="btnAddOrg" runat="server" Text="Add Organization" CssClass="dnnPrimaryAction" OnClick="btnAddOrg_Click"/>
</div>
<div>
    <asp:GridView ID="grdOrgs" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="OrgGrid_RowCancelingEdit" OnRowEditing="OrgGrid_RowEditing" OnRowUpdating="OrgGrid_RowUpdating" CssClass="dnnGrid">
        <Columns>
            <asp:BoundField DataField="Organization" HeaderText="Organization Name" />
            <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditOrg" runat="server" ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" AlternateText="Edit" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="btnUpdateOrg" runat="server" ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" AlternateText="Update" CommandName="Update" />
                            <asp:ImageButton ID="btnCancelOrg" runat="server" ImageUrl="~/Icons/Sigma/Cancel_16X16_Standard.png" AlternateText="Cancel" CommandName="Cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
</div>
