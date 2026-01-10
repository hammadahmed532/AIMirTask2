using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalAssessmentTask2.Models
{
    [Table("SurveyResponses")]
    public class SurveyResponse
    {
        [Key]
        public int ID { get; set; }
        public int RespondentId { get; set; }

        [MaxLength(50)]
        public string? EmployeeId { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        public int? Age { get; set; }

        [Column(TypeName = "decimal(18,1)")]
        public decimal? Tenure { get; set; }

        [MaxLength(50)]
        public string? Gender { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? Manager { get; set; }

        [MaxLength(100)]
        public string? LeadershipLevel { get; set; }

        // Why Pride questions
        public int? WhyPride1 { get; set; }
        public int? WhyPride2 { get; set; }

        // Why Sense questions
        public int? WhySense1 { get; set; }
        public int? WhySense2 { get; set; }
        public int? WhySense3 { get; set; }

        // Why Meaningfulness questions
        public int? WhyMeaningfulness1 { get; set; }
        public int? WhyMeaningfulness2 { get; set; }

        // Who Faith Leadership questions
        public int? WhoFaithLeadership1 { get; set; }
        public int? WhoFaithLeadership2 { get; set; }
        public int? WhoFaithLeadership3 { get; set; }

        // Who Trust questions
        public int? WhoTrust1 { get; set; }
        public int? WhoTrust2 { get; set; }
        public int? WhoTrust3 { get; set; }

        // Who Relationship questions
        public int? WhoRelationship1 { get; set; }
        public int? WhoRelationship2 { get; set; }
        public int? WhoRelationship3 { get; set; }
        public int? WhoRelationship4 { get; set; }
        public int? WhoRelationship5 { get; set; }
        public int? WhoRelationship6 { get; set; }
        public int? WhoRelationship7 { get; set; }

        // Who Sense Belonging questions
        public int? WhoSenseBelonging1 { get; set; }

        // How Sense Direction questions
        public int? HowSenseDirection1 { get; set; }

        // How Role Clarity questions
        public int? HowRoleClarity1 { get; set; }

        // How Line Sight questions
        public int? HowLineSight1 { get; set; }

        // How System Tools questions
        public int? HowSystemTools1 { get; set; }
        public int? HowSystemTools2 { get; set; }
        public int? HowSystemTools3 { get; set; }
        public int? HowSystemTools4 { get; set; }

        // How Sense Progress questions
        public int? HowSenseProgress1 { get; set; }

        // What Work Culture questions
        public int? WhatWorkCulture1 { get; set; }
        public int? WhatWorkCulture2 { get; set; }
        public int? WhatWorkCulture3 { get; set; }
        public int? WhatWorkCulture4 { get; set; }
        public int? WhatWorkCulture5 { get; set; }

        // What Talent Development questions
        public int? WhatTalentDevelopment1 { get; set; }
        public int? WhatTalentDevelopment2 { get; set; }

        // What Recognition questions
        public int? WhatRecognition1 { get; set; }

        // What Compensation Benefits questions
        public int? WhatCompensationBenefits1 { get; set; }

        // What Work Life Balance questions
        public int? WhatWorkLifeBalance1 { get; set; }
        public int? WhatWorkLifeBalance2 { get; set; }
        public int? WhatWorkLifeBalance3 { get; set; }

        // What Psychological Safety questions
        public int? WhatPsychologicalSafety1 { get; set; }
        public int? WhatPsychologicalSafety2 { get; set; }
        public int? WhatPsychologicalSafety3 { get; set; }
        public int? WhatPsychologicalSafety4 { get; set; }
        public int? WhatPsychologicalSafety5 { get; set; }
        public int? WhatPsychologicalSafety6 { get; set; }
        public int? WhatPsychologicalSafety7 { get; set; }

        // Outcome questions
        public int? OutcomeAdvocacy1 { get; set; }
        public int? OutcomeDiscretionaryEffort1 { get; set; }
        public int? OutcomeIntentToStay1 { get; set; }
    }
}
