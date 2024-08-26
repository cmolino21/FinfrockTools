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


namespace FinfrockTools
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Get the Revit version
            string revitVersion = application.ControlledApplication.VersionNumber;

            // Create a custom ribbon tab
            string tabName = "Finfrock Tools";
            application.CreateRibbonTab(tabName);

            // Create a ribbon panel within the custom tab
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "DualDecks");

            // Add the first button
            string firstCommandAssembly;
            if (revitVersion.StartsWith("2025"))
            {
                firstCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DualDeckEditor\2025\DualDeckEditorAddin.dll";
            }
            else
            {
                firstCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DualDeckEditor\2021-2024\DualDeckEditorAddin.dll";
            }
            firstCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DualDeckEditor\2021-2024\DualDeckEditorAddin.dll";
            PushButtonData firstButtonData = new PushButtonData("DualDeck Editor", "DualDeck \nEditor", firstCommandAssembly, "DualDeckEditorAddin.DualDeckEditorCommand");
            PushButton firstButton = ribbonPanel.AddItem(firstButtonData) as PushButton;
            firstButton.ToolTip = "Edit DualDeck Parameters.";
            firstButton.LargeImage = ToImageSource(Resource.DD_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);

            // Add the second button
            string secondCommandAssembly;
            // Set the path based on the Revit version
            if (revitVersion.StartsWith("2024"))
            {
                secondCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2024\CleanFileAutomation.dll";
            }
            else if (revitVersion.StartsWith("2025"))
            {
                secondCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2025\CleanFileAutomation.dll";
            }
            else
            {
                secondCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2021-2023\CleanFileAutomation.dll";
            }
            PushButtonData secondButtonData = new PushButtonData("Clean File Exporter", "Clean File \nExporter", secondCommandAssembly, "CleanFileAutomation.CleanFileCommand");
            PushButton secondButton = ribbonPanel.AddItem(secondButtonData) as PushButton;
            secondButton.ToolTip = "Automatically Export Clean Files.";
            secondButton.LargeImage = ToImageSource(Resource.EX_32, FinfrockTools.BitmapSourceConverter.ImageType.Large);

            // Handle the document opened event to check for the DualDeck model
            application.ControlledApplication.DocumentOpened += (sender, args) =>
            {
                Document doc = args.Document;

                // Disable the buttons if the document title does not contain "DUALDECK"
                bool isDualDeckModel = doc != null && doc.Title.ToUpper().Contains("DUALDECK");
                firstButton.Enabled = isDualDeckModel;
                secondButton.Enabled = isDualDeckModel;
            };

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
