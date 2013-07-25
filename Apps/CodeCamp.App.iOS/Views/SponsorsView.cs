using System.Linq;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Extensions;
using CodeCamp.App.iOS.Views.Elements;
using System.Drawing;

namespace CodeCamp.App.iOS.Views
{
    public class SponsorsView : DialogViewControllerBase
    {
        public SponsorsView()
            : base(UITableViewStyle.Plain)
        {
            Root = new RootElement("Sponsors");

            RefreshRequested += (s, e) => ViewModel.RefreshDataCommand.Execute(null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.BindLoadingMessage(View, model => model.IsRefreshing, "Refreshing...");
            ViewModel.DataRefreshComplete += (s, successful) => 
            {
                if (successful)
                    loadSponsors();

                ReloadComplete();
            };
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            loadSponsors();
        }

        private void loadSponsors()
        {
            Root.Clear();
            Root.Add(
                from tier in ViewModel.SponsorTiers
                select new CommandBindableSection<SponsorElement>("", ViewModel.ViewSponsorCommand)
                {
                    ItemsSource = tier.Sponsors,
                    HeaderView = AppStyles.CreateListHeader(tier.Name)
                }
            );
            ReloadData();
        }

        private new SponsorsViewModel ViewModel
        {
            get { return (SponsorsViewModel)base.ViewModel; }
        }
    }
}