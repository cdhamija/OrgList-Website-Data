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
using System.Collections.Generic;
using System.Data;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using SAI.Modules.SAIOrganizationList.Components.Framework;
using SAI.Modules.SAIOrganizationList.Data;
using Microsoft.VisualBasic.FileIO;

namespace SAI.Modules.SAIOrganizationList.Components 
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for SAIOrganizationList
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class SAIOrganizationListController //: IPortable, ISearchable, IUpgradeable
    {
        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<SAIOrganizationListInfo> colSAIOrganizationLists = GetSAIOrganizationLists(ModuleID);
        //if (colSAIOrganizationLists.Count != 0)
        //{
        //    strXML += "<SAIOrganizationLists>";

        //    foreach (SAIOrganizationListInfo objSAIOrganizationList in colSAIOrganizationLists)
        //    {
        //        strXML += "<SAIOrganizationList>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objSAIOrganizationList.Content) + "</content>";
        //        strXML += "</SAIOrganizationList>";
        //    }
        //    strXML += "</SAIOrganizationLists>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlSAIOrganizationLists = DotNetNuke.Common.Globals.GetContent(Content, "SAIOrganizationLists");
        //foreach (XmlNode xmlSAIOrganizationList in xmlSAIOrganizationLists.SelectNodes("SAIOrganizationList"))
        //{
        //    SAIOrganizationListInfo objSAIOrganizationList = new SAIOrganizationListInfo();
        //    objSAIOrganizationList.ModuleId = ModuleID;
        //    objSAIOrganizationList.Content = xmlSAIOrganizationList.SelectSingleNode("content").InnerText;
        //    objSAIOrganizationList.CreatedByUser = UserID;
        //    AddSAIOrganizationList(objSAIOrganizationList);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<SAIOrganizationListInfo> colSAIOrganizationLists = GetSAIOrganizationLists(ModInfo.ModuleID);

        //foreach (SAIOrganizationListInfo objSAIOrganizationList in colSAIOrganizationLists)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objSAIOrganizationList.Content, objSAIOrganizationList.CreatedByUser, objSAIOrganizationList.CreatedDate, ModInfo.ModuleID, objSAIOrganizationList.ItemId.ToString(), objSAIOrganizationList.Content, "ItemId=" + objSAIOrganizationList.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

        /// <summary>
        /// Gets a single OrgLists object based on its database Id and the ModuleId
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public OrgList GetOrgListById(int Id, int ModuleId)
        {
            return DataProvider.Instance().GetOrgListById(Id, ModuleId);
        }

        public OrgList GetOrgListByName(string OrgName, int ModuleId)
        {
            return DataProvider.Instance().GetOrgListByName(OrgName, ModuleId);
        }


        public OrgLists GetOrgLists(int ModuleId)
        {
            return DataProvider.Instance().GetOrgLists(ModuleId);
        }

        /// <summary>
        /// Inserts a new OrgLists object in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public OrgList InsertOrgList(OrgList obj)
        {
            return DataProvider.Instance().InsertOrgList(obj);
        }
        /// <summary>
        /// Updates the OrgLists object in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public OrgList UpdateOrgList(OrgList obj)
        {
            return DataProvider.Instance().UpdateOrgList(obj);
        }

        /// <summary>
        /// Gets a single Advisors object based on its database Id and the ModuleId
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public Advisor GetAdvisorById(int Id, int ModuleId)
        {
            return DataProvider.Instance().GetAdvisorById(Id, ModuleId);
        }

        public Advisors GetAdvisors(int ModuleId)
        {
            return DataProvider.Instance().GetAdvisors(ModuleId);
        }
        /// <summary>
        /// Inserts a new Advisors object in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Advisor InsertAdvisor(Advisor obj)
        {
            return DataProvider.Instance().InsertAdvisor(obj);
        }
        /// <summary>
        /// Updates the Advisors object in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Advisor UpdateAdvisor(Advisor obj)
        {
            return DataProvider.Instance().UpdateAdvisor(obj);
        }

        public Advisor AssignAdvisors(int firstLetter, int ModuleId)
        {
            Advisor advisor = new Advisor();
            if (firstLetter >= 1 && firstLetter <= 8)
            {
                advisor = GetAdvisorById(1, ModuleId);
                return advisor;
            }
            else if (firstLetter >= 9 && firstLetter <= 17)
            {
                advisor = GetAdvisorById(2, ModuleId);
                return advisor;
            }
            else if (firstLetter >= 18 && firstLetter <= 26)
            {
                advisor = GetAdvisorById(3, ModuleId);
                return advisor;
            }
            else
                return null;
        }

        public int AlphabetToNumber(string letter)
        {
            int retVal = 0;
            string col = letter.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        public String NumberToAlphabet(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }
        
    }

}
