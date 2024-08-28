using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinfrockTools.Settings
{
    // Add the Transaction attribute
    [Transaction(TransactionMode.Manual)]
    public class SettingsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Show the settings form with current settings
            SettingsForm settingsForm = new SettingsForm(App.Settings.IsWorksetPromptEnabled, App.Settings.BuildVersion);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Update and save settings
                App.Settings.IsWorksetPromptEnabled = settingsForm.IsWorksetPromptEnabled();
                App.Settings.Save();
            }

            return Result.Succeeded;
        }
    }
}
