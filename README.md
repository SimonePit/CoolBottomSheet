
[![Nuget](https://img.shields.io/badge/Nuget-1.0.2-green?style=flat)](https://www.nuget.org/packages/CoolBottomSheet/)  

<img height="400" src="https://github.com/SimonePit/CoolBottomSheet/blob/master/CoolBottomSheetGif.gif?v=3&s=200" title="gif" alt="gif"></a>
<img height="400" src="https://github.com/SimonePit/CoolBottomSheet/blob/master/BottomSheetIos.jpg?v=3&s=200" title="gif" alt="gif"></a>

# CoolBottomSheet
Xamarin Forms: Material Design bottom sheet

## Installation

- OPTION 1) Install from nuget package manager looking for `CoolBottomSheet`
- OPTION 2) Install by command line with command -> `Install-Package CoolBottomSheet -Version 1.0.1`

## Setup
- Import CoolBottomSheet
```xaml
xmlns:customControl="clr-namespace:CoolBottomSheet;assembly=CoolBottomSheet"
```
## Sample

```xaml
<customControl:BottomSheet
        CornerRadiusBottomSheet="25"
        BackgroundBottomSheetColor="LightGray"
        >
        <customControl:BottomSheet.ContentMainPage>
            <StackLayout>
                ....
            </StackLayout>
        </customControl:BottomSheet.ContentMainPage>
        <customControl:BottomSheet.ContentBottomSheet>
            <StackLayout>
                ....
            </StackLayout>
        </customControl:BottomSheet.ContentBottomSheet>
  </customControl:BottomSheet>
```

## Features

### Properties
1. `ContentMainPage` - Is the content view of Main page (you can use every layout)

2. `ContentBottomSheet` - Is the content view of BottomSheet (you can use every layout)

3. `BackgroundBottomSheetColor` - Background color of bottom sheet

4. `CornerRadiusBottomSheet` - Corner radius of bottom sheet

5. `PercentageLockMainPageTranslation` - Percentage of screen where the scroll of the main page must be locked and the bottom sheet         starts to overlap the main page

6. `PercentageExpandBottomSheet` - Is the height of bottom sheet after click the bottom button

7. `PercentageHideBottomSheet` - When BottomSheet reaches this percentage will be auto-hidden

8. `MinPercentageBottomSheetSwipe` - Is the min percentage who bottomSheet can reach

9. `MaxPercentageBottomSheetSwipe` - Is the max percentage who bottomSheet can reach

10. `PercentageHeightMainPage` - Initial height of mainpage express in percentage of screen ( if initial height of mainpage is 70 % of 
                                 screen height, the bottomSheet percentage will be 30%)
                                 
11. `IsBottomButtonEnable` - If enabled a button appear when the BottomSheet disappear

12. `BottomButtonText` - Text of the bottom button
