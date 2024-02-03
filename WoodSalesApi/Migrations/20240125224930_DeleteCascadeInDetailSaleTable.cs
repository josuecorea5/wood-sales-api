using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WoodSalesApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeInDetailSaleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale");

            migrationBuilder.DropForeignKey(
                name: "FK__sale_deta__id_sa__5BE2A6F2",
                table: "sale_detail");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "029533af-2c09-44a3-bae7-78b52ff6b5cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57b3301a-4d1d-4ff5-b42a-639bbda129b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1714c259-14a3-48bd-91d6-f69fad4eb4b3", null, "User", null },
                    { "98c36b00-c985-4e47-9aa8-5a24496b6d91", null, "Admin", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale",
                column: "id_client",
                principalTable: "client",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sale_deta__id_sa__5BE2A6F2",
                table: "sale_detail",
                column: "id_sale",
                principalTable: "sale",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale");

            migrationBuilder.DropForeignKey(
                name: "FK__sale_deta__id_sa__5BE2A6F2",
                table: "sale_detail");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1714c259-14a3-48bd-91d6-f69fad4eb4b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98c36b00-c985-4e47-9aa8-5a24496b6d91");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "029533af-2c09-44a3-bae7-78b52ff6b5cf", null, "User", null },
                    { "57b3301a-4d1d-4ff5-b42a-639bbda129b4", null, "Admin", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale",
                column: "id_client",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__sale_deta__id_sa__5BE2A6F2",
                table: "sale_detail",
                column: "id_sale",
                principalTable: "sale",
                principalColumn: "id");
        }
    }
}
