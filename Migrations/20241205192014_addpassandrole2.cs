using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    public partial class addpassandrole2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3971116b-3c39-4e25-b620-672712fc6911", "AQAAAAEAACcQAAAAEEanrN5kXdc2R+lpxAVipBvEGJGVlEmSAUNQVh0u0r9b1zcgPo+q8mqVEGiC+qETpQ==", "927ae43f-44c3-4535-9daf-ff1f968ea5e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f16c78a-39aa-453c-9530-6a4745aac2ae", "AQAAAAEAACcQAAAAEIsem/LuvngBNST15OnAKQONxTKi1IQtaFqkQHsf2lyBf9v8mGLERzR4bdCd6WRw6g==", "1169400f-a152-4e1a-b80c-2f4959def464" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6ea045e-5292-42ef-adc5-b1fa9dfedad6", null, "c5ea73ff-4eb7-4802-a0e7-672875a398f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e20ea5b2-391f-4f68-b30e-9384f851e927", null, "62f12398-c695-4d53-985c-1626286452f9" });
        }
    }
}
