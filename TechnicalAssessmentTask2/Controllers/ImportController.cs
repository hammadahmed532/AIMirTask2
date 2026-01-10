using Microsoft.AspNetCore.Mvc;
using TechnicalAssessmentTask2.Services;

namespace TechnicalAssessmentTask2.Controllers
{
    public class ImportController : Controller
    {
        private readonly ExcelImportService _excelImportService;
        private readonly ILogger<ImportController> _logger;
        private readonly IWebHostEnvironment _environment;

        public ImportController(
            ExcelImportService excelImportService,
            ILogger<ImportController> logger,
            IWebHostEnvironment environment)
        {
            _excelImportService = excelImportService;
            _logger = logger;
            _environment = environment;
        }

        private bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file, int surveyCycleId = 1)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }

            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select a file to upload.";
                return RedirectToAction("Index");
            }

            // Validate file extension
            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                TempData["ErrorMessage"] = "Invalid file type. Please upload an Excel file (.xlsx or .xls).";
                return RedirectToAction("Index");
            }

            try
            {
                // Save uploaded file temporarily
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Import the Excel file
                var result = await _excelImportService.ImportEngagementDataAsync(filePath, surveyCycleId);

                // Clean up temporary file
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = result.ErrorMessage ?? "An error occurred during import.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing Excel file");
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportFromImagesFolder(int surveyCycleId = 1)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                var imagesFolder = Path.Combine(_environment.WebRootPath, "images");
                var filePath = Path.Combine(imagesFolder, "engagement_responses_with_dummy_data.xlsx");

                if (!System.IO.File.Exists(filePath))
                {
                    TempData["ErrorMessage"] = "Excel file not found in images folder.";
                    return RedirectToAction("Index");
                }

                var result = await _excelImportService.ImportEngagementDataAsync(filePath, surveyCycleId);

                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = result.ErrorMessage ?? "An error occurred during import.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing Excel file from images folder");
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
