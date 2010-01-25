using System;
using System.Windows.Forms;
using Microsoft.ManagementConsole;
using System.Text;
using MmcAction = Microsoft.ManagementConsole.Action;

namespace Federal
{
    /// <summary>
    /// This class provides list of icons and names in the results pane.
    /// </summary>
    public class SelectionListView : MmcListView
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectionListView()
        {
        }

        /// <summary>
        /// Defines the structure of the list view.
        /// </summary>
        /// <param name="status"></param>
        protected override void OnInitialize(AsyncStatus status)
        {
            base.OnInitialize(status);
            //+ Create a set of columns for use in the list view
            //+ Define the default column title
            Columns[0].Title = "User";
            Columns[0].SetWidth(300);
            //+ Add detail column
            Columns.Add(new MmcListViewColumn("Birthday", 200));
            //+ Set to show all columns
            Mode = MmcListViewMode.Report;
            //+ Set to show refresh as an option
            SelectionData.EnabledStandardVerbs = StandardVerbs.Refresh;
            //+ Load the list with values
            Refresh();
        }

        /// <summary>
        /// Defines actions for selection.
        /// </summary>
        /// <param name="status"></param>
        protected override void OnSelectionChanged(SyncStatus status)
        {
            if (SelectedNodes.Count == 0)
            {
                SelectionData.Clear();
            }
            else
            {
                SelectionData.Update(GetSelectedUsers(), SelectedNodes.Count > 1, null, null);
                SelectionData.ActionsPaneItems.Clear();
                SelectionData.ActionsPaneItems.Add(new MmcAction("Show Selected", "Shows list of selected Users.", -1, "ShowSelected"));
            }
        }

        /// <summary>
        /// Placeholder.
        /// </summary>
        /// <param name="status"></param>
        protected override void OnRefresh(AsyncStatus status)
        {
            MessageBox.Show("The method or operation is not implemented.");
        }

        /// <summary>
        /// Handles menu actions.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="status"></param>
        protected override void OnSelectionAction(MmcAction action, AsyncStatus status)
        {
            switch ((string)action.Tag)
            {
                case "ShowSelected":
                    {
                        ShowSelected();
                        break;
                    }
            }
        }

        /// <summary>
        /// Shows selected items.
        /// </summary>
        private void ShowSelected()
        {
            MessageBox.Show("Selected Users: \n" + GetSelectedUsers());
        }

        /// <summary>
        /// Builds a string of selected users.
        /// </summary>
        /// <returns></returns>
        private string GetSelectedUsers()
        {
            var selectedUsers = new StringBuilder();
            foreach (ResultNode resultNode in SelectedNodes)
            {
                selectedUsers.Append(resultNode.DisplayName + "\n");
            }
            return selectedUsers.ToString();
        }

        /// <summary>
        /// Loads the list view with data.
        /// </summary>
        public void Refresh()
        {
            //+ Clear existing information.
            ResultNodes.Clear();
            //+ Use fictitious data to populate the lists.
            string[][] users = {
                new string[] {"Karen", "February 14th"},
                new string[] {"Sue", "May 5th"},
                new string[] {"Tina", "April 15th"},
                new string[] {"Lisa", "March 27th"},
                new string[] {"Tom", "December 25th"},
                new string[] {"John", "January 1st"},
                new string[] {"Harry", "October 31st"},
                new string[] {"Bob", "July 4th"}
            };
            //+ Populate the list.
            foreach (string[] user in users)
            {
                ResultNode node = new ResultNode();
                node.DisplayName = user[0];
                node.SubItemDisplayNames.Add(user[1]);
                ResultNodes.Add(node);
            }
        }
    }
}