using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Views.Elements;

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
            var bindings = this.CreateInlineBindingTarget<SpeakersViewModel>();

            Root = new RootElement("Speakers")
            {
                new CommandBindableSection<SpeakerElement>(null, ViewModel.ViewSpeakerCommand)
                    .Bind(bindings, element => element.ItemsSource, vm => vm.Speakers)
            };
        }

        private new SpeakersViewModel ViewModel
        {
            get { return (SpeakersViewModel)base.ViewModel; }
        }
    }
}