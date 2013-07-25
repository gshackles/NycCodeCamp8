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
        public static UIColor ListHeaderBackgroundColor = UIColor.Clear;
        public static UIFont ListHeaderFont = UIFont.FromName("Copperplate", 18);

        public static UILabel CreateListHeader(string text)
        {
            return new UILabel(new RectangleF(0, 0, UIScreen.MainScreen.Bounds.Width, ListHeaderHeight))
            {
                BackgroundColor = AppStyles.ListHeaderBackgroundColor,
                TextColor = AppStyles.ListHeaderColor,
                Font = AppStyles.ListHeaderFont,
                Text = text
            };
        }
    }
}