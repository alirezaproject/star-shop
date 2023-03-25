using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_EditCategoryTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryTypeId",
                table: "CategoryTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTypes_ParentCategoryTypeId",
                table: "CategoryTypes",
                column: "ParentCategoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTypes_CategoryTypes_ParentCategoryTypeId",
                table: "CategoryTypes",
                column: "ParentCategoryTypeId",
                principalTable: "CategoryTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTypes_CategoryTypes_ParentCategoryTypeId",
                table: "CategoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTypes_ParentCategoryTypeId",
                table: "CategoryTypes");

            migrationBuilder.DropColumn(
                name: "ParentCategoryTypeId",
                table: "CategoryTypes");
        }
    }
}
