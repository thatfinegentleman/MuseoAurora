using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuseoAurora.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTicketConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tickets_exhibitions_exhibition_id",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_tickets_guided_tours_guided_tour_id",
                table: "tickets");

            migrationBuilder.AlterColumn<int>(
                name: "guided_tour_id",
                table: "tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "exhibition_id",
                table: "tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_tickets_exhibitions_exhibition_id",
                table: "tickets",
                column: "exhibition_id",
                principalTable: "exhibitions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_tickets_guided_tours_guided_tour_id",
                table: "tickets",
                column: "guided_tour_id",
                principalTable: "guided_tours",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tickets_exhibitions_exhibition_id",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_tickets_guided_tours_guided_tour_id",
                table: "tickets");

            migrationBuilder.AlterColumn<int>(
                name: "guided_tour_id",
                table: "tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "exhibition_id",
                table: "tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_tickets_exhibitions_exhibition_id",
                table: "tickets",
                column: "exhibition_id",
                principalTable: "exhibitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tickets_guided_tours_guided_tour_id",
                table: "tickets",
                column: "guided_tour_id",
                principalTable: "guided_tours",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
