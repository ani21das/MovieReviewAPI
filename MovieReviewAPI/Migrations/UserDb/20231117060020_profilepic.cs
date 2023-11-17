using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieReviewAPI.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class profilepic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6706cb5f-4e85-4652-be2a-cd84e6e059b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79877f7-51aa-4bea-8587-7bacb8d30af7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d70e474-9a73-4cde-a14a-36fd75158abc", "1", "Admin", "Admin" },
                    { "1faf1ca0-a174-46f3-b768-66ab3a30a379", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d70e474-9a73-4cde-a14a-36fd75158abc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1faf1ca0-a174-46f3-b768-66ab3a30a379");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6706cb5f-4e85-4652-be2a-cd84e6e059b2", "1", "Admin", "Admin" },
                    { "e79877f7-51aa-4bea-8587-7bacb8d30af7", "2", "User", "User" }
                });
        }
    }
}
