using Microsoft.EntityFrameworkCore.Migrations;

namespace LogisticAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryAId = table.Column<int>(type: "int", nullable: false),
                    CountryBId = table.Column<int>(type: "int", nullable: false),
                    CostOfRoad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryConnections", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryCode", "CountryName" },
                values: new object[,]
                {
                    { 1, "CAN", "Canada" },
                    { 2, "USA", "United States of America" },
                    { 3, "MEX", "Mexico" },
                    { 4, "BLZ", "Belize" },
                    { 5, "GTM", "Guatemala" },
                    { 6, "SLV", "Salvador" },
                    { 7, "HND", "Honduras" },
                    { 8, "NIC", "Nicaragua" },
                    { 9, "CRI", "Costa Rica" },
                    { 10, "PAN", "Panama" }
                });

            migrationBuilder.InsertData(
                table: "CountryConnections",
                columns: new[] { "Id", "CostOfRoad", "CountryAId", "CountryBId" },
                values: new object[,]
                {
                    { 10, 1, 7, 8 },
                    { 9, 1, 7, 8 },
                    { 8, 1, 6, 7 },
                    { 7, 1, 5, 7 },
                    { 6, 1, 5, 6 },
                    { 1, 1, 1, 2 },
                    { 4, 1, 3, 5 },
                    { 3, 1, 3, 4 },
                    { 2, 1, 2, 3 },
                    { 11, 1, 8, 9 },
                    { 5, 1, 4, 5 },
                    { 12, 1, 9, 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "CountryConnections");
        }
    }
}
