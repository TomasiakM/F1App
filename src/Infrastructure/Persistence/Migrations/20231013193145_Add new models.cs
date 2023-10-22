using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addnewmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralClassifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralClassifications_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceWeeks_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceWeeks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverClassifications",
                columns: table => new
                {
                    GeneralClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverClassifications", x => new { x.GeneralClassificationId, x.Id });
                    table.ForeignKey(
                        name: "FK_DriverClassifications_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverClassifications_GeneralClassifications_GeneralClassificationId",
                        column: x => x.GeneralClassificationId,
                        principalTable: "GeneralClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamClassifications",
                columns: table => new
                {
                    GeneralClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamClassifications", x => new { x.GeneralClassificationId, x.Id });
                    table.ForeignKey(
                        name: "FK_TeamClassifications_GeneralClassifications_GeneralClassificationId",
                        column: x => x.GeneralClassificationId,
                        principalTable: "GeneralClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamClassifications_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FP1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FP1_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FP2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FP2_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FP3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FP3_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceQualifications_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SprintQualifications_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RaceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_RaceWeeks_RaceWeekId",
                        column: x => x.RaceWeekId,
                        principalTable: "RaceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FP1Results",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    FastestLap = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP1Results", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_FP1Results_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FP1Results_FP1_SessionId",
                        column: x => x.SessionId,
                        principalTable: "FP1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FP1Results_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FP2Results",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    FastestLap = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP2Results", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_FP2Results_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FP2Results_FP2_SessionId",
                        column: x => x.SessionId,
                        principalTable: "FP2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FP2Results_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FP3Results",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    FastestLap = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP3Results", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_FP3Results_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FP3Results_FP3_SessionId",
                        column: x => x.SessionId,
                        principalTable: "FP3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FP3Results_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceQualificationsResults",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Q1Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Q2Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Q3Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceQualificationsResults", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_RaceQualificationsResults_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceQualificationsResults_RaceQualifications_SessionId",
                        column: x => x.SessionId,
                        principalTable: "RaceQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceQualificationsResults_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceResults",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPosition = table.Column<int>(type: "int", nullable: false),
                    FinishTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    FinishType = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    FastestLap = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceResults", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_RaceResults_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceResults_Races_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceResults_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintQualificationResults",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Q1Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Q2Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Q3Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintQualificationResults", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_SprintQualificationResults_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintQualificationResults_SprintQualifications_SessionId",
                        column: x => x.SessionId,
                        principalTable: "SprintQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintQualificationResults_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintResults",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPosition = table.Column<int>(type: "int", nullable: false),
                    FinishTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    FinishType = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    FastestLap = table.Column<TimeSpan>(type: "time", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintResults", x => new { x.SessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_SprintResults_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintResults_Sprints_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintResults_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverClassifications_DriverId",
                table: "DriverClassifications",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Slug",
                table: "Drivers",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FP1_RaceWeekId",
                table: "FP1",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FP1Results_DriverId",
                table: "FP1Results",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FP1Results_TeamId",
                table: "FP1Results",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FP2_RaceWeekId",
                table: "FP2",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FP2Results_DriverId",
                table: "FP2Results",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FP2Results_TeamId",
                table: "FP2Results",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FP3_RaceWeekId",
                table: "FP3",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FP3Results_DriverId",
                table: "FP3Results",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FP3Results_TeamId",
                table: "FP3Results",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralClassifications_SeasonId",
                table: "GeneralClassifications",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceQualifications_RaceWeekId",
                table: "RaceQualifications",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaceQualificationsResults_DriverId",
                table: "RaceQualificationsResults",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceQualificationsResults_TeamId",
                table: "RaceQualificationsResults",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceResults_DriverId",
                table: "RaceResults",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceResults_TeamId",
                table: "RaceResults",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_RaceWeekId",
                table: "Races",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaceWeeks_SeasonId",
                table: "RaceWeeks",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceWeeks_Slug_SeasonId",
                table: "RaceWeeks",
                columns: new[] { "Slug", "SeasonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaceWeeks_TrackId_SeasonId",
                table: "RaceWeeks",
                columns: new[] { "TrackId", "SeasonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_Year",
                table: "Seasons",
                column: "Year",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SprintQualificationResults_DriverId",
                table: "SprintQualificationResults",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintQualificationResults_TeamId",
                table: "SprintQualificationResults",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintQualifications_RaceWeekId",
                table: "SprintQualifications",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SprintResults_DriverId",
                table: "SprintResults",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintResults_TeamId",
                table: "SprintResults",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_RaceWeekId",
                table: "Sprints",
                column: "RaceWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamClassifications_TeamId",
                table: "TeamClassifications",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Slug",
                table: "Teams",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_Slug",
                table: "Tracks",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverClassifications");

            migrationBuilder.DropTable(
                name: "FP1Results");

            migrationBuilder.DropTable(
                name: "FP2Results");

            migrationBuilder.DropTable(
                name: "FP3Results");

            migrationBuilder.DropTable(
                name: "RaceQualificationsResults");

            migrationBuilder.DropTable(
                name: "RaceResults");

            migrationBuilder.DropTable(
                name: "SprintQualificationResults");

            migrationBuilder.DropTable(
                name: "SprintResults");

            migrationBuilder.DropTable(
                name: "TeamClassifications");

            migrationBuilder.DropTable(
                name: "FP1");

            migrationBuilder.DropTable(
                name: "FP2");

            migrationBuilder.DropTable(
                name: "FP3");

            migrationBuilder.DropTable(
                name: "RaceQualifications");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "SprintQualifications");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "GeneralClassifications");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "RaceWeeks");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
