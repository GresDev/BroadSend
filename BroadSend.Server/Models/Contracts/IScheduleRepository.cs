using System.Collections.Generic;

namespace BroadSend.Server.Models.Contracts
{
    public interface IScheduleRepository
    {

        IEnumerable<Schedule> GetScheduleList(int year, int month);

        void UpdateSchedule(IEnumerable<Schedule> scheduleList);
    }
}
