using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Persistency.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    ApiKey = table.Column<string>(nullable: true),
                    ApiSecret = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movies_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    MovieRating = table.Column<float>(nullable: true),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name", "Role" },
                values: new object[] { 1, "e2216fc2-a605-4994-a25e-982048f97969", "48279e3a-05e9-42d3-9eb1-211f2c968905", "Admin1", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name", "Role" },
                values: new object[] { 2, "ce300cae-bc10-40fb-988a-4b819abef9e2", "f731267b-c6de-4772-bfda-74ee4848dd8b", "Admin2", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name", "Role" },
                values: new object[] { 3, "62058c84-7f6a-4a07-b2c6-b1b0c2f1961a", "82eaa805-2060-40fe-a0e5-c606733383bc", "User1", "user" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name", "Role" },
                values: new object[] { 4, "5402738c-04d7-47cd-996d-97f6c392ad74", "c3ce5efc-34a9-47d8-9ef7-db721381d26f", "User2", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserID",
                table: "Movies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieID",
                table: "Ratings",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieID",
                table: "Reviews",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
