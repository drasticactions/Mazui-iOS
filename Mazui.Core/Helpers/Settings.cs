using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace Mazui.Core.Helpers
{
    public enum AppTheme
    {
        Default,
        Yospos,
        YosposAmber,
        Byob,
        ImpZone
    }
    public static class Settings
    {
        static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        const string LoggedIn = "logged_in";
        static readonly bool LoggedInDefault = false;
        const string AppThemeKey = "theme_key";
        const int AppThemeDefault = (int)AppTheme.Default;
        #endregion
        public static AppTheme AppTheme
        {
            get
            {
                return (AppTheme)AppSettings.GetValueOrDefault(AppThemeKey, AppThemeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AppThemeKey, (int)value);
            }
        }

        public static bool IsLoggedIn
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoggedIn, LoggedInDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoggedIn, value);
            }
        }

    }
}
