using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogCore_ASPNetMVC_Net8.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string storedProcedure = @"CREATE PROCEDURE [dbo].[spGetCategories]
                                        AS
                                        BEGIN
                                            SET NOCOUNT ON;
                                            SELECT * FROM Category
                                        END";
            migrationBuilder.Sql(storedProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string storedProcedure = @"DROP PROCEDURE [dbo].[spGetCategories]
                                        AS
                                        BEGIN
                                            SET NOCOUNT ON;
                                            SELECT * FROM Category
                                        END";
            migrationBuilder.Sql(storedProcedure);
        }
    }
}
