using System.Linq;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class SponsorsView : DialogViewControllerBase
    {
        public SponsorsView()
            : base(UITableViewStyle.Plain)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Sponsors";
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            Root = new RootElement("Schedule")
            {
                from tier in ViewModel.SponsorTiers
                select new CommandBindableSection<SponsorElement>(tier.Name, ViewModel.ViewSponsorCommand)
                {
                    ItemsSource = tier.Sponsors
                }
            };
        }

        private new SponsorsViewModel ViewModel
        {
            get { return (SponsorsViewModel)base.ViewModel; }
        }
    }
}