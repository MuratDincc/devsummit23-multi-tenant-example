using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tenant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "pool",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    host = table.Column<string>(type: "text", nullable: false),
                    port = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pool", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pool_database",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pool_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pool_database", x => x.id);
                    table.ForeignKey(
                        name: "fk_pool_database_pool_pool_id",
                        column: x => x.pool_id,
                        principalSchema: "public",
                        principalTable: "pool",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alias_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pool_database_id = table.Column<int>(type: "integer", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenant_pool_database_pool_database_id",
                        column: x => x.pool_database_id,
                        principalSchema: "public",
                        principalTable: "pool_database",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenant_user_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "public",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_tenant_user_tenant_user_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "tenant_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "pool",
                columns: new[] { "id", "created_on_utc", "deleted", "host", "password", "port", "title", "updated_on_utc", "username" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2023, 10, 1, 13, 39, 11, 881, DateTimeKind.Unspecified).AddTicks(6260), new TimeSpan(0, 0, 0, 0, 0)), false, "localhost", "postgres", "5438", "Local Pool", null, "postgres" });

            migrationBuilder.CreateIndex(
                name: "ix_pool_database_pool_id",
                schema: "public",
                table: "pool_database",
                column: "pool_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_pool_database_id",
                schema: "public",
                table: "tenant",
                column: "pool_database_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_user_tenant_id",
                schema: "public",
                table: "tenant_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_user_user_id",
                schema: "public",
                table: "tenant_user",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tenant_user_user_user_id",
                schema: "public",
                table: "tenant_user",
                column: "user_id",
                principalSchema: "public",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pool_database_pool_pool_id",
                schema: "public",
                table: "pool_database");

            migrationBuilder.DropForeignKey(
                name: "fk_tenant_pool_database_pool_database_id",
                schema: "public",
                table: "tenant");

            migrationBuilder.DropForeignKey(
                name: "fk_tenant_user_tenant_tenant_id",
                schema: "public",
                table: "tenant_user");

            migrationBuilder.DropForeignKey(
                name: "fk_tenant_user_user_user_id",
                schema: "public",
                table: "tenant_user");

            migrationBuilder.DropTable(
                name: "pool",
                schema: "public");

            migrationBuilder.DropTable(
                name: "pool_database",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tenant",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tenant_user",
                schema: "public");
        }
    }
}
