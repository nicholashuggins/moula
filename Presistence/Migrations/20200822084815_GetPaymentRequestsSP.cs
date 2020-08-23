using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class GetPaymentRequestsSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = ScriptReader.GetScript("GetPaymentRequests.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE GetPaymentRequests";
            migrationBuilder.Sql(sql);

        }
    }
}
