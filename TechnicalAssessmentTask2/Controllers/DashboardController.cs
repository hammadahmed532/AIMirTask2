using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalAssessmentTask2.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        public IActionResult CommentsReport()
        {
            return View();
        }
        public IActionResult HeatMap()
        {
            return View();
        }
        public IActionResult PriorityMatrix()
        {
            return View();
        }
    }
}
