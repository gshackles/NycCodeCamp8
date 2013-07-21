using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views
{
    public class SessionView : DialogViewControllerBase
    {
        public SessionView()
            : base(UITableViewStyle.Grouped)
        {
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            var bindings = this.CreateInlineBindingTarget<SessionViewModel>();

            Root = new RootElement("Session Info")
            {
                new Section
                {
                    new StringElement().Bind(bindings, el => el.Caption, vm => vm.Session.Title),
                    new StringElement().Bind(bindings, el => el.Caption, vm => vm.Session.SpeakerName)
                }
            };
        }

        private new SessionViewModel ViewModel
        {
            get { return (SessionViewModel)base.ViewModel; }
        }
    }
}