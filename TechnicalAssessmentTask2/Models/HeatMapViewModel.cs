namespace TechnicalAssessmentTask2.Models
{
    public class HeatMapViewModel
    {
        public List<string> Locations { get; set; } = new List<string>();
        public List<string> Departments { get; set; } = new List<string>();
        public List<string> Genders { get; set; } = new List<string>();
        public List<SelectModel> Ages { get; set; } = new List<SelectModel>();
        public List<SelectModel> Tenures { get; set; } = new List<SelectModel>();
        public List<CorrelationItem> CorrelationData { get; set; }
        public List<TopDriverItem> TopDiscretionaryEffortDrivers { get; set; } = new List<TopDriverItem>();
        public List<TopDriverItem> TopIntentToStayDrivers { get; set; } = new List<TopDriverItem>();
        public List<TopDriverItem> TopAdvocacy { get; set; } = new List<TopDriverItem>();
    }

    public class CorrelationItem
    {
        public string Driver { get; set; }
        public double DiscretionaryEffort { get; set; }
        public double IntentToStay { get; set; }
        public double Advocacy { get; set; }
    }

    public class TopDriverItem
    {
        public string Driver { get; set; }
        public double Correlation { get; set; }
    }
}
