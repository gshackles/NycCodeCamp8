using System.Linq;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

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

            Title = "Sessions";
        }

        protected override void OnLoadingComplete()
        {
            Root = new RootElement("Sessions")
            {
                from slot in ViewModel.TimeSlots
                select new Section(slot.StartTime.TimeOfDay.ToString())
                {
                    from session in slot.Sessions
                    select new StyledStringElement(session.Title, () => ViewModel.ViewSessionCommand.Execute(session))
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator
                    }
                }
            };
        }

        private new SessionsViewModel ViewModel
        {
            get { return (SessionsViewModel)base.ViewModel; }
        }
    }
}