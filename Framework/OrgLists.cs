using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SAI.Modules.SAIOrganizationList.Components.Framework
{
    public class OrgList
    {
        #region Public Properties
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Organization { get; set; }
        public int FirstLetter { get; set; }
        public int Advisor { get; set; }
        #endregion

        #region Public Static Methods
        public static OrgList Fill(IDataReader reader)
        {
            OrgList orgLists = new OrgList();
            orgLists.Id = (int)reader["Id"];
            orgLists.ModuleId = (int)reader["ModuleId"];
            orgLists.Organization = (string)reader["Organization"];
            orgLists.FirstLetter = (int)reader["FirstLetter"];
            orgLists.Advisor = (int)reader["Advisor"];
            return orgLists;
        }
        #endregion

        internal void Update()
        {
            if (Id <= 0)
            {
                try
                {
                    (new SAIOrganizationListController()).InsertOrgList(this);
                }
                catch (Exception ex)
                {
                    DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                }
            }
        }

    }
    public class OrgLists : List<OrgList>
    {
        internal static OrgLists GetOrgLists(int ModuleId)
        {
            return (new SAIOrganizationListController()).GetOrgLists(ModuleId);
        }
    }
}