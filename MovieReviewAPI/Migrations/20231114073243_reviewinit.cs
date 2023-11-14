using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class reviewinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommended = table.Column<bool>(type: "bit", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieListModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieListModelId",
                        column: x => x.MovieListModelId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieListModelId",
                table: "Reviews",
                column: "MovieListModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
