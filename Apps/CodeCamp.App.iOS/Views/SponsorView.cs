using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

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
                    new StringElement().Bind(bindings, el => el.Caption, vm => vm.Sponsor.Name)
                },
                new Section
                {
                    new StyledStringElement("View Website") 
                    { 
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        ShouldDeselectAfterTouch = true,
                        Image = UIImage.FromFile("globe.png")
                    }.Bind(bindings, el => el.SelectedCommand, vm => vm.ViewWebsiteCommand)
                },
                new Section
                {
                    new MultilineElement().Bind(bindings, el => el.Caption, vm => vm.Sponsor.Description)
                }
            };
        }

        private new SponsorViewModel ViewModel
        {
            get { return (SponsorViewModel)base.ViewModel; }
        }
    }
}