using MonoTouch.UIKit;
using System.Drawing;

namespace CodeCamp.App.iOS
{
    public static class AppStyles
    {
        public static UIColor DefaultBackgroundImage = UIColor.FromPatternImage(UIImage.FromFile("linen.png"));
        public static UIColor NavigationBarTintColor = UIColor.Black;

        public static float ListHeaderHeight = 50;
        public static UIColor ListHeaderColor = UIColor.White;
        public static UIFont ListHeaderFont = UIFont.FromName("Avenir-Book", 18);

        public static UIColor MenuCellBackgroundColor = UIColor.FromRGBA(200, 200, 200, 0.8f);
        public static UIColor SemiTransparentCellBackgroundColor = UIColor.FromRGBA(255, 255, 255, 0.2f);
        public static UIFont ListTitleFont = UIFont.FromName("Avenir-Medium", 18);
        public static UIColor ListTitleColor = UIColor.White;
        public static UIFont ListSubtitleFont = UIFont.FromName("Avenir-Medium", 14);

        public static UIColor StandardTextColor = UIColor.White;
        public static UIFont StandardFont = UIFont.FromName("Avenir-Roman", 16);
        public static UIFont EntityTitleFont = UIFont.FromName("Avenir-Heavy", 22);

        public static UIView CreateListHeader(string text, UITableViewStyle style)
        {
            var headerView = new UIView(new RectangleF(0, 0, UIScreen.MainScreen.Bounds.Width, ListHeaderHeight))
            {
                BackgroundColor = style == UITableViewStyle.Grouped ? UIColor.Clear : UIColor.Black
            };
            var label = new UILabel(new RectangleF(15, 0, UIScreen.MainScreen.Bounds.Width - 15, ListHeaderHeight))
            {
                BackgroundColor = UIColor.Clear,
                TextColor = AppStyles.ListHeaderColor,
                Font = AppStyles.ListHeaderFont,
                Text = text
            };

            headerView.AddSubview(label);

            return headerView;
        }
    }
}