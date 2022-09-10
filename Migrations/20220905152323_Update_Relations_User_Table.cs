using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webApi.Migrations
{
    /// <inheritdoc />
    public partial class Update_Relations_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneBookId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletDollerId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletRialId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneBookId",
                table: "Users",
                column: "PhoneBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WalletDollerId",
                table: "Users",
                column: "WalletDollerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WalletRialId",
                table: "Users",
                column: "WalletRialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PhoneBooks_PhoneBookId",
                table: "Users",
                column: "PhoneBookId",
                principalTable: "PhoneBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WalletDollers_WalletDollerId",
                table: "Users",
                column: "WalletDollerId",
                principalTable: "WalletDollers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WalletRials_WalletRialId",
                table: "Users",
                column: "WalletRialId",
                principalTable: "WalletRials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PhoneBooks_PhoneBookId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_WalletDollers_WalletDollerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_WalletRials_WalletRialId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneBookId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WalletDollerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WalletRialId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneBookId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletDollerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletRialId",
                table: "Users");
        }
    }
}
