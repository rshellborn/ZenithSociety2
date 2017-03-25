using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZenithWebsite.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetUsers",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetRoles",
                nullable: false);
        }
    }
}
