using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BrightLifeIMS.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredLanguage",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "en-US");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NameBn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NameBn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UsageCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    TitleBn = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionBn = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatorId = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomIdFormat = table.Column<string>(type: "jsonb", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    LikesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ViewsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomString1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString1Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString1Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString1Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString1Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomString2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString2Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString2Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString2Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString2Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomString3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString3Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString3Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString3Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString3Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomInt1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInt1Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInt1Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt1Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt1Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomInt2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInt2Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInt2Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt2Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt2Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomInt3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInt3Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInt3Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt3Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInt3Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomBool1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomBool1Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomBool1Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool1Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool1Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomBool2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomBool2Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomBool2Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool2Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool2Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomBool3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomBool3Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomBool3Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool3Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomBool3Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomText1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText1Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText1Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText1Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText1Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomText2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText2Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText2Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText2Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText2Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomText3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText3Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText3Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText3Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomText3Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomUrl1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl1Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl1Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl1Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl1Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomUrl2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl2Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl2Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl2Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl2Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomUrl3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl3Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl3Displayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl3Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomUrl3Order = table.Column<int>(type: "INTEGER", nullable: false),
                    LastSavedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AutoSaveData = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AutoSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    SaveData = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoSaves_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoSaves_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTags",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTags", x => new { x.InventoryId, x.TagId });
                    table.ForeignKey(
                        name: "FK_InventoryTags_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    LikesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomString1 = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CustomString2 = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CustomString3 = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CustomInt1 = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomInt2 = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomInt3 = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomBool1 = table.Column<bool>(type: "INTEGER", nullable: true),
                    CustomBool2 = table.Column<bool>(type: "INTEGER", nullable: true),
                    CustomBool3 = table.Column<bool>(type: "INTEGER", nullable: true),
                    CustomText1 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText2 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomText3 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomUrl1 = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CustomUrl2 = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CustomUrl3 = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CloudImages = table.Column<string>(type: "jsonb", nullable: true),
                    CustomFields = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ParentCommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemLikes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "Icon", "IsActive", "Name", "NameBn" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 1, 5, 45, 13, 178, DateTimeKind.Utc).AddTicks(4134), null, 1, null, true, "Physical Goods", "শারীরিক পণ্য" },
                    { 2, new DateTime(2025, 9, 1, 5, 45, 13, 178, DateTimeKind.Utc).AddTicks(6110), null, 2, null, true, "Membership Services", "সদস্যপদ সেবা" },
                    { 3, new DateTime(2025, 9, 1, 5, 45, 13, 178, DateTimeKind.Utc).AddTicks(6113), null, 3, null, true, "Partner Claims", "অংশীদার দাবি" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AutoSaves_InventoryId_CreatedAt",
                table: "AutoSaves",
                columns: new[] { "InventoryId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AutoSaves_UserId",
                table: "AutoSaves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DisplayOrder",
                table: "Categories",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedAt",
                table: "Comments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ItemId",
                table: "Comments",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_inventories_fts",
                table: "Inventories",
                columns: new[] { "Title", "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CategoryId",
                table: "Inventories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CreatedAt",
                table: "Inventories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CreatorId_IsPublic",
                table: "Inventories",
                columns: new[] { "CreatorId", "IsPublic" });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_IsActive",
                table: "Inventories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Title",
                table: "Inventories",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "idx_inventory_tags_composite",
                table: "InventoryTags",
                columns: new[] { "InventoryId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTags_TagId",
                table: "InventoryTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "idx_unique_like_per_user_item",
                table: "ItemLikes",
                columns: new[] { "ItemId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemLikes_UserId",
                table: "ItemLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_unique_custom_id_per_inventory",
                table: "Items",
                columns: new[] { "InventoryId", "CustomId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedAt",
                table: "Items",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedById",
                table: "Items",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventoryId",
                table: "Items",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UsageCount",
                table: "Tags",
                column: "UsageCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoSaves");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "InventoryTags");

            migrationBuilder.DropTable(
                name: "ItemLikes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreferredLanguage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");
        }
    }
}
