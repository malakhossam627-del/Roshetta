using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;
using Rosetta.DAL.Entites.Catalog;
using Rosetta.DAL.Entites.Core;
using Rosetta.DAL.Entites.Order;
using Rosetta.DAL.Entites.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roshetta.BLL.Services.DataSeeder
{
    public static class DemoDataSeeder
    {
        public static async Task SeedAsync(RoshettaDbContext context)
        {
            // Seed Pharmacy
            if (!await context.Pharmacies.AnyAsync())
            {
                var pharmacy = new PharmacyModel
                {
                    Name = "Roshetta Central Pharmacy - Zamalek",
                    LicenseNumber = "EDA-PHARM-2026-4412",
                    AddressText = "12 Gezira St, Zamalek, Cairo",
                    Phone = "+20 2 2736 1234",
                    IsActive = true
                };
                context.Pharmacies.Add(pharmacy);
                await context.SaveChangesAsync();
            }

            var defaultPharmacy = await context.Pharmacies.FirstAsync();

            // Seed Default User (David Samir / Fady Maged)
            if (!await context.AppUsers.AnyAsync())
            {
                var user = new AppUser
                {
                    FullName = "David Samir",
                    Email = "david.samir@roshetta.eg",
                    Phone = "01012345678",
                    PreferredLanguage = "en",
                    TimeZone = "Egypt Standard Time",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                };
                context.AppUsers.Add(user);
                await context.SaveChangesAsync();

                var address = new Address
                {
                    UserId = user.UserId,
                    Governorate = "Cairo",
                    City = "Zamalek",
                    Street = "26th of July St.",
                    Building = "Bldg 14, Apt 4B",
                    IsDefault = true
                };
                context.Addresses.Add(address);
                await context.SaveChangesAsync();
            }

            var defaultUser = await context.AppUsers.FirstAsync();

            // Seed Medicines & Products & Inventories
            if (!await context.Medicines.AnyAsync())
            {
                var medicines = new List<Medicine>
                {
                    new Medicine
                    {
                        NameEn = "Panadol Advance 500mg",
                        NameAr = "بنادول أدفانس ٥٠٠ مجم",
                        IntendedUse = "Pain relief and fever reduction with Optizorb technology",
                        SideEffects = "Rare allergic reactions; safe when taken as directed",
                        Warnings = "Do not exceed 8 tablets in 24 hours. Avoid taking other paracetamol products.",
                        PrescriptionType = "OTC",
                        ContentStatus = "Approved"
                    },
                    new Medicine
                    {
                        NameEn = "Limitless Omega 3",
                        NameAr = "ليمتلس أوميجا ٣",
                        IntendedUse = "Cardiovascular, brain, and joint support dietary supplement",
                        SideEffects = "Mild fishy aftertaste",
                        Warnings = "Consult physician if taking anticoagulants.",
                        PrescriptionType = "OTC",
                        ContentStatus = "Approved"
                    },
                    new Medicine
                    {
                        NameEn = "Concor 5mg",
                        NameAr = "كونكور ٥ مجم",
                        IntendedUse = "Hypertension and chronic heart conditions (Bisoprolol)",
                        SideEffects = "Dizziness, fatigue, slow heart rate",
                        Warnings = "Prescription only. Do not discontinue abruptly.",
                        PrescriptionType = "Rx",
                        ContentStatus = "Approved"
                    },
                    new Medicine
                    {
                        NameEn = "Augmentin 1g",
                        NameAr = "أوجمنتين ١ جم",
                        IntendedUse = "Bacterial infections antibiotic (Amoxicillin / Clavulanate)",
                        SideEffects = "Diarrhea, nausea, mild skin rash",
                        Warnings = "Complete the entire course as prescribed by doctor.",
                        PrescriptionType = "Rx",
                        ContentStatus = "Approved"
                    },
                    new Medicine
                    {
                        NameEn = "C-Retard 500mg",
                        NameAr = "سي-ريتارد ٥٠٠ مجم",
                        IntendedUse = "Immune defense sustained-release Vitamin C capsules",
                        SideEffects = "None common",
                        Warnings = "Store in dry cool place below 25°C.",
                        PrescriptionType = "OTC",
                        ContentStatus = "Approved"
                    },
                    new Medicine
                    {
                        NameEn = "Antinal 200mg",
                        NameAr = "أنتينال ٢٠٠ مجم",
                        IntendedUse = "Intestinal antiseptic and acute diarrhea treatment (Nifuroxazide)",
                        SideEffects = "Well tolerated with minimal systemic absorption",
                        Warnings = "Consult doctor if fever persists over 48 hours.",
                        PrescriptionType = "OTC",
                        ContentStatus = "Approved"
                    }
                };

                context.Medicines.AddRange(medicines);
                await context.SaveChangesAsync();

                var products = new List<Product>
                {
                    new Product
                    {
                        MedicineId = medicines[0].MedicineId,
                        BrandNameEn = "Panadol Advance",
                        BrandNameAr = "بنادول أدفانس",
                        Strength = "500mg",
                        DosageForm = "Film-coated Tablets",
                        PackSize = "24 Tablets",
                        SKU = "PAN-ADV-500",
                        IsActive = true
                    },
                    new Product
                    {
                        MedicineId = medicines[1].MedicineId,
                        BrandNameEn = "Limitless Omega 3",
                        BrandNameAr = "ليمتلس أوميجا ٣",
                        Strength = "1000mg",
                        DosageForm = "Softgel Capsules",
                        PackSize = "30 Capsules",
                        SKU = "LIM-OMG-1000",
                        IsActive = true
                    },
                    new Product
                    {
                        MedicineId = medicines[2].MedicineId,
                        BrandNameEn = "Concor 5mg",
                        BrandNameAr = "كونكور ٥ مجم",
                        Strength = "5mg",
                        DosageForm = "Film-coated Tablets",
                        PackSize = "30 Tablets",
                        SKU = "CNC-5MG-30",
                        IsActive = true
                    },
                    new Product
                    {
                        MedicineId = medicines[3].MedicineId,
                        BrandNameEn = "Augmentin 1g",
                        BrandNameAr = "أوجمنتين ١ جم",
                        Strength = "1000mg",
                        DosageForm = "Oral Tablets",
                        PackSize = "14 Tablets",
                        SKU = "AUG-1G-14",
                        IsActive = true
                    },
                    new Product
                    {
                        MedicineId = medicines[4].MedicineId,
                        BrandNameEn = "C-Retard 500mg",
                        BrandNameAr = "سي ريتارد ٥٠٠ مجم",
                        Strength = "500mg",
                        DosageForm = "Sustained Release Capsules",
                        PackSize = "10 Capsules",
                        SKU = "CRT-500-10",
                        IsActive = true
                    },
                    new Product
                    {
                        MedicineId = medicines[5].MedicineId,
                        BrandNameEn = "Antinal 200mg",
                        BrandNameAr = "أنتينال ٢٠٠ مجم",
                        Strength = "200mg",
                        DosageForm = "Capsules",
                        PackSize = "24 Capsules",
                        SKU = "ANT-200-24",
                        IsActive = true
                    }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();

                var inventories = new List<Inventory>
                {
                    new Inventory
                    {
                        PharmacyId = defaultPharmacy.PharmacyId,
                        ProductId = products[0].ProductId,
                        UnitPrice = 45.00m,
                        StockQuantity = 120,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Inventory
                    {
                        PharmacyId = defaultPharmacy.PharmacyId,
                        ProductId = products[1].ProductId,
                        UnitPrice = 265.00m,
                        StockQuantity = 65,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Inventory
                    {
                        PharmacyId = defaultPharmacy.PharmacyId,
                        ProductId = products[2].ProductId,
                        UnitPrice = 68.00m,
                        StockQuantity = 80,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Inventory
                    {
                        PharmacyId = defaultPharmacy.PharmacyId,
                        ProductId = products[3].ProductId,
                        UnitPrice = 131.00m,
                        StockQuantity = 40,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Inventory
                    {
                        PharmacyId = defaultPharmacy.PharmacyId,
                        ProductId = products[4].ProductId,
                        UnitPrice = 32.50m,
                        StockQuantity = 150,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Inventory
                    {
                        PharmacyId = defaultPharmacy.PharmacyId,
                        ProductId = products[5].ProductId,
                        UnitPrice = 42.00m,
                        StockQuantity = 90,
                        UpdatedAt = DateTime.UtcNow
                    }
                };

                context.Inventories.AddRange(inventories);
                await context.SaveChangesAsync();
            }

            // Seed Initial Cart Items if cart is empty
            var existingCart = await context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == defaultUser.UserId && c.Status == "Active");

            if (existingCart == null)
            {
                existingCart = new Rosetta.DAL.Entites.Cart.Cart
                {
                    UserId = defaultUser.UserId,
                    PharmacyId = defaultPharmacy.PharmacyId,
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow
                };
                context.Carts.Add(existingCart);
                await context.SaveChangesAsync();
            }

            if (!existingCart.CartItems.Any())
            {
                var firstInv = await context.Inventories.FirstOrDefaultAsync();
                var secondInv = await context.Inventories.Skip(1).FirstOrDefaultAsync();
                if (firstInv != null)
                {
                    context.CartItems.Add(new Rosetta.DAL.Entites.Cart.CartItem
                    {
                        CartId = existingCart.CartId,
                        InventoryId = firstInv.InventoryId,
                        Quantity = 2
                    });
                }
                if (secondInv != null)
                {
                    context.CartItems.Add(new Rosetta.DAL.Entites.Cart.CartItem
                    {
                        CartId = existingCart.CartId,
                        InventoryId = secondInv.InventoryId,
                        Quantity = 1
                    });
                }
                await context.SaveChangesAsync();
            }

            // Seed Sample Orders for demonstration
            if (!await context.CustomerOrders.AnyAsync(o => o.CustomerUserId == defaultUser.UserId))
            {
                var allInvs = await context.Inventories.Include(i => i.Product).ToListAsync();
                if (allInvs.Count >= 3)
                {
                    // Order 1: Delivered
                    var order1 = new CustomerOrder
                    {
                        OrderNumber = "RSH-20261121-1042",
                        CustomerUserId = defaultUser.UserId,
                        PharmacyId = defaultPharmacy.PharmacyId,
                        Status = "Delivered",
                        TotalAmount = 310.00m,
                        CreatedAt = DateTime.UtcNow.AddDays(-2)
                    };
                    context.CustomerOrders.Add(order1);
                    await context.SaveChangesAsync();

                    context.OrderItems.AddRange(
                        new OrderItem
                        {
                            OrderId = order1.OrderId,
                            InventoryId = allInvs[0].InventoryId,
                            Quantity = 1,
                            UnitPriceSnapshot = 45.00m,
                            ProductNameSnapshot = "Panadol Advance 500mg",
                            LineTotal = 45.00m
                        },
                        new OrderItem
                        {
                            OrderId = order1.OrderId,
                            InventoryId = allInvs[1].InventoryId,
                            Quantity = 1,
                            UnitPriceSnapshot = 265.00m,
                            ProductNameSnapshot = "Limitless Omega 3 - 30 Capsules",
                            LineTotal = 265.00m
                        }
                    );

                    context.Payments.Add(new Payment
                    {
                        OrderId = order1.OrderId,
                        Method = "Cash on Delivery",
                        Amount = 310.00m,
                        Status = "Completed",
                        PaidAt = DateTime.UtcNow.AddDays(-1)
                    });

                    context.OrderStatusHistories.AddRange(
                        new OrderStatusHistory { OrderId = order1.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Placed", ChangedAt = DateTime.UtcNow.AddDays(-2) },
                        new OrderStatusHistory { OrderId = order1.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Confirmed", ChangedAt = DateTime.UtcNow.AddDays(-2).AddHours(1) },
                        new OrderStatusHistory { OrderId = order1.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Packed", ChangedAt = DateTime.UtcNow.AddDays(-1).AddHours(3) },
                        new OrderStatusHistory { OrderId = order1.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Out for delivery", ChangedAt = DateTime.UtcNow.AddDays(-1).AddHours(5) },
                        new OrderStatusHistory { OrderId = order1.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Delivered", ChangedAt = DateTime.UtcNow.AddDays(-1).AddHours(7) }
                    );

                    // Order 2: Active / Out for delivery
                    var order2 = new CustomerOrder
                    {
                        OrderNumber = "RSH-20261123-5591",
                        CustomerUserId = defaultUser.UserId,
                        PharmacyId = defaultPharmacy.PharmacyId,
                        Status = "Out for delivery",
                        TotalAmount = 125.50m,
                        CreatedAt = DateTime.UtcNow.AddHours(-3)
                    };
                    context.CustomerOrders.Add(order2);
                    await context.SaveChangesAsync();

                    context.OrderItems.AddRange(
                        new OrderItem
                        {
                            OrderId = order2.OrderId,
                            InventoryId = allInvs[2].InventoryId,
                            Quantity = 1,
                            UnitPriceSnapshot = 68.00m,
                            ProductNameSnapshot = "Concor 5mg - 30 Tablets",
                            LineTotal = 68.00m
                        },
                        new OrderItem
                        {
                            OrderId = order2.OrderId,
                            InventoryId = allInvs[4].InventoryId,
                            Quantity = 1,
                            UnitPriceSnapshot = 32.50m,
                            ProductNameSnapshot = "C-Retard 500mg - 10 Capsules",
                            LineTotal = 32.50m
                        }
                    );

                    context.Payments.Add(new Payment
                    {
                        OrderId = order2.OrderId,
                        Method = "Cash on Delivery",
                        Amount = 125.50m,
                        Status = "Pending"
                    });

                    context.OrderStatusHistories.AddRange(
                        new OrderStatusHistory { OrderId = order2.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Placed", ChangedAt = DateTime.UtcNow.AddHours(-3) },
                        new OrderStatusHistory { OrderId = order2.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Confirmed", ChangedAt = DateTime.UtcNow.AddHours(-2) },
                        new OrderStatusHistory { OrderId = order2.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Packed", ChangedAt = DateTime.UtcNow.AddHours(-1) },
                        new OrderStatusHistory { OrderId = order2.OrderId, ChangedByUserId = defaultUser.UserId, Status = "Out for delivery", ChangedAt = DateTime.UtcNow.AddMinutes(-30) }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
