using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace after_sales.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Clients_ClientId1",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "CustomId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId1",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Clients_ClientId1",
                table: "Articles",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Clients_ClientId1",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CustomId",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId1",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Clients_ClientId1",
                table: "Articles",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
