using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class GettingCodeFirstToWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateIndex(
            //    name: "IX_PersistedGrants_ConsumedTime",
            //    table: "PersistedGrants",
            //    column: "ConsumedTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_PersistedGrants_ConsumedTime",
            //    table: "PersistedGrants");
        }
    }
}
