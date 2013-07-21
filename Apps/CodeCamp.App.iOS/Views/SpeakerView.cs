using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class SpeakerView : DialogViewControllerBase
    {
        public SpeakerView()
            : base(UITableViewStyle.Grouped)
        {
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();
          
            var bindings = this.CreateInlineBindingTarget<SpeakerViewModel>();

            Root = new RootElement("Speaker Info")
            {
                new Section
                {
                    new StringElement().Bind(bindings, el => el.Caption, vm => vm.Speaker.Name),
                    new MultilineElement().Bind(bindings, el => el.Caption, vm => vm.Speaker.Bio)
                },
                new CommandBindableSection<SessionElement>("Sessions", ViewModel.ViewSessionCommand)
                    .Bind(bindings, element => element.ItemsSource, vm => vm.Sessions)
            };
        }

        private new SpeakerViewModel ViewModel
        {
            get { return (SpeakerViewModel)base.ViewModel; }
        }
    }
}