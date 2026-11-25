using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;
using Rosetta.DAL.Entites.Cart;
using Roshetta.BLL.DTOs;
using Roshetta.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roshetta.BLL.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly RoshettaDbContext _context;

        public CartService(RoshettaDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await GetOrCreateActiveCartAsync(userId);

            var cartDto = new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                PharmacyId = cart.PharmacyId,
                PharmacyName = cart.Pharmacy?.Name ?? "Roshetta Partner Pharmacy",
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    CartItemId = ci.CartItemId,
                    InventoryId = ci.InventoryId,
                    ProductId = ci.Inventory.ProductId,
                    ProductName = ci.Inventory.Product.BrandNameEn ?? ci.Inventory.Product.Medicine.NameEn,
                    BrandNameAr = ci.Inventory.Product.BrandNameAr ?? ci.Inventory.Product.Medicine.NameAr,
                    DosageForm = ci.Inventory.Product.DosageForm,
                    Strength = ci.Inventory.Product.Strength,
                    UnitPrice = ci.Inventory.UnitPrice,
                    Quantity = ci.Quantity,
                    AvailableStock = ci.Inventory.StockQuantity,
                    PrescriptionType = ci.Inventory.Product.Medicine.PrescriptionType
                }).ToList()
            };

            return cartDto;
        }

        public async Task<(bool Success, string Message)> AddToCartAsync(int userId, int inventoryId, int quantity = 1)
        {
            if (quantity <= 0)
                return (false, "Quantity must be greater than zero.");

            var inventory = await _context.Inventories
                .Include(i => i.Product)
                    .ThenInclude(p => p.Medicine)
                .FirstOrDefaultAsync(i => i.InventoryId == inventoryId);

            if (inventory == null)
                return (false, "Product inventory not found.");

            if (inventory.StockQuantity < quantity)
                return (false, $"Only {inventory.StockQuantity} unit(s) available in stock.");

            var cart = await GetOrCreateActiveCartAsync(userId, inventory.PharmacyId);

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.InventoryId == inventoryId);
            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity + quantity;
                if (newQuantity > inventory.StockQuantity)
                {
                    return (false, $"Cannot add more. Only {inventory.StockQuantity} available, and you have {existingItem.Quantity} in cart.");
                }
                existingItem.Quantity = newQuantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.CartId,
                    InventoryId = inventoryId,
                    Quantity = quantity
                };
                cart.CartItems.Add(newItem);
                _context.CartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
            var prodName = inventory.Product.BrandNameEn ?? inventory.Product.Medicine.NameEn;
            return (true, $"{prodName} added to your cart successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateQuantityAsync(int userId, int cartItemId, int quantity)
        {
            var cart = await GetOrCreateActiveCartAsync(userId);
            var item = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (item == null)
                return (false, "Cart item not found.");

            if (quantity <= 0)
            {
                return await RemoveFromCartAsync(userId, cartItemId);
            }

            var inventory = await _context.Inventories.FindAsync(item.InventoryId);
            if (inventory == null || inventory.StockQuantity < quantity)
            {
                var avail = inventory?.StockQuantity ?? 0;
                return (false, $"Insufficient stock. Maximum available: {avail}.");
            }

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
            return (true, "Cart updated successfully.");
        }

        public async Task<(bool Success, string Message)> RemoveFromCartAsync(int userId, int cartItemId)
        {
            var cart = await GetOrCreateActiveCartAsync(userId);
            var item = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (item == null)
                return (false, "Cart item not found.");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return (true, "Item removed from cart.");
        }

        public async Task<(bool Success, string Message)> ClearCartAsync(int userId)
        {
            var cart = await GetOrCreateActiveCartAsync(userId);
            if (cart.CartItems.Any())
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
            return (true, "Cart cleared successfully.");
        }

        public async Task<int> GetCartItemCountAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == "Active");

            return cart?.CartItems.Sum(ci => ci.Quantity) ?? 0;
        }

        private async Task<Cart> GetOrCreateActiveCartAsync(int userId, int? targetPharmacyId = null)
        {
            var cart = await _context.Carts
                .Include(c => c.Pharmacy)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Inventory)
                        .ThenInclude(i => i.Product)
                            .ThenInclude(p => p.Medicine)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == "Active");

            if (cart == null)
            {
                var pharmacyId = targetPharmacyId ?? (await _context.Pharmacies.Select(p => p.PharmacyId).FirstOrDefaultAsync());
                if (pharmacyId == 0)
                {
                    // Fallback create pharmacy if DB is not seeded yet
                    var defaultPharmacy = new Rosetta.DAL.Entites.Pharmacy.PharmacyModel
                    {
                        Name = "Roshetta Partner Pharmacy - Cairo",
                        LicenseNumber = "EGY-PHARM-2026-001",
                        AddressText = "26 July St, Zamalek, Cairo",
                        Phone = "02-27350000",
                        IsActive = true
                    };
                    _context.Pharmacies.Add(defaultPharmacy);
                    await _context.SaveChangesAsync();
                    pharmacyId = defaultPharmacy.PharmacyId;
                }

                cart = new Cart
                {
                    UserId = userId,
                    PharmacyId = pharmacyId,
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
