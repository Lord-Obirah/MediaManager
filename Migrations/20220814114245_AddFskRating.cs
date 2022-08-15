using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaManager.Migrations
{
    public partial class AddFskRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FskRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FskRatings", x => x.Id);
                });

            var fsk18 = Guid.NewGuid();
            migrationBuilder.InsertData(
                table: "FskRatings",
                columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    { Guid.NewGuid(), "FSK 0" },
                    { Guid.NewGuid(), "FSK 6" },
                    { Guid.NewGuid(), "FSK 12" },
                    { Guid.NewGuid(), "FSK 16" },
                    { fsk18, "FSK 18" },
                });

            migrationBuilder.AddColumn<Guid>(
                name: "FskRatingId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: fsk18);

            migrationBuilder.AddColumn<Guid>(
                name: "FskRatingId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: fsk18);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FskRatingId",
                table: "Movies",
                column: "FskRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_FskRatingId",
                table: "Games",
                column: "FskRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_FskRating_FskRatingId",
                table: "Games",
                column: "FskRatingId",
                principalTable: "FskRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_FskRating_FskRatingId",
                table: "Movies",
                column: "FskRatingId",
                principalTable: "FskRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}