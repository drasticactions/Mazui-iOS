﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Mazui.Core.ViewModels;

namespace Mazui.Core.Common
{
    public class AutoFacConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Register View Models
            //builder.RegisterType<SearchPageViewModel>().SingleInstance();
            //builder.RegisterType<NewPrivateMessageViewModel>().SingleInstance();
            //builder.RegisterType<PrivateMessagePageViewModel>().SingleInstance();
            //builder.RegisterType<LastPostPageViewModel>().SingleInstance();
            //builder.RegisterType<MainPageViewModel>().SingleInstance();
            builder.RegisterType<MainForumsPageViewModel>().SingleInstance();
            //builder.RegisterType<ThreadListPageViewModel>().SingleInstance();
            builder.RegisterType<BookmarksPageViewModel>().SingleInstance();
            //builder.RegisterType<PrivateMessageListViewModel>().SingleInstance();
            //builder.RegisterType<ThreadPageViewModel>().SingleInstance();
            //builder.RegisterType<NewThreadReplyPageViewModel>().SingleInstance();
            //builder.RegisterType<NewThreadPageViewModel>().SingleInstance();
            //builder.RegisterType<PostIconListPageViewModel>().SingleInstance();
            //builder.RegisterType<BbCodeListPageViewModel>().SingleInstance();
            //builder.RegisterType<SmiliesPageViewModel>().SingleInstance();
            //builder.RegisterType<PreviewThreadPageViewModel>().SingleInstance();
            //builder.RegisterType<ForumUserPageViewModel>().SingleInstance();
            //builder.RegisterType<SaclopediaPageViewModel>().SingleInstance();
            //builder.RegisterType<MainPage>();
            builder.RegisterType<LoginPageViewModel>().SingleInstance();
            return builder.Build();
        }
    }
}
