using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaisonTelecom.Migrations
{
    /// <inheritdoc />
    public partial class initialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RAM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    BasicSpecs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialFeatures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTrending = table.Column<bool>(type: "bit", nullable: false),
                    IsLatest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
