using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class GetPaymentRequestSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = ScriptReader.GetScript("GetPaymentRequest.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE GetPaymentRequest";
            migrationBuilder.Sql(sql);

        }
    }
}
