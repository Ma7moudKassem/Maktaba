using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maktaba.Services.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "510a4542-195b-478d-90a6-43a77bc7d048", null, "SuperAdmin", "SUPERADMIN" },
                    { "65b0712e-49d0-4190-9151-44e840ed521a", null, "Admin", "ADMIN" },
                    { "8857304d-369c-4fa8-96e6-4db8ec2f08dc", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "510a4542-195b-478d-90a6-43a77bc7d048");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65b0712e-49d0-4190-9151-44e840ed521a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8857304d-369c-4fa8-96e6-4db8ec2f08dc");
        }
    }
}
