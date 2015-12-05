using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AwfulForumsLibrary.Entity;
using Foundation;
using UIKit;

namespace Mazui.TableViewSource
{
    public class MainForumsTableSource : UITableViewSource
    {
        readonly ObservableCollection<ForumCategoryEntity> _tableItems = Core.Locator.ViewModels.MainForumsPageVm.ForumGroupList;
        string CellIdentifier = "TableCell";

        public override nint NumberOfSections(UITableView tableView)
        {
            return _tableItems.Count;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _tableItems[(int)section].ForumList.Count;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return _tableItems[(int)section].Name;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            var item = _tableItems[indexPath.Section].ForumList[indexPath.Row];

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }
            cell.TextLabel.Text = item.Name;

            return cell;
        }
    }
}
