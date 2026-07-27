using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    /// <inheritdoc />
    public partial class Upgraded_To_Abp_10_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionTime",
                table: "AbpBackgroundJobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpBackgroundJobs_ApplicationName_CompletionTime_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                columns: new[] { "ApplicationName", "CompletionTime", "IsAbandoned", "NextTryTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpBackgroundJobs_ApplicationName_CompletionTime_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs");

            migrationBuilder.DropColumn(
                name: "CompletionTime",
                table: "AbpBackgroundJobs");

            migrationBuilder.CreateIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });
        }
    }
}
