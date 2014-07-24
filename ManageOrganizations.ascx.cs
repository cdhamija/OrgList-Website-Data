using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.DynamicData;
using SAI.Modules.SAIOrganizationList.Components.Framework;
using SAI.Modules.SAIOrganizationList.Components;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common.Utilities;

namespace SAI.Modules.SAIOrganizationList.DynamicData.FieldTemplates
{
    public partial class ManageOrganizations : SAIOrganizationListModuleBase
    {
        #region Private Methods
        private void BindOrganizations()
        {
            grdOrgs.DataSource = OrgLists.GetOrgLists(ModuleId);
            grdOrgs.DataKeyNames = new string[] { "Id" };
            grdOrgs.DataBind();
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    BindOrganizations();
                string addOrgURL = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "OrganizationPage", "mid=" + this.ModuleId.ToString());
                string OrgPopupUrl = UrlUtils.PopUpUrl(addOrgURL, this, PortalSettings, true, false);

                btnAddOrg.OnClientClick = OrgPopupUrl + "; return false";
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void OrgGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SAIOrganizationListController controller = new SAIOrganizationListController();
            int orgID = Convert.ToInt32(e.Keys["Id"].ToString());
            OrgList orgList = (new SAIOrganizationListController()).GetOrgListById(orgID, ModuleId);
            orgList.Organization = e.NewValues["Organization"].ToString();
            controller.UpdateOrgList(orgList);
            grdOrgs.EditIndex = -1;
            BindOrganizations();
        }

        protected void OrgGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            grdOrgs.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindOrganizations();
        }

        protected void OrgGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            grdOrgs.EditIndex = -1;
            //Bind data to the GridView control.
            BindOrganizations();
        }

        protected void btnAddOrg_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId, "OrganizationPage", "mid=" + this.ModuleId.ToString()), true);
        }
        #endregion
    }
}
