﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Caiyuan.DB.Access.Migrations
{
    public partial class no4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WxOpenID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OpenUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenUsers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    OpenID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenUsers", x => x.ID);
                });

            migrationBuilder.AddColumn<string>(
                name: "WxOpenID",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
