using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views
{
    public class SpeakersView : DialogViewControllerBase
    {
        public SpeakersView()
            : base(UITableViewStyle.Plain)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Speakers";
        }

        protected override void OnLoadingComplete()
        {
            Root = new RootElement("Speakers")
            {
            };
        }

        private new SpeakersViewModel ViewModel
        {
            get { return (SpeakersViewModel)base.ViewModel; }
        }
    }
}