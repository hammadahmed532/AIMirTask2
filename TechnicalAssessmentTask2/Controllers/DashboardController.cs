using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalAssessmentTask2.Controllers
{
    public class DashboardController : Controller
    {
        private bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

      

        public IActionResult CommentsReport()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");

            return View();
        }

        public IActionResult HeatMap()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public IActionResult PriorityMatrix()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}
