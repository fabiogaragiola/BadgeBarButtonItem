using System;

using UIKit;
using CoreGraphics;
using CoreAnimation;
using Foundation;

namespace Utility
{
	public sealed class BadgeBarButtonItem : UIBarButtonItem
	{
		#region Fields

		UILabel _badge;

		string _badgeValue;

		UIColor _badgeBGColor;
		UIColor _badgeTextColor;
		UIColor _badgeBorderColor;

		UIFont _badgeFont;

		nfloat _badgePadding;
		nfloat _badgeMinSize;
		nfloat _badgeOriginX;
		nfloat _badgeOriginY;
		nfloat _badgeBorderWidth;

		bool _shouldHideBadgeAtZero;
		bool _shouldAnimateBadge;

		#endregion

		#region Public properties

		// Each time you change one of the properties, the badge will refresh with your changes
	
		/// <summary>
		/// Gets or sets the badge value to be displayed.
		/// </summary>
		/// <value>The badge value.</value>
		public string BadgeValue {
			get {
				return _badgeValue;
			}
			set {
				_badgeValue = value;
				SetBadgeValue (value);
			}
		}

		/// <summary>
		/// Gets or sets the badge background color.
		/// </summary>
		/// <value>The color of the badge background.</value>
		public UIColor BadgeBGColor {
			get {
				return _badgeBGColor;
			}
			set {
				_badgeBGColor = value;
				if (_badge != null) {
					this.RefreshBadge ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the badge text color.
		/// </summary>
		/// <value>The color of the badge text.</value>
		public UIColor BadgeTextColor {
			get {
				return _badgeTextColor;
			}
			set {
				_badgeTextColor = value;
				if (_badge != null) {
					this.RefreshBadge ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the badge border color.
		/// </summary>
		/// <value>The color of the badge border.</value>
		public UIColor BadgeBorderColor {
			get {
				return _badgeBorderColor;
			}
			set {
				_badgeBorderColor = value;
				if (_badge != null) {
					this.RefreshBadge();
				}
			}
		}

		/// <summary>
		/// Gets or sets the badge font.
		/// </summary>
		/// <value>The badge font.</value>
		public UIFont BadgeFont {
			get {
				return _badgeFont;
			}
			set {
				_badgeFont = value;
				if (_badge != null) {
					this.RefreshBadge ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the padding value for the badge.
		/// </summary>
		/// <value>The badge padding.</value>
		public nfloat BadgePadding {
			get {
				return _badgePadding;
			}
			set {
				_badgePadding = value;
				if (_badge != null) {
					this.UpdateBadgeFrame ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum size of the badge minimum.
		/// </summary>
		/// <value>The minimum size of the badge.</value>
		public nfloat BadgeMinSize {
			get {
				return _badgeMinSize;
			}
			set {
				_badgeMinSize = value;
				if (_badge != null) {
					this.UpdateBadgeFrame ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the X value for offsetting the badge over the BarButtonItem.
		/// </summary>
		/// <value>The badge X origin.</value>
		public nfloat BadgeOriginX {
			get {
				return _badgeOriginX;
			}
			set {
				_badgeOriginX = value;
				if (_badge != null) {
					this.UpdateBadgeFrame ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the Y value for offsetting the badge over the BarButtonItem.
		/// </summary>
		/// <value>The badge Y origin.</value>
		public nfloat BadgeOriginY {
			get {
				return _badgeOriginY;
			}
			set {
				_badgeOriginY = value;
				if (_badge != null) {
					this.UpdateBadgeFrame ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the badge border.
		/// </summary>
		/// <value>The badge border width.</value>
		public nfloat BadgeBorderWidth {
			get {
				return _badgeBorderWidth;
			}
			set {
				_badgeBorderWidth = value;
				if (_badge != null) {
					this.RefreshBadge();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Utility.BadgeBarButtonItem"/> should remove the badge when reaching zero.
		/// </summary>
		/// <value><c>true</c> if should hide badge at zero; otherwise, <c>false</c>.</value>
		public bool ShouldHideBadgeAtZero {
			get {
				return _shouldHideBadgeAtZero;
			}
			set {
				_shouldHideBadgeAtZero = value;
				if (_badge != null) {
					this.UpdateBadgeFrame ();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Utility.BadgeBarButtonItem"/> should animate the badge when its value changes.
		/// </summary>
		/// <value><c>true</c> if should animate badge; otherwise, <c>false</c>.</value>
		public bool ShouldAnimateBadge {
			get {
				return _shouldAnimateBadge;
			}
			set {
				_shouldAnimateBadge = value;
				if (_badge != null) {
					this.UpdateBadgeFrame ();
				}
			}
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Utility.BadgeBarButtonItem"/> class.
		/// </summary>
		/// <param name="customButton">Custom button.</param>
		public BadgeBarButtonItem (UIButton customButton)
		{
			this.CustomView = customButton;
			if (this.CustomView != null) {
				Initializer();
			}
		}

		void Initializer()
		{
			// Default design initialization
			this.BadgeBGColor     = UIColor.Red;
			this.BadgeTextColor   = UIColor.White;			
			this.BadgeFont        = UIFont.SystemFontOfSize (12);
			this.BadgePadding     = 6;
			this.BadgeMinSize     = 8;
			this.BadgeOriginX     = 7;
			this.BadgeOriginY     = -9;
			this.BadgeBorderWidth = 1f;
			this.ShouldHideBadgeAtZero = true;
			this.ShouldAnimateBadge = true;
			// Avoids badge to be clipped when animating its scale
			this.CustomView.ClipsToBounds = false;
		}

		#region Utility methods

		// Handles badge display when its properties have been changed (background, color and font)
		void RefreshBadge()
		{
			// Change new attributes
			_badge.TextColor        = this.BadgeTextColor;
			_badge.BackgroundColor  = this.BadgeBGColor;
			_badge.Font             = this.BadgeFont;

			var borderColor = this.BadgeBorderColor;
			if (borderColor != null)
			{
				_badge.Layer.BorderColor = borderColor.CGColor;
				_badge.Layer.BorderWidth = BadgeBorderWidth;
			}
			else
			{
				_badge.Layer.BorderColor = null;
			}
		}

		void UpdateBadgeFrame()
		{
			// When the value changes the badge could need to get bigger
			// Calculate expected size to fit new value
			// Use an intermediate label to get expected size thanks to sizeToFit
			// We don't call sizeToFit on the true label to avoid bad display
			UILabel frameLabel = DuplicateLabel(_badge);

			frameLabel.SizeToFit ();

			CGSize expectedLabelSize = frameLabel.Frame.Size;

			// Make sure that for small value, the badge will be big enough
			nfloat minHeight = expectedLabelSize.Height;

			// Using a const we make sure the badge respect the minimum size
			minHeight = (minHeight < this.BadgeMinSize) ? this.BadgeMinSize : expectedLabelSize.Height;
			nfloat minWidth = expectedLabelSize.Width;
			nfloat padding = this.BadgePadding;

			// Using const we make sure the badge doesn't get too smal
			minWidth = (minWidth < minHeight) ? minHeight : expectedLabelSize.Width;
			_badge.Frame = new CGRect(this.BadgeOriginX, this.BadgeOriginY, minWidth + padding, minHeight + padding);
			_badge.Layer.CornerRadius = (minHeight + padding) / 2;
			_badge.Layer.MasksToBounds = true;
		}

		// Handle the badge changing value
		void UpdateBadgeValueAnimated(bool animated)
		{
			// Bounce animation on badge if value changed and if animation authorized
			if (animated && this.ShouldAnimateBadge && _badge.Text != this.BadgeValue)
			{
				var animation = new CABasicAnimation ();
				animation.KeyPath = @"transform.scale";
				animation.From = NSObject.FromObject (1.5);
				animation.To = NSObject.FromObject (1);
				animation.Duration = 0.2;
				animation.TimingFunction = new CAMediaTimingFunction(0.4f,1.3f,1f,1f);
				_badge.Layer.AddAnimation (animation, @"bounceAnimation");
			}

			// Set the new value
			_badge.Text = this.BadgeValue;

			// Animate the size modification if needed
			double duration = animated ? 0.2 : 0;
			UIView.Animate (duration, UpdateBadgeFrame);
		}

		static UILabel DuplicateLabel(UILabel labelToCopy)
		{
			var duplicateLabel = new UILabel(labelToCopy.Frame);
			duplicateLabel.Text = labelToCopy.Text;
			duplicateLabel.Font = labelToCopy.Font;

			return duplicateLabel;
		}

		void RemoveBadge()
		{
			if (_badge != null) {
				// Animate badge removal
				UIView.AnimateNotify (0.15f, 0.0F,
					UIViewAnimationOptions.CurveEaseIn, 
					() => {
						_badge.Transform = CGAffineTransform.MakeScale (0.1f, 0.1f);
					},
					completed => {
						_badge.RemoveFromSuperview();
						_badge = null;
					}
				);
			}
		}

		#endregion

		#region Setter

		void SetBadgeValue(string badgeValue)
		{
			// Set new value
			_badgeValue = badgeValue;

			// When changing the badge value check if we need to remove the badge
			if (string.IsNullOrEmpty (badgeValue) || (badgeValue == @"0" && this.ShouldHideBadgeAtZero)) {
				RemoveBadge();
			} else if (_badge == null) {
				// Create a new badge because not existing
				_badge = new UILabel (new CGRect (this.BadgeOriginX, this.BadgeOriginY, 20, 20)) {
					TextColor = this.BadgeTextColor,
					BackgroundColor = this.BadgeBGColor,
					Font = this.BadgeFont,
					TextAlignment = UITextAlignment.Center,
				};
				// Fix for iOS 9: Correctly apply the CornerRadius later on
				_badge.Layer.MasksToBounds = true;

				var borderColor = this.BadgeBorderColor;
				if (borderColor != null)
				{
					_badge.Layer.BorderColor = borderColor.CGColor;
					_badge.Layer.BorderWidth = BadgeBorderWidth;
				}

				this.CustomView.AddSubview (_badge);
				this.UpdateBadgeValueAnimated(false);
			} else {
				this.UpdateBadgeValueAnimated(true);
			}
		}

		#endregion
	}
}

