using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rosetta.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APP_ROLE",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_ROLE", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "APP_USER",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PreferredLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_USER", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "INGREDIENT",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INGREDIENT", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "PHARMACY",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHARMACY", x => x.PharmacyId);
                });

            migrationBuilder.CreateTable(
                name: "ADDRESS",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Building = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESS", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_ADDRESS_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUDIT_LOG",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorUserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDIT_LOG", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_AUDIT_LOG_APP_USER_ActorUserId",
                        column: x => x.ActorUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CONSENT_RECORD",
                columns: table => new
                {
                    ConsentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false),
                    PolicyVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GrantedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WithdrawnAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSENT_RECORD", x => x.ConsentId);
                    table.ForeignKey(
                        name: "FK_CONSENT_RECORD_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CONVERSATION",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONVERSATION", x => x.ConversationId);
                    table.ForeignKey(
                        name: "FK_CONVERSATION_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INTERACTION_RULESET",
                columns: table => new
                {
                    RuleSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApprovedByUserId = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERACTION_RULESET", x => x.RuleSetId);
                    table.ForeignKey(
                        name: "FK_INTERACTION_RULESET_APP_USER_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "KB_DOCUMENT",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReviewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReviewedByUserId = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KB_DOCUMENT", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_KB_DOCUMENT_APP_USER_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MEDICATION_PLAN",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICATION_PLAN", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_MEDICATION_PLAN_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MEDICINE",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IntendedUse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SideEffects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warnings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescriptionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContentVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReviewedByUserId = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICINE", x => x.MedicineId);
                    table.ForeignKey(
                        name: "FK_MEDICINE_APP_USER_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLE", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_USER_ROLE_APP_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "APP_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CART",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_CART_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CART_PHARMACY_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PHARMACY",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER_ORDER",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerUserId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RxReviewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReviewedByUserId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER_ORDER", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ORDER_APP_USER_CustomerUserId",
                        column: x => x.CustomerUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ORDER_APP_USER_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ORDER_PHARMACY_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PHARMACY",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PHARMACY_STAFF",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StaffRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHARMACY_STAFF", x => new { x.PharmacyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PHARMACY_STAFF_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PHARMACY_STAFF_PHARMACY_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PHARMACY",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    SenderType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SafetyStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MESSAGE_CONVERSATION_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "CONVERSATION",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INTERACTION",
                columns: table => new
                {
                    InteractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleSetId = table.Column<int>(type: "int", nullable: false),
                    IngredientAId = table.Column<int>(type: "int", nullable: false),
                    IngredientBId = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClinicalEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendedAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceReference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERACTION", x => x.InteractionId);
                    table.ForeignKey(
                        name: "FK_INTERACTION_INGREDIENT_IngredientAId",
                        column: x => x.IngredientAId,
                        principalTable: "INGREDIENT",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_INGREDIENT_IngredientBId",
                        column: x => x.IngredientBId,
                        principalTable: "INGREDIENT",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_INTERACTION_RULESET_RuleSetId",
                        column: x => x.RuleSetId,
                        principalTable: "INTERACTION_RULESET",
                        principalColumn: "RuleSetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INTERACTION_CHECK",
                columns: table => new
                {
                    InteractionCheckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RuleSetId = table.Column<int>(type: "int", nullable: false),
                    ConsentId = table.Column<int>(type: "int", nullable: true),
                    CheckedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultSummary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERACTION_CHECK", x => x.InteractionCheckId);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_CONSENT_RECORD_ConsentId",
                        column: x => x.ConsentId,
                        principalTable: "CONSENT_RECORD",
                        principalColumn: "ConsentId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_INTERACTION_RULESET_RuleSetId",
                        column: x => x.RuleSetId,
                        principalTable: "INTERACTION_RULESET",
                        principalColumn: "RuleSetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KB_CHUNK",
                columns: table => new
                {
                    ChunkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    ChunkIndex = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingReference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KB_CHUNK", x => x.ChunkId);
                    table.ForeignKey(
                        name: "FK_KB_CHUNK_KB_DOCUMENT_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "KB_DOCUMENT",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MEDICINE_INGREDIENT",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    IngredientStrength = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICINE_INGREDIENT", x => new { x.MedicineId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_MEDICINE_INGREDIENT_INGREDIENT_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "INGREDIENT",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MEDICINE_INGREDIENT_MEDICINE_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "MEDICINE",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    BrandNameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrandNameAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Strength = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DosageForm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PackSize = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_PRODUCT_MEDICINE_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "MEDICINE",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_STATUS_HISTORY",
                columns: table => new
                {
                    StatusHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_STATUS_HISTORY", x => x.StatusHistoryId);
                    table.ForeignKey(
                        name: "FK_ORDER_STATUS_HISTORY_APP_USER_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDER_STATUS_HISTORY_CUSTOMER_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CUSTOMER_ORDER",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PAYMENT_CUSTOMER_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CUSTOMER_ORDER",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RX_UPLOAD",
                columns: table => new
                {
                    RxUploadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UploadedByUserId = table.Column<int>(type: "int", nullable: false),
                    FileReference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReviewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RX_UPLOAD", x => x.RxUploadId);
                    table.ForeignKey(
                        name: "FK_RX_UPLOAD_APP_USER_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RX_UPLOAD_CUSTOMER_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CUSTOMER_ORDER",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_FEEDBACK",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_FEEDBACK", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_MESSAGE_FEEDBACK_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGE_FEEDBACK_MESSAGE_MessageId",
                        column: x => x.MessageId,
                        principalTable: "MESSAGE",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SAFETY_FLAG",
                columns: table => new
                {
                    SafetyFlagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    InteractionCheckId = table.Column<int>(type: "int", nullable: true),
                    FlagType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReviewedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAFETY_FLAG", x => x.SafetyFlagId);
                    table.ForeignKey(
                        name: "FK_SAFETY_FLAG_APP_USER_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SAFETY_FLAG_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAFETY_FLAG_CONVERSATION_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "CONVERSATION",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SAFETY_FLAG_INTERACTION_CHECK_InteractionCheckId",
                        column: x => x.InteractionCheckId,
                        principalTable: "INTERACTION_CHECK",
                        principalColumn: "InteractionCheckId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SAFETY_FLAG_MESSAGE_MessageId",
                        column: x => x.MessageId,
                        principalTable: "MESSAGE",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_CITATION",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ChunkId = table.Column<int>(type: "int", nullable: false),
                    CitationOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_CITATION", x => new { x.MessageId, x.ChunkId });
                    table.ForeignKey(
                        name: "FK_MESSAGE_CITATION_KB_CHUNK_ChunkId",
                        column: x => x.ChunkId,
                        principalTable: "KB_CHUNK",
                        principalColumn: "ChunkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGE_CITATION_MESSAGE_MessageId",
                        column: x => x.MessageId,
                        principalTable: "MESSAGE",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INVENTORY",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVENTORY", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_INVENTORY_PHARMACY_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PHARMACY",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INVENTORY_PRODUCT_ProductId",
                        column: x => x.ProductId,
                        principalTable: "PRODUCT",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PLAN_ITEM",
                columns: table => new
                {
                    PlanItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    MedicineNameEntered = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DoseAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    DoseUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplyRemaining = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    RefillThreshold = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAN_ITEM", x => x.PlanItemId);
                    table.ForeignKey(
                        name: "FK_PLAN_ITEM_MEDICATION_PLAN_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MEDICATION_PLAN",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLAN_ITEM_MEDICINE_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "MEDICINE",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PLAN_ITEM_PRODUCT_ProductId",
                        column: x => x.ProductId,
                        principalTable: "PRODUCT",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CART_ITEM",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART_ITEM", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CART_ITEM_CART_CartId",
                        column: x => x.CartId,
                        principalTable: "CART",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CART_ITEM_INVENTORY_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "INVENTORY",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_ITEM",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPriceSnapshot = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductNameSnapshot = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_ITEM", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_ORDER_ITEM_CUSTOMER_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CUSTOMER_ORDER",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDER_ITEM_INVENTORY_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "INVENTORY",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INTERACTION_CHECK_ITEM",
                columns: table => new
                {
                    CheckItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InteractionCheckId = table.Column<int>(type: "int", nullable: false),
                    PlanItemId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    EnteredName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERACTION_CHECK_ITEM", x => x.CheckItemId);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_ITEM_INTERACTION_CHECK_InteractionCheckId",
                        column: x => x.InteractionCheckId,
                        principalTable: "INTERACTION_CHECK",
                        principalColumn: "InteractionCheckId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_ITEM_MEDICINE_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "MEDICINE",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_ITEM_PLAN_ITEM_PlanItemId",
                        column: x => x.PlanItemId,
                        principalTable: "PLAN_ITEM",
                        principalColumn: "PlanItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MEDICATION_SCHEDULE",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanItemId = table.Column<int>(type: "int", nullable: false),
                    ScheduleType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IntervalHours = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICATION_SCHEDULE", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_MEDICATION_SCHEDULE_PLAN_ITEM_PlanItemId",
                        column: x => x.PlanItemId,
                        principalTable: "PLAN_ITEM",
                        principalColumn: "PlanItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REFILL_ALERT",
                columns: table => new
                {
                    RefillAlertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanItemId = table.Column<int>(type: "int", nullable: false),
                    TriggeredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThresholdAtTrigger = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REFILL_ALERT", x => x.RefillAlertId);
                    table.ForeignKey(
                        name: "FK_REFILL_ALERT_PLAN_ITEM_PlanItemId",
                        column: x => x.PlanItemId,
                        principalTable: "PLAN_ITEM",
                        principalColumn: "PlanItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CHECK_ITEM_INGREDIENT",
                columns: table => new
                {
                    CheckItemId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHECK_ITEM_INGREDIENT", x => new { x.CheckItemId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_CHECK_ITEM_INGREDIENT_INGREDIENT_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "INGREDIENT",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHECK_ITEM_INGREDIENT_INTERACTION_CHECK_ITEM_CheckItemId",
                        column: x => x.CheckItemId,
                        principalTable: "INTERACTION_CHECK_ITEM",
                        principalColumn: "CheckItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INTERACTION_CHECK_RESULT",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InteractionCheckId = table.Column<int>(type: "int", nullable: false),
                    InteractionId = table.Column<int>(type: "int", nullable: false),
                    CheckItemId = table.Column<int>(type: "int", nullable: false),
                    SeveritySnapshot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExplanationSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERACTION_CHECK_RESULT", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_RESULT_INTERACTION_CHECK_ITEM_CheckItemId",
                        column: x => x.CheckItemId,
                        principalTable: "INTERACTION_CHECK_ITEM",
                        principalColumn: "CheckItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_RESULT_INTERACTION_CHECK_InteractionCheckId",
                        column: x => x.InteractionCheckId,
                        principalTable: "INTERACTION_CHECK",
                        principalColumn: "InteractionCheckId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INTERACTION_CHECK_RESULT_INTERACTION_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "INTERACTION",
                        principalColumn: "InteractionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOSE_EVENT",
                columns: table => new
                {
                    DoseEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    ScheduledAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SnoozedUntilUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOSE_EVENT", x => x.DoseEventId);
                    table.ForeignKey(
                        name: "FK_DOSE_EVENT_MEDICATION_SCHEDULE_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "MEDICATION_SCHEDULE",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SCHEDULE_TIME",
                columns: table => new
                {
                    ScheduleTimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    LocalTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULE_TIME", x => x.ScheduleTimeId);
                    table.ForeignKey(
                        name: "FK_SCHEDULE_TIME_MEDICATION_SCHEDULE_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "MEDICATION_SCHEDULE",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SCHEDULE_WEEKDAY",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULE_WEEKDAY", x => new { x.ScheduleId, x.DayOfWeek });
                    table.ForeignKey(
                        name: "FK_SCHEDULE_WEEKDAY_MEDICATION_SCHEDULE_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "MEDICATION_SCHEDULE",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICATION",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DoseEventId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    RefillAlertId = table.Column<int>(type: "int", nullable: true),
                    NotificationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScheduledAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATION", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_APP_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_CUSTOMER_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CUSTOMER_ORDER",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_DOSE_EVENT_DoseEventId",
                        column: x => x.DoseEventId,
                        principalTable: "DOSE_EVENT",
                        principalColumn: "DoseEventId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_REFILL_ALERT_RefillAlertId",
                        column: x => x.RefillAlertId,
                        principalTable: "REFILL_ALERT",
                        principalColumn: "RefillAlertId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESS_UserId",
                table: "ADDRESS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_APP_ROLE_Name",
                table: "APP_ROLE",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_APP_USER_Email",
                table: "APP_USER",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AUDIT_LOG_ActorUserId",
                table: "AUDIT_LOG",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CART_PharmacyId",
                table: "CART",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_CART_UserId",
                table: "CART",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CART_ITEM_CartId",
                table: "CART_ITEM",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CART_ITEM_InventoryId",
                table: "CART_ITEM",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CHECK_ITEM_INGREDIENT_IngredientId",
                table: "CHECK_ITEM_INGREDIENT",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CONSENT_RECORD_UserId",
                table: "CONSENT_RECORD",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CONVERSATION_UserId",
                table: "CONVERSATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ORDER_CustomerUserId",
                table: "CUSTOMER_ORDER",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ORDER_OrderNumber",
                table: "CUSTOMER_ORDER",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ORDER_PharmacyId",
                table: "CUSTOMER_ORDER",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ORDER_ReviewedByUserId",
                table: "CUSTOMER_ORDER",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DOSE_EVENT_ScheduleId",
                table: "DOSE_EVENT",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_IngredientAId",
                table: "INTERACTION",
                column: "IngredientAId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_IngredientBId",
                table: "INTERACTION",
                column: "IngredientBId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_RuleSetId",
                table: "INTERACTION",
                column: "RuleSetId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_ConsentId",
                table: "INTERACTION_CHECK",
                column: "ConsentId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_RuleSetId",
                table: "INTERACTION_CHECK",
                column: "RuleSetId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_UserId",
                table: "INTERACTION_CHECK",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_ITEM_InteractionCheckId",
                table: "INTERACTION_CHECK_ITEM",
                column: "InteractionCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_ITEM_MedicineId",
                table: "INTERACTION_CHECK_ITEM",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_ITEM_PlanItemId",
                table: "INTERACTION_CHECK_ITEM",
                column: "PlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_RESULT_CheckItemId",
                table: "INTERACTION_CHECK_RESULT",
                column: "CheckItemId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_RESULT_InteractionCheckId",
                table: "INTERACTION_CHECK_RESULT",
                column: "InteractionCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_CHECK_RESULT_InteractionId",
                table: "INTERACTION_CHECK_RESULT",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACTION_RULESET_ApprovedByUserId",
                table: "INTERACTION_RULESET",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_INVENTORY_PharmacyId",
                table: "INVENTORY",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_INVENTORY_ProductId",
                table: "INVENTORY",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_KB_CHUNK_DocumentId",
                table: "KB_CHUNK",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_KB_DOCUMENT_ReviewedByUserId",
                table: "KB_DOCUMENT",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICATION_PLAN_UserId",
                table: "MEDICATION_PLAN",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICATION_SCHEDULE_PlanItemId",
                table: "MEDICATION_SCHEDULE",
                column: "PlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICINE_ReviewedByUserId",
                table: "MEDICINE",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICINE_INGREDIENT_IngredientId",
                table: "MEDICINE_INGREDIENT",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_ConversationId",
                table: "MESSAGE",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_CITATION_ChunkId",
                table: "MESSAGE_CITATION",
                column: "ChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_FEEDBACK_MessageId",
                table: "MESSAGE_FEEDBACK",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_FEEDBACK_UserId",
                table: "MESSAGE_FEEDBACK",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_DoseEventId",
                table: "NOTIFICATION",
                column: "DoseEventId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_OrderId",
                table: "NOTIFICATION",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_RefillAlertId",
                table: "NOTIFICATION",
                column: "RefillAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_UserId",
                table: "NOTIFICATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEM_InventoryId",
                table: "ORDER_ITEM",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEM_OrderId",
                table: "ORDER_ITEM",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_STATUS_HISTORY_ChangedByUserId",
                table: "ORDER_STATUS_HISTORY",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_STATUS_HISTORY_OrderId",
                table: "ORDER_STATUS_HISTORY",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENT_OrderId",
                table: "PAYMENT",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PHARMACY_LicenseNumber",
                table: "PHARMACY",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PHARMACY_STAFF_UserId",
                table: "PHARMACY_STAFF",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PLAN_ITEM_MedicineId",
                table: "PLAN_ITEM",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PLAN_ITEM_PlanId",
                table: "PLAN_ITEM",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PLAN_ITEM_ProductId",
                table: "PLAN_ITEM",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_MedicineId",
                table: "PRODUCT",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_SKU",
                table: "PRODUCT",
                column: "SKU",
                unique: true,
                filter: "[SKU] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_REFILL_ALERT_PlanItemId",
                table: "REFILL_ALERT",
                column: "PlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RX_UPLOAD_OrderId",
                table: "RX_UPLOAD",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RX_UPLOAD_UploadedByUserId",
                table: "RX_UPLOAD",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SAFETY_FLAG_ConversationId",
                table: "SAFETY_FLAG",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_SAFETY_FLAG_InteractionCheckId",
                table: "SAFETY_FLAG",
                column: "InteractionCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_SAFETY_FLAG_MessageId",
                table: "SAFETY_FLAG",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_SAFETY_FLAG_ReviewedByUserId",
                table: "SAFETY_FLAG",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SAFETY_FLAG_UserId",
                table: "SAFETY_FLAG",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULE_TIME_ScheduleId",
                table: "SCHEDULE_TIME",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_RoleId",
                table: "USER_ROLE",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADDRESS");

            migrationBuilder.DropTable(
                name: "AUDIT_LOG");

            migrationBuilder.DropTable(
                name: "CART_ITEM");

            migrationBuilder.DropTable(
                name: "CHECK_ITEM_INGREDIENT");

            migrationBuilder.DropTable(
                name: "INTERACTION_CHECK_RESULT");

            migrationBuilder.DropTable(
                name: "MEDICINE_INGREDIENT");

            migrationBuilder.DropTable(
                name: "MESSAGE_CITATION");

            migrationBuilder.DropTable(
                name: "MESSAGE_FEEDBACK");

            migrationBuilder.DropTable(
                name: "NOTIFICATION");

            migrationBuilder.DropTable(
                name: "ORDER_ITEM");

            migrationBuilder.DropTable(
                name: "ORDER_STATUS_HISTORY");

            migrationBuilder.DropTable(
                name: "PAYMENT");

            migrationBuilder.DropTable(
                name: "PHARMACY_STAFF");

            migrationBuilder.DropTable(
                name: "RX_UPLOAD");

            migrationBuilder.DropTable(
                name: "SAFETY_FLAG");

            migrationBuilder.DropTable(
                name: "SCHEDULE_TIME");

            migrationBuilder.DropTable(
                name: "SCHEDULE_WEEKDAY");

            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "CART");

            migrationBuilder.DropTable(
                name: "INTERACTION_CHECK_ITEM");

            migrationBuilder.DropTable(
                name: "INTERACTION");

            migrationBuilder.DropTable(
                name: "KB_CHUNK");

            migrationBuilder.DropTable(
                name: "DOSE_EVENT");

            migrationBuilder.DropTable(
                name: "REFILL_ALERT");

            migrationBuilder.DropTable(
                name: "INVENTORY");

            migrationBuilder.DropTable(
                name: "CUSTOMER_ORDER");

            migrationBuilder.DropTable(
                name: "MESSAGE");

            migrationBuilder.DropTable(
                name: "APP_ROLE");

            migrationBuilder.DropTable(
                name: "INTERACTION_CHECK");

            migrationBuilder.DropTable(
                name: "INGREDIENT");

            migrationBuilder.DropTable(
                name: "KB_DOCUMENT");

            migrationBuilder.DropTable(
                name: "MEDICATION_SCHEDULE");

            migrationBuilder.DropTable(
                name: "PHARMACY");

            migrationBuilder.DropTable(
                name: "CONVERSATION");

            migrationBuilder.DropTable(
                name: "CONSENT_RECORD");

            migrationBuilder.DropTable(
                name: "INTERACTION_RULESET");

            migrationBuilder.DropTable(
                name: "PLAN_ITEM");

            migrationBuilder.DropTable(
                name: "MEDICATION_PLAN");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "MEDICINE");

            migrationBuilder.DropTable(
                name: "APP_USER");
        }
    }
}
