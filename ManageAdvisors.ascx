<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageAdvisors.ascx.cs" Inherits="SAI.Modules.SAIOrganizationList.ManageAdvisors" %>
<%@ Register TagPrefix="dnn" TagName="UrlControl" Src="~/controls/UrlControl.ascx"%>
<%@ Register TagPrefix="SAI" TagName="FormMessage" Src="~/DesktopModules/SAIOrganizationList/NotificationControl.ascx" %>

<asp:HiddenField ID="advisor_Hidden1" runat="server" Value="-1" ClientIDMode="Static" />

<div class="dnnForm" id="panels-demo">
    <div class="dnnFormExpandContent"><a href="">Expand All</a></div>
    <h2 id="advisorGrid" class="dnnFormSectionHead"><a href="#">Advisors</a></h2>
    <fieldset class="dnnClear" style="margin-right:2%">
        <div class="dnnLeft" aria-pressed="undefined">
            <asp:LinkButton ID="btnAddAdvisor" runat="server" OnClick="btnAddAdvisor_Click" Text="Add Advisor" CssClass="dnnPrimaryAction"></asp:LinkButton>
            <br />
            <asp:Label ID="lblDebug" runat="server" />
            <asp:GridView ID="grdAdvisors" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdAdvisors_PageIndexChanging" OnRowCancelingEdit="grdAdvisors_RowCancelingEdit" OnRowEditing="grdAdvisors_RowEditing"   OnRowUpdating="grdAdvisors_RowUpdating" style="margin-right: 7px" CssClass="dnnGrid" Width="719px">
                <Columns>
                    <asp:BoundField DataField="AdvisorName" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone No." />
                    <asp:BoundField DataField="SL" HeaderText="Start Letter" />
                    <asp:BoundField DataField="EL" HeaderText="End Letter" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditAdvisor" runat="server" ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" AlternateText="Edit" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="btnUpdateAdvisor" runat="server" ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" AlternateText="Update" CommandName="Update" />
                            <asp:ImageButton ID="btnCancelAdvisor" runat="server" ImageUrl="~/Icons/Sigma/Cancel_16X16_Standard.png" AlternateText="Cancel" CommandName="Cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>
            <br />
        </div>
    </fieldset>
    <h2 id="Upload" class="dnnFormSectionHead"><a href="#">Choose Data From CSV</a></h2>
    <fieldset class="dnnClear">
        <div class="dnnLeft">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <SAI:FormMessage ID="frmUploadMessage" runat="server" />
                    <asp:FileUpload runat="server" ID="FileUpload2" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="ButtonSubmit" Text="Upload CSV" OnClick="ButtonSubmit_Click" CssClass="dnnPrimaryAction"/>
                    <br />
                    <asp:CheckBox ID="chkRemove" runat="server" Text="Remove Existing Organizations" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ButtonSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </fieldset>
</div>
<asp:Panel ID="pnlMessage" runat="server" CssClass="dnnFormMessage dnnFormInfo" Visible="false">
<asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    </asp:Panel>




















