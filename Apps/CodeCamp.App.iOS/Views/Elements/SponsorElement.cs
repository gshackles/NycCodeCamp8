using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.Data.Entities;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class SponsorElement : StyledStringElement, IBindableElement
    {       
        public IMvxBindingContext BindingContext { get; set; }

        public SponsorElement()
        {
            ShouldDeselectAfterTouch = true;
            Accessory = UITableViewCellAccessory.DisclosureIndicator;

            this.CreateBindingContext();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SponsorElement, Sponsor>();
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
    }
}

