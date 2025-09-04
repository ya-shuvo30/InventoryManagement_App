// src/BrightLifeIMS.Web/Controllers/ItemController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Data.Repositories;
using BrightLifeIMS.Web.Services.Core;
using System.Security.Claims;

namespace BrightLifeIMS.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IIDGeneratorService _idGenerator;

        public ItemController(
            IItemRepository itemRepository,
            IInventoryRepository inventoryRepository,
            IIDGeneratorService idGenerator)
        {
            _itemRepository = itemRepository;
            _inventoryRepository = inventoryRepository;
            _idGenerator = idGenerator;
        }

        public async Task<IActionResult> Create(int inventoryId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            if (inventory == null)
            {
                return NotFound();
            }

            // Check ownership
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (inventory.OwnerId != userId)
            {
                return Forbid();
            }

            var item = new Item
            {
                InventoryId = inventoryId,
                CustomId = await _idGenerator.GenerateIDAsync(
                    inventory.CustomIdFormat ?? "ITEM-{SEQUENCE:000000}", 
                    inventoryId)
            };

            ViewBag.Inventory = inventory;
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(item.InventoryId);
            if (inventory == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (inventory.OwnerId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                item.CreatedById = userId;
                
                // Generate custom ID if not provided
                if (string.IsNullOrEmpty(item.CustomId))
                {
                    item.CustomId = await _idGenerator.GenerateIDAsync(
                        inventory.CustomIdFormat ?? "ITEM-{SEQUENCE:000000}", 
                        item.InventoryId);
                }

                await _itemRepository.CreateAsync(item);
                return RedirectToAction("Items", "Inventory", new { id = item.InventoryId });
            }

            ViewBag.Inventory = inventory;
            return View(item);
        }
    }
}