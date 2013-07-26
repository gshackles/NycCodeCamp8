using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class SponsorView : DialogViewControllerBase
    {
        public SponsorView()
            : base(UITableViewStyle.Grouped)
        {
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            var bindings = this.CreateInlineBindingTarget<SponsorViewModel>();

            Root = new RootElement("Sponsor Info")
            {
                new Section
                {
                    new TransparentStringElement
                    {
                        Font = AppStyles.EntityTitleFont
                    }.Bind(bindings, el => el.Caption, vm => vm.Sponsor.Name)
                },
                new Section
                {
                    new StyledStringElement("View Website") 
                    { 
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        ShouldDeselectAfterTouch = true,
                        Image = UIImage.FromFile("globe.png"),
                        BackgroundColor = AppStyles.SemiTransparentCellBackgroundColor,
                        TextColor = AppStyles.ListTitleColor,
                        Font = AppStyles.ListTitleFont
                    }.Bind(bindings, el => el.SelectedCommand, vm => vm.ViewWebsiteCommand)
                },
                new Section
                {
                    new TransparentMultilineElement().Bind(bindings, el => el.Caption, vm => vm.Sponsor.Description)
                }
            };
        }

        private new SponsorViewModel ViewModel
        {
            get { return (SponsorViewModel)base.ViewModel; }
        }
    }
}