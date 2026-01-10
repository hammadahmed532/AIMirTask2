using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalAssessmentTask2.Data;
using TechnicalAssessmentTask2.Models;

namespace TechnicalAssessmentTask2.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

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

            // Get distinct locations from SurveyResponses table
            var locations = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Location))
                .Select(s => s.Location!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            // Get distinct locations from SurveyResponses table
            var departments = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Department))
                .Select(s => s.Department!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            // Get distinct locations from SurveyResponses table
            var genders = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Gender))
                .Select(s => s.Gender!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();


            var tenures = new List<SelectModel>();
            tenures.Add(new SelectModel { Value = 0, Text = "0-5" });
            tenures.Add(new SelectModel { Value = 5, Text = "5.1-8" });
            tenures.Add(new SelectModel { Value = 8, Text = "8.1-12" });
            tenures.Add(new SelectModel { Value = 12, Text = "12.15" });
            tenures.Add(new SelectModel { Value = 15, Text = "15.1-20" });
            tenures.Add(new SelectModel { Value = 20, Text = "20.1-30" });
            tenures.Add(new SelectModel { Value = 30, Text = "30.1-40" });

            var ages = new List<SelectModel> ();
            ages.Add(new SelectModel { Value = 18, Text = "18-30" });
            ages.Add(new SelectModel { Value = 31, Text = "31-40" });
            ages.Add(new SelectModel { Value = 41, Text = "41-50" });
            ages.Add(new SelectModel { Value = 51, Text = "51-60" });
            ages.Add(new SelectModel { Value = 61, Text = "61-70" });
                
            var viewModel = new CommentsReportViewModel
            {
                Locations = locations,
                Departments= departments,
                Genders= genders,
                Tenures= tenures,
                Ages =ages
            };

            return View(viewModel);
        }

        public IActionResult HeatMap()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }

            // Get distinct locations from SurveyResponses table
            var locations = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Location))
                .Select(s => s.Location!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            // Get distinct locations from SurveyResponses table
            var departments = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Department))
                .Select(s => s.Department!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            // Get distinct locations from SurveyResponses table
            var genders = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Gender))
                .Select(s => s.Gender!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();


            var tenures = new List<SelectModel>();
            tenures.Add(new SelectModel { Value = 0, Text = "0-5" });
            tenures.Add(new SelectModel { Value = 5, Text = "5.1-8" });
            tenures.Add(new SelectModel { Value = 8, Text = "8.1-12" });
            tenures.Add(new SelectModel { Value = 12, Text = "12.15" });
            tenures.Add(new SelectModel { Value = 15, Text = "15.1-20" });
            tenures.Add(new SelectModel { Value = 20, Text = "20.1-30" });
            tenures.Add(new SelectModel { Value = 30, Text = "30.1-40" });

            var ages = new List<SelectModel>();
            ages.Add(new SelectModel { Value = 18, Text = "18-30" });
            ages.Add(new SelectModel { Value = 31, Text = "31-40" });
            ages.Add(new SelectModel { Value = 41, Text = "41-50" });
            ages.Add(new SelectModel { Value = 51, Text = "51-60" });
            ages.Add(new SelectModel { Value = 61, Text = "61-70" });

            var viewModel = new HeatMapViewModel
            {
                Locations = locations,
                Departments = departments,
                Genders = genders,
                Tenures = tenures,
                Ages = ages
            };
            return View(viewModel);
        }

        public IActionResult PriorityMatrix()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }

            // Get distinct locations from SurveyResponses table
            var locations = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Location))
                .Select(s => s.Location!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            // Get distinct locations from SurveyResponses table
            var departments = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Department))
                .Select(s => s.Department!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            // Get distinct locations from SurveyResponses table
            var genders = _context.SurveyResponses
                .Where(s => !string.IsNullOrEmpty(s.Gender))
                .Select(s => s.Gender!)
                .Distinct()
                .OrderBy(l => l)
                .ToList();


            var tenures = new List<SelectModel>();
            tenures.Add(new SelectModel { Value = 0, Text = "0-5" });
            tenures.Add(new SelectModel { Value = 5, Text = "5.1-8" });
            tenures.Add(new SelectModel { Value = 8, Text = "8.1-12" });
            tenures.Add(new SelectModel { Value = 12, Text = "12.15" });
            tenures.Add(new SelectModel { Value = 15, Text = "15.1-20" });
            tenures.Add(new SelectModel { Value = 20, Text = "20.1-30" });
            tenures.Add(new SelectModel { Value = 30, Text = "30.1-40" });

            var ages = new List<SelectModel>();
            ages.Add(new SelectModel { Value = 18, Text = "18-30" });
            ages.Add(new SelectModel { Value = 31, Text = "31-40" });
            ages.Add(new SelectModel { Value = 41, Text = "41-50" });
            ages.Add(new SelectModel { Value = 51, Text = "51-60" });
            ages.Add(new SelectModel { Value = 61, Text = "61-70" });

            var viewModel = new PriorityMatrixViewModel
            {
                Locations = locations,
                Departments = departments,
                Genders = genders,
                Tenures = tenures,
                Ages = ages
            };
            return View(viewModel);
        }
    }
}
