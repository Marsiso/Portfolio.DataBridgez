using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DataBridgez.IdentityProvider.Persistence.Migrations.Identity
{
    /// <inheritdoc />
    public partial class IdentityModelUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_logins_users_id",
                schema: "dbo",
                table: "user_logins");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "dbo",
                table: "user_logins",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_logins_id",
                schema: "dbo",
                table: "user_logins",
                newName: "IX_user_logins_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_logins_users_user_id",
                schema: "dbo",
                table: "user_logins",
                column: "user_id",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_logins_users_user_id",
                schema: "dbo",
                table: "user_logins");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "dbo",
                table: "user_logins",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_user_logins_user_id",
                schema: "dbo",
                table: "user_logins",
                newName: "IX_user_logins_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_logins_users_id",
                schema: "dbo",
                table: "user_logins",
                column: "id",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
