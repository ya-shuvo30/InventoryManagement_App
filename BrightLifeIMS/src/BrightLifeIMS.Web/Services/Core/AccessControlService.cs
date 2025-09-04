// src/BrightLifeIMS.Web/Services/Core/AccessControlService.cs
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Data.Repositories;

namespace BrightLifeIMS.Web.Services.Core
{
    public class AccessControlService : IAccessControlService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<AccessControlService> _logger;

        public AccessControlService(
            IInventoryRepository inventoryRepository,
            IItemRepository itemRepository,
            ILogger<AccessControlService> logger)
        {
            _inventoryRepository = inventoryRepository;
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<bool> CanViewInventoryAsync(User user, int inventoryId)
        {
            if (user == null)
            {
                // Check if inventory is public
                var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
                return inventory != null && inventory.IsPublic;
            }

            if (IsAdmin(user))
            {
                return true;
            }

            var inv = await _inventoryRepository.GetByIdAsync(inventoryId);
            if (inv == null)
            {
                return false;
            }

            return inv.IsPublic || inv.CreatorId == user.Id;
        }

        public async Task<bool> CanEditInventoryAsync(User user, int inventoryId)
        {
            if (user == null)
            {
                return false;
            }

            if (IsAdmin(user))
            {
                return true;
            }

            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            if (inventory == null)
            {
                return false;
            }

            return inventory.CreatorId == user.Id;
        }

        public async Task<bool> CanDeleteInventoryAsync(User user, int inventoryId)
        {
            return await CanEditInventoryAsync(user, inventoryId);
        }

        public async Task<bool> CanAddItemAsync(User user, int inventoryId)
        {
            if (user == null)
            {
                return false;
            }

            if (IsAdmin(user))
            {
                return true;
            }

            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            if (inventory == null)
            {
                return false;
            }

            // Inventory creator can always add items
            if (inventory.CreatorId == user.Id)
            {
                return true;
            }

            // Check if user has at least User role and inventory is public
            return user.Role >= UserRole.User && inventory.IsPublic;
        }

        public async Task<bool> CanEditItemAsync(User user, int itemId)
        {
            if (user == null)
            {
                return false;
            }

            if (IsAdmin(user))
            {
                return true;
            }

            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                return false;
            }

            // Item creator can edit their own items
            if (item.CreatedById == user.Id)
            {
                return true;
            }

            // Inventory owner can edit all items in their inventory
            var inventory = await _inventoryRepository.GetByIdAsync(item.InventoryId);
            return inventory != null && inventory.CreatorId == user.Id;
        }

        public async Task<bool> CanDeleteItemAsync(User user, int itemId)
        {
            return await CanEditItemAsync(user, itemId);
        }

        public bool IsAdmin(User user)
        {
            return user?.Role == UserRole.Admin;
        }

        public bool IsCreator(User user)
        {
            return user?.Role >= UserRole.Creator;
        }
    }
}
