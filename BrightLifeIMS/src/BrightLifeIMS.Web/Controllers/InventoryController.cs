// src/BrightLifeIMS.Web/Controllers/InventoryController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Data.Repositories;
using System.Security.Claims;

namespace BrightLifeIMS.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly UserManager<User> _userManager;

        public InventoryController(
            IInventoryRepository inventoryRepository,
            IItemRepository itemRepository,
            UserManager<User> userManager)
        {
            _inventoryRepository = inventoryRepository;
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            
            var inventories = await _inventoryRepository.GetByUserAsync(userId);
            return View(inventories);
        }

        public IActionResult Create()
        {
            return View(new Inventory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account");
                }
                
                inventory.CreatorId = userId;
                inventory.OwnerId = userId;
                
                // Set default ID format if not provided
                if (string.IsNullOrEmpty(inventory.CustomIdFormat))
                {
                    inventory.CustomIdFormat = "ITEM-{SEQUENCE:000000}";
                }

                await _inventoryRepository.CreateAsync(inventory);
                return RedirectToAction(nameof(Index));
            }

            return View(inventory);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
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

            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existing = await _inventoryRepository.GetByIdAsync(id);
            
            if (existing == null)
            {
                return NotFound();
            }

            if (existing.OwnerId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                // Preserve ownership
                inventory.CreatorId = existing.CreatorId;
                inventory.OwnerId = existing.OwnerId;
                
                await _inventoryRepository.UpdateAsync(inventory);
                return RedirectToAction(nameof(Index));
            }

            return View(inventory);
        }

        public async Task<IActionResult> Items(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id, includeItems: true);
            if (inventory == null)
            {
                return NotFound();
            }

            // Check access
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!inventory.IsPublic && inventory.OwnerId != userId)
            {
                return Forbid();
            }

            ViewBag.Inventory = inventory;
            var items = await _itemRepository.GetByInventoryAsync(id);
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id, includeItems: true);
            if (inventory == null)
            {
                return NotFound();
            }

            // Check access
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!inventory.IsPublic && inventory.OwnerId != userId)
            {
                return Forbid();
            }

            return View(inventory);
        }
    }
}
