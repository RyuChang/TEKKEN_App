using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TekkenApp.Models.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<byte>(type: "tinyint", nullable: false),
                    code_name = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    season = table.Column<byte>(type: "tinyint", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character", x => x.id);
                    table.UniqueConstraint("AK_character_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "command",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    command = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    key = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_command", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hitType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hitType", x => x.id);
                    table.UniqueConstraint("AK_hitType_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.id);
                    table.UniqueConstraint("AK_language_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "moveType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moveType", x => x.id);
                    table.UniqueConstraint("AK_moveType_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "StateGroup",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateGroup", x => x.id);
                    table.UniqueConstraint("AK_StateGroup_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "tekkenVersion",
                columns: table => new
                {
                    version = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: true),
                    season = table.Column<byte>(type: "tinyint", nullable: false),
                    updateDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_version", x => x.version);
                });

            migrationBuilder.CreateTable(
                name: "moveSubType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    character_code = table.Column<byte>(type: "tinyint", nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moveSubType_id", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_moveSubType_code", x => x.code);
                    table.ForeignKey(
                        name: "FK_moveSubType_moveSubType",
                        column: x => x.character_code,
                        principalTable: "character",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "moveText",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    character_code = table.Column<byte>(type: "tinyint", nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moveText", x => x.id);
                    table.UniqueConstraint("AK_moveText_code", x => x.code);
                    table.ForeignKey(
                        name: "FK_moveText_character",
                        column: x => x.character_code,
                        principalTable: "character",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hitType_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hitType_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    @checked = table.Column<bool>(name: "checked", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hitType_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_hitType_name_hitType",
                        column: x => x.hitType_code,
                        principalTable: "hitType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    character_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_character_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "command_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    command_code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_command_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_command_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "moveType_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moveType_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moveType_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_moveType_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moveType_name_moveType",
                        column: x => x.moveType_code,
                        principalTable: "moveType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false),
                    StateGroup_code = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.id);
                    table.UniqueConstraint("AK_State_code", x => x.code);
                    table.ForeignKey(
                        name: "FK_State_StateGroup",
                        column: x => x.StateGroup_code,
                        principalTable: "StateGroup",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StateGroup_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateGroup_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateGroup_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_StateGroup_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StateGroup_name_StateGroup",
                        column: x => x.StateGroup_code,
                        principalTable: "StateGroup",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    character_code = table.Column<byte>(type: "tinyint", nullable: false),
                    number = table.Column<short>(type: "smallint", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    version = table.Column<decimal>(type: "decimal(4,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.id);
                    table.UniqueConstraint("AK_Move_code", x => x.code);
                    table.ForeignKey(
                        name: "FK_Move_character",
                        column: x => x.character_code,
                        principalTable: "character",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Move_version",
                        column: x => x.version,
                        principalTable: "tekkenVersion",
                        principalColumn: "version",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "moveSubType_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moveSubType_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moveSubType_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_moveSubType_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moveSubType_name_moveSubType_name",
                        column: x => x.moveSubType_code,
                        principalTable: "moveSubType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "moveText_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moveText_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moveText_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_moveText_name_language1",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moveText_name_moveText",
                        column: x => x.moveText_code,
                        principalTable: "moveText",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    @checked = table.Column<bool>(name: "checked", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_State_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_State_name_State",
                        column: x => x.state_code,
                        principalTable: "State",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Move_command",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    move_code = table.Column<int>(type: "int", nullable: false),
                    command = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move_command", x => x.id);
                    table.ForeignKey(
                        name: "FK_Move_command_Move1",
                        column: x => x.move_code,
                        principalTable: "Move",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "move_command_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Move_Command_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    @checked = table.Column<bool>(name: "checked", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_move_command_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_move_command_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_command_Move",
                        column: x => x.Move_Command_code,
                        principalTable: "Move",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "move_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Move_Code = table.Column<int>(type: "int", nullable: false),
                    moveType_code = table.Column<int>(type: "int", nullable: true),
                    moveSubType_code = table.Column<int>(type: "int", nullable: true),
                    hitCount = table.Column<byte>(type: "tinyint", nullable: false),
                    hitLevel = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    damage = table.Column<short>(type: "smallint", nullable: false),
                    startFrame = table.Column<short>(type: "smallint", nullable: false),
                    startFrame_Display = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    startType_code = table.Column<int>(type: "int", nullable: true),
                    guardFrame = table.Column<short>(type: "smallint", nullable: false),
                    guardFrame_Display = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    guardType_code = table.Column<int>(type: "int", nullable: false),
                    hitFrame = table.Column<short>(type: "smallint", nullable: false),
                    hitFrame_Display = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    hitType_code = table.Column<int>(type: "int", nullable: false),
                    counterFrame = table.Column<short>(type: "smallint", nullable: false),
                    counterFrame_Display = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    counterType_code = table.Column<int>(type: "int", nullable: false),
                    breakThrow = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    afterBreak = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    homing = table.Column<bool>(type: "bit", nullable: false),
                    powerCrush = table.Column<bool>(type: "bit", nullable: false),
                    technicallyCrouching = table.Column<bool>(type: "bit", nullable: false),
                    technicallyJumping = table.Column<bool>(type: "bit", nullable: false),
                    tailSpin = table.Column<bool>(type: "bit", nullable: false),
                    wallSplat = table.Column<bool>(type: "bit", nullable: false),
                    note = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    version = table.Column<decimal>(type: "decimal(4,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_move_data", x => x.id);
                    table.UniqueConstraint("AK_move_data_Move_Code", x => x.Move_Code);
                    table.ForeignKey(
                        name: "FK_move_data_hitType",
                        column: x => x.guardType_code,
                        principalTable: "hitType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_data_hitType1",
                        column: x => x.startType_code,
                        principalTable: "hitType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_data_hitType2",
                        column: x => x.hitType_code,
                        principalTable: "hitType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_data_hitType3",
                        column: x => x.counterType_code,
                        principalTable: "hitType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_data_Move",
                        column: x => x.Move_Code,
                        principalTable: "Move",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_move_data_moveSubType",
                        column: x => x.moveSubType_code,
                        principalTable: "moveSubType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_data_moveType",
                        column: x => x.moveType_code,
                        principalTable: "moveType",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "move_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    move_code = table.Column<int>(type: "int", nullable: false),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_move_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_move_name_language",
                        column: x => x.language_code,
                        principalTable: "language",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_move_name_Move",
                        column: x => x.move_code,
                        principalTable: "Move",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Move_Data_Name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Move_Data_Code = table.Column<int>(type: "int", nullable: false),
                    moveType_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    moveSubType_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartType_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guardtype_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hitType_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    counterType_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language_code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    @checked = table.Column<bool>(name: "checked", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move_Data_Name", x => x.id);
                    table.ForeignKey(
                        name: "FK_Move_Data_Name_move_data",
                        column: x => x.Move_Data_Code,
                        principalTable: "move_data",
                        principalColumn: "Move_Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character",
                table: "character",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_code_Unique",
                table: "character",
                column: "code_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_name",
                table: "character_name",
                columns: new[] { "character_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_name_language_code",
                table: "character_name",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "IX_command",
                table: "command",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_command_name",
                table: "command_name",
                columns: new[] { "command_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_command_name_language_code",
                table: "command_name",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "IX_hitType",
                table: "hitType",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hitType_1",
                table: "hitType",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hitType_name_hitType_code",
                table: "hitType_name",
                column: "hitType_code");

            migrationBuilder.CreateIndex(
                name: "IX_language",
                table: "language",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move",
                table: "Move",
                columns: new[] { "character_code", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_1",
                table: "Move",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_version",
                table: "Move",
                column: "version");

            migrationBuilder.CreateIndex(
                name: "IX_Move_command_1",
                table: "Move_command",
                column: "move_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_move_command",
                table: "move_command_name",
                columns: new[] { "language_code", "Move_Command_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_move_command_name_Move_Command_code",
                table: "move_command_name",
                column: "Move_Command_code");

            migrationBuilder.CreateIndex(
                name: "IX_move_data",
                table: "move_data",
                column: "Move_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_move_data_counterType_code",
                table: "move_data",
                column: "counterType_code");

            migrationBuilder.CreateIndex(
                name: "IX_move_data_guardType_code",
                table: "move_data",
                column: "guardType_code");

            migrationBuilder.CreateIndex(
                name: "IX_move_data_hitType_code",
                table: "move_data",
                column: "hitType_code");

            migrationBuilder.CreateIndex(
                name: "IX_move_data_moveSubType_code",
                table: "move_data",
                column: "moveSubType_code");

            migrationBuilder.CreateIndex(
                name: "IX_move_data_moveType_code",
                table: "move_data",
                column: "moveType_code");

            migrationBuilder.CreateIndex(
                name: "IX_move_data_startType_code",
                table: "move_data",
                column: "startType_code");

            migrationBuilder.CreateIndex(
                name: "IX_Move_Data_Name",
                table: "Move_Data_Name",
                columns: new[] { "Move_Data_Code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_move_name",
                table: "move_name",
                columns: new[] { "move_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_move_name_language_code",
                table: "move_name",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "IX_moveSubType",
                table: "moveSubType",
                columns: new[] { "character_code", "code" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_moveSubType_character_code_number",
                table: "moveSubType",
                columns: new[] { "character_code", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveSubType_code",
                table: "moveSubType",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveSubType_name",
                table: "moveSubType_name",
                columns: new[] { "moveSubType_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveSubType_name_language_code",
                table: "moveSubType_name",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "IX_moveText_character_code_number",
                table: "moveText",
                columns: new[] { "character_code", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveText_code",
                table: "moveText",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveText_name",
                table: "moveText_name",
                columns: new[] { "moveText_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveText_name_language_code",
                table: "moveText_name",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "IX_moveType_1",
                table: "moveType",
                columns: new[] { "code", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveType_code_unique",
                table: "moveType",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveType_name",
                table: "moveType_name",
                columns: new[] { "language_code", "moveType_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moveType_name_moveType_code",
                table: "moveType_name",
                column: "moveType_code");

            migrationBuilder.CreateIndex(
                name: "IX_State",
                table: "State",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_1",
                table: "State",
                columns: new[] { "StateGroup_code", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_name",
                table: "State_name",
                columns: new[] { "state_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_name_language_code",
                table: "State_name",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "IX_StateGroup",
                table: "StateGroup",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateGroup_1",
                table: "StateGroup",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateGroup_name",
                table: "StateGroup_name",
                columns: new[] { "StateGroup_code", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateGroup_name_language_code",
                table: "StateGroup_name",
                column: "language_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_name");

            migrationBuilder.DropTable(
                name: "command");

            migrationBuilder.DropTable(
                name: "command_name");

            migrationBuilder.DropTable(
                name: "hitType_name");

            migrationBuilder.DropTable(
                name: "Move_command");

            migrationBuilder.DropTable(
                name: "move_command_name");

            migrationBuilder.DropTable(
                name: "Move_Data_Name");

            migrationBuilder.DropTable(
                name: "move_name");

            migrationBuilder.DropTable(
                name: "moveSubType_name");

            migrationBuilder.DropTable(
                name: "moveText_name");

            migrationBuilder.DropTable(
                name: "moveType_name");

            migrationBuilder.DropTable(
                name: "State_name");

            migrationBuilder.DropTable(
                name: "StateGroup_name");

            migrationBuilder.DropTable(
                name: "move_data");

            migrationBuilder.DropTable(
                name: "moveText");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "hitType");

            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "moveSubType");

            migrationBuilder.DropTable(
                name: "moveType");

            migrationBuilder.DropTable(
                name: "StateGroup");

            migrationBuilder.DropTable(
                name: "tekkenVersion");

            migrationBuilder.DropTable(
                name: "character");
        }
    }
}
