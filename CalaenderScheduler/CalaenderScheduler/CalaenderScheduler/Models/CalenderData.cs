using System;
using System.Collections.Generic;
using System.Text;

namespace CalaenderScheduler.Models
{
    public class CalenderData
    {
        public string CalenderDay { get; set; }
        public string CurrentDay { get; set; }
        public int NumberOfDaysGapping { get; set; }
        public string FullDate { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int NoOfDaysInMonth { get; set; }
    }
}
