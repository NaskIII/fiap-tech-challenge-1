using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FIxUsersMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8833c9af-a91a-411c-8b54-b554171287c0"),
                column: "PasswordHash",
                value: "$2a$11$zMzsiOBzimzJNGVmnkdRleV34aqRNAtSe9Ys5lKqDZwN3hJS86jzK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8833c9af-a91a-411c-8b54-b554171287c0"),
                column: "PasswordHash",
                value: "$2a$11$L0fAw.yjtQSDH4rzyyPJqOBxDIgmI/y4ropxTedMeKeS925qBmnga");
        }
    }
}
