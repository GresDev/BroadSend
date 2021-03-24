using System.Collections.Generic;
using System.Threading.Tasks;

namespace BroadSend.Server.Models.Contracts
{
    public interface IScheduleRepository
    {

        Task<IEnumerable<Schedule>> GetScheduleListAsync(int year, int month);

        void UpdateSchedule(IEnumerable<Schedule> scheduleList);
    }
}
