using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using PoliHack.Models;

namespace PoliHack.Controllers
{
    public class HomeController : Controller
    {
        private List<Line> lines = new List<Line>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int resX, int resY, int resX1, int resY1, string cost)
        {
            if (resX != 0)
            {
                (int, int) p1 = (resX, resY);
                (int, int) p2 = (resX1, resY1);
                lines.Add(new Line(p1, p2, 0));
                Console.WriteLine(resX + ", " + resY + ", "  + resX1 + ", " + resY1 + ", " + cost);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}