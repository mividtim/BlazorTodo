using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorTodoService.Migrations
{
    public partial class AddIdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("09a77428-8057-4068-8824-f419fb09331d"), "70aa7c1c-e9ee-4ef4-8ae5-ef8cb0f7954d", "Visitor", "VISITOR" },
                    { new Guid("56a191e4-d637-460e-bd1a-538655c600b2"), "a57e3fbe-4c21-48df-92c4-6d24c1db66d8", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("09a77428-8057-4068-8824-f419fb09331d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("56a191e4-d637-460e-bd1a-538655c600b2"));
        }
    }
}
