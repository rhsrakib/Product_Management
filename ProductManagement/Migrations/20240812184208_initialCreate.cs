using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PRO");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "PRO",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalOrderQty = table.Column<int>(type: "int", nullable: false),
                    TotalOrderPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "PRO",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "PRO",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "PRO",
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "OrderNo", "Phone", "TotalOrderPrice", "TotalOrderQty" },
                values: new object[,]
                {
                    { 1, "Panthapath, Dhaka", "Md. Monir Hossen", "ORDER00001", "01800000000", 3700m, 18 },
                    { 2, "Mohammadpur, Dhaka", "Md. Rakib Hasan", "ORDER00002", "01600000000", 25m, 5 },
                    { 3, "Mirpur, Dhaka", "Md. Rokon Islam", "ORDER00003", "01700000000", 110m, 4 }
                });

            migrationBuilder.InsertData(
                schema: "PRO",
                table: "Products",
                columns: new[] { "ProductId", "CustomerId", "ProductName", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "Book", "5", 500m, 100m },
                    { 2, 1, "Pen", "10", 200m, 20m },
                    { 3, 1, "Calculator", "3", 3000m, 1000m },
                    { 4, 2, "Sharpner", "2", 10m, 5m },
                    { 5, 2, "Eraser", "3", 15m, 5m },
                    { 6, 3, "Pencil", "2", 10m, 5m },
                    { 7, 3, "Marker", "2", 100m, 50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                schema: "PRO",
                table: "Products",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "PRO");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "PRO");
        }
    }
}
