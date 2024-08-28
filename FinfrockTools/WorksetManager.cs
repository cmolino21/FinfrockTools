using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinfrockTools
{
    internal class WorksetManager
    {
        public void PromptUserToSetActiveWorkset(Document doc)
        {
            // Get all worksets
            FilteredWorksetCollector worksetCollector = new FilteredWorksetCollector(doc).OfKind(WorksetKind.UserWorkset);
            IList<Workset> worksets = worksetCollector.ToWorksets();

            // Create a list of workset names
            List<string> worksetNames = worksets.Select(w => w.Name).ToList();

            // Show a dialog to the user to select the active workset
            TaskDialog taskDialog = new TaskDialog("Select Active Workset")
            {
                MainInstruction = "Select the active workset:",
                AllowCancellation = false
            };

            // Create a command link for each workset
            Dictionary<int, Workset> worksetMap = new Dictionary<int, Workset>();
            int commandLinkIndex = 1;
            foreach (Workset workset in worksets)
            {
                taskDialog.AddCommandLink((TaskDialogCommandLinkId)commandLinkIndex, workset.Name);
                worksetMap[commandLinkIndex] = workset;
                commandLinkIndex++;
            }

            // Show the dialog
            TaskDialogResult result = taskDialog.Show();

            // The selected command link ID corresponds to the key in our worksetMap
            int selectedCommandLinkId = (int)result;
            if (worksetMap.TryGetValue(selectedCommandLinkId, out Workset selectedWorkset))
            {
                // Use WorksetTable to set the active workset
                WorksetTable worksetTable = doc.GetWorksetTable();
                using (Transaction trans = new Transaction(doc, "Set Active Workset"))
                {
                    trans.Start();
                    worksetTable.SetActiveWorksetId(selectedWorkset.Id);
                    trans.Commit();
                }
            }
        }
    }
}
