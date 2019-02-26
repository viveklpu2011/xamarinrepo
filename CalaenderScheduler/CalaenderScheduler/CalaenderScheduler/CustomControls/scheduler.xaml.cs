using CalaenderScheduler.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalaenderScheduler.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scheduler : ContentView
    {

        int monthname = 0;
        int yearname = 0;
        string SelectedDate = string.Empty;
        List<CalenderData> lstCalenderData = new List<CalenderData>();
        List<ActivitiesData> lstActivitiesData = new List<ActivitiesData>();
        public scheduler()
        {
            InitializeComponent();
            monthname = DateTime.Today.Month;
            yearname = DateTime.Today.Year;
            DrawCalender(DateTime.Today.Month, DateTime.Today.Day, yearname);
            overlay.ScaleTo(2, 2000);
            FabbuttonAddActivities.Clicked += FabButtonClicked;
            closeoverlay.Clicked += CloseOverlayClicked;
            btncancelactivities.Clicked += CloseOverlayClicked;
            btnsubmitactivities.Clicked += Btnsubmitactivities_Clicked;
            lblSelectedDate.MinimumDate = DateTime.Now;
            #region Month Year Navigation Clicked Events
            button_month_right.Clicked += Button_month_right_Clicked;
            button_month_left.Clicked += Button_month_left_Clicked;
            button_year_left.Clicked += Button_year_left_Clicked;
            button_year_right.Clicked += Button_year_right_Clicked;

            #endregion
        }

        private async void Btnsubmitactivities_Clicked(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            int uniqueId = (int)(zeroDate.Ticks / 10000);

            ActivitiesData data = new ActivitiesData();
            data.activitiesDate = lblSelectedDate.Date;
            data.activityDescription = editorActivities.Text;
            data.activityHeading = entryactivitytitle.Text;
            data.activityId = uniqueId;
            lstActivitiesData.Add(data);
            editorActivities.Unfocus();
            grdOverlay.VerticalOptions = LayoutOptions.Center;
            overlay.IsVisible = false;
            await overlay.ScaleTo(2, 2000);

            int currday = 0;
            if (DateTime.Today.Month == monthname && DateTime.Today.Year == yearname)
            {
                currday = DateTime.Today.Day;
            }
            else
            {
                currday = 0;
            }
            DrawCalender(monthname, currday, yearname);

            //lstactivities.ItemsSource = null;
            //var lstactivity = lstActivitiesData.Where(x => x.activitiesDate == Convert.ToDateTime(lblSelectedDate.Date)).ToList();
            //lstactivities.ItemsSource = lstactivity;
        }

        private async void FabButtonClicked(object sender, EventArgs e)
        {
            
            overlay.IsVisible = true;
            editorActivities.Text = string.Empty;
            entryactivitytitle.Text = string.Empty;
            grdOverlay.VerticalOptions = LayoutOptions.Center;
            await overlay.ScaleTo(1, 500);
            lblSelectedDate.Date = string.IsNullOrEmpty(SelectedDate) ? DateTime.Now : Convert.ToDateTime(SelectedDate);
        }
        private async void CloseOverlayClicked(object sender, EventArgs e)
        {
            editorActivities.Unfocus();
            grdOverlay.VerticalOptions = LayoutOptions.Center;
            overlay.IsVisible = false;

            await overlay.ScaleTo(2, 2000);
        }

        #region Month Year Event Navigation Functions
        private void Button_year_right_Clicked(object sender, EventArgs e)
        {
            int currday = 0;
            yearname++;
            lblYear.Text = getMonthName(monthname) + ", " + yearname;
            if (DateTime.Today.Month == monthname && DateTime.Today.Year == yearname)
            {
                currday = DateTime.Today.Day;
            }
            else
            {
                currday = 0;
            }
            DrawCalender(monthname, currday, yearname);
            lstactivities.ItemsSource = null;
        }
        private void Button_year_left_Clicked(object sender, EventArgs e)
        {
            int currday = 0;
            yearname--;
            lblYear.Text = getMonthName(monthname) + ", " + yearname;
            if (DateTime.Today.Month == monthname && DateTime.Today.Year == yearname)
            {
                currday = DateTime.Today.Day;
            }
            else
            {
                currday = 0;
            }
            DrawCalender(monthname, currday, yearname);
            lstactivities.ItemsSource = null;
        }
        private void Button_month_left_Clicked(object sender, EventArgs e)
        {
            int currday = 0;
            monthname--;
            if (monthname < 1)
            {
                monthname = 12;
                yearname = yearname - 1;
                lblYear.Text = getMonthName(monthname) + ", " + yearname;
            }
            lblmonth.Text = getMonthName(monthname);

            if (DateTime.Today.Month == monthname && DateTime.Today.Year == yearname)
            {
                currday = DateTime.Today.Day;
            }
            else
            {
                currday = 0;
            }
            DrawCalender(monthname, currday, yearname);
            lstactivities.ItemsSource = null;
        }
        private void Button_month_right_Clicked(object sender, EventArgs e)
        {
            int currday = 0;
            monthname++;
            if (monthname > 12)
            {
                monthname = 1;
                yearname = yearname + 1;
                lblYear.Text = getMonthName(monthname) + ", " + yearname;
            }
            lblmonth.Text = getMonthName(monthname);

            if (DateTime.Today.Month == monthname && DateTime.Today.Year == yearname)
            {
                currday = DateTime.Today.Day;
            }
            else
            {
                currday = 0;
            }
            DrawCalender(monthname, currday, yearname);
            lstactivities.ItemsSource = null;
        }
        #endregion

        #region Draw and display calender
        private void DrawCalender(int monthnumber, int currday, int yearname)
        {
            CalenderDatesView[] b2 = new CalenderDatesView[100];

            grdCalender.Children.Clear();
            int CurrentYear = yearname;
            int CurrentMonth = monthnumber;
            int CurrentDay = currday;
            displayCalendar(CurrentYear, CurrentMonth, CurrentDay);
            var tileslist = lstCalenderData;
            grdCalender.RowSpacing = 0;
            grdCalender.ColumnSpacing = 0;

            grdCalender.RowDefinitions = new RowDefinitionCollection();
            grdCalender.ColumnDefinitions = new ColumnDefinitionCollection();

            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grdCalender.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            int k = 0;
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    if (k >= tileslist.Count())
                    {
                        break;
                    }
                    for (int i = 0; i < tileslist[k].NumberOfDaysGapping; i++)
                    {
                        if (k <= tileslist[k].NumberOfDaysGapping)
                        {
                            c++;
                            continue;
                        }
                    }
                    grdCalender.Children.Add(b2[k] = new CalenderDatesView
                    {
                        DateGridBackgroundColor = Color.White,
                        TextDate = tileslist[k].CalenderDay.ToString(),
                        IsActivityCountVisible = lstActivitiesData.Where(x => x.activitiesDate == Convert.ToDateTime(tileslist[k].FullDate)).Count() > 0 ? true : false,
                        TextDateClassId = tileslist[k].CalenderDay.ToString(),
                        DateCornerRadius = 0,
                        DateHasShadow = false,
                        HeightRequest=50,
                        DateTextColor = tileslist[k].CurrentDay == Convert.ToString(DateTime.Now.Day) ? Color.DeepPink : Convert.ToDateTime(tileslist[k].FullDate) >= DateTime.Now ? Color.Black : Color.LightGray,
                        ActivityCountText = Convert.ToString(lstActivitiesData.Where(x => x.activitiesDate == Convert.ToDateTime(tileslist[k].FullDate)).Count()),
                        ActivityCountVerticalLayoutOption = LayoutOptions.CenterAndExpand,
                        IsDateEnabled = Convert.ToDateTime(tileslist[k].FullDate) >= DateTime.Now ? true : tileslist[k].CurrentDay == Convert.ToString(DateTime.Now.Day) ? true:false
                    }, c, r);
                    b2[k].FrameDateClicked += (object sender, EventArgs e) =>
                    {
                        var lst = grdCalender.Children.ToList();
                        foreach (var item in lst)
                        {
                            var frame = item.FindByName<Frame>("frmDate");
                            if (frame.BorderColor == Color.Red)
                            {
                                frame.BorderColor = Color.White;
                            }
                        }
                        var btn = (Frame)sender;

                        var data = tileslist.Where(x => x.CalenderDay == btn.ClassId).FirstOrDefault();
                        SelectedDate = data.FullDate;
                        if (Convert.ToInt32(data.CalenderDay) == DateTime.Today.Day && data.CurrentMonth == DateTime.Today.Month && data.CurrentYear == DateTime.Today.Year)
                        {
                            btn.BackgroundColor = Color.White;
                        }
                        else
                        {
                            btn.BorderColor = Color.Red;
                        }
                        var lstactivity = lstActivitiesData.Where(x => x.activitiesDate == Convert.ToDateTime(data.FullDate)).ToList();
                        lstactivities.ItemsSource = lstactivity;
                    };
                    k = k + 1;
                }
            }
        }
        private string getMonthName(int theMonth)
        {
            // changes suggested by Brandon Croft.  Much shorter than
            // using the arraylist!
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            string month = info.MonthNames[theMonth - 1];
            return month;
        }
        private void displayCalendar(int TheYear, int TheMonth, int TheDay)
        {
            lstCalenderData.Clear();
            lstCalenderData = null;
            lstCalenderData = new List<CalenderData>();
            // default to the first of the month
            Int16 FirstDayOfMonth = 1;
            Int32 NumberOfDaysInMonth = DateTime.DaysInMonth(TheYear, TheMonth);
            DateTime FullDateToUse = new DateTime(TheYear, TheMonth, FirstDayOfMonth);

            // this is the day of week we're gonna start with (0-6)
            Int32 StartDay = Convert.ToInt32(FullDateToUse.DayOfWeek);

            // this indicates how much padding we need for
            // the first day of the month.
            Int32 NumberOfTabs = StartDay;

            // this will display the month name and
            // the headings for the days of the week.
            //displayHeader(getMonthName(TheMonth), TheYear.ToString(), true);

            // accumulator used so we'll know when to wrap 
            // to the next week.
            int DayOfWeek = StartDay;
            CalenderData data = new CalenderData();
            for (int Counter = 1; Counter <= NumberOfDaysInMonth; Counter++)
            {
                data = new CalenderData();
                string DayString = "";
                // if it's the first day of the month, we'll need
                // padding so we start on the correct "day"
                if (Counter == 1)
                {
                    //String Padding = new String('', NumberOfTabs);
                    data.NumberOfDaysGapping = NumberOfTabs;
                    DayString = Counter.ToString();
                    data.CalenderDay = DayString;
                    data.CurrentMonth = monthname;
                    data.CurrentYear = yearname;
                    data.FullDate = Convert.ToString(monthname) + "/" + DayString + "/" + Convert.ToString(yearname);
                    data.NoOfDaysInMonth = NumberOfDaysInMonth;
                }
                else
                {
                    DayString = Counter.ToString();
                    data.CalenderDay = DayString;
                    data.CurrentMonth = monthname;
                    data.CurrentYear = yearname;
                    data.FullDate = Convert.ToString(monthname) + "/" + DayString + "/" + Convert.ToString(yearname);
                    data.NoOfDaysInMonth = NumberOfDaysInMonth;
                }

                // highlight todays date (using *)
                if (TheDay != 1 && Counter == TheDay)
                {
                    DayString = String.Concat("", DayString);
                    data.CurrentDay = DayString;
                    data.CurrentMonth = monthname;
                    data.CurrentYear = yearname;
                    data.FullDate = Convert.ToString(monthname) + "/" + DayString + "/" + Convert.ToString(yearname);
                    data.NoOfDaysInMonth = NumberOfDaysInMonth;
                }

                // start a new line only if this isn't the first day
                if (DayOfWeek % 7 == 0 && Counter > 1)
                {
                    DayString = String.Concat("", Counter.ToString());
                    data.CalenderDay = DayString;
                    data.CurrentMonth = monthname;
                    data.CurrentYear = yearname;
                    data.FullDate = Convert.ToString(monthname) + "/" + DayString + "/" + Convert.ToString(yearname);
                    data.NoOfDaysInMonth = NumberOfDaysInMonth;
                }

                // separate each day with a tab
                data.CalenderDay = DayString;
                data.CurrentMonth = monthname;
                data.CurrentYear = yearname;
                data.NoOfDaysInMonth = NumberOfDaysInMonth;
                data.FullDate = Convert.ToString(monthname) + "/" + DayString + "/" + Convert.ToString(yearname);
                Console.Write("{0}\t", DayString);
                lstCalenderData.Add(data);
                DayOfWeek++;
            }
            Console.WriteLine();
        }
        private static void displayHeader(string theMonthName, string theYear, bool ShowCurrentDate)
        {
            // the Month, year
            string Header = String.Concat(theMonthName, ", ");
            Header = String.Concat(Header, theYear);
            String Days = "S\tM\tT\tW\tTh\tF\tS";
            String Divider = new String('-', 55);

            if (ShowCurrentDate)
            {
                Console.WriteLine();
                Console.WriteLine("\t\tToday is {0}/{1}/{2}.", DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year);
            }

            Console.WriteLine();
            Console.WriteLine(String.Concat("\t\t", Header));

            Console.WriteLine(Days);
            Console.WriteLine(Divider);
        }
        #endregion
    }
}