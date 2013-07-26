using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.Data.Entities;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class SessionElement : StyledStringElement, IBindableElement, IElementSizing
    {
        public IMvxBindingContext BindingContext { get; set; }

        public SessionElement()
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            ShouldDeselectAfterTouch = true;
            Style = UITableViewCellStyle.Subtitle;
            BackgroundColor = AppStyles.SemiTransparentCellBackgroundColor;
            TextColor = AppStyles.ListTitleColor;
            Font = AppStyles.ListTitleFont;
            SubtitleFont = AppStyles.ListSubtitleFont;

            this.CreateBindingContext();
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SessionElement, Session>();
                set.Bind().For(my => my.Caption).To(session => session.Title);
                set.Bind().For(my => my.Value).To(session => session).WithConversion("SessionDetails");
                set.Apply();
            });
        }

        protected override void PrepareCell(UITableViewCell cell)
        {
            base.PrepareCell(cell);

            if (cell != null && cell.DetailTextLabel != null)
            {
                cell.DetailTextLabel.BackgroundColor = UIColor.Clear;
                cell.DetailTextLabel.TextColor = AppStyles.StandardTextColor;
            }
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

        public float GetHeight(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            return 66;
        }
    }
}