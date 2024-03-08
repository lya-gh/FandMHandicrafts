using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace FandMHandicrafts.Data.Migrations
{

    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID = "8de004c7-685f-4d3d-b8c6-e7200aa1ee8d";
        const string ADMIN_ROLE_GUID = "65aa8213-21b2-49e2-a17f-cef7944d75c3";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var passwordHash = hasher.HashPassword(null, "Password123!");
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled, LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp,FirstName,LastName,Address1,Address2,PostCode)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}'");
            sb.AppendLine(",'admin@famhc.co.nz'");
            sb.AppendLine(",'ADMIN@FAMHC.CO.NZ'");
            sb.AppendLine(",'admin@famhc.co.nz'");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",'ADMIN@FAMHC.CO.NZ'");
            sb.AppendLine($",'{passwordHash}'");
            sb.AppendLine(",''");
            sb.AppendLine(",'Admin'");
            sb.AppendLine(",'Admin'");
            sb.AppendLine(",'Address1'");
            sb.AppendLine(",'Address2'");
            sb.AppendLine(",'1234'");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());
            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}','Admin','ADMIN')");
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId='{ADMIN_USER_GUID}' AND RoleId='{ADMIN_ROLE_GUID}'");
            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id='{ADMIN_USER_GUID}' ");
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id='{ADMIN_ROLE_GUID}'");
           
        }
    }
}
