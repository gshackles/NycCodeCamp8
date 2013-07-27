using System.Linq;
using CodeCamp.Core.Extensions;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Extensions;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class SessionsView : DialogViewControllerBase
    {
        public SessionsView()
            : base(UITableViewStyle.Plain)
        {
            Root = new RootElement("Schedule");

            RefreshRequested += (s, e) => ViewModel.RefreshDataCommand.Execute(null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.BindLoadingMessage(View, model => model.IsRefreshing, "Refreshing...");
            ViewModel.DataRefreshComplete += (s, successful) => 
            {
                if (successful)
                    loadSessions();

                ReloadComplete();
            };
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            loadSessions();
        }

        private void loadSessions()
        {
            Root.Clear();
            Root.Add(
                from slot in ViewModel.TimeSlots
                select new CommandBindableSection<SessionElement>("", ViewModel.ViewSessionCommand)
                {
                    ItemsSource = slot.Sessions,
                    HeaderView = AppStyles.CreateListHeader(string.Format("{0} - {1}", slot.StartTime.FormatTime(), slot.EndTime.FormatTime()), UITableViewStyle.Plain)
                }
            );
            ReloadData();
        }

        private new SessionsViewModel ViewModel
        {
            get { return (SessionsViewModel)base.ViewModel; }
        }
    }
}