using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStudio.Common;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.Instagram;
using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.LocalStorage;
using BabyMayasAdventures.Sections;


namespace BabyMayasAdventures.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems)
        {
            PageTitle = "Baby Maya's Adventures";
            AboutBabyMaya = new ListViewModel<LocalStorageDataConfig, HtmlSchema>(new AboutBabyMayaConfig(), visibleItems);
            BabyMayasInstagram = new ListViewModel<InstagramDataConfig, InstagramSchema>(new BabyMayasInstagramConfig(), visibleItems);
            BabyMayasYouTubeAdventures = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new BabyMayasYouTubeAdventuresConfig(), visibleItems);
            Actions = new List<ActionInfo>();

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = new RelayCommand(Refresh),
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

        public string PageTitle { get; set; }
        public ListViewModel<LocalStorageDataConfig, HtmlSchema> AboutBabyMaya { get; private set; }
        public ListViewModel<InstagramDataConfig, InstagramSchema> BabyMayasInstagram { get; private set; }
        public ListViewModel<YouTubeDataConfig, YouTubeSchema> BabyMayasYouTubeAdventures { get; private set; }

        public RelayCommand<INavigable> SectionHeaderClickCommand
        {
            get
            {
                return new RelayCommand<INavigable>(item =>
                    {
                        NavigationService.NavigateTo(item);
                    });
            }
        }

        public DateTime? LastUpdated
        {
            get
            {
                return GetViewModels().Select(vm => vm.LastUpdated)
                            .OrderByDescending(d => d).FirstOrDefault();
            }
        }

        public List<ActionInfo> Actions { get; private set; }

        public bool HasActions
        {
            get
            {
                return Actions != null && Actions.Count > 0;
            }
        }

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData)
                                        .Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<DataViewModelBase> GetViewModels()
        {
            yield return AboutBabyMaya;
            yield return BabyMayasInstagram;
            yield return BabyMayasYouTubeAdventures;
        }
    }
}
