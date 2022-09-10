using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webApi.Migrations
{
    /// <inheritdoc />
    public partial class Create_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_walletRials",
                table: "walletRials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_walletDollers",
                table: "walletDollers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_phoneBooks",
                table: "phoneBooks");

            migrationBuilder.RenameTable(
                name: "walletRials",
                newName: "WalletRials");

            migrationBuilder.RenameTable(
                name: "walletDollers",
                newName: "WalletDollers");

            migrationBuilder.RenameTable(
                name: "phoneBooks",
                newName: "PhoneBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletRials",
                table: "WalletRials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletDollers",
                table: "WalletDollers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneBooks",
                table: "PhoneBooks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletRials",
                table: "WalletRials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletDollers",
                table: "WalletDollers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneBooks",
                table: "PhoneBooks");

            migrationBuilder.RenameTable(
                name: "WalletRials",
                newName: "walletRials");

            migrationBuilder.RenameTable(
                name: "WalletDollers",
                newName: "walletDollers");

            migrationBuilder.RenameTable(
                name: "PhoneBooks",
                newName: "phoneBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_walletRials",
                table: "walletRials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_walletDollers",
                table: "walletDollers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_phoneBooks",
                table: "phoneBooks",
                column: "Id");
        }
    }
}
