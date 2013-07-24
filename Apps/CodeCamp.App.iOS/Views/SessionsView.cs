using System.Linq;
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
                select new CommandBindableSection<SessionElement>(string.Format("{0:t} - {1:t}", slot.StartTime, slot.EndTime), ViewModel.ViewSessionCommand)
                {
                    ItemsSource = slot.Sessions
                }
            );
            Root.TableView.ReloadData();
        }

        private new SessionsViewModel ViewModel
        {
            get { return (SessionsViewModel)base.ViewModel; }
        }
    }
}