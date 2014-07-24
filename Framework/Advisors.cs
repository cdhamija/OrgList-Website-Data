using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SAI.Modules.SAIOrganizationList.Components.Framework
{
    public class Advisor
    {

        #region Public Properties
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int StartLetter { get; set; }
        public int EndLetter { get; set; }
        public string AdvisorName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SL { get; set; }
        public string EL { get; set; }
        #endregion

        #region Public Static Methods
        public static Advisor Fill(IDataReader reader)
        {
            reader.Read();
            Advisor advisors = new Advisor();
            advisors.Id = (int)reader["Id"];
            advisors.ModuleId = (int)reader["ModuleId"];
            advisors.StartLetter = (int)reader["StartLetter"];
            advisors.EndLetter = (int)reader["EndLetter"];
            advisors.AdvisorName = (string)reader["AdvisorName"];
            advisors.Email = (string)reader["Email"];
            advisors.Phone = (string)reader["Phone"];
            reader.Close();
            return advisors;
        }
        #endregion

        internal void Update()
        {
            if (Id <= 0)
            {
                try
                {
                    (new SAIOrganizationListController()).InsertAdvisor(this);
                }
                catch (Exception ex)
                {
                    DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                }
            }
        }
    }
    public class Advisors : List<Advisor>
    {
        internal static Advisors GetAdvisors(int ModuleId)
        {
            return (new SAIOrganizationListController()).GetAdvisors(ModuleId);
        }
    }
}