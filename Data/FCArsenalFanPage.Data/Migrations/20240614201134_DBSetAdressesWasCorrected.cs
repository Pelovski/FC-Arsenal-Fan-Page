using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCArsenalFanPage.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBSetAdressesWasCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adress_AspNetUsers_UserId",
                table: "Adress");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_News_NewsId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adress",
                table: "Adress");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "Adress",
                newName: "Adresses");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ParentId",
                table: "Comments",
                newName: "IX_Comments_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_NewsId",
                table: "Comments",
                newName: "IX_Comments_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_IsDeleted",
                table: "Comments",
                newName: "IX_Comments_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Adress_UserId",
                table: "Adresses",
                newName: "IX_Adresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adress_IsDeleted",
                table: "Adresses",
                newName: "IX_Adresses_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_AspNetUsers_UserId",
                table: "Adresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_AspNetUsers_UserId",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Adress");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentId",
                table: "Comment",
                newName: "IX_Comment_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_NewsId",
                table: "Comment",
                newName: "IX_Comment_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_IsDeleted",
                table: "Comment",
                newName: "IX_Comment_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_UserId",
                table: "Adress",
                newName: "IX_Adress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_IsDeleted",
                table: "Adress",
                newName: "IX_Adress_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adress",
                table: "Adress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adress_AspNetUsers_UserId",
                table: "Adress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentId",
                table: "Comment",
                column: "ParentId",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_News_NewsId",
                table: "Comment",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
