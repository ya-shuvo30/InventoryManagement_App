// src/BrightLifeIMS.Web/Services/Core/IDGeneratorService.cs
using BrightLifeIMS.Web.Data.Repositories;
using System.Text.RegularExpressions;
using System.Text;

namespace BrightLifeIMS.Web.Services.Core
{
    public class IDGeneratorService : IIDGeneratorService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<IDGeneratorService> _logger;
        private readonly Random _random = new Random();

        public IDGeneratorService(
            IInventoryRepository inventoryRepository,
            ILogger<IDGeneratorService> logger)
        {
            _inventoryRepository = inventoryRepository;
            _logger = logger;
        }

        public async Task<string> GenerateIDAsync(string format, int inventoryId)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "ITEM-{SEQUENCE:000000}";
            }

            var sequenceNumber = await _inventoryRepository.GetNextSequenceNumberAsync(inventoryId);
            return ProcessFormat(format, sequenceNumber);
        }

        public string PreviewID(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return "ITEM-000001";
            }

            return ProcessFormat(format, 1);
        }

        private string ProcessFormat(string format, int sequenceNumber)
        {
            var result = format;

            // Process SEQUENCE
            var sequencePattern = @"\{SEQUENCE:(\d+)\}";
            var sequenceMatch = Regex.Match(format, sequencePattern);
            if (sequenceMatch.Success)
            {
                var padding = sequenceMatch.Groups[1].Value.Length;
                result = Regex.Replace(result, sequencePattern, 
                    sequenceNumber.ToString().PadLeft(padding, '0'));
            }

            // Process GUID
            result = result.Replace("{GUID}", Guid.NewGuid().ToString());
            result = result.Replace("{GUID:8}", Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper());

            // Process YEAR
            result = result.Replace("{YEAR}", DateTime.Now.Year.ToString());
            result = result.Replace("{YEAR:2}", DateTime.Now.Year.ToString().Substring(2));

            // Process DATE
            result = result.Replace("{DATE}", DateTime.Now.ToString("yyyyMMdd"));
            result = result.Replace("{DATE:SHORT}", DateTime.Now.ToString("MMdd"));

            // Process TIME
            result = result.Replace("{TIME}", DateTime.Now.ToString("HHmmss"));

            // Process RANDOM
            result = result.Replace("{RANDOM:6}", _random.Next(100000, 999999).ToString());
            result = result.Replace("{RANDOM:9}", _random.Next(100000000, 999999999).ToString());
            result = result.Replace("{RANDOM:20}", GenerateRandomBits(20));
            result = result.Replace("{RANDOM:32}", GenerateRandomBits(32));

            return result;
        }

        private string GenerateRandomBits(int bits)
        {
            var maxValue = Math.Pow(2, bits) - 1;
            return ((long)(_random.NextDouble() * maxValue)).ToString();
        }

        public bool ValidateFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return false;
            }

            // Check for valid placeholders
            var validPlaceholders = new[]
            {
                @"\{SEQUENCE:\d+\}",
                @"\{GUID\}",
                @"\{GUID:8\}",
                @"\{YEAR\}",
                @"\{YEAR:2\}",
                @"\{DATE\}",
                @"\{DATE:SHORT\}",
                @"\{TIME\}",
                @"\{RANDOM:6\}",
                @"\{RANDOM:9\}",
                @"\{RANDOM:20\}",
                @"\{RANDOM:32\}"
            };

            // Remove all valid placeholders and check if any invalid ones remain
            var temp = format;
            foreach (var placeholder in validPlaceholders)
            {
                temp = Regex.Replace(temp, placeholder, "");
            }

            // Check for any remaining curly braces (invalid placeholders)
            return !temp.Contains("{") && !temp.Contains("}");
        }

        public List<string> GetAvailableComponents()
        {
            return new List<string>
            {
                "Fixed Text",
                "{SEQUENCE:000000} - Sequential number with padding",
                "{GUID} - Full GUID",
                "{GUID:8} - 8-character GUID",
                "{YEAR} - Current year (4 digits)",
                "{YEAR:2} - Current year (2 digits)",
                "{DATE} - Current date (YYYYMMDD)",
                "{DATE:SHORT} - Current date (MMDD)",
                "{TIME} - Current time (HHMMSS)",
                "{RANDOM:6} - 6-digit random number",
                "{RANDOM:9} - 9-digit random number",
                "{RANDOM:20} - 20-bit random number",
                "{RANDOM:32} - 32-bit random number"
            };
        }
    }
}