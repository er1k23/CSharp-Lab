using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobRecruitmentApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeJobOwnerNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Jobs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_OwnerId",
                table: "Jobs",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_OwnerId",
                table: "Jobs",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_OwnerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_OwnerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Jobs");
        }
    }
}
