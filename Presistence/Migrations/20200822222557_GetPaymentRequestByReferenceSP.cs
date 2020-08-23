using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class GetPaymentRequestByReferenceSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = ScriptReader.GetScript("GetPaymentRequestByReference.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE GetPaymentRequestByReference";
            migrationBuilder.Sql(sql);

        }
    }
}
