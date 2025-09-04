using CsvHelper;
using System.Globalization;
using System.Text;
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Data.Repositories;

namespace BrightLifeIMS.Web.Services.Core
{
    public class ExportService : IExportService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ExportService> _logger;

        public ExportService(
            IInventoryRepository inventoryRepository,
            IItemRepository itemRepository,
            ILogger<ExportService> logger)
        {
            _inventoryRepository = inventoryRepository;
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<byte[]> ExportToCSVAsync(int inventoryId)
        {
            var items = await _itemRepository.GetAllByInventoryIdAsync(inventoryId);
            
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            // Write headers
            csv.WriteField("Id");
            csv.WriteField("CustomId");
            csv.WriteField("CreatedAt");
            csv.NextRecord();

            // Write data
            foreach (var item in items)
            {
                csv.WriteField(item.Id);
                csv.WriteField(item.CustomId ?? "");
                csv.WriteField(item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                csv.NextRecord();
            }

            await writer.FlushAsync();
            return memoryStream.ToArray();
        }

        public async Task<byte[]> ExportToExcelAsync(int inventoryId)
        {
            // For now, return CSV data as Excel is not implemented without ClosedXML
            // TODO: Implement proper Excel export when ClosedXML package is added
            return await ExportToCSVAsync(inventoryId);
        }

        public async Task<string> GenerateExportFilenameAsync(int inventoryId, string extension)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            var sanitizedTitle = inventory?.Title ?? "inventory";
            sanitizedTitle = string.Join("_", sanitizedTitle.Split(Path.GetInvalidFileNameChars()));
            return $"{sanitizedTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
        }
    }
}