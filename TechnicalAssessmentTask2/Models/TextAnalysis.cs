using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalAssessmentTask2.Models
{
    [Table("TextAnalyses")]
    public class TextAnalysis
    {
        [Key]
        public int ID { get; set; }
        public int SurveyID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? OpenEndedStart1 { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? OpenEndedContinue1 { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? OpenEndedStop1 { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? OpenEndedAnythingElse1 { get; set; }
        public bool Positive { get; set; }
        public string SupportingInfo { get; set; }

        // Navigation property (optional, if you want to link to SurveyResponse)
        public virtual SurveyResponse? SurveyResponse { get; set; }
    }
}
