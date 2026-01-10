namespace TechnicalAssessmentTask2.Models
{
    public class PriorityMatrixViewModel
    {
        public List<string> Locations { get; set; } = new List<string>();
        public List<string> Departments { get; set; } = new List<string>();
        public List<string> Genders { get; set; } = new List<string>();
        public List<SelectModel> Ages { get; set; } = new List<SelectModel>();
        public List<SelectModel> Tenures { get; set; } = new List<SelectModel>();
    }
}
