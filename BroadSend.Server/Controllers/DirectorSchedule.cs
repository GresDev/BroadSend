using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BroadSend.Server.Controllers
{
    public class DirectorSchedule : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IDirectorRepository _directorRepository;
        private int _year;
        private int _month;


        public DirectorSchedule(IScheduleRepository sсheduleRepository, IDirectorRepository directorRepository)
        {
            _scheduleRepository = sсheduleRepository;
            _directorRepository = directorRepository;
        }
        public async Task<IActionResult> Index(int? year, string month)

        {
            if (year != null && year >= 2015 && year <= DateTime.Now.Year + 1)
            {
                _year = (int)year;
            }
            else
            {
                _year = DateTime.Now.Year;
            }

            _month = !string.IsNullOrEmpty(month) ? DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month : DateTime.Now.Month;

            IEnumerable<Schedule> scheduleList = await _scheduleRepository.GetScheduleListAsync(_year, _month);
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel
            {
                DirectorSchedule = scheduleList,
                Year = _year,
                Month = _month
            };

            var directorList = await _directorRepository.GetAllItemsAsync();
            scheduleViewModel.Directors = directorList.OrderBy(n => n.Alias);

            return View(scheduleViewModel);
        }

        public IActionResult UpdateSchedule(ScheduleViewModel scheduleViewModel)
        {
            return View("Index");
        }

    }
}
