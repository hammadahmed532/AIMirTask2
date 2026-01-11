namespace TechnicalAssessmentTask2.Service
{
    public class CommonUtility
    {
        public string GetColorForValue(double value)
        {
            if (value >= 0.5) return "#4CAF50"; // Strong Positive - Green
            if (value >= 0.2) return "#8BC34A"; // Moderate Positive - Light Green
            if (value >= 0.1) return "#CDDC39"; // Weak Positive - Lime
            if (value >= -0.1) return "#FFFFFF"; // Neutral - White
            if (value >= -0.2) return "#FFC107"; // Weak Negative - Amber
            if (value >= -0.5) return "#FF9800"; // Moderate Negative - Orange
            return "#F44336"; // Strong Negative - Red
        }
    }
}
