// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Mazui
{
	[Register ("ThreadTableViewCell")]
	partial class ThreadTableViewCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ThreadIcon { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel ThreadName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ThreadIcon != null) {
				ThreadIcon.Dispose ();
				ThreadIcon = null;
			}
			if (ThreadName != null) {
				ThreadName.Dispose ();
				ThreadName = null;
			}
		}
	}
}
