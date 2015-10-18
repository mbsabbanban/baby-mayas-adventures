using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.Instagram;
using BabyMayasAdventures;
using BabyMayasAdventures.Sections;
using BabyMayasAdventures.ViewModels;

namespace BabyMayasAdventures.Views
{
    public sealed partial class BabyMayasInstagramListPage : PageBase     {
        public BabyMayasInstagramListPage()
        {
            this.ViewModel = new ListViewModel<InstagramDataConfig, InstagramSchema>(new BabyMayasInstagramConfig());
            this.InitializeComponent();
}

        public ListViewModel<InstagramDataConfig, InstagramSchema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
