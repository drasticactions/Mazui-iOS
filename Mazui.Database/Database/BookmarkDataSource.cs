﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwfulForumsLibrary.Entity;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace Mazui.Database.Database
{
    public class BookmarkDataSource : IDisposable
    {
        protected SQLiteAsyncConnection Db { get; set; }

        public Repository<ForumThreadEntity> BookmarkForumRepository { get; set; }


        public BookmarkDataSource(ISQLitePlatform platform, string dbPath)
        {
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(platform, new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)));
            Db = new SQLiteAsyncConnection(connectionFactory);

            BookmarkForumRepository = new Repository<ForumThreadEntity>(Db);
        }

        public void CreateDatabase()
        {
            var existingTables =
                Db.QueryAsync<sqlite_master>("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;")
                  .GetAwaiter()
                  .GetResult();

            if (existingTables.Any(x => x.name == "ForumEntity") != true)
            {
                Db.CreateTableAsync<ForumThreadEntity>().GetAwaiter().GetResult();
            }
        }

        public void Dispose()
        {
            this.Db = null;
        }
    }
}
