using System.Drawing;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class TransparentStringElement : StyledStringElement
    {
        public TransparentStringElement(string caption = "")
            : base(caption)
        {
            TextColor = AppStyles.StandardTextColor;
            Font = AppStyles.StandardFont;
        }

        protected override UITableViewCell GetCellImpl(UITableView tv)
        {
            var cell = base.GetCellImpl(tv);

            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.BackgroundView = new UIView(RectangleF.Empty);

            return cell;
        }
    }
}

