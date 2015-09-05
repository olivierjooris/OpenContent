#region Copyright

// 
// Copyright (c) 2015
// by Satrabel
// 

#endregion

#region Using Statements

using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Framework;
using DotNetNuke.Services.Localization;
using System.IO;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.Client;
using DotNetNuke.Entities.Portals;
using Satrabel.OpenContent.Components;


#endregion

namespace Satrabel.OpenContent
{
    public partial class AddEdit : PortalModuleBase
    {
        #region Event Handlers
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //string AddEditControl = PortalController.GetPortalSetting("OpenContent_AddEditControl", ModuleContext.PortalId, "");
            var template = OpenContentUtils.GetTemplateFolder(ModuleContext.Settings);
            if (!string.IsNullOrEmpty(template))
            {
                string TemplateFolder = Path.GetDirectoryName(template).Replace("\\", "/");
                var manifest = OpenContentUtils.GetManifest(TemplateFolder);
                string AddEditControl = manifest.AdditionalEditControl;
                if (!string.IsNullOrEmpty(AddEditControl))
                {
                    var contr = LoadControl(AddEditControl);
                    PortalModuleBase mod = contr as PortalModuleBase;
                    if (mod != null)
                    {
                        mod.ModuleConfiguration = this.ModuleConfiguration;
                        mod.ModuleId = this.ModuleId;
                        mod.LocalResourceFile = this.LocalResourceFile;
                    }
                    this.Controls.Add(contr);
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
            }
        }
        #endregion
        public string CurrentCulture
        {
            get
            {
                return LocaleController.Instance.GetCurrentLocale(PortalId).Code;
            }
        }
    }
}
