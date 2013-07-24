using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Extensions;

namespace CodeCamp.App.iOS.Views
{
    public class OverviewView : DialogViewControllerBase
    {
        public OverviewView()
            : base(UITableViewStyle.Grouped)
        {
            Root = new RootElement("Code Camp");

            RefreshRequested += (s, e) => ViewModel.RefreshDataCommand.Execute(null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.BindLoadingMessage(View, model => model.IsRefreshing, "Refreshing...");
            ViewModel.DataRefreshComplete += (s, successful) => 
            {
                ReloadComplete();
            };
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            Root.Add(
                new Section
                {
                    new StringElement("Welcome!")
                }
            );
        }

        private new OverviewViewModel ViewModel
        {
            get { return (OverviewViewModel) base.ViewModel; }
        }
    }
}