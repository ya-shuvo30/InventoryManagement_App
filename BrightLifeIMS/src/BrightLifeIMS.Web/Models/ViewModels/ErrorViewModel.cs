// src/BrightLifeIMS.Web/Models/ViewModels/ErrorViewModel.cs
namespace BrightLifeIMS.Web.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Message { get; set; } = "An error occurred while processing your request.";
        public int StatusCode { get; set; } = 500;
    }
}