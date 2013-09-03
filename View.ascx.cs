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
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Services.Search;
using System.Collections.Generic;


namespace InnovAction.Modules.InnovActionAdvanceSQL
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from InnovActionAdvanceSQLModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : InnovActionAdvanceSQLModuleBase, IActionable
    {
         #region Page Load region

            /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                LiteralQuery.Text = string.Empty;
                LiteralException.Text = string.Empty;
                RenderCodeLiteral("QueryHeader", LiteralHeader);
                RenderCodeDataList("QueryBody");
                RenderCodeLiteral("QueryFooter", LiteralFooter);
            }
            catch (Exception ex)
            {
                 Excepcion( ex);
            }
        }

        /// <summary>
        /// this method does something awesome
        /// </summary>
        void RenderCodeLiteral(string Query, System.Web.UI.WebControls.Literal LiteralAux)
        {

            if (Settings.Contains(Query))
            {

                var mySQL = ReemplazoTags(Query);
                if (mySQL.Trim()!= "") 
                {

                    var myDefaultValues = GetDefaultValues(mySQL);
                    //Reemplazar lo que tenemos:
                    mySQL = ReemplazoKeys(mySQL, myDefaultValues);
                    //Reemplazar lo que NO tenemos:
                    mySQL = ReemplazoDefaultValue(mySQL, myDefaultValues);
                    // Llamo al SQL
                    LlamoSQL_Literal(mySQL, LiteralAux);

                 }
                else
                    AvisoNoHaySqlQuery(Query);
            }

            else
            {
                AvisoNoHaySqlQuery(Query);
            }

        }
        /// <summary>
        /// this method does something awesome
        /// </summary>
        void RenderCodeDataList(string Query)
        {

            if (Settings.Contains(Query))
            {

                var mySQL = ReemplazoTags(Query);
                if (mySQL.Trim() != "")
                {

                    var myDefaultValues = GetDefaultValues(mySQL);
                    //Reemplazar lo que tenemos:
                    mySQL = ReemplazoKeys(mySQL, myDefaultValues);
                    //Reemplazar lo que NO tenemos:
                    mySQL = ReemplazoDefaultValue(mySQL, myDefaultValues);
                    // Llamo al SQL
                    LlamoSQL_Datalits(mySQL);

                }
                else
                    AvisoNoHaySqlQuery(Query);
            }

            else
            {
                AvisoNoHaySqlQuery(Query);
            }

        }

        private void AvisoNoHaySqlQuery(string Query)
        {
            if (IsEditable)
            {
                LiteralException.Visible = true; // prende el literal de Excepcion para mostrarlo
                LiteralException.Text = LiteralException.Text + "<br />Puede Ingresar una query en los settings de " + Query;
            }
        }

        private void LlamoSQL_Literal(string mySQL, System.Web.UI.WebControls.Literal LiteralAux)
        {
            try
            {
                ///////////  Aqui llama al SQL 
                ShowQuery(mySQL);
                var mydatareader = DotNetNuke.Data.DataProvider.Instance().ExecuteSQL(mySQL);
                if (mydatareader.Read())
                    LiteralAux.Text = mydatareader.GetString(0);
                else { LiteralAux.Text = ""; }
            }
            catch (Exception ex) // error 
            {
                ShowQuery(mySQL);
                if (IsEditable) // si es administrador muestra el mensaje
                {
                    LiteralException.Visible = true; // prende el literal de Excepcion para mostrarlo
                    LiteralException.Text = LiteralException.Text + "<br />La query es : " + mySQL + "<br />";
                }
                Excepcion(ex);
            }
        }

        private void LlamoSQL_Datalits(string mySQL)
        {
            try
            {
                ///////////  Aqui llama al SQL 
                ShowQuery(mySQL);
                var mydatareader = DotNetNuke.Data.DataProvider.Instance().ExecuteSQL(mySQL);

                    // LiteralAux.Text = mydatareader.GetString(0);


                    if (Settings.Contains("BodyDirection")) // direccion de repeticion
                    {
                        if (Settings["BodyDirection"].ToString() == "H")
                            DataListBody.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
                        else
                            DataListBody.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Vertical;
                    }
                    var dt = new System.Data.DataTable();
                    dt.Load(mydatareader);
                    DataListBody.DataSource = dt;
                    DataListBody.DataBind();
               
            }
            catch (Exception ex) // error 
            {
                ShowQuery(mySQL);
                if (IsEditable) // si es administrador muestra el mensaje
                {
                    LiteralException.Visible = true; // prende el literal de Excepcion para mostrarlo
                    LiteralException.Text = LiteralException.Text + "<br />La query es : " + mySQL + "<br />";
                }
                Excepcion(ex);
            }
        }


        private static string ReemplazoDefaultValue(string mySQL, Dictionary<string, string> myDefaultValues)
        {
            foreach (var pair in myDefaultValues)
            {
                //reemplazo con defaultValue! =)
                mySQL = mySQL.Replace("[Query:" + pair.Key.ToString() + ":" + pair.Value.ToString() + "]", pair.Value.ToString());
            }
            return mySQL;
        }

        private string ReemplazoKeys(string mySQL, Dictionary<string, string> myDefaultValues)
        {

            foreach (var key in Request.QueryString.AllKeys) // 
            {
                var QueryStringWithoutInjection = CheckInjection(Request.QueryString[key]);

                mySQL = mySQL.Replace("[Query:" + key + "]", QueryStringWithoutInjection);
                try
                {
                    string StringAReemplazar = "[Query:" + key + ":" + myDefaultValues[key] + "]";

                    mySQL = mySQL.Replace(StringAReemplazar, QueryStringWithoutInjection);
                }
                catch { }

            }
            return mySQL;
        }

        private string ReemplazoTags(string Query)
        {
            var mySQL = Settings[Query].ToString();

            mySQL = mySQL.Replace("[DNN:UserID]", UserId.ToString());
            mySQL = mySQL.Replace("[DNN:TabID]", TabId.ToString());
            mySQL = mySQL.Replace("[DNN:ModuleID]", ModuleId.ToString());
            mySQL = mySQL.Replace("[DNN:PortalID]", PortalId.ToString());

            try // si está logueado exite user info
            {
                if (UserId != -1) // si está logueado exite user info
                {
                    mySQL = mySQL.Replace("[DNN:Username]", UserInfo.Username.ToString());
                    mySQL = mySQL.Replace("[DNN:DisplayName]", UserInfo.DisplayName.ToString());
                    mySQL = mySQL.Replace("[DNN:Email]", UserInfo.Email.ToString());
                    mySQL = mySQL.Replace("[DNN:LastName]", UserInfo.LastName.ToString());
                    mySQL = mySQL.Replace("[DNN:FirstName]", UserInfo.FirstName.ToString());
                }
            }

            catch (Exception ex) // error en userinfo
            {
                Excepcion(ex);
            }
            return mySQL;
        }

        Dictionary<string, string> GetDefaultValues(string Query)
        {
            Dictionary<string, string> StuffToReplace = new Dictionary<string, string>();

            string temp = Query;
            while (temp.IndexOf("[Query:") >= 0)
            {
                int myindex = temp.IndexOf("[Query:");
                myindex += 7; //So we add the "[Query:" part
                string Start = temp.Substring(myindex); // ID:5] from misupertabla where hola = [Query:HOLI:CHAUCHIS]
                int squaredBracketIndex = Start.IndexOf("]");
                string KeyAndDefaultValue = Start.Substring(0, squaredBracketIndex); // ID:5
                try
                {
                    string Key = KeyAndDefaultValue.Split(':')[0]; //ID
                    string Value = KeyAndDefaultValue.Split(':')[1]; //5
                    StuffToReplace.Add(Key, Value);
                }
                catch { }
                finally { temp = temp.Substring(myindex); }
                
            }

            return StuffToReplace;
        }

        #endregion

        #region Injection

        //Comparo con la Black list
        //
        private string CheckInjection(string QueryString)
        {
            string[] blackList = {"--",";--",";","/*","*/","@@","@",
                                           "char","nchar","varchar","nvarchar",
                                           "alter","begin","cast","create","cursor","declare","delete","drop","end","exec","execute",
                                           "fetch","insert","kill","open",
                                           "select", "sys","sysobjects","syscolumns",
                                           "table","update"};

            for (int i = 0; i < blackList.Length; i++)
            {
                if ((QueryString.IndexOf(blackList[i].ToLower(), StringComparison.OrdinalIgnoreCase) >= 0))
                {
                   Exception Ex= new Exception("Intento de Inyección de código maligno en Módulo: " +  ModuleId.ToString() + " TabId: " + TabId.ToString());
                   Excepcion(Ex);
                }
                 
            }
            return QueryString;
        }

       #endregion


        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                //Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion
        private void Excepcion(Exception ex)
        {

            //Exceptions.ProcessModuleLoadException(this, ex);
            if (IsEditable) // si es administrador muestra el mensaje
            {
                LiteralException.Visible = true; // prende el literal de Excepcion para mostrarlo
                LiteralException.Text = LiteralException.Text + "<br />Exception thrown.<br />" + ex.Message;
            }
            else
                LiteralException.Visible = false;
            
        }

        private void ShowQuery(string Query)
        {
            if (Settings.Contains("ShowQuery"))
            {
                if (Settings["ShowQuery"].ToString() == "1")
                {
                    LiteralQuery.Visible = true; // prende el literal de Excepcion para mostrarlo
                    LiteralQuery.Text =LiteralQuery.Text+ "<br />Query para Debug: " + Query + "<br />";
                }
                else
                    LiteralQuery.Visible = false;
            }
            else
                LiteralQuery.Visible = false; // lo apago por las dudas.
        }

        void datalistwrite()
        {


        }

        protected void DataListBody_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}