using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.YouTube;
using BabyMayasAdventures;
using BabyMayasAdventures.Sections;
using BabyMayasAdventures.ViewModels;

namespace BabyMayasAdventures.Views
{
    public sealed partial class BabyMayasYouTubeAdventuresListPage : PageBase     {
        public BabyMayasYouTubeAdventuresListPage()
        {
            this.ViewModel = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new BabyMayasYouTubeAdventuresConfig());
            this.InitializeComponent();
}

        public ListViewModel<YouTubeDataConfig, YouTubeSchema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
