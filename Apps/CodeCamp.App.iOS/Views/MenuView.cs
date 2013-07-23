using System;
using System.Linq.Expressions;
using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views
{
    public class MenuView : DialogViewControllerBase
    {
        public MenuView()
            : base(UITableViewStyle.Plain)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var frame = View.Frame;
            frame.Width = 200;
            View.Frame = frame;

            TableView.ScrollEnabled = false;

            var binder = this.CreateBindingSet<MenuView, MenuViewModel>();

            Func<string, Expression<Func<MenuViewModel, object>>, StyledStringElement> createElement = (text, property) =>
            {
                var element = new StyledStringElement(text) 
                { 
                    Accessory = UITableViewCellAccessory.DisclosureIndicator,
                    ShouldDeselectAfterTouch = true
                };

                binder.Bind(element)
                      .For(el => el.SelectedCommand)
                      .To(property);

                return element;
            };
              
            Root = new RootElement("")
            {
                new Section
                {
                    createElement("Overview", vm => vm.ShowOverviewCommand),
                    createElement("Full Schedule", vm => vm.ShowSessionsCommand),
                    createElement("Speakers", vm => vm.ShowSpeakersCommand),
                    createElement("Sponsors", vm => vm.ShowSponsorsCommand),
                    createElement("Map", vm => vm.ShowMapCommand)
                }
            };

            binder.Apply();
        }

        private new MenuViewModel ViewModel
        {
            get { return (MenuViewModel)base.ViewModel; }
        }
    }
}