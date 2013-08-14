using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Extensions;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Extensions;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class OverviewView : DialogViewControllerBase
    {
        public OverviewView()
            : base(UITableViewStyle.Plain)
        {
            Root = new RootElement("NYC Code Camp");

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

            Root.Clear();
            Root.Add(
                from slot in (ViewModel.TimeSlots ?? new List<TimeSlot>())
                select new CommandBindableSection<SessionElement>("", ViewModel.ViewSessionCommand)
                {
                    ItemsSource = slot.Sessions,
                    HeaderView = AppStyles.CreateListHeader(string.Format("{0} - {1}", slot.StartTime.FormatTime(), slot.EndTime.FormatTime()), UITableViewStyle.Plain)
                }
            );

            var fullScheduleSection = new Section
            {
                HeaderView = new UIView(new RectangleF(0, 0, UIScreen.MainScreen.Bounds.Width, 33))
                {
                    BackgroundColor = UIColor.Clear
                }
            };
            fullScheduleSection.Add(
                new StyledStringElement("See full schedule") 
                { 
                    Accessory = UITableViewCellAccessory.DisclosureIndicator,
                    SelectedCommand = ViewModel.ViewFullScheduleCommand,
                    BackgroundColor = AppStyles.SemiTransparentCellBackgroundColor,
                    TextColor = AppStyles.ListTitleColor,
                    Font = AppStyles.ListTitleFont
                }
            );
            Root.Add(fullScheduleSection);
            ReloadData();
        }

        private new OverviewViewModel ViewModel
        {
            get { return (OverviewViewModel) base.ViewModel; }
        }
    }
}