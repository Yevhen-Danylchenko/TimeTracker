using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeTracker.Models;
using TimeTracker.Services;

namespace TimeTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TimeTrackerService _service;

        public HomeController(TimeTrackerService service)
        {
            _service = service;
        }

        // Список записів за сьогодні
        public IActionResult Index()
        {
            var entries = _service.FilterEntries(null, "today");
            return View(entries);
        }

        // Форма додавання
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TimeEntry
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            });
        }

        [HttpPost]
        public IActionResult Create(TimeEntry entry)
        {
            if (ModelState.IsValid)
            {
                _service.AddTimeEntry(entry);
                return RedirectToAction(nameof(Index));
            }
            return View(entry);
        }

        // Сторінка статистики
        public IActionResult Statistics(string period = "today", CategoryEnum? category = null)
        {
            var stats = period switch
            {
                "week" => _service.StatisticsThisWeek(),
                "all" => _service.StatisticsChanged(),
                _ => _service.StatisticsToday()
            };

            var filtered = _service.FilterEntries(category, period);
            ViewBag.FilteredEntries = filtered;

            return View(stats);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

