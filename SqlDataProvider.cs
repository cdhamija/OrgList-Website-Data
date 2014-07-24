/*
' Copyright (c) 2013 Christoc.com
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
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
using SAI.Modules.SAIOrganizationList.Components.Framework;

namespace SAI.Modules.SAIOrganizationList.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// 
    /// This concreted data provider class provides the implementation of the abstract methods 
    /// from data dataprovider.cs
    /// 
    /// In most cases you will only modify the Public methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "SAIOrganizationList_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        // used to prefect your database objects (stored procedures, tables, views, etc)
        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private static object GetNull(object field)
        {
            return Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region Public Methods
        #region OrgLists Methods
        public override OrgLists GetOrgLists(int ModuleId)
        {
            OrgLists orgLists_List = new OrgLists();
            SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, "SAIOrganizationListDB_GetOrganizationsByModuleId", ModuleId);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    orgLists_List.Add(GetOrgListById((int)reader["Id"], ModuleId));

                    // this is to be written at update: this.Id = (int)Database.ExecuteScalar(
                }
            }
            reader.Close();
            return orgLists_List;
        }

        public override OrgList GetOrgListById(int Id, int ModuleId)
        {
            OrgList orgLists = new OrgList();
            SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, "SAIOrganizationListDB_GetOrganizationsById", Id);
            if (reader.HasRows)
            {
                reader.Read();
                orgLists = OrgList.Fill(reader);
            }
            reader.Close();
            return orgLists;
        }

        public override OrgList GetOrgListByName(string OrgName, int ModuleId)
        {
            OrgList orgLists = new OrgList();
            SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, "SAIOrganizationListDB_GetOrganizationByName", OrgName);
            if (reader.HasRows)
            {
                reader.Read();
                orgLists = OrgList.Fill(reader);
            }
            reader.Close();
            return orgLists;
        }

        public override OrgList UpdateOrgList(OrgList item)
        {
            item.Id = (int)SqlHelper.ExecuteScalar(ConnectionString,"SAIOrganizationListDB_UpdateOrganizations", item.Id, item.ModuleId, item.Organization, (int)item.Advisor, (int)item.FirstLetter);
            return item;
        }

        public override OrgList InsertOrgList(OrgList item)
        {
            item.Id = (int)SqlHelper.ExecuteScalar(ConnectionString, "SAIOrganizationListDB_SAIOrgs_OrgLists_Insert", item.Id, item.ModuleId, item.Organization, item.Advisor, item.FirstLetter);
            return item;
        }
        #endregion
      
        #region Advisors Methods
        public override Advisors GetAdvisors(int ModuleId)
        {
            Advisors advisors_List = new Advisors();
            SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, "SAIOrganizationListDB_GetAdvisorsByModuleId", ModuleId);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    advisors_List.Add(GetAdvisorById((int)reader["Id"], ModuleId));
                }
            }
            reader.Close();
            return advisors_List;
        }

        public override Advisor GetAdvisorById(int Id, int ModuleId)
        {
            Advisor advisors = new Advisor();
            SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, "SAIOrganizationListDB_GetAdvisorsById", Id);
            if (reader.HasRows)
            {
                advisors = Advisor.Fill(reader);
                if (advisors.StartLetter == 0)
                    advisors.SL = null;
                else
                    advisors.SL = new SAI.Modules.SAIOrganizationList.Components.SAIOrganizationListController().NumberToAlphabet(advisors.StartLetter, true);
                if (advisors.EndLetter == 0)
                    advisors.EL = null;
                else
                    advisors.EL = new SAI.Modules.SAIOrganizationList.Components.SAIOrganizationListController().NumberToAlphabet(advisors.EndLetter, true);
            }
            reader.Close();

            return advisors;
        }

        public override Advisor UpdateAdvisor(Advisor item)
        {
            item.Id = (int)SqlHelper.ExecuteScalar(ConnectionString, "SAIOrganizationListDB_UpdateAdvisors", item.Id, item.ModuleId, (int)item.StartLetter, (int)item.EndLetter, item.AdvisorName, item.Email, (string)item.Phone);
            return item;
        }

        public override Advisor InsertAdvisor(Advisor item)
        {
            if(item.Id <= 0)
            item.Id = (int)SqlHelper.ExecuteScalar(ConnectionString, "SAIOrganizationListDB_SAIOrgs_Advisors_Insert", item.Id, item.ModuleId, item.StartLetter, item.EndLetter, item.AdvisorName, item.Email, item.Phone);
            else
                item.Id = (int)SqlHelper.ExecuteScalar(ConnectionString, "SAIOrganizationListDB_SAIOrgs_Advisors_Insert", item.ModuleId, item.StartLetter, item.EndLetter, item.AdvisorName, item.Email, item.Phone);
            return item;
        }
        #endregion

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}
        #endregion

    }

}