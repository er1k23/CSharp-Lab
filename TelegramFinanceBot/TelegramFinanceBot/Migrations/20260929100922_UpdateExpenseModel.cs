using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramFinanceBot.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Expenses",
                newName: "ChatId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expenses",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Expenses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpentAt",
                table: "Expenses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "SpentAt",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Expenses",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Expenses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Expenses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
