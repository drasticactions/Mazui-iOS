using Foundation;
using System;
using System.CodeDom.Compiler;
using Mazui.TableViewSource;
using UIKit;

namespace Mazui
{
	partial class BookmarkTableViewController : UITableViewController
	{
		public BookmarkTableViewController (IntPtr handle) : base (handle)
		{
		}

	    public override async void ViewDidLoad()
	    {
            await Core.Locator.ViewModels.BookmarksPageVm.Initialize();
            //TableView.Source = new BookmarkTableSource();
        }
	}
}
