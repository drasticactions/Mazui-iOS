using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwfulForumsLibrary.Entity;
using AwfulForumsLibrary.Manager;
using AwfulForumsLibrary.Tools;
using Mazui.Core.Common;
using Mazui.Database.Database;

namespace Mazui.Core.ViewModels
{
    public class MainForumsPageViewModel : NotifierBase
    {
        private readonly ForumManager _forumManager = new ForumManager();
        private ObservableCollection<ForumCategoryEntity> _forumGroupList;
        private MainForumsDatabase _db = new MainForumsDatabase();
        private ForumCategoryEntity _favoritesEntity;
        private ObservableCollection<ForumCategoryEntity> _favoriteForumGroupList;

        public ObservableCollection<ForumCategoryEntity> ForumGroupList
        {
            get { return _forumGroupList; }
            set
            {
                SetProperty(ref _forumGroupList, value);
                OnPropertyChanged();
            }
        }
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

        public MainForumsPageViewModel()
        {
            if (ForumGroupList != null) return;
            ForumGroupList = new ObservableCollection<ForumCategoryEntity>();
            Initialize();
        }

        public async Task GetFavoriteForums()
        {
            var forumEntities = await _db.GetFavoriteForumsAsync();
            var favorites = ForumGroupList.FirstOrDefault(node => node.Name.Equals("Favorites"));
            if (!forumEntities.Any())
            {
                if (favorites != null)
                {
                    ForumGroupList.Remove(favorites);
                }
                OnPropertyChanged("ForumGroupList");
                return;
            }

            _favoritesEntity = new ForumCategoryEntity
            {
                Name = "Favorites",
                Location = string.Format(Constants.ForumPage, "forumid=48"),
                ForumList = forumEntities
            };

            if (favorites == null)
            {
                ForumGroupList.Insert(0, _favoritesEntity);
            }
            else
            {
                ForumGroupList.RemoveAt(0);
                ForumGroupList.Insert(0, _favoritesEntity);
            }
            OnPropertyChanged("ForumGroupList");
        }

        private async Task GetMainPageForumsAsync()
        {
            var forumCategoryEntities = new List<ForumCategoryEntity>();
            //forumCategoryEntities = await _db.GetMainForumsList();
            //if (forumCategoryEntities.Any())
            //{
            //    foreach (var forumCategoryEntity in forumCategoryEntities)
            //    {
            //        ForumGroupList.Add(forumCategoryEntity);
            //    }
            //    return;
            //}

            forumCategoryEntities = await _forumManager.GetForumCategoryMainPage();
            foreach (var forumCategoryEntity in forumCategoryEntities)
            {
                ForumGroupList.Add(forumCategoryEntity);
            }
            await _db.SaveMainForumsList(ForumGroupList.ToList());
        }

        public async Task Initialize()
        {
            IsLoading = true;
            try
            {
                //await GetFavoriteForums();
                await GetMainPageForumsAsync();
                OnPropertyChanged("ForumGroupList");
            }
            catch (Exception ex)
            {
                //AwfulDebugger.SendMessageDialogAsync("Error getting the main forums dialog", ex);
            }
            IsLoading = false;
        }
    }
}
