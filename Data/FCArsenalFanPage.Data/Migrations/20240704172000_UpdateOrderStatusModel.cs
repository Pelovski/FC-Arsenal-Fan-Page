using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCArsenalFanPage.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderStatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "OrderStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderStatuses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderStatuses");
        }
    }
}
