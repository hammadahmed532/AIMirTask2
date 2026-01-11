using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using TechnicalAssessmentTask2.Data;
using TechnicalAssessmentTask2.Models;
using TechnicalAssessmentTask2.Service;

namespace TechnicalAssessmentTask2.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHeatmapService _heatmapService;

        public DashboardController(AppDbContext context, IHeatmapService heatmapService)
        {
            _context = context;
            _heatmapService = heatmapService;

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
            tenures.Add(new SelectModel { Value = 12, Text = "12.1-15" });
            tenures.Add(new SelectModel { Value = 15, Text = "15.1-20" });
            tenures.Add(new SelectModel { Value = 20, Text = "20.1-30" });
            tenures.Add(new SelectModel { Value = 30, Text = "30.1-40" });

            var ages = new List<SelectModel>();
            ages.Add(new SelectModel { Value = 18, Text = "18-30" });
            ages.Add(new SelectModel { Value = 31, Text = "31-40" });
            ages.Add(new SelectModel { Value = 41, Text = "41-50" });
            ages.Add(new SelectModel { Value = 51, Text = "51-60" });
            ages.Add(new SelectModel { Value = 61, Text = "61-70" });



            var DataOfCommentsAndWordCloud = getWordCloudData();

            var viewModel = new CommentsReportViewModel
            {
                TotalComments = DataOfCommentsAndWordCloud.TotalComments,
                Positive = DataOfCommentsAndWordCloud.Positive,
                Negative = DataOfCommentsAndWordCloud.Negative,
                RecentComments = DataOfCommentsAndWordCloud.RecentComments,
                Locations = locations,
                Departments = departments,
                Genders = genders,
                Tenures = tenures,
                Ages = ages
            };


            ViewBag.WordCloudData = DataOfCommentsAndWordCloud.WordCloud;

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult GetFilteredWordCloudData([FromBody] FilterRequest request)
        {
            if (!IsAuthenticated())
            {
                return Unauthorized();
            }

            var filteredData = GetWordCloudDataWithFilters(
                request.Location,
                request.Department,
                request.Gender,
                request.Tenure,
                request.Age
            );
            return Json(filteredData);
        }



        private CommentsReportViewModel getWordCloudData()
        {
            return GetWordCloudDataWithFilters(null, null, null, null, null);
        }

        private CommentsReportViewModel GetWordCloudDataWithFilters(string? location, string? department, string? gender, decimal? tenure, int? age)
        {
            var keywords = new List<string>
            {
                "Flexibility", "Culture", "Communication", "Goals", "Management", "Recognition",
                "Performance", "Processes", "Tools", "Growth", "Team", "Workload", "Benefits",
                "Salary", "Rewards", "Training", "Transparency", "Process", "Remote"
            };

            var query = _context.SurveyResponses.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(s => s.Location == location);
            }

            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(s => s.Department == department);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(s => s.Gender == gender);
            }

            var tenures = new List<SelectModel>();
            tenures.Add(new SelectModel { Value = 0, Text = "0-5" });
            tenures.Add(new SelectModel { Value = 5, Text = "5.1-8" });
            tenures.Add(new SelectModel { Value = 8, Text = "8.1-12" });
            tenures.Add(new SelectModel { Value = 12, Text = "12.1-15" });
            tenures.Add(new SelectModel { Value = 15, Text = "15.1-20" });
            tenures.Add(new SelectModel { Value = 20, Text = "20.1-30" });
            tenures.Add(new SelectModel { Value = 30, Text = "30.1-40" });

            var ages = new List<SelectModel>();
            ages.Add(new SelectModel { Value = 18, Text = "18-30" });
            ages.Add(new SelectModel { Value = 31, Text = "31-40" });
            ages.Add(new SelectModel { Value = 41, Text = "41-50" });
            ages.Add(new SelectModel { Value = 51, Text = "51-60" });
            ages.Add(new SelectModel { Value = 61, Text = "61-70" });

            if (tenure.HasValue && tenure > 0)
            {
                decimal startTenure = decimal.Parse(tenures.FirstOrDefault(a => a.Value == tenure)?.Text.Split("-")[0] ?? "0");
                decimal endTenure = decimal.Parse(tenures.FirstOrDefault(a => a.Value == tenure)?.Text.Split("-")[1] ?? "0");
                query = query.Where(s => s.Tenure >= startTenure && s.Tenure <= endTenure);
            }

            if (age.HasValue && age > 0)
            {
                int startAge = int.Parse(ages.FirstOrDefault(a => a.Value == age)?.Text.Split("-")[0] ?? "0");
                int endAge = int.Parse(ages.FirstOrDefault(a => a.Value == age)?.Text.Split("-")[1] ?? "0");
                query = query.Where(s => s.Age >= startAge && s.Age <= endAge);
            }
                query = query.Where(s => s.Location == "karachi");

            var filteredIds = query.Select(s => s.ID).ToList();

            var filterdComments = _context.TextAnalyses
                                    .AsNoTracking()
                                    .Where(sc => filteredIds.Contains(sc.SurveyID))
                                    .AsEnumerable().ToList();

            var totalComments = filterdComments.Where(a => !string.IsNullOrEmpty(a.OpenEndedStop1)).Count() +
                 filterdComments.Where(a => !string.IsNullOrEmpty(a.OpenEndedContinue1)).Count() +
                filterdComments.Where(a => !string.IsNullOrEmpty(a.OpenEndedAnythingElse1)).Count() +
                filterdComments.Where(a => !string.IsNullOrEmpty(a.OpenEndedStart1)).Count();

            var PositiveCount = (double)filterdComments.Where(a => a.Positive).Count() / filterdComments.Count() * 100.0;
            var NegativeCount = (double)filterdComments.Where(a => a.Positive == false).Count() / filterdComments.Count() * 100.0;

            var RecentComments = filterdComments.Where(a => a.Positive == false);
            var recentCommentsList = filterdComments.Where(a => a.Positive).Take(4)
                .Select(a => new RecentComment { CommentText = a.OpenEndedStart1, Positive = a.Positive, SupportingInfo = a.SupportingInfo })
                .ToList();
            recentCommentsList.AddRange(filterdComments.Where(a => a.Positive == false).Take(4)
                .Select(a => new RecentComment { CommentText = a.OpenEndedStart1, Positive = a.Positive, SupportingInfo = a.SupportingInfo })
                .ToList());


            var allComments = filterdComments.SelectMany(sc => new[]
                                    {
                                        sc.OpenEndedStart1,
                                        sc.OpenEndedContinue1,
                                        sc.OpenEndedStop1,
                                        sc.OpenEndedAnythingElse1
                                    })
                                    .Where(comment => !string.IsNullOrWhiteSpace(comment) &&
                                                      !comment.Equals("No additional comments.", StringComparison.OrdinalIgnoreCase))
                                    .ToList();

            // Convert all comments to lowercase to do case-insensitive matching
            var allCommentsLower = allComments.Select(c => c.ToLower()).ToList();

            // Count frequency of each keyword in all comments
            var keywordFrequency = keywords
                .Select(keyword => new
                {
                    Keyword = keyword,
                    Frequency = allCommentsLower.Count(comment => comment.Contains(keyword.ToLower()))
                })
                .Where(kf => kf.Frequency > 0)  // Only keywords that appeared
                .OrderByDescending(kf => kf.Frequency)
                .ToList();

            var words = new List<object[]>();

            foreach (var item in keywordFrequency)
            {
                words.Add(new object[] { item.Keyword, item.Frequency });
            }
            return new CommentsReportViewModel
            {
                TotalComments = totalComments,
                Positive = Math.Round(PositiveCount),
                Negative = Math.Round(NegativeCount),
                RecentComments = recentCommentsList,
                WordCloud = words
            };
        }


        public IActionResult HeatMap()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }


            var model = _heatmapService.GetHeatmapData(null, null, null, null, null);

            return View(model);
        }


        [HttpPost]
        public IActionResult GetFilteredHeatMapData([FromBody] FilterRequest request)
        {
            if (!IsAuthenticated())
            {
                return Unauthorized();
            }


            var model = _heatmapService.GetHeatmapData(request.Location,
                request.Department,
                request.Gender,
                request.Tenure,
                request.Age);

            return Json(model);
        }

        //[HttpPost]
        //public IActionResult Filter(string location, string demographic)
        //{
        //    var model = _heatmap_service.GetHeatmapData();
        //    model.SelectedLocation = location;
        //    model.SelectedDemographic = demographic;

        //    // In a real application, you would filter the data here
        //    // For now, we'll just return the same data with updated filter labels

        //    return View("Index", model);
        //}



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