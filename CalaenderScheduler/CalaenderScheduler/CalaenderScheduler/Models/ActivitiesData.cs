using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalaenderScheduler.Models
{
    public class ActivitiesData
    {
        public DateTime activitiesDate { get; set; }
        public int activityId { get; set; }
        public string activityHeading { get; set; }
        public string activityDescription { get; set; }
        public FormattedString FormattedDateString
        {
            get
            {
                return new FormattedString
                {
                    Spans = {
                        new Span { Text = "Date: ", ForegroundColor = Color.Gray, FontAttributes=FontAttributes.Bold, FontSize=13 },
                        new Span { Text = Convert.ToString(activitiesDate.Date.ToShortDateString()), ForegroundColor = Color.Gray, FontSize=13 }
                    }
                };
            }
        }

    }
}
