using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCArsenalFanPage.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamesOfUserPropertyInNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "News");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
