using MonoTouch.UIKit;
using CrossUI.Touch.Dialog.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class OverviewView : DialogViewControllerBase
    {
        public OverviewView()
            : base(UITableViewStyle.Grouped)
        {
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            Root = new RootElement("Code Camp")
            {
                new Section
                {
                    new StringElement("Welcome!")
                }
            };
        }
    }
}