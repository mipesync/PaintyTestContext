using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaintyTestContext.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "UrlList",
                table: "Users",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "FriendsIdList",
                table: "Users",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string[]>(
                name: "UrlList",
                table: "Users",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AlterColumn<Guid[]>(
                name: "FriendsIdList",
                table: "Users",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");
        }
    }
}
