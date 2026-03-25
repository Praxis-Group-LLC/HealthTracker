using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScriptureModeEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_seen_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "coping_exercises",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coping_exercises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "device_credentials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token_hash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    revoked_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_credentials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "journal_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    MoodRating = table.Column<int>(type: "integer", nullable: false),
                    SleepHours = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Text = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journal_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coping_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CopingExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coping_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_coping_sessions_coping_exercises_CopingExerciseId",
                        column: x => x.CopingExerciseId,
                        principalTable: "coping_exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "coping_exercises",
                columns: new[] { "id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "BOX_BREATHING", "Inhale 4, hold 4, exhale 4, hold 4.", "Box Breathing" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "GROUNDING_5_4_3_2_1", "Notice 5 things you see, 4 you feel, 3 you hear, 2 you smell, 1 you taste.", "5-4-3-2-1 Grounding" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_coping_exercises_Code",
                table: "coping_exercises",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coping_sessions_CopingExerciseId",
                table: "coping_sessions",
                column: "CopingExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_coping_sessions_UserId_StartedAt",
                table: "coping_sessions",
                columns: new[] { "UserId", "StartedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_device_credentials_token_hash",
                table: "device_credentials",
                column: "token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_journal_entries_UserId_Date",
                table: "journal_entries",
                columns: new[] { "UserId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_users");

            migrationBuilder.DropTable(
                name: "coping_sessions");

            migrationBuilder.DropTable(
                name: "device_credentials");

            migrationBuilder.DropTable(
                name: "journal_entries");

            migrationBuilder.DropTable(
                name: "coping_exercises");
        }
    }
}
