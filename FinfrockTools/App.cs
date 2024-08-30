using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Private.InfoCenter;
using System.IO;
using static FinfrockTools.BitmapSourceConverter;
using System.Windows.Forms;
using FinfrockTools.Settings;


namespace FinfrockTools
{
    public class App : IExternalApplication
    {
        private WorksetManager _worksetManager;
        public static AddinSettings Settings { get; private set; }

        public Result OnStartup(UIControlledApplication application)
        {
            _worksetManager = new WorksetManager();

            // Load settings
            Settings = AddinSettings.Load();

            // Get the Revit version
            string revitVersion = application.ControlledApplication.VersionNumber;

            // Create a custom ribbon tab
            string tabName = "Finfrock Tools";
            application.CreateRibbonTab(tabName);

            RibbonPanel generalPanel = application.CreateRibbonPanel(tabName, "General");

            // Add the settings button
            PushButtonData settingsButtonData = new PushButtonData("Settings", "Settings", Assembly.GetExecutingAssembly().Location, "FinfrockTools.Settings.SettingsCommand");
            PushButton settingsButton = generalPanel.AddItem(settingsButtonData) as PushButton;
            settingsButton.ToolTip = "Configure settings for Finfrock Tools.";
            settingsButton.LargeImage = ToImageSource(Resource.Settings_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);

            // Create a ribbon panel within the custom tab
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "DualDecks");

            // Add the DDEditor button
            string DDEditorCommandAssembly;
            if (revitVersion.StartsWith("2025"))
            {
                DDEditorCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DualDeckEditor\2025\DualDeckEditorAddin.dll";
            }
            else
            {
                DDEditorCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DualDeckEditor\2021-2024\DualDeckEditorAddin.dll";
            }
            PushButtonData DDEditorButtonData = new PushButtonData("DualDeck Editor", "DualDeck \nEditor", DDEditorCommandAssembly, "DualDeckEditorAddin.DualDeckEditorCommand");
            PushButton DDEditorButton = ribbonPanel.AddItem(DDEditorButtonData) as PushButton;
            DDEditorButton.ToolTip = "Edit DualDeck Parameters.";
            DDEditorButton.LargeImage = ToImageSource(Resource.DD_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);

            // Add the DDAssemblyMaker Button
            string DDAssemblyMakerCommandAssembly;
            if (revitVersion.StartsWith("2025"))
            {
                DDAssemblyMakerCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DDAssemblyMaker\2025\DDAssemblyMaker.dll";
            }
            else if (revitVersion.StartsWith("2024"))
            {
                DDAssemblyMakerCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DDAssemblyMaker\2024\DDAssemblyMaker.dll";
            }
            else
            {
                DDAssemblyMakerCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DDAssemblyMaker\2021-2023\DDAssemblyMaker.dll";
            }
            PushButtonData DDAssemblyMakerButtonData = new PushButtonData("DualDeck Assembly Maker", "DualDeck \nAssembly Maker", DDAssemblyMakerCommandAssembly, "DDAssemblyMaker.DDAssemblyMaker");
            PushButton DDAssemblyMakerButton = ribbonPanel.AddItem(DDAssemblyMakerButtonData) as PushButton;
            DDAssemblyMakerButton.ToolTip = "Create Dualdeck Assemblies.";
            DDAssemblyMakerButton.LargeImage = ToImageSource(Resource.DD_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);

            // Add the CleanFileAutomation button
            string CFAutomationCommandAssembly;
            // Set the path based on the Revit version
            if (revitVersion.StartsWith("2024"))
            {
                CFAutomationCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2024\CleanFileAutomation.dll";
            }
            else if (revitVersion.StartsWith("2025"))
            {
                CFAutomationCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2025\CleanFileAutomation.dll";
            }
            else
            {
                CFAutomationCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2021-2023\CleanFileAutomation.dll";
            }
            PushButtonData CFAutomationButtonData = new PushButtonData("Clean File Exporter", "Clean File \nExporter", CFAutomationCommandAssembly, "CleanFileAutomation.CleanFileCommand");
            PushButton CFAutomationButton = ribbonPanel.AddItem(CFAutomationButtonData) as PushButton;
            CFAutomationButton.ToolTip = "Automatically Export Clean Files.";
            CFAutomationButton.LargeImage = ToImageSource(Resource.EX_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);




            // Create a ribbon panel within the custom tab
            RibbonPanel ribbonPanelMEP = application.CreateRibbonPanel(tabName, "MEP");

            // Add the DDEditor button
            string DryerExhaustCommandAssembly;
            if (revitVersion.StartsWith("2025"))
            {
                DryerExhaustCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\ExhaustLengthChecker\2025\ExhaustLengthChecker.dll";
            }
            else
            {
                DryerExhaustCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\ExhaustLengthChecker\2021-2024\ExhaustLengthChecker.dll";
            }
            PushButtonData DryerExhaustButtonData = new PushButtonData("Dryer Exhaust Calculator", "Dryer Ex \nCalculator", DryerExhaustCommandAssembly, "ExhaustLengthChecker.CalculateDuctLength");
            PushButton DryerExhaustButton = ribbonPanelMEP.AddItem(DryerExhaustButtonData) as PushButton;
            DryerExhaustButton.ToolTip = "Calculate the equivalent length of dryer exhausts.";
            DryerExhaustButton.LargeImage = ToImageSource(Resource.DD_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);








            // Handle the document opened event to check for the DualDeck model
            application.ControlledApplication.DocumentOpened += (sender, args) =>
            {
                Document doc = args.Document;

                // Disable the buttons if the document title does not contain "DUALDECK"
                bool isDualDeckModel = doc != null && doc.Title.ToUpper().Contains("DUALDECK");
                DDEditorButton.Enabled = isDualDeckModel;
                CFAutomationButton.Enabled = isDualDeckModel;
                DDAssemblyMakerButton.Enabled = isDualDeckModel;

                // Prompt the user to confirm the active workset if enabled
                if (Settings.IsWorksetPromptEnabled && doc != null)
                {
                    _worksetManager.PromptUserToSetActiveWorkset(doc);
                }
            };

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
