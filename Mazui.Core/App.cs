using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AwfulForumsLibrary.Manager;
using Mazui.Core.Common;
using Mazui.Database.Database;
using SQLite.Net.Interop;

namespace Mazui.Core
{
    public class App
    {
        public static IContainer Container;

        public static void Init(ISQLitePlatform platform, string dbPath)
        {
            MainForumsDatabase.DbLocation = dbPath;
            MainForumsDatabase.Platform = platform;
            var db = new DataSource(platform, dbPath);
            var bdb = new BookmarkDataSource(platform, dbPath);
            bdb.CreateDatabase();
            db.CreateDatabase();

            Container = AutoFacConfiguration.Configure();
        }

        public async Task<bool> HasCookies()
        {
            var localStorageManager = new LocalStorageManager();
            CookieContainer cookieTest = await localStorageManager.LoadCookie("SACookies2.txt");
            return cookieTest.Count > 0;
        }
    }
}
