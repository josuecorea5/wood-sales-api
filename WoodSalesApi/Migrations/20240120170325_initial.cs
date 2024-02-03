using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodSalesApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__client__3213E83F5D61896A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product__3213E83F7B03D659", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sale",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_sale = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_client = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sale__3213E83F76A51D31", x => x.id);
                    table.ForeignKey(
                        name: "FK__sale__id_client__4D94879B",
                        column: x => x.id_client,
                        principalTable: "client",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sale_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_sale = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    id_product = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sale_det__3213E83F97D7E904", x => x.id);
                    table.ForeignKey(
                        name: "FK__sale_deta__id_pr__5070F446",
                        column: x => x.id_product,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__sale_deta__id_sa__5BE2A6F2",
                        column: x => x.id_sale,
                        principalTable: "sale",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_sale_id_client",
                table: "sale",
                column: "id_client");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_id_product",
                table: "sale_detail",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_id_sale",
                table: "sale_detail",
                column: "id_sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sale_detail");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "sale");

            migrationBuilder.DropTable(
                name: "client");
        }
    }
}
