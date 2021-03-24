using BroadSend.Server.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BroadSend.Server.ViewModels
{
    public class ScheduleViewModel
    {
        public IEnumerable<Director> Directors { get; set; }
        public IEnumerable<Schedule> DirectorSchedule { get; set; }

        public string[] MonthList { get; set; }

        public ScheduleViewModel()
        {
            MonthList = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToArray();
        }

        public int Year { get; set; }
        public int Month { get; set; }
    }
}
