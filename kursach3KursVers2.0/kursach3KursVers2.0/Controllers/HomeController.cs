using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace kursach3KursVers2._0.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}