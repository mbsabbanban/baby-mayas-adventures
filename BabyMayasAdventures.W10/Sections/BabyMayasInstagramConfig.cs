using System;
using System.Collections.Generic;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Instagram;
using BabyMayasAdventures.Config;
using BabyMayasAdventures.ViewModels;

namespace BabyMayasAdventures.Sections
{
    public class BabyMayasInstagramConfig : SectionConfigBase<InstagramDataConfig, InstagramSchema>
    {
        public override DataProviderBase<InstagramDataConfig, InstagramSchema> DataProvider
        {
            get
            {
                return new InstagramDataProvider(new InstagramOAuthTokens
                {
                    ClientId = "150c8cd4bdb94e2783eb8c48b3a37500"

                });
            }
        }

        public override InstagramDataConfig Config
        {
            get
            {
                return new InstagramDataConfig
                {
                    QueryType = InstagramQueryType.Id,
                    Query = @"734186501"
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("BabyMayasInstagramListPage");
            }
        }

        public override ListPageConfig<InstagramSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<InstagramSchema>
                {
                    Title = "Baby Maya's Instagram",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = null;
                        viewModel.Description = null;
                        viewModel.Image = item.ThumbnailUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return NavigationInfo.FromPage("BabyMayasInstagramDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<InstagramSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, InstagramSchema>>();

                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Author.ToSafeString();
                    viewModel.Image = item.ImageUrl.ToSafeString();
                    viewModel.Content = null;
                });

				var actions = new List<ActionConfig<InstagramSchema>>
				{
                    ActionConfig<InstagramSchema>.Link("Go To Source", (item) => item.SourceUrl.ToSafeString()),
				};

                return new DetailPageConfig<InstagramSchema>
                {
                    Title = "Baby Maya's Instagram",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        public override string PageTitle
        {
            get { return "Baby Maya's Instagram"; }
        }

    }
}
