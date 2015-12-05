using Foundation;
using System;
using System.CodeDom.Compiler;
using Mazui.TableViewSource;
using UIKit;

namespace Mazui
{
	partial class MainForumsTableViewController : UITableViewController
	{
		public MainForumsTableViewController (IntPtr handle) : base (handle)
		{
		}

	    public override async void ViewDidLoad()
	    {
	        await Core.Locator.ViewModels.MainForumsPageVm.Initialize();
            TableView.Source = new MainForumsTableSource();
	    }

    }
}
