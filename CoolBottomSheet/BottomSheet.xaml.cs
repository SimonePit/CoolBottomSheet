using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoolBottomSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomSheet : ContentView
    {
        double y;
        double factorScale = 0;
        public BottomSheet()
        {
            InitializeComponent();
            SetInitialPositionPages();
        }
        void SetInitialPositionPages()
        {
            RelativeLayout.SetHeightConstraint(contentview, Constraint.RelativeToParent((parent) =>
            {
                return parent.Height * PercentageHeightMainPage;
            }));
            RelativeLayout.SetYConstraint(frame, Constraint.RelativeToParent((parent) =>
            {
                return parent.Height * PercentageHeightMainPage;
            }));
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    var YTranslation = y + e.TotalY;
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.

                    var framePos = Height * PercentageHeightMainPage;
                    var finalPos = framePos + YTranslation;
                    factorScale = finalPos / Height;

                    if (factorScale > MaxPercentageBottomSheetSwipe && factorScale < MinPercentageBottomSheetSwipe)
                    {
                        frame.TranslationY = YTranslation;
                        if (factorScale > PercentageLockMainPageTranslation)
                        {
                            RelativeLayout.SetHeightConstraint(contentview, Constraint.RelativeToParent((parent) =>
                            {
                                return parent.Height * factorScale;
                            }));
                        }
                    }

                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    var initFramePos = Height * PercentageHeightMainPage;
                    var finalFramePos = initFramePos + frame.TranslationY;
                    factorScale = finalFramePos / Height;
                    if (factorScale > PercentageHideBottomSheet)
                    {
                        var ytrans = Height * (1 - PercentageHeightMainPage);
                        frame.TranslateTo(0, ytrans, 300, Easing.BounceOut);
                        y = ytrans;
                        RelativeLayout.SetHeightConstraint(contentview, Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Height;
                        }));
                        expandeButton.FadeTo(1, 500, Easing.CubicIn);
                    }
                    else
                    {
                        y = frame.TranslationY;
                    }
                    break;
            }

        }

        private void ExpandeButton_Clicked(object sender, EventArgs e)
        {
            var translationY = frame.TranslationY - (Height - Height * PercentageExpandBottomSheet);
            RelativeLayout.SetHeightConstraint(contentview, Constraint.RelativeToParent((parent) =>
            {
                return parent.Height * PercentageExpandBottomSheet;
            }));

            frame.TranslateTo(frame.X, translationY, 10, Easing.BounceIn);
            y = translationY;
            expandeButton.FadeTo(0, 100, Easing.CubicOut);
        }

        //PROPRIETA BINDIBILI COMPONENTE BOTTOMSHEET

        //VIEW BOTTOM SHEET
        public static readonly BindableProperty ContentBottomSheetProperty = BindableProperty.Create(
           nameof(ContentBottomSheet),
           typeof(View),
           typeof(BottomSheet)
           );
        public View ContentBottomSheet
        {
            get => (View)GetValue(ContentBottomSheetProperty);
            set => SetValue(ContentBottomSheetProperty, value);
        }

        //VIEW MAIN PAGE
        public static readonly BindableProperty ContentMainPageProperty = BindableProperty.Create(
           nameof(ContentMainPage),
           typeof(View),
           typeof(BottomSheet)
           );
        public View ContentMainPage
        {
            get => (View)GetValue(ContentMainPageProperty);
            set => SetValue(ContentMainPageProperty, value);
        }

        //INITIAL HEIGHT IN PERCENTAGE MAIN PAGE
        public static readonly BindableProperty PercentageHeightMainPageProperty = BindableProperty.Create(
           nameof(PercentageHeightMainPage),
           typeof(double),
           typeof(BottomSheet)
           );
        public double PercentageHeightMainPage
        {
            get => (double)GetValue(PercentageHeightMainPageProperty);
            set => SetValue(PercentageHeightMainPageProperty, value);
        }

        //POSIZIONE MASSIMA IN PERCENTUALE CHE PUO RAGGIUNGERE IL BOTTOM SHEET
        //0.5 IL BOTTOM SHEET PUO SALIRE FINO A META SCHERMO
        //0 IL BOTTOM SHEET ARRIVA FINO IN CIMA ALLO SCHERMO
        public static readonly BindableProperty MaxPercentageBottomSheetSwipeProperty = BindableProperty.Create(
           nameof(MaxPercentageBottomSheetSwipe),
           typeof(double),
           typeof(BottomSheet),
           defaultValue: -0.02
           );
        public double MaxPercentageBottomSheetSwipe
        {
            get => (double)GetValue(MaxPercentageBottomSheetSwipeProperty);
            set => SetValue(MaxPercentageBottomSheetSwipeProperty, value);
        }

        //POSIZIONE MINIMA IN PERCENTUALE CHE PUO RAGGIUNGERE IL BOTTOM SHEET
        //0.5 IL BOTTOM SHEET PUO SCENDERE FINO A META SCHERMO
        //1 IL BOTTOM SHEET PUO SCENDERE FINO ALLA FINE DELLO SCHERMO
        public static readonly BindableProperty MinPercentageBottomSheetSwipeProperty = BindableProperty.Create(
           nameof(MinPercentageBottomSheetSwipe),
           typeof(double),
           typeof(BottomSheet),
           defaultValue: 1.1
           );
        public double MinPercentageBottomSheetSwipe
        {
            get => (double)GetValue(MinPercentageBottomSheetSwipeProperty);
            set => SetValue(MinPercentageBottomSheetSwipeProperty, value);
        }

        /*
          Quando si raggiunge la percentuale indicata e si completa la pan gesture
          il bottom sheet scompare e appare il bottomButton 
        */
        public static readonly BindableProperty PercentageHideBottomSheetProperty = BindableProperty.Create(
           nameof(PercentageHideBottomSheet),
           typeof(double),
           typeof(BottomSheet),
           defaultValue: 0.9
           );
        public double PercentageHideBottomSheet
        {
            get => (double)GetValue(PercentageHideBottomSheetProperty);
            set => SetValue(PercentageHideBottomSheetProperty, value);
        }

        /*
          Altezza in percentuale del bottom sheet quando si clicca il botton di espansione 
          0 si espande copre tutto lo schermo
          0,5 si espande fino a metà schermo,
          1 non si espande
        */
        public static readonly BindableProperty PercentageExpandBottomSheetProperty = BindableProperty.Create(
           nameof(PercentageExpandBottomSheet),
           typeof(double),
           typeof(BottomSheet),
           defaultValue: 0.9
           );
        public double PercentageExpandBottomSheet
        {
            get => (double)GetValue(PercentageExpandBottomSheetProperty);
            set => SetValue(PercentageExpandBottomSheetProperty, value);
        }

        //PERCENTUALE ALLA QUALE LA MAIN PAGE SI BLOCCA E IL BOTTOM SHEET INIZIA A SWIPARCI SOPRA
        public static readonly BindableProperty PercentageLockMainPageTranslationProperty = BindableProperty.Create(
           nameof(PercentageLockMainPageTranslation),
           typeof(double),
           typeof(BottomSheet),
           defaultValue: 0.4
           );
        public double PercentageLockMainPageTranslation
        {
            get => (double)GetValue(PercentageLockMainPageTranslationProperty);
            set => SetValue(PercentageLockMainPageTranslationProperty, value);
        }

        //TESTO DEL BOTTONE CHE APPARE QUANDO IL BOTTOM SHEET SI CHIUDE
        public static readonly BindableProperty BottomButtonTextProperty = BindableProperty.Create(
           nameof(BottomButtonText),
           typeof(string),
           typeof(BottomSheet),
           defaultValue: "Expand"
           );
        public string BottomButtonText
        {
            get => (string)GetValue(BottomButtonTextProperty);
            set => SetValue(BottomButtonTextProperty, value);
        }

        //PROPERTY CHE STABILISCE SE DEVE ESISTERE IL BOTTOM BUTTON
        public static readonly BindableProperty IsBottomButtonEnableProperty = BindableProperty.Create(
           nameof(IsBottomButtonEnable),
           typeof(bool),
           typeof(BottomSheet),
           defaultValue: true
           );
        public bool IsBottomButtonEnable
        {
            get => (bool)GetValue(IsBottomButtonEnableProperty);
            set => SetValue(IsBottomButtonEnableProperty, value);
        }

        /*
          Corner radius bottom sheet
        */
        public static readonly BindableProperty CornerRadiusBottomSheetProperty = BindableProperty.Create(
           nameof(CornerRadiusBottomSheet),
           typeof(int),
           typeof(BottomSheet),
           defaultValue: 15
           );
        public int CornerRadiusBottomSheet
        {
            get => (int)GetValue(CornerRadiusBottomSheetProperty);
            set => SetValue(CornerRadiusBottomSheetProperty, value);
        }

        /*
         background bottom sheet color
       */
        public static readonly BindableProperty BackgroundBottomSheetColorProperty = BindableProperty.Create(
           nameof(BackgroundBottomSheetColor),
           typeof(Color),
           typeof(BottomSheet)
           );
        public Color BackgroundBottomSheetColor
        {
            get => (Color)GetValue(BackgroundBottomSheetColorProperty);
            set => SetValue(BackgroundBottomSheetColorProperty, value);
        }
    }
}