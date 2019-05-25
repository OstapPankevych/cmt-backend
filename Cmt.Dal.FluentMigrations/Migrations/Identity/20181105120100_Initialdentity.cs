using Cmt.FluentMigrations.Infrastructure;
using FluentMigrator;

namespace Cmt.Migrations.Migrations.Identity
{
    [Tags(Db.Identity)]
    [Migration(20181105120100)]
    public class InitialIdentity : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetUsers")
                .WithColumn("Id").AsInt32().Identity().NotNullable()
                    .PrimaryKey("PK_AspNetUsers")
                .WithColumn("UserName").AsString(256).NotNullable()
                .WithColumn("NormalizedUserName").AsString(256).NotNullable()
                    .Indexed("UserNameIndex")
                .WithColumn("Email").AsString(256).NotNullable()
                .WithColumn("NormalizedEmail").AsString(256).NotNullable()
                    .Indexed("UserEmailIndex")
                .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("PasswordHash").AsString(int.MaxValue).NotNullable()
                .WithColumn("SecurityStamp").AsString(int.MaxValue).Nullable()
                .WithColumn("ConcurrencyStamp").AsString(int.MaxValue).Nullable()
                .WithColumn("PhoneNumber").AsString(int.MaxValue).Nullable()
                .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
                .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
                .WithColumn("LockoutEnd").AsDateTime().Nullable()
                .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
                .WithColumn("AccessFailedCount").AsInt32().NotNullable();

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsInt32().Identity().NotNullable()
                    .PrimaryKey("PK_AspNetRoles")
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("NormalizedName").AsString(256).Unique().NotNullable()
                .WithColumn("ConcurrencyStamp").AsString(int.MaxValue).Nullable();

            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsInt32().Identity()
                    .PrimaryKey("PK_AspNetUserClaims")
                .WithColumn("UserId").AsInt32().NotNullable()
                    .Indexed("IX_AspNetUserClaims_UserId")
                    .ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUsers", "Id")
                        .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("ClaimType").AsString(int.MaxValue).NotNullable()
                .WithColumn("ClaimValue").AsString(int.MaxValue).NotNullable();

            Create.Table("AspNetUserLogins")
                .WithColumn("LoginProvider").AsString(450).NotNullable()
                    .PrimaryKey("PK_AspNetUserLogins")
                .WithColumn("ProviderKey").AsString(450).NotNullable()
                    .PrimaryKey("PK_AspNetUserLogins")
                .WithColumn("ProviderDisplayName").AsString(int.MaxValue).Nullable()
                .WithColumn("UserId").AsInt32().NotNullable()
                    .Indexed("IX_AspNetUserLogins_UserId")
                    .ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUsers", "Id")
                        .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                    .PrimaryKey("PK_AspNetUserRoles")
                    .ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUsers", "Id")
                        .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("RoleId").AsInt32()
                    .PrimaryKey("PK_AspNetUserRoles")
                    .Indexed("IX_AspNetUserRoles_RoleId")
                    .ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetRoles", "Id")
                        .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.Table("AspNetUserTokens")
                .WithColumn("UserId").AsInt32().NotNullable()
                    .PrimaryKey("PK_AspNetUserTokens")
                    .ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", "AspNetUsers", "Id")
                        .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("LoginProvider").AsString(450).NotNullable()
                    .PrimaryKey("PK_AspNetUserTokens")
                .WithColumn("Name").AsString(450).NotNullable()
                    .PrimaryKey("PK_AspNetUserTokens")
                .WithColumn("Value").AsString(int.MaxValue).NotNullable();

            Create.Table("AspNetRoleClaims")
                .WithColumn("Id").AsInt32().Identity().NotNullable()
                    .PrimaryKey("PK_AspNetRoleClaims")
                .WithColumn("RoleId").AsInt32().NotNullable()
                    .Indexed("IX_AspNetRoleClaims_RoleId")
                    .ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", "AspNetRoles", "Id")
                        .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("ClaimType").AsString(int.MaxValue).NotNullable()
                .WithColumn("ClaimValue").AsString(int.MaxValue).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AspNetUsers");
            Delete.Table("AspNetRoles");
            Delete.Table("AspNetUserClaims");
            Delete.Table("AspNetUserLogins");
            Delete.Table("AspNetUserRoles"); 
            Delete.Table("AspNetUserTokens");
            Delete.Table("AspNetRoleClaims");
        }

    }
}
