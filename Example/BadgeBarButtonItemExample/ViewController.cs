using System;

using UIKit;
using CoreGraphics;

using Utility;

namespace BadgeBarButtonItemExample
{
	public partial class ViewController : UIViewController
	{
		// Some colors for the badge
		UIColor[] _colors = { UIColor.Blue, UIColor.DarkGray, UIColor.Yellow,
			UIColor.Orange, UIColor.Purple, UIColor.White, UIColor.Brown, UIColor.Red };

		// Some positions for the badge
		Tuple<int, int>[] _positions = {
			new Tuple<int, int> (-8, -4),
			new Tuple<int, int> (-8, 14),
			new Tuple<int, int> (20, 14) ,
			new Tuple<int, int> (20, -4)
		};

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			int counter = 42;

			// Use a UIButton as customView to handle touch events 
			var customButton = new UIButton (new CGRect (0, 0, 32, 32));
			customButton.SetImage (UIImage.FromBundle (@"trolley"), UIControlState.Normal);

			// Create the custom BadgeBarButtonItem
			var barButton = new BadgeBarButtonItem (customButton) {
				// Set an initial value and position for the badge
				BadgeValue = counter.ToString (),
				BadgeOriginX = _positions[_positions.Length -1].Item1,
				BadgeOriginY = _positions[_positions.Length -1].Item2,
			};
				
			// Reset the badge when tapping on the customButton
			customButton.TouchUpInside += (sender, e) => { 
				counter = 0;
				barButton.BadgeValue = counter.ToString ();
			};

			// Add the actions to the other buttons

			// add an item to the cart
			ButtonAdd.TouchUpInside += (sender, e) =>  { 
				barButton.BadgeValue = (++counter).ToString ();
			};

			var i = 0;

			// Cycle through the colors
			ButtonChangeColor.TouchUpInside += (sender, e) => {
				var c = _colors[i++];
				barButton.BadgeBGColor = c;
				barButton.BadgeTextColor = c == UIColor.White || c == UIColor.Yellow
					? UIColor.Black : UIColor.White;

				if (i >= _colors.Length)
					i = 0;
			};

			var j = 0;

			// Cycle through the positions
			ButtonChangePosition.TouchUpInside += (sender, e) => {

				barButton.BadgeOriginX = _positions[j].Item1;
				barButton.BadgeOriginY = _positions[j].Item2;

				j++;

				if (j >= _positions.Length)
					j = 0;
			};

			// Add the barButton as the RightBarButtonItem of the navigation bar
			this.NavigationItem.RightBarButtonItem = barButton;
		}
			
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

