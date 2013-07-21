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
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Schedule";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.OnViewWillAppear(ViewModel, onLoadingComplete);
        }

        private void onLoadingComplete()
        {
            Root = new RootElement("Schedule")
            {
                from slot in ViewModel.TimeSlots
                select new CommandBindableSection<SessionElement>(string.Format("{0:t} - {1:t}", slot.StartTime, slot.EndTime), ViewModel.ViewSessionCommand)
                {
                    ItemsSource = slot.Sessions
                }
            };
        }

        private new SessionsViewModel ViewModel
        {
            get { return (SessionsViewModel)base.ViewModel; }
        }
    }
}