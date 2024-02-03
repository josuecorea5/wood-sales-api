using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WoodSalesApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeInSaleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ab45d9d-46db-443d-a4d2-81c66903ddc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "811836c3-150b-4d84-9849-82da6f997e0f");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale");

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
                    { "6ab45d9d-46db-443d-a4d2-81c66903ddc5", null, "User", null },
                    { "811836c3-150b-4d84-9849-82da6f997e0f", null, "Admin", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK__sale__id_client__4D94879B",
                table: "sale",
                column: "id_client",
                principalTable: "client",
                principalColumn: "id");
        }
    }
}
