using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Extensions;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class SpeakersView : DialogViewControllerBase
    {
        public SpeakersView()
            : base(UITableViewStyle.Plain)
        {
            Root = new RootElement("Speakers");

            RefreshRequested += (s, e) => ViewModel.RefreshDataCommand.Execute(null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.BindLoadingMessage(View, model => model.IsRefreshing, "Refreshing...");
            ViewModel.DataRefreshComplete += (s, a) => 
            {
                loadSpeakers();

                ReloadComplete();
            };
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            loadSpeakers();
        }

        private void loadSpeakers()
        {
            var bindings = this.CreateInlineBindingTarget<SpeakersViewModel>();

            Root.Clear();
            Root.Add(
                new CommandBindableSection<SpeakerElement>(null, ViewModel.ViewSpeakerCommand)
                    .Bind(bindings, element => element.ItemsSource, vm => vm.Speakers)
            );
            ReloadData();
        }

        private new SpeakersViewModel ViewModel
        {
            get { return (SpeakersViewModel)base.ViewModel; }
        }
    }
}