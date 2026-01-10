using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TechnicalAssessmentTask2.Data;
using TechnicalAssessmentTask2.Models;

namespace TechnicalAssessmentTask2.Services
{
    public class ExcelImportService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExcelImportService> _logger;

        public ExcelImportService(AppDbContext context, ILogger<ExcelImportService> logger)
        {
            _context = context;
            _logger = logger;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<ImportResult> ImportEngagementDataAsync(string filePath, int surveyCycleId)
        {
            var result = new ImportResult();

            try
            {
                if (!File.Exists(filePath))
                {
                    result.ErrorMessage = $"File not found: {filePath}";
                    return result;
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // First worksheet
                    if (worksheet == null)
                    {
                        result.ErrorMessage = "No worksheet found in Excel file";
                        return result;
                    }

                    var rowCount = worksheet.Dimension?.Rows ?? 0;
                    if (rowCount < 2) // Need at least header + 1 data row
                    {
                        result.ErrorMessage = "Excel file is empty or has no data rows";
                        return result;
                    }

                    // Read header row to identify column positions
                    //var headers = ReadHeaders(worksheet);
                    //_logger.LogInformation($"Found {headers.Count} columns in Excel file");

                    // Process data rows
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            //var response = await ProcessRowAsync(worksheet, row, headers, surveyCycleId);
                            //if (response != null)
                            //{
                            //    result.SuccessCount++;
                            //}
                            //else
                            //{
                            //    result.SkippedCount++;
                            //}
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error processing row {row}");
                            result.ErrorCount++;
                        }
                    }

                    await _context.SaveChangesAsync();
                    result.Success = true;
                    result.Message = $"Successfully imported {result.SuccessCount} responses. {result.SkippedCount} skipped, {result.ErrorCount} errors.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing Excel file");
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        
    }

    public class ImportResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public int SuccessCount { get; set; }
        public int SkippedCount { get; set; }
        public int ErrorCount { get; set; }
    }
}
