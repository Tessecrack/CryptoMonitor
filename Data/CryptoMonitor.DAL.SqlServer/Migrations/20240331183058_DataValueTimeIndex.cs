using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoMonitor.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class DataValueTimeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Values_Time",
                table: "Values",
                column: "Time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Values_Time",
                table: "Values");
        }
    }
}
