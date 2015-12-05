using Foundation;
using System;
using System.CodeDom.Compiler;
using AwfulForumsLibrary.Exceptions;
using AwfulForumsLibrary.Manager;
using UIKit;
using static System.String;

namespace Mazui
{

    partial class LoginPageViewController : UIViewController
	{
        public event EventHandler OnLoginSuccess;

        public LoginPageViewController (IntPtr handle) : base (handle)
		{
        }

        partial void LoginButton_TouchUpInside(UIButton sender)
        {
            VerifyUser(sender);
        }

        private async void VerifyUser(UIButton sender)
        {
            //Validate our Username & Password.
            if (IsUserNameValid() && IsPasswordValid())
            {
                bool loginResult;
                var authManager = new AuthenticationManager();
                try
                {
                    loginResult = await authManager.Authenticate(UserNameTextView.Text, PasswordTextView.Text);
                }
                catch (LoginFailedException ex)
                {
                    loginResult = false;
                }
                if (loginResult)
                {
                    //We have successfully authenticated a the user,
                    //Now fire our OnLoginSuccess Event.
                    OnLoginSuccess?.Invoke(sender, new EventArgs());
                }
            }
            else
            {
                new UIAlertView("Login Error", "Bad user name or password", null, "OK", null).Show();
            }
        }

        private bool IsUserNameValid()
        {
            return !IsNullOrEmpty(UserNameTextView.Text.Trim());
        }

        private bool IsPasswordValid()
        {
            return !IsNullOrEmpty(PasswordTextView.Text.Trim());
        }
    }
}
