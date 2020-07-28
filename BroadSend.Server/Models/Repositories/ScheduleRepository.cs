using BroadSend.Server.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BroadSend.Server.Models.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        internal readonly AppDbContext AppDbContext;

        public ScheduleRepository(AppDbContext appDbContext)
        {

            AppDbContext = appDbContext;
        }

        public IEnumerable<Schedule> GetScheduleList(int year, int month)
        {

            List<Schedule> scheduleList = new List<Schedule>();

            var numOfDays = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= numOfDays; i++)
            {
                var selectedDate = year.ToString("0000") + "/" + month.ToString("00") + "/" + i.ToString("00");

                var result = AppDbContext.Set<Schedule>().AsNoTracking().FirstOrDefault(s => s.Date == selectedDate);

                if (result == null)
                {
                    var schedule = new Schedule
                    {
                        Date = selectedDate
                    };

                    result = schedule;

                    AppDbContext.Set<Schedule>().Add(result);

                }

                else
                {
                    result.Interval01 = AppDbContext.Schedules.AsNoTracking().Where(s => s.Date == selectedDate).Select(s => s.Interval01).ToArray()[0];
                    result.Interval02 = AppDbContext.Schedules.AsNoTracking().Where(s => s.Date == selectedDate).Select(s => s.Interval02).ToArray()[0];
                    result.Interval03 = AppDbContext.Schedules.AsNoTracking().Where(s => s.Date == selectedDate).Select(s => s.Interval03).ToArray()[0];
                    result.Interval04 = AppDbContext.Schedules.AsNoTracking().Where(s => s.Date == selectedDate).Select(s => s.Interval04).ToArray()[0];

                }

                scheduleList.Add(result);
            }

            AppDbContext.SaveChanges();

            return scheduleList;
        }

        public void UpdateSchedule(IEnumerable<Schedule> scheduleList)
        {
            foreach (var schedule in scheduleList)
            {
                AppDbContext.Schedules.Add(schedule);
            }
        }


    }
}
