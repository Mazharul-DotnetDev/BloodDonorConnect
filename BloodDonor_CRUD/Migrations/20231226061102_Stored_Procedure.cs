using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonor_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class Stored_Procedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string sqlCode = @"create proc spDeleteDonor
                  @donorid int

                   as

                    delete from Donations where DonorId = @donorid
                    delete from Donors where DonorId = @donorid";

            migrationBuilder.Sql(sqlCode);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop proc spDeleteDonor");
        }
    }
}
