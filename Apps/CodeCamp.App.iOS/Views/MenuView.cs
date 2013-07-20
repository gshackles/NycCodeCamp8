using CodeCamp.Core.ViewModels;
using MonoTouch.UIKit;
using CrossUI.Touch.Dialog.Elements;
using Cirrious.MvvmCross.Binding.BindingContext;
using System;

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

            Func<string, StyledStringElement> createElement = text =>
                new StyledStringElement(text) 
                { 
                    Accessory = UITableViewCellAccessory.DisclosureIndicator,
                    ShouldDeselectAfterTouch = true
                };

            StyledStringElement sessionsElement = createElement("Sessions"),
                                speakersElement = createElement("Speakers"),
                                mapElement = createElement("Map");

            var binder = this.CreateBindingSet<MenuView, MenuViewModel>();
            binder.Bind(sessionsElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowSessionsCommand);
            binder.Bind(speakersElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowSpeakersCommand);
            binder.Bind(mapElement)
                  .For(el => el.SelectedCommand)
                  .To(vm => vm.ShowMapCommand);
            binder.Apply();

            Root = new RootElement("")
            {
                new Section
                {
                    sessionsElement, speakersElement, mapElement
                }
            };
        }

        private new MenuViewModel ViewModel
        {
            get { return (MenuViewModel)base.ViewModel; }
        }
    }
}