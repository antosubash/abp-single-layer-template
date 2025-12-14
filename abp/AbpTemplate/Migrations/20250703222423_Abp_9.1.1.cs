using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbpTemplate.Migrations
{
    /// <inheritdoc />
    public partial class Abp_911 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "cmskit");

            migrationBuilder.CreateTable(
                name: "AbpBlobContainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBlobContainers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsBlogFeatures",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlogId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureName = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsBlogFeatures", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsBlogs",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    Slug = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsBlogs", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsComments",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityType = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    EntityId = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    Text = table.Column<string>(
                        type: "character varying(512)",
                        maxLength: 512,
                        nullable: false
                    ),
                    RepliedCommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    Url = table.Column<string>(
                        type: "character varying(512)",
                        maxLength: 512,
                        nullable: true
                    ),
                    IdempotencyToken = table.Column<string>(
                        type: "character varying(32)",
                        maxLength: 32,
                        nullable: true
                    ),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsComments", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsEntityTags",
                schema: "cmskit",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsEntityTags", x => new { x.EntityId, x.TagId });
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsGlobalResources",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Value = table.Column<string>(
                        type: "text",
                        maxLength: 2147483647,
                        nullable: false
                    ),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsGlobalResources", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsMediaDescriptors",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityType = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    Name = table.Column<string>(
                        type: "character varying(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    MimeType = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Size = table.Column<long>(
                        type: "bigint",
                        maxLength: 2147483647,
                        nullable: false
                    ),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsMediaDescriptors", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsMenuItems",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    DisplayName = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Url = table.Column<string>(
                        type: "character varying(1024)",
                        maxLength: 1024,
                        nullable: false
                    ),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Target = table.Column<string>(type: "text", nullable: true),
                    ElementId = table.Column<string>(type: "text", nullable: true),
                    CssClass = table.Column<string>(type: "text", nullable: true),
                    PageId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequiredPermissionName = table.Column<string>(
                        type: "character varying(128)",
                        maxLength: 128,
                        nullable: true
                    ),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsMenuItems", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsPages",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    Slug = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    Content = table.Column<string>(
                        type: "text",
                        maxLength: 2147483647,
                        nullable: true
                    ),
                    Script = table.Column<string>(type: "text", nullable: true),
                    Style = table.Column<string>(type: "text", nullable: true),
                    IsHomePage = table.Column<bool>(type: "boolean", nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    LayoutName = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPages", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsRatings",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityType = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    EntityId = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    StarCount = table.Column<short>(type: "smallint", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRatings", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsTags",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityType = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    Name = table.Column<string>(
                        type: "character varying(32)",
                        maxLength: 32,
                        nullable: false
                    ),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsTags", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsUserMarkedItems",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    EntityId = table.Column<string>(type: "text", nullable: false),
                    EntityType = table.Column<string>(type: "text", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserMarkedItems", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsUserReactions",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityType = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    EntityId = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    ReactionName = table.Column<string>(
                        type: "character varying(32)",
                        maxLength: 32,
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserReactions", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsUsers",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    Email = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    Name = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: true
                    ),
                    Surname = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: true
                    ),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EmailConfirmed = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    PhoneNumber = table.Column<string>(
                        type: "character varying(16)",
                        maxLength: 16,
                        nullable: true
                    ),
                    PhoneNumberConfirmed = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUsers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AbpBlobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    Content = table.Column<byte[]>(
                        type: "bytea",
                        maxLength: 2147483647,
                        nullable: true
                    ),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBlobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpBlobs_AbpBlobContainers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "AbpBlobContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "CmsBlogPosts",
                schema: "cmskit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlogId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(
                        type: "character varying(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    Slug = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    ShortDescription = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    Content = table.Column<string>(
                        type: "text",
                        maxLength: 2147483647,
                        nullable: true
                    ),
                    CoverImageMediaId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: false
                    ),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: false
                    ),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(
                        type: "timestamp without time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsBlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsBlogPosts_CmsUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "cmskit",
                        principalTable: "CmsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_AbpBlobContainers_TenantId_Name",
                table: "AbpBlobContainers",
                columns: new[] { "TenantId", "Name" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_AbpBlobs_ContainerId",
                table: "AbpBlobs",
                column: "ContainerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AbpBlobs_TenantId_ContainerId_Name",
                table: "AbpBlobs",
                columns: new[] { "TenantId", "ContainerId", "Name" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsBlogPosts_AuthorId",
                schema: "cmskit",
                table: "CmsBlogPosts",
                column: "AuthorId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsBlogPosts_Slug_BlogId",
                schema: "cmskit",
                table: "CmsBlogPosts",
                columns: new[] { "Slug", "BlogId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsComments_TenantId_EntityType_EntityId",
                schema: "cmskit",
                table: "CmsComments",
                columns: new[] { "TenantId", "EntityType", "EntityId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsComments_TenantId_RepliedCommentId",
                schema: "cmskit",
                table: "CmsComments",
                columns: new[] { "TenantId", "RepliedCommentId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntityTags_TenantId_EntityId_TagId",
                schema: "cmskit",
                table: "CmsEntityTags",
                columns: new[] { "TenantId", "EntityId", "TagId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsPages_TenantId_Slug",
                schema: "cmskit",
                table: "CmsPages",
                columns: new[] { "TenantId", "Slug" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsRatings_TenantId_EntityType_EntityId_CreatorId",
                schema: "cmskit",
                table: "CmsRatings",
                columns: new[] { "TenantId", "EntityType", "EntityId", "CreatorId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsTags_TenantId_Name",
                schema: "cmskit",
                table: "CmsTags",
                columns: new[] { "TenantId", "Name" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserMarkedItems_TenantId_CreatorId_EntityType_EntityId",
                schema: "cmskit",
                table: "CmsUserMarkedItems",
                columns: new[] { "TenantId", "CreatorId", "EntityType", "EntityId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserMarkedItems_TenantId_EntityType_EntityId",
                schema: "cmskit",
                table: "CmsUserMarkedItems",
                columns: new[] { "TenantId", "EntityType", "EntityId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserReactions_TenantId_CreatorId_EntityType_EntityId_Rea~",
                schema: "cmskit",
                table: "CmsUserReactions",
                columns: new[] { "TenantId", "CreatorId", "EntityType", "EntityId", "ReactionName" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserReactions_TenantId_EntityType_EntityId_ReactionName",
                schema: "cmskit",
                table: "CmsUserReactions",
                columns: new[] { "TenantId", "EntityType", "EntityId", "ReactionName" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsUsers_TenantId_Email",
                schema: "cmskit",
                table: "CmsUsers",
                columns: new[] { "TenantId", "Email" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CmsUsers_TenantId_UserName",
                schema: "cmskit",
                table: "CmsUsers",
                columns: new[] { "TenantId", "UserName" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AbpBlobs");

            migrationBuilder.DropTable(name: "CmsBlogFeatures", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsBlogPosts", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsBlogs", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsComments", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsEntityTags", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsGlobalResources", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsMediaDescriptors", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsMenuItems", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsPages", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsRatings", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsTags", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsUserMarkedItems", schema: "cmskit");

            migrationBuilder.DropTable(name: "CmsUserReactions", schema: "cmskit");

            migrationBuilder.DropTable(name: "AbpBlobContainers");

            migrationBuilder.DropTable(name: "CmsUsers", schema: "cmskit");
        }
    }
}
