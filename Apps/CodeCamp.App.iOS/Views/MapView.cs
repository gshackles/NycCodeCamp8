using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Annotations;
using CodeCamp.App.iOS.Extensions;

namespace CodeCamp.App.iOS.Views
{
    [DisableMenuGesture]
    public class MapView : MvxViewController
    {
        private UIScrollView _scrollView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Facility Map";

            var mapImage = new UIImageView(UIImage.FromFile("microsoftmap.jpg"));
            _scrollView = new UIScrollView
            {
                ContentSize = mapImage.Frame.Size,
                MinimumZoomScale = 0.5f,
                MaximumZoomScale = 3.0f,
                MultipleTouchEnabled = true,
                ViewForZoomingInScrollView = _ => mapImage,
                BackgroundColor = UIColor.Black
            };

            _scrollView.AddSubview(mapImage);
            Add(_scrollView);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.OnViewWillAppear(ViewModel, () => {});

            _scrollView.Frame = View.Bounds;
        }
    }
}