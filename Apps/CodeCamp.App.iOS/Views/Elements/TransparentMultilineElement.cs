using System.Drawing;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class TransparentMultilineElement : StyledMultilineElement
    { 
        public TransparentMultilineElement()
        {
            TextColor = AppStyles.StandardTextColor;
            Font = AppStyles.StandardFont;
        }

        protected override UITableViewCell GetCellImpl(MonoTouch.UIKit.UITableView tv)
        {
            var cell = base.GetCellImpl(tv);

            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.BackgroundView = new UIView(RectangleF.Empty);

            return cell;
        }
    }
}

