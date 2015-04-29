BadgeBarButtonItem
==================

This is a porting to Xamarin.iOS Unified API of [BBBadgeBarButtonItem](https://github.com/TanguyAladenise/BBBadgeBarButtonItem) by [Tanguy Aladenise](https://github.com/TanguyAladenise)

This class creates an easily customizable BarButtonItem with a badge on top. The BarButtonItem can be any custom view and the badge can display numbers or strings.

<img alt="ScreenShot BarButtonItem" src="https://github.com/fabiogaragiola/BadgeBarButtonItem/blob/master/Screenshot.png?raw=true" width="320px"/>


How To Get Started
------------------

Just drop the BadgeBarButtonItem.cs file into your project and you're ready to go.
Add a ```using Utility;``` directive in your code or change the namespace in the BadgeBarButtonItem.cs file to suite your needs.


Usage
-----

You only need to instantiate the BadgeBarButtonItem and add it to your navigation bar:

```c#
UIButton customButton = new UIButton();
//...

// Create and add our custom BadgeBarButtonItem
BadgeBarButtonItem barButton = new BBBadgeBarButtonItem(customButton);
// Set a value for the badge
barButton.badgeValue = @"1";

// Add it as the RightBarButtonItem of the navigation bar
this.NavigationItem.RightBarButtonItem = barButton;
```

If you want the BarButtonItem to handle touch events, use a UIButton as customView.


Public properties
-----------------

Take a look at BadgeBarButtonItem.cs to see how easily and quickly you can customize the badge.
Remember that each time you change one of these value, the badge will be refreshed to display your changes.

```c#
// Each time you change one of properties, the badge will refresh with your changes.

// Badge value
public string BadgeValue { get; set; }

// Badge background color.
public UIColor BadgeBGColor { get; set; }

// Badge text color.
public UIColor BadgeTextColor { get; set; }

// Badge font.
public UIFont BadgeFont { get; set; }

// Padding value for the badge.
public nfloat BadgePadding { get; set; }

// Minimum size of the badge minimum.
public nfloat BadgeMinSize { get; set; }

// X value for offsetting the badge over the BarButtonItem.
public nfloat BadgeOriginX { get; set; }

// Y value for offseting the badge over the BarButtonItem.
public nfloat BadgeOriginY { get; set; }

// Value indicating whether the badge should be removed when reaching zero.
public bool ShouldHideBadgeAtZero { get; set; }

// Value indicating whether the badge should be animated when its value changes.
public bool ShouldAnimateBadge { get; set; }

```

You can also choose to turn off the little bounce animation triggered when changing the badge value or decide if 0 should be displayed or not.

To do
-----

Add support for tint color.

