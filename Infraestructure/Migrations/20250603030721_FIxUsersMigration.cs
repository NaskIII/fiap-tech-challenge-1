using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FIxUsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "PasswordHash", "UserName" },
                values: new object[] { new Guid("8833c9af-a91a-411c-8b54-b554171287c0"), "admin@admin.com", "$2a$11$3z3pocCPE7Lof.dKnzGXzOSU6elPCOrXUzJpzQ4eH2CyMy3LRRfwy", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8833c9af-a91a-411c-8b54-b554171287c0"));
        }
    }
}
