using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class customeridIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = ScriptReader.GetScript("CreateCustomerIdIndex.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP INDEX IF EXISTS idxCustormerId ON PaymentRequests";
            migrationBuilder.Sql(sql);

        }
    }
}
