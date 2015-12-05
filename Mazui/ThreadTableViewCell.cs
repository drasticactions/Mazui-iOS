using Foundation;
using System;
using System.CodeDom.Compiler;
using AwfulForumsLibrary.Entity;
using UIKit;

namespace Mazui
{
	partial class ThreadTableViewCell : UITableViewCell
	{
		public ThreadTableViewCell (IntPtr handle) : base (handle)
		{
		}

	    public void BindDataToCell(ForumThreadEntity thread)
	    {
	        ThreadName.Text = thread.Name;
	    }
	}
}
