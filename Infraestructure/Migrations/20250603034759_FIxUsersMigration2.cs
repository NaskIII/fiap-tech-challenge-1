using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FIxUsersMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8833c9af-a91a-411c-8b54-b554171287c0"),
                column: "PasswordHash",
                value: "$2a$11$L0fAw.yjtQSDH4rzyyPJqOBxDIgmI/y4ropxTedMeKeS925qBmnga");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8833c9af-a91a-411c-8b54-b554171287c0"),
                column: "PasswordHash",
                value: "$2a$11$3z3pocCPE7Lof.dKnzGXzOSU6elPCOrXUzJpzQ4eH2CyMy3LRRfwy");
        }
    }
}
