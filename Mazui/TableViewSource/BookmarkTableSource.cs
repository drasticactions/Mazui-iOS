using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AwfulForumsLibrary.Entity;
using Foundation;
using UIKit;

namespace Mazui.TableViewSource
{
    public class BookmarkTableSource : UITableViewSource
    {
        readonly ObservableCollection<ForumThreadEntity> _tableItems = Core.Locator.ViewModels.BookmarksPageVm.BookmarkedThreads;
        string CellIdentifier = "TableCell";

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _tableItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            ThreadTableViewCell cell = tableView.DequeueReusableCell(CellIdentifier) as ThreadTableViewCell;
            var item = _tableItems[indexPath.Row];
            cell.BindDataToCell(item);
            return cell;
        }
    }
}
