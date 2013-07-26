using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.Data.Entities;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class SpeakerElement : StyledStringElement, IBindableElement, IElementSizing
    {       
        public IMvxBindingContext BindingContext { get; set; }

        public SpeakerElement()
        {
            ShouldDeselectAfterTouch = true;
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            BackgroundColor = AppStyles.SemiTransparentCellBackgroundColor;
            TextColor = AppStyles.ListTitleColor;
            Font = AppStyles.ListTitleFont;

            this.CreateBindingContext();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SpeakerElement, Speaker>();
                set.Bind().For(me => me.Caption).To(p => p.Name);
                set.Apply();
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BindingContext.ClearAllBindings();
            }
            base.Dispose(disposing);
        }

        public virtual object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }

        public float GetHeight(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            return 54;
        }
    }
}

