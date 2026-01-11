using Microsoft.EntityFrameworkCore;
using TechnicalAssessmentTask2.Models;

namespace TechnicalAssessmentTask2.Service
{
    public interface IHeatmapService
    {
        public HeatMapViewModel GetHeatmapData(string? location, string? department, string? gender, decimal? tenure, int? age);

        public double CalculateNetScore(Dictionary<int, int> ratingDistribution);
        
    }
}
