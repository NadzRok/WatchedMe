using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingColumnsUserInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "UsersMovieInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UsersMovieInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifideDate",
                table: "UsersMovieInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UsersMovieInfo");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "UsersMovieInfo");

            migrationBuilder.DropColumn(
                name: "ModifideDate",
                table: "UsersMovieInfo");
        }
    }
}
