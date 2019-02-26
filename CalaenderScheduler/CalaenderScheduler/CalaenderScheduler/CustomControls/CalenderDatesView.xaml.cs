using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalaenderScheduler.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalenderDatesView : ContentView
	{
        public event EventHandler FrameDateClicked;
        public CalenderDatesView ()
		{
			InitializeComponent ();

            TapGestureRecognizer gridTap = new TapGestureRecognizer();
            gridTap.Tapped += (sender, e) => {
                FrameDateClicked.Invoke(sender, e);
            };
            frmDate.GestureRecognizers.Add(gridTap);
            dtFrame.IsVisible = false;
            frmDate.CornerRadius = 0;
            frmDate.HasShadow = false;
            dtFrame.HorizontalOptions = LayoutOptions.Center;
            dtFrame.VerticalOptions = LayoutOptions.EndAndExpand;
            frmDate.IsEnabled = false;
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextDateProperty.PropertyName)
            {
                lblDate.Text = TextDate;
            }
            else if (propertyName == DateGridBackgroundColorProperty.PropertyName)
            {
                frmDate.BackgroundColor = DateGridBackgroundColor;
            }
            else if (propertyName == IsActivityCountVisibleProperty.PropertyName)
            {
                dtFrame.IsVisible = IsActivityCountVisible;
            }
            else if (propertyName == TextDateClassIdProperty.PropertyName)
            {
                frmDate.ClassId = TextDateClassId;
            }
            else if (propertyName == DateCornerRadiusProperty.PropertyName)
            {
                frmDate.CornerRadius = DateCornerRadius;
            }
            else if (propertyName == DateHasShadowProperty.PropertyName)
            {
                frmDate.HasShadow = DateHasShadow;
            }
            else if (propertyName == DateTextColorProperty.PropertyName)
            {
                lblDate.TextColor = DateTextColor;
            }
            else if (propertyName == DateTextFontAttributesProperty.PropertyName)
            {
                lblDate.FontAttributes = DateTextFontAttributes;
            }
            else if (propertyName == ActivityCountTextProperty.PropertyName)
            {
                lblActivitiesCount.Text = ActivityCountText;
            }
            else if (propertyName == ActivityCountBackgroundColorProperty.PropertyName)
            {
                dtFrame.BackgroundColor = ActivityCountBackgroundColor;
            }
            else if (propertyName == ActivityCountHorizontalLayoutOptionProperty.PropertyName)
            {
                dtFrame.HorizontalOptions = ActivityCountHorizontalLayoutOption;
            }
            else if (propertyName == ActivityCountVerticalLayoutOptionProperty.PropertyName)
            {
               
                dtFrame.VerticalOptions = ActivityCountVerticalLayoutOption;
            }
            else if (propertyName == IsDateEnabledProperty.PropertyName)
            {

                frmDate.IsEnabled = IsDateEnabled;
            }
        }
        public string TextDate
        {
            get
            {
                return (string)GetValue(TextDateProperty);
            }

            set
            {
                SetValue(TextDateProperty, value);
            }
        }
        public Color DateGridBackgroundColor
        {
            get
            {
                return (Color)GetValue(DateGridBackgroundColorProperty);
            }

            set
            {
                SetValue(DateGridBackgroundColorProperty, value);
            }
        }
        public Color ActivityCountBackgroundColor
        {
            get
            {
                return (Color)GetValue(ActivityCountBackgroundColorProperty);
            }

            set
            {
                SetValue(ActivityCountBackgroundColorProperty, value);
            }
        }
        public Color DateTextColor
        {
            get
            {
                return (Color)GetValue(DateTextColorProperty);
            }

            set
            {
                SetValue(DateTextColorProperty, value);
            }
        }
        public FontAttributes DateTextFontAttributes
        {
            get
            {
                return (FontAttributes)GetValue(DateTextFontAttributesProperty);
            }

            set
            {
                SetValue(DateTextFontAttributesProperty, value);
            }
        }
        public bool IsActivityCountVisible
        {
            get
            {
                return (bool)GetValue(IsActivityCountVisibleProperty);
            }

            set
            {
                SetValue(IsActivityCountVisibleProperty, value);
            }
        }
        public string TextDateClassId
        {
            get
            {
                return (string)GetValue(TextDateClassIdProperty);
            }

            set
            {
                SetValue(TextDateClassIdProperty, value);
            }
        }
        public float DateCornerRadius
        {
            get
            {
                return (float)GetValue(DateCornerRadiusProperty);
            }

            set
            {
                SetValue(DateCornerRadiusProperty, value);
            }
        }
        public bool DateHasShadow
        {
            get
            {
                return (bool)GetValue(DateHasShadowProperty);
            }

            set
            {
                SetValue(DateHasShadowProperty, value);
            }
        }
        public bool IsDateEnabled
        {
            get
            {
                return (bool)GetValue(IsDateEnabledProperty);
            }

            set
            {
                SetValue(IsDateEnabledProperty, value);
            }
        }
        public string ActivityCountText
        {
            get
            {
                return (string)GetValue(ActivityCountTextProperty);
            }

            set
            {
                SetValue(ActivityCountTextProperty, value);
            }
        }
        public LayoutOptions ActivityCountHorizontalLayoutOption
        {
            get
            {
                return (LayoutOptions)GetValue(ActivityCountHorizontalLayoutOptionProperty);
            }

            set
            {
                SetValue(ActivityCountHorizontalLayoutOptionProperty, value);
            }
        }
        public LayoutOptions ActivityCountVerticalLayoutOption
        {
            get
            {
                return (LayoutOptions)GetValue(ActivityCountVerticalLayoutOptionProperty);
            }

            set
            {
                SetValue(ActivityCountVerticalLayoutOptionProperty, value);
            }
        }

        public static readonly BindableProperty TextDateProperty = BindableProperty.Create(nameof(TextDate), typeof(string), typeof(CalenderDatesView), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty DateGridBackgroundColorProperty = BindableProperty.Create(nameof(DateGridBackgroundColor), typeof(Color), typeof(CalenderDatesView), default(Color), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty IsActivityCountVisibleProperty = BindableProperty.Create(nameof(IsActivityCountVisible), typeof(bool), typeof(CalenderDatesView), default(bool), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty TextDateClassIdProperty = BindableProperty.Create(nameof(TextDateClassId), typeof(string), typeof(CalenderDatesView), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty DateCornerRadiusProperty = BindableProperty.Create(nameof(DateCornerRadius), typeof(float), typeof(CalenderDatesView), default(float), BindingMode.TwoWay);
        public static readonly BindableProperty DateHasShadowProperty = BindableProperty.Create(nameof(DateHasShadow), typeof(bool), typeof(CalenderDatesView), default(bool), BindingMode.TwoWay);
        public static readonly BindableProperty DateTextColorProperty = BindableProperty.Create(nameof(DateTextColor), typeof(Color), typeof(CalenderDatesView), default(Color), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty DateTextFontAttributesProperty = BindableProperty.Create(nameof(DateTextFontAttributes), typeof(FontAttributes), typeof(CalenderDatesView), default(FontAttributes), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty ActivityCountTextProperty = BindableProperty.Create(nameof(ActivityCountText), typeof(string), typeof(CalenderDatesView), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty ActivityCountBackgroundColorProperty = BindableProperty.Create(nameof(ActivityCountBackgroundColor), typeof(Color), typeof(CalenderDatesView), default(Color), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty ActivityCountHorizontalLayoutOptionProperty = BindableProperty.Create(nameof(ActivityCountHorizontalLayoutOption), typeof(LayoutOptions), typeof(CalenderDatesView), default(LayoutOptions), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty ActivityCountVerticalLayoutOptionProperty = BindableProperty.Create(nameof(ActivityCountVerticalLayoutOption), typeof(LayoutOptions), typeof(CalenderDatesView), default(LayoutOptions), Xamarin.Forms.BindingMode.TwoWay);
        public static readonly BindableProperty IsDateEnabledProperty = BindableProperty.Create(nameof(IsDateEnabled), typeof(bool), typeof(CalenderDatesView), default(bool), Xamarin.Forms.BindingMode.TwoWay);
    }
}