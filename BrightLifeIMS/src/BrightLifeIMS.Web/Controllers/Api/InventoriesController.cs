using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BrightLifeIMS.Web.Data;
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Services.Core;
using BrightLifeIMS.Web.Models.DTOs;

namespace BrightLifeIMS.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAccessControlService _accessControlService;
        private readonly IIDGeneratorService _idGeneratorService;
        private readonly ILogger<InventoriesController> _logger;

        public InventoriesController(
            AppDbContext context,
            IAccessControlService accessControlService,
            IIDGeneratorService idGeneratorService,
            ILogger<InventoriesController> logger)
        {
            _context = context;
            _accessControlService = accessControlService;
            _idGeneratorService = idGeneratorService;
            _logger = logger;
        }

        // GET: api/inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetInventories()
        {
            try
            {
                var userId = User.Identity?.Name;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var inventories = await _context.Inventories
                    .Include(i => i.Category)
                    .Where(i => i.CreatorId == userId || i.IsPublic)
                    .OrderByDescending(i => i.CreatedAt)
                    .Select(i => new InventoryDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        TitleBn = i.TitleBn,
                        Description = i.Description,
                        DescriptionBn = i.DescriptionBn,
                        ImageUrl = i.ImageUrl,
                        CreatorId = i.CreatorId,
                        OwnerId = i.OwnerId,
                        CategoryId = i.CategoryId,
                        CategoryName = i.Category != null ? i.Category.Name : null,
                        IsPublic = i.IsPublic,
                        IsActive = i.IsActive,
                        LikesCount = i.LikesCount,
                        ViewsCount = i.ViewsCount,
                        CreatedAt = i.CreatedAt,
                        UpdatedAt = i.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(inventories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inventories");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventory(int id)
        {
            try
            {
                var userId = User.Identity?.Name;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var inventory = await _context.Inventories
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (inventory == null)
                    return NotFound();

                // Check access control
                var user = await _context.Users.FindAsync(userId);
                if (user == null || !await _accessControlService.CanViewInventoryAsync(user, id))
                    return Forbid();

                // Increment view count
                inventory.ViewsCount++;
                await _context.SaveChangesAsync();

                var inventoryDto = new InventoryDto
                {
                    Id = inventory.Id,
                    Title = inventory.Title,
                    TitleBn = inventory.TitleBn,
                    Description = inventory.Description,
                    DescriptionBn = inventory.DescriptionBn,
                    ImageUrl = inventory.ImageUrl,
                    CreatorId = inventory.CreatorId,
                    OwnerId = inventory.OwnerId,
                    CategoryId = inventory.CategoryId,
                    CategoryName = inventory.Category != null ? inventory.Category.Name : null,
                    IsPublic = inventory.IsPublic,
                    IsActive = inventory.IsActive,
                    LikesCount = inventory.LikesCount,
                    ViewsCount = inventory.ViewsCount,
                    CreatedAt = inventory.CreatedAt,
                    UpdatedAt = inventory.UpdatedAt
                };

                return Ok(inventoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inventory {InventoryId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/inventories
        [HttpPost]
        public async Task<ActionResult<InventoryDto>> CreateInventory([FromBody] CreateInventoryDto createDto)
        {
            try
            {
                var userId = User.Identity?.Name;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var inventory = new Inventory
                {
                    Title = createDto.Title,
                    TitleBn = createDto.TitleBn,
                    Description = createDto.Description,
                    DescriptionBn = createDto.DescriptionBn,
                    ImageUrl = createDto.ImageUrl,
                    CreatorId = userId,
                    OwnerId = userId,
                    CategoryId = createDto.CategoryId,
                    IsPublic = createDto.IsPublic,
                    IsActive = true,
                    CustomIdFormat = createDto.CustomIdFormat,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();

                // Generate custom ID if format is provided
                if (!string.IsNullOrEmpty(createDto.CustomIdFormat))
                {
                    var customId = await _idGeneratorService.GenerateIDAsync(createDto.CustomIdFormat, inventory.Id);
                    // You might want to store this custom ID in a separate field
                }

                var inventoryDto = new InventoryDto
                {
                    Id = inventory.Id,
                    Title = inventory.Title,
                    TitleBn = inventory.TitleBn,
                    Description = inventory.Description,
                    DescriptionBn = inventory.DescriptionBn,
                    ImageUrl = inventory.ImageUrl,
                    CreatorId = inventory.CreatorId,
                    OwnerId = inventory.OwnerId,
                    CategoryId = inventory.CategoryId,
                    IsPublic = inventory.IsPublic,
                    IsActive = inventory.IsActive,
                    LikesCount = inventory.LikesCount,
                    ViewsCount = inventory.ViewsCount,
                    CreatedAt = inventory.CreatedAt,
                    UpdatedAt = inventory.UpdatedAt
                };

                _logger.LogInformation("Created inventory {InventoryId} by user {UserId}", inventory.Id, userId);
                return CreatedAtAction(nameof(GetInventory), new { id = inventory.Id }, inventoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating inventory");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/inventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] UpdateInventoryDto updateDto)
        {
            try
            {
                var userId = User.Identity?.Name;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var inventory = await _context.Inventories.FindAsync(id);
                if (inventory == null)
                    return NotFound();

                // Check access control
                var user = await _context.Users.FindAsync(userId);
                if (user == null || !await _accessControlService.CanEditInventoryAsync(user, id))
                    return Forbid();

                // Optimistic concurrency check
                if (inventory.Version != updateDto.Version)
                    return Conflict("Inventory has been modified by another user");

                inventory.Title = updateDto.Title;
                inventory.TitleBn = updateDto.TitleBn;
                inventory.Description = updateDto.Description;
                inventory.DescriptionBn = updateDto.DescriptionBn;
                inventory.ImageUrl = updateDto.ImageUrl;
                inventory.CategoryId = updateDto.CategoryId;
                inventory.IsPublic = updateDto.IsPublic;
                inventory.IsActive = updateDto.IsActive;
                inventory.Version++;
                inventory.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Updated inventory {InventoryId} by user {UserId}", id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating inventory {InventoryId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                var userId = User.Identity?.Name;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var inventory = await _context.Inventories.FindAsync(id);
                if (inventory == null)
                    return NotFound();

                // Check access control
                var user = await _context.Users.FindAsync(userId);
                if (user == null || !await _accessControlService.CanDeleteInventoryAsync(user, id))
                    return Forbid();

                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Deleted inventory {InventoryId} by user {UserId}", id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting inventory {InventoryId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/inventories/5/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetInventoryItems(int id)
        {
            try
            {
                var userId = User.Identity?.Name;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                // Check access control
                var user = await _context.Users.FindAsync(userId);
                if (user == null || !await _accessControlService.CanViewInventoryAsync(user, id))
                    return Forbid();

                var items = await _context.Items
                    .Where(item => item.InventoryId == id)
                    .OrderBy(item => item.CreatedAt)
                    .Select(item => new ItemDto
                    {
                        Id = item.Id,
                        InventoryId = item.InventoryId,
                        CustomId = item.CustomId,
                        CreatedById = item.CreatedById,
                        LikesCount = item.LikesCount,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving items for inventory {InventoryId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
