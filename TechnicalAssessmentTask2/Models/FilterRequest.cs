namespace TechnicalAssessmentTask2.Models
{
    public class FilterRequest
    {
       
            public string? Location { get; set; }
            public string? Department { get; set; }
            public string? Gender { get; set; }
            public decimal? Tenure { get; set; }
            public int? Age { get; set; }
        
    }
}
