using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace FinfrockTools
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            string tabName = "Finfrock Tools";
            application.CreateRibbonTab(tabName);

            // Create a ribbon panel within the custom tab
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Commands");

            // Add the first button
            string firstCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\DualDeckEditor\2023\DualDeckEditorAddin.dll";
            PushButtonData firstButtonData = new PushButtonData("DualDeck Editor", "DualDeck \nEditor", firstCommandAssembly, "DualDeckEditorAddin.DualDeckEditorCommand");
            PushButton firstButton = ribbonPanel.AddItem(firstButtonData) as PushButton;
            firstButton.ToolTip = "Edit DualDeck Parameters.";
            firstButton.LargeImage = new BitmapImage(new Uri("pack://application:,,,/FinfrockTools;component/Images/1_32.png"));

            // Add the second button
            string secondCommandAssembly = @"J:\Autodesk Standards\Revit\Add-Ins\CleanFileAutomation\2023\CleanFileAutomation.dll";
            PushButtonData secondButtonData = new PushButtonData("Clean File Exporter", "Clean File \nExporter", secondCommandAssembly, "CleanFileAutomation.CleanFileCommand");
            PushButton secondButton = ribbonPanel.AddItem(secondButtonData) as PushButton;
            secondButton.ToolTip = "Automatically Export Clean Files.";
            secondButton.LargeImage = new BitmapImage(new Uri("pack://application:,,,/FinfrockTools;component/Images/1_32.png"));

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
