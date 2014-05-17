using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using System.Collections.Generic;

namespace XamarinStore.iOS
{
	public class ProductImageViewController : UIViewController
	{
		List<UIImage> Images;
		UIPageControl pageControl;
		UIScrollView scrollView;

		public ProductImageViewController (List<UIImage> images)
		{
			Title = "Images";
			Images = images;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.EdgesForExtendedLayout = UIRectEdge.None; 
			this.View.BackgroundColor = UIColor.White;

			scrollView = new UIScrollView (new RectangleF (0,0,this.View.Frame.Width,this.View.Frame.Height));
			scrollView.ShowsHorizontalScrollIndicator = false;
			scrollView.ShowsVerticalScrollIndicator = false;
			scrollView.PagingEnabled = true;
			scrollView.ScrollEnabled = true;
			scrollView.Scrolled += ScrollViewScrolled;
			scrollView.ContentSize = new SizeF(this.View.Frame.Width * Images.Count, this.View.Frame.Height);
			scrollView.ContentMode = UIViewContentMode.Top;
			scrollView.Bounces = false;

			pageControl = new UIPageControl (new RectangleF (this.View.Frame.Width / 2 - 100, this.View.Frame.Height - 150, 200, 100));
			pageControl.Pages = Images.Count;
			pageControl.TouchUpInside += (object sender, EventArgs e) => {scrollView.ContentOffset = new PointF(pageControl.CurrentPage * this.View.Frame.Width, 0);};

			for (int counter = 0; counter < Images.Count; counter++) 
			{
				UIScrollView scrollViewInside = new UIScrollView(new RectangleF((this.View.Frame.Width * counter),0,this.View.Frame.Width - 2,this.View.Frame.Height));
				UIImageView imageView = new UIImageView (Images[counter]);

				imageView.Frame = new RectangleF(0, 0, scrollViewInside.Frame.Width, scrollViewInside.Frame.Height);
				imageView.ContentMode = UIViewContentMode.ScaleAspectFill;

				scrollViewInside.AddSubview (imageView);
				scrollViewInside.ShowsHorizontalScrollIndicator = false;
				scrollViewInside.ShowsVerticalScrollIndicator = false;
				scrollViewInside.MaximumZoomScale = 3f;
				scrollViewInside.MinimumZoomScale = .1f;
				scrollViewInside.ViewForZoomingInScrollView += (UIScrollView sv) => { return imageView; };
	
				scrollView.AddSubview (scrollViewInside);
			}

			this.View.AddSubview (scrollView);
			this.View.AddSubview (pageControl);
		}

		private void ScrollViewScrolled(object sender, EventArgs e)
		{
			double page = Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / Images.Count) / scrollView.Frame.Width) + 1;			
			pageControl.CurrentPage = (int)page;
		}
			
		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (NavigationController == null)
				return;

			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}
	}
}

