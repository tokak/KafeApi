using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KafeApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatereview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CafeInfos_CafeInfoId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CafeInfoId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CafeInfoId",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CafeInfoId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CafeInfoId",
                table: "Reviews",
                column: "CafeInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CafeInfos_CafeInfoId",
                table: "Reviews",
                column: "CafeInfoId",
                principalTable: "CafeInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
