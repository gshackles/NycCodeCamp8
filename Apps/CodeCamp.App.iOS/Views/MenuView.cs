using System;
using System.Linq.Expressions;
using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using System.Drawing;

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

            Func<string, string, Expression<Func<MenuViewModel, object>>, StyledStringElement> createElement = (text, imageFileName, property) =>
            {
                var element = new StyledStringElement(text) 
                { 
                    Accessory = UITableViewCellAccessory.DisclosureIndicator,
                    ShouldDeselectAfterTouch = true,
                    Image = UIImage.FromFile(imageFileName),
                    BackgroundColor = AppStyles.MenuCellBackgroundColor
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
                    createElement("Overview", "home.png", vm => vm.ShowOverviewCommand),
                    createElement("Full Schedule", "clock.png", vm => vm.ShowSessionsCommand),
                    createElement("Speakers", "users.png", vm => vm.ShowSpeakersCommand),
                    createElement("Sponsors", "sponsors.png", vm => vm.ShowSponsorsCommand),
                    createElement("Facility Map", "map.png", vm => vm.ShowMapCommand)
                }
            };

            var headerView = new UIView(new RectangleF(0, 0, View.Frame.Width, 60));
            headerView.Add(new UILabel(new RectangleF(15, 0, headerView.Frame.Width - 15, headerView.Frame.Height))
            {
                Text = "NYC Code Camp 8",
                Font = UIFont.FromName("HelveticaNeue-Bold", 18),
                BackgroundColor = UIColor.Clear,
                TextColor = AppStyles.StandardTextColor
            });
            TableView.TableHeaderView = headerView;

            binder.Apply();
        }

        private new MenuViewModel ViewModel
        {
            get { return (MenuViewModel)base.ViewModel; }
        }

        protected override UIColor BackgroundColor
        {
//            get { return UIColor.FromPatternImage(UIImage.FromFile("dots.png")); }
            get { return UIColor.FromRGB(20, 20, 20); }
        }
    }
}