using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwfulForumsLibrary.Entity;
using Mazui.Core.Common;
using Mazui.Core.Tools;
using Mazui.Database.Database;

namespace Mazui.Core.ViewModels
{
    public class BookmarksPageViewModel : NotifierBase
    {
        private readonly MainForumsDatabase _bookmarkManager = new MainForumsDatabase();
        private ObservableCollection<ForumThreadEntity> _bookmarkedThreads;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ForumThreadEntity> BookmarkedThreads
        {
            get { return _bookmarkedThreads; }
            set
            {
                SetProperty(ref _bookmarkedThreads, value);
                OnPropertyChanged();
            }
        }

        public async Task Initialize()
        {
            if (BookmarkedThreads != null && BookmarkedThreads.Any())
            {
                return;
            }
            IsLoading = true;
            try
            {
                BookmarkedThreads = new ObservableCollection<ForumThreadEntity>();
                DateTime refreshDate = DateTime.UtcNow;
                var bookmarks = await _bookmarkManager.GetBookmarkedThreadsFromDb();
                if (bookmarks != null && bookmarks.Any())
                {
                    BookmarkedThreads = bookmarks.ToObservableCollection();
                }
                if ((!BookmarkedThreads.Any() || refreshDate < (DateTime.UtcNow.AddHours(-1.00))))
                {
                    await Refresh();
                }
            }
            catch (Exception ex)
            {
                //AwfulDebugger.SendMessageDialogAsync("Failed to get Bookmarks", ex);
            }
            IsLoading = false;
        }

        public async Task Refresh()
        {
            IsLoading = true;
            try
            {
                var test = await _bookmarkManager.RefreshBookmarkedThreads();
                BookmarkedThreads = new ObservableCollection<ForumThreadEntity>();
                foreach (var item in test)
                {
                    BookmarkedThreads.Add(item);
                }
                test = null;
                GC.Collect();
                //_localSettings.Values["RefreshBookmarks"] = DateTime.UtcNow.ToString();
            }
            catch (Exception ex)
            {
                //AwfulDebugger.SendMessageDialogAsync("Failed to get Bookmarks", ex);
            }
            IsLoading = false;
        }

        public void UpdateThreadList()
        {
            OnPropertyChanged("BookmarkedThreads");
        }

    }
}
