using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using PoliHack.Models;
using PoliHack.Service;

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

        public IActionResult Index(int resX, int resY, int resX1, int resY1, string costVtoE, string costEtoV, string nrVtoE, string nrEtoV, string isModifiable, string name)
        {
            bool modifiable;
            if (!string.IsNullOrEmpty(isModifiable))
            {
                modifiable = isModifiable.Equals("da") ? true : false;
            }
            else modifiable = false;
            
            if (resX == 0 | resY == 0 | resX1 == 0 | resY1 == 0) return View();
            (int, int) p1 = (resX, resY);
            (int, int) p2 = (resX1, resY1);
            var cost = (int.Parse(costVtoE), int.Parse(costEtoV));
            var nrLanes = (int.Parse(nrVtoE), int.Parse(nrEtoV));
            lines.Add(new Line(p1, p2, cost, nrLanes, name, modifiable));
            Console.WriteLine(resX + ", " + resY + ", "  + resX1 + ", " + resY1 + ", " + costVtoE + ", " + costEtoV + ", " + nrVtoE + ", " + nrEtoV + ", " + isModifiable + ", " + name);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TrafficResult()
        {
            var linesToGraph = new LinesToGraph();
            linesToGraph.DoTheThing(lines);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}