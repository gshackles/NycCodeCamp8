using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.Data.Entities;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class SessionElement : StyledStringElement, IBindableElement
    {
        public IMvxBindingContext BindingContext { get; set; }

        public SessionElement()
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            ShouldDeselectAfterTouch = true;
            Style = UITableViewCellStyle.Subtitle;

            this.CreateBindingContext();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SessionElement, Session>();
                set.Bind().For(my => my.Caption).To(session => session.Title);
                set.Bind().For(my => my.Value).To(session => session.SpeakerName);
                set.Apply();
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                BindingContext.ClearAllBindings();

            base.Dispose(disposing);
        }

        public virtual object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }
    }
}