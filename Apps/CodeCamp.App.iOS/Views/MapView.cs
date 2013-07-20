using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views
{
    public class MapView : DialogViewControllerBase
    {
        public MapView()
            : base(UITableViewStyle.Grouped)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Map";
        }
    }
}