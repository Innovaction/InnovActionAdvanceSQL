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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace InnovAction.Modules.InnovActionAdvanceSQL
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from InnovActionAdvanceSQLSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : InnovActionAdvanceSQLModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    //Check for existing settings and use those on this page
                    if (Settings.Contains("QueryHeader"))
                    {
                        Txt_HeaderQuery.Text = Settings["QueryHeader"].ToString();
                    }

                    if (Settings.Contains("QueryBody"))
                    {
                        Txt_BodyQuery.Text = Settings["QueryBody"].ToString();
                    }


                    if (Settings.Contains("BodyDirection"))
                    {
                        RadioButtonDirection.SelectedValue = Settings["BodyDirection"].ToString();
                    }
                    else
                        RadioButtonDirection.SelectedValue = "V";


                    if (Settings.Contains("QueryFooter"))
                    {
                        Txt_FooterQuery.Text = Settings["QueryFooter"].ToString();
                    }


                    if (Settings.Contains("ShowQuery"))
                    {
                        if ( Settings["ShowQuery"].ToString() == "0")
                            CheckBoxMostrarQuery.Checked = false;
                        else
                            CheckBoxMostrarQuery.Checked = true;

                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                ModuleController modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                 //tab module settings
                modules.UpdateTabModuleSetting(TabModuleId, "QueryHeader", Txt_HeaderQuery.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "QueryBody", Txt_BodyQuery.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "BodyDirection", RadioButtonDirection.SelectedItem.Value);
                modules.UpdateTabModuleSetting(TabModuleId, "QueryFooter", Txt_FooterQuery.Text);

                if (CheckBoxMostrarQuery.Checked == true)   
                    modules.UpdateTabModuleSetting(TabModuleId, "ShowQuery", 1.ToString());
                else
                    modules.UpdateTabModuleSetting(TabModuleId, "ShowQuery", 0.ToString());
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
           

        }

        #endregion

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Txt_BodyQuery_TextChanged(object sender, EventArgs e)
        {

        }

        protected void QuerySetting_TextChanged(object sender, EventArgs e)
        {

        }

         protected void RadioButtonDirection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     

     }

}

