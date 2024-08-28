using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingMinutesApp.Migrations
{
    /// <inheritdoc />
    public partial class ic4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItems_MeetingItemStatuses_MeetingItemStatusId",
                table: "MeetingItems");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_MeetingTypeId",
                table: "Meetings",
                newName: "IX_Meeting_MeetingTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingItems_MeetingId",
                table: "MeetingItems",
                newName: "IX_MeetingItem_MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItems_MeetingItemStatuses_MeetingItemStatusId",
                table: "MeetingItems",
                column: "MeetingItemStatusId",
                principalTable: "MeetingItemStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItems_MeetingItemStatuses_MeetingItemStatusId",
                table: "MeetingItems");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_MeetingTypeId",
                table: "Meetings",
                newName: "IX_Meetings_MeetingTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingItem_MeetingId",
                table: "MeetingItems",
                newName: "IX_MeetingItems_MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItems_MeetingItemStatuses_MeetingItemStatusId",
                table: "MeetingItems",
                column: "MeetingItemStatusId",
                principalTable: "MeetingItemStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
