using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class wholeDbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "Reviews");
        }
    }
}
