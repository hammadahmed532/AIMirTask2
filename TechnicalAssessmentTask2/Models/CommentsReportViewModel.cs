namespace TechnicalAssessmentTask2.Models
{
    public class CommentsReportViewModel
    {
        public List<string> Locations { get; set; } = new List<string>();
        public List<string> Departments { get; set; } = new List<string>();
        public List<string> Genders { get; set; } = new List<string>();
        public List<SelectModel> Ages { get; set; } = new List<SelectModel>();
        public List<SelectModel> Tenures { get; set; } = new List<SelectModel>();
        public int TotalComments { get; set; } 
        public double Positive { get; set; } 
        public double Negative { get; set; } 
        public new List<object[]> WordCloud { get; set; } 
        public new List<RecentComment> RecentComments { get; set; }
    }

    public class RecentComment
    {
        public string CommentText { get; set; }
        public string SupportingInfo { get; set; }
        public bool Positive { get; set; }
    }
}
