/*
' Copyright (c) 2013  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/


using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.VisualBasic.FileIO;
using SAI.Modules.SAIOrganizationList.Components;
using SAI.Modules.SAIOrganizationList.Components.Framework;
using SAI.Modules.SAIOrganizationList.Data;

namespace SAI.Modules.SAIOrganizationList
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditSAIOrganizationList class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from SAIOrganizationListModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ManageAdvisors : SAIOrganizationListModuleBase
    {
        #region Private Properties
        string[] contentRow;
        string[] contentCol;
        string temp = "";
        string line = "";
        int rowCount = 0;
        char c = '"';
        int fl = 0;
        int el = 0;
        
        //string csv_file_path = @"C:\Users\Public\Documents\Orgs.csv";
        #endregion

        #region Private Methods
        private void BindData()
        {
            grdAdvisors.DataSource = Advisors.GetAdvisors(ModuleId);
            grdAdvisors.DataKeyNames = new string[] { "Id" };
            grdAdvisors.DataBind();

        }

        private void UploadCSVData(string csv_file_path)
        {
            if (chkRemove.Checked)
            {
                OrgLists orgLists = new SAIOrganizationListController().GetOrgLists(ModuleId);
                orgLists.RemoveRange(1, orgLists.Count);
            }
                
            Advisors advisors = new Advisors();
            advisors = Advisors.GetAdvisors(ModuleId);
            if (csv_file_path != null)
            {
                StreamReader reader = new StreamReader(csv_file_path);

                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    temp += line + "\n";
                    rowCount++;
                }
                contentCol = temp.Split(new char[] { '\n' });
                for (int i = 0; i <= rowCount - 1; i++)
                {
                    OrgList org = new OrgList();
                    contentRow = contentCol[i].Split(new char[] { ',' }, 3);

                    org.ModuleId = ModuleId;
                    org.Organization = contentRow[1].ToString();

                    if (org.Organization[0] == c)
                    {
                        //org.Organization[0].ToString().Remove(0);
                        //org.Organization.Trim(c);
                        org.Organization[0].ToString().Replace(org.Organization[0], org.Organization[1]);
                    }
                    org.FirstLetter = new SAIOrganizationListController().AlphabetToNumber(org.Organization[0].ToString());
                    //org.Advisor = Convert.ToInt16(contentRow[3]);
                    foreach (Advisor adv in advisors)
                    {
                        int fl = adv.StartLetter;
                        int el = adv.EndLetter;
                        for (int j = fl; j <= el; j++)
                        {
                            if (org.Organization[0].ToString().ToUpper() == new SAIOrganizationListController().NumberToAlphabet(j, true))
                            {
                                org.Advisor = adv.Id;
                            }
                        }
                    }
                    org.Update();
                }
                reader.Close();
                reader.Dispose();
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    BindData();
                
                string addAdvisorURL = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "AdvisorPage", "mid=" + this.ModuleId.ToString());
                string advisorPopupUrl = UrlUtils.PopUpUrl(addAdvisorURL, this, PortalSettings, true, false);

                btnAddAdvisor.OnClientClick = advisorPopupUrl + "; return false";
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            
            if (FileUpload2.HasFile)
            {
                try
                {
                    string path = "D:\\DNN7_New\\DesktopModules\\SAIOrganizationList\\Temp\\" + FileUpload2.FileName;
                    FileUpload2.SaveAs(path);
                    FileInfo info = new FileInfo(path);
                    Label1.Visible = true;
                    Label1.Text = "Done!";
                    if (info.Extension == ".csv")
                    {
                        UploadCSVData(path);
                        try 
                        { 
                            System.IO.File.Delete(path); 
                        }
                        catch (Exception ex)
                        {
                    
                        }
                    }
                    else
                    {
                        frmUploadMessage.Message = "Choose an appropriate .CSV File!";
                        frmUploadMessage.Type = NotificationControl.MessageType.Warning;

                        frmUploadMessage.Show();
                        try
                        {
                            System.IO.File.Delete(path);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                frmUploadMessage.Message = "File not found!";
                frmUploadMessage.Type = NotificationControl.MessageType.Warning;

                frmUploadMessage.Show();
            }
        }

        protected void btnAddAdvisor_Click(object sender, EventArgs e)
        {
            //string redirectUrl = UrlUtils.PopUpUrl(DotNetNuke.Common.Globals.NavigateURL(this.TabId, "AdvisorPage", "mid=" + this.ModuleId.ToString()), this, PortalSettings, true, true, 300, 400);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId, "AdvisorPage", "mid=" + this.ModuleId.ToString()), true);
        }

        protected void grdAdvisors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAdvisors.PageIndex = e.NewPageIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void grdAdvisors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            grdAdvisors.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void grdAdvisors_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            grdAdvisors.EditIndex = -1;
            //Bind data to the GridView control.
            BindData();
        }

        protected void grdAdvisors_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SAIOrganizationListController controller = new SAIOrganizationListController();
            OrgLists orgs = new SAIOrganizationListController().GetOrgLists(ModuleId);
            int advisorID = Convert.ToInt32(e.Keys["Id"].ToString());
            Advisor advisor = (new SAIOrganizationListController()).GetAdvisorById(advisorID, ModuleId);
            advisor.AdvisorName = e.NewValues["AdvisorName"].ToString();
            advisor.Email = e.NewValues["Email"].ToString();
            advisor.Phone = e.NewValues["Phone"].ToString();
            advisor.StartLetter = controller.AlphabetToNumber(e.NewValues["SL"].ToString());
            advisor.EndLetter = controller.AlphabetToNumber(e.NewValues["EL"].ToString());
            fl = advisor.StartLetter;
            el = advisor.EndLetter;
            controller.UpdateAdvisor(advisor);
            if (orgs.Count > 0)
            {
                if (el != 0 || fl != 0)
                {
                    for (int j = fl; j <= el; j++)
                    {
                        foreach (OrgList org in orgs)
                        {
                            if (org.Organization[0].ToString().ToUpper() == new SAIOrganizationListController().NumberToAlphabet(j, true))
                            {
                                
                                org.Advisor = advisor.Id;
                                new SAIOrganizationListController().UpdateOrgList(org);
                            }
                        }
                    }
                }
            }

            grdAdvisors.EditIndex = -1;
            BindData();
        }
        #endregion
    }
}