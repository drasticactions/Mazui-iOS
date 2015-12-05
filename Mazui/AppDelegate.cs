using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AwfulForumsLibrary.Manager;
using AwfulForumsLibrary.Tools;
using Foundation;
using Mazui.Core;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;

namespace Mazui
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public UIStoryboard MainStoryboard => UIStoryboard.FromName("MazuiStoryboard", NSBundle.MainBundle);

        public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
        {
            return storyboard.InstantiateViewController(viewControllerName);
        }

        //Sets the RootViewController of the Apps main window with an option for animation.
        public void SetRootViewController(UIViewController rootViewController, bool animate)
        {
            if (animate)
            {
                var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;

                Window.RootViewController = rootViewController;
                UIView.Transition(Window, 0.5, transitionType,
                                  () => Window.RootViewController = rootViewController,
                                  null);
            }
            else
            {
                Window.RootViewController = rootViewController;
            }
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var dbLocation = "forums.db3";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, dbLocation);
            App.Init(new SQLitePlatformIOS(), path);
            var localStorageManager = new LocalStorageManager();
            Task<CookieContainer> cookieContainerAsync = Task.Run(() => localStorageManager.LoadCookie("SACookies2.txt"));
            var cookieContainer = cookieContainerAsync.Result;
            bool cookieTest = true;
            foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(Constants.CookieDomainUrl)))
            {
                if (cookie.Expired)
                {
                    cookieTest = false;
                }
            }

            if (cookieContainer.Count > 0)
            {
                //We are already authenticated, so go to the main tab bar controller;
                var tabBarController = GetViewController(MainStoryboard, "MainPageTabController");
                SetRootViewController(tabBarController, false);
            }
            else
            {
                //User needs to log in, so show the Login View Controlller
                var loginViewController = (LoginPageViewController)GetViewController(MainStoryboard, "LoginPageViewController");
                loginViewController.OnLoginSuccess += LoginViewController_OnLoginSuccess;
                SetRootViewController(loginViewController, false);
            }

            return true;
        }

        void LoginViewController_OnLoginSuccess(object sender, EventArgs e)
        {
            //We have successfully Logged In
            var tabBarController = GetViewController(MainStoryboard, "MainPageTabController");
            SetRootViewController(tabBarController, true);
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}


