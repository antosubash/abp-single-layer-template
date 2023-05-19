using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbpTemplate.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantHost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "AbpTenants",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "AbpTenants");
        }
    }
}
