using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalAssessmentTask2.Data;
using TechnicalAssessmentTask2.Models;
namespace TechnicalAssessmentTask2.Service
{
    public class HeatmapService : IHeatmapService
    {
        private readonly AppDbContext _context;

        public HeatmapService(AppDbContext context)
        {
            _context = context;
        }
        public HeatMapViewModel GetHeatmapData(string? location, string? department, string? gender, decimal? tenure, int? age)
        {

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

            if (tenure.HasValue && tenure > 0)
            {
                decimal startTenure = decimal.Parse(tenures.FirstOrDefault(a => a.Value == tenure)?.Text.Split("-")[0] ?? "0");
                decimal endTenure = decimal.Parse(tenures.FirstOrDefault(a => a.Value == tenure)?.Text.Split("-")[1] ?? "0");
                query = query.Where(s => s.Tenure >= startTenure && s.Tenure <= endTenure);
            }

            if (age.HasValue)
            {
                int startAge = int.Parse(ages.FirstOrDefault(a => a.Value == age)?.Text.Split("-")[0] ?? "0");
                int endAge = int.Parse(ages.FirstOrDefault(a => a.Value == age)?.Text.Split("-")[1] ?? "0");
                query = query.Where(s => s.Age >= startAge && s.Age <= endAge);
            }

            var data = query.Select(r => new
            {
                Leadership = (r.WhoFaithLeadership1 + r.WhoFaithLeadership2 + r.WhoFaithLeadership3) / 3.0,

                Teamwork = (r.WhyPride1 + r.WhyPride2) / 2.0,

                Meaningfulness = (r.WhyMeaningfulness1 + r.WhyMeaningfulness2) / 2.0,

                Worklifebalance = (r.WhatWorkLifeBalance1 + r.WhatWorkLifeBalance2 + r.WhatWorkLifeBalance3) / 3.0,

                Recognition = r.WhatRecognition1,

                SenseOfContribution = (r.WhySense1 + r.WhySense2 + r.WhySense3) / 3.0,

                Trust = (r.WhoTrust1 + r.WhoTrust2 + r.WhoTrust3) / 3.0,

                RoleClarity = r.HowRoleClarity1,

                SenseOfBelonging = r.WhoSenseBelonging1,

                DiscretionaryEffort = r.OutcomeDiscretionaryEffort1,
                IntentToStay = r.OutcomeIntentToStay1,
                Advocacy = r.OutcomeAdvocacy1
            }).ToList();

            var discretionaryEffort = data.Select(d => (double)d.DiscretionaryEffort).ToList();
            var intentToStay = data.Select(d => (double)d.IntentToStay).ToList();
            var advocacy = data.Select(d => (double)d.Advocacy).ToList();


            var drivers = new Dictionary<string, List<double>>
                {
                    { "Leadership", data.Select(d => d.Leadership).ToList() },
                    { "Teamwork", data.Select(d => d.Teamwork).ToList() },
                    { "Meaningfulness", data.Select(d => d.Meaningfulness).ToList() },
                    { "Work Life Balance", data.Select(d => d.Worklifebalance).ToList() },
                    { "Recognition", data.Select(d => (double)d.Recognition).ToList() },
                    { "Sense Of Contribution", data.Select(d => d.SenseOfContribution).ToList() },
                    { "Trust", data.Select(d => d.Trust).ToList() },
                    { "Role Clarity", data.Select(d => (double)d.RoleClarity).ToList() },
                    { "Sense Of Belonging", data.Select(d => (double)d.SenseOfBelonging).ToList() }
                };


            var records = new List<CorrelationItem>();
            if (data.Count > 0)
            {
                foreach (var driver in drivers)
                {
                    records.Add(new CorrelationItem
                    {
                        Driver = driver.Key,
                        DiscretionaryEffort = PearsonCorrelation(driver.Value, discretionaryEffort),
                        IntentToStay = PearsonCorrelation(driver.Value, intentToStay),
                        Advocacy = PearsonCorrelation(driver.Value, advocacy)
                    });
                }
            }
            else
            {
                foreach (var driver in drivers)
                {
                    records.Add(new CorrelationItem
                    {
                        Driver = driver.Key,
                        DiscretionaryEffort = 0,
                        IntentToStay = 0,
                        Advocacy = 0
                    });
                }
            }


           var topDiscretionaryEffort = records.Where(x => x.DiscretionaryEffort > 0)
                  .OrderByDescending(x => Math.Abs(x.DiscretionaryEffort))
                  .Take(4).Select(x => new TopDriverItem
                  {
                      Correlation = x.DiscretionaryEffort,
                      Driver = x.Driver
                  })
                  .ToList();

             


            var topIntentToStay = records.Where(x => x.IntentToStay > 0)
                .OrderByDescending(x => Math.Abs(x.IntentToStay))
                .Take(4).Select(x => new TopDriverItem
                {
                    Correlation = x.DiscretionaryEffort,
                    Driver = x.Driver
                })
                .ToList();

            var topAdvocacy = records.Where(x => x.Advocacy > 0)
                .OrderByDescending(x => Math.Abs(x.Advocacy))
                .Take(4).Select(x => new TopDriverItem
                {
                    Correlation = x.DiscretionaryEffort,
                    Driver = x.Driver
                })
                .ToList();

            return new HeatMapViewModel
            {
                TopAdvocacy= topAdvocacy,
                TopDiscretionaryEffortDrivers= topDiscretionaryEffort,
                TopIntentToStayDrivers= topIntentToStay,

                CorrelationData = records,
                Locations = locations,
                Departments = departments,
                Genders = genders,
                Tenures = tenures,
                Ages = ages
            };
        }

        double PearsonCorrelation(List<double> x, List<double> y)
        {
            int n = x.Count;
            double avgX = x.Average();
            double avgY = y.Average();

            double sumXY = 0, sumX2 = 0, sumY2 = 0;

            for (int i = 0; i < n; i++)
            {
                double dx = x[i] - avgX;
                double dy = y[i] - avgY;
                sumXY += dx * dy;
                sumX2 += dx * dx;
                sumY2 += dy * dy;
            }

            return sumXY / Math.Sqrt(sumX2 * sumY2);
        }


        public double CalculateNetScore(Dictionary<int, int> ratingDistribution)
        {
            int totalResponses = ratingDistribution.Sum(x => x.Value);

            if (totalResponses == 0) return 0;

            double p5 = (double)ratingDistribution.GetValueOrDefault(5) / totalResponses;
            double p4 = (double)ratingDistribution.GetValueOrDefault(4) / totalResponses;
            double p2 = (double)ratingDistribution.GetValueOrDefault(2) / totalResponses;
            double p1 = (double)ratingDistribution.GetValueOrDefault(1) / totalResponses;

            return (p5 * 1.0 + p4 * 0.8) - (p1 * 1.0 + p2 * 0.8);
        }

    }
}