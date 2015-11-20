// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BadgeBarButtonItemExample
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton ButtonAdd { get; set; }

		[Outlet]
		UIKit.UIButton ButtonChangeBorderColor { get; set; }

		[Outlet]
		UIKit.UIButton ButtonChangeColor { get; set; }

		[Outlet]
		UIKit.UIButton ButtonChangePosition { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ButtonAdd != null) {
				ButtonAdd.Dispose ();
				ButtonAdd = null;
			}

			if (ButtonChangeColor != null) {
				ButtonChangeColor.Dispose ();
				ButtonChangeColor = null;
			}

			if (ButtonChangePosition != null) {
				ButtonChangePosition.Dispose ();
				ButtonChangePosition = null;
			}

			if (ButtonChangeBorderColor != null) {
				ButtonChangeBorderColor.Dispose ();
				ButtonChangeBorderColor = null;
			}
		}
	}
}
