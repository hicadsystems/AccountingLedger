﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NavyAccountWeb.Data;

namespace NavyAccountWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200228130652_AddLoanAppNo")]
    partial class AddLoanAppNo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("FullAddress");

                    b.Property<string>("HomeNumber");

                    b.Property<bool?>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NextofkinType");

                    b.Property<int>("PersonID");

                    b.Property<string>("PlaceOfWork");

                    b.Property<int?>("RelationshipId");

                    b.Property<DateTime?>("dateofbirth");

                    b.HasKey("Id");

                    b.HasIndex("PersonID");

                    b.HasIndex("RelationshipId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Country", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Code");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.LoanStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("loanStatus");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action");

                    b.Property<string>("Code");

                    b.Property<string>("Controller");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("MenuGroupId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("MenuGroupId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.MenuGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("MenuGroups");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Birthplace");

                    b.Property<int?>("BloodGroupid");

                    b.Property<int?>("Commandid");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("Departmentid");

                    b.Property<string>("ExitReason");

                    b.Property<int?>("ExitTypeid");

                    b.Property<string>("Factory");

                    b.Property<string>("FileName");

                    b.Property<byte[]>("FingerPrint");

                    b.Property<string>("FirstName");

                    b.Property<string>("GSMNumber");

                    b.Property<int?>("IdentificationId");

                    b.Property<string>("IdentifyNumber");

                    b.Property<DateTime?>("IdentityExpiryDate");

                    b.Property<string>("IdentityType");

                    b.Property<string>("Initials");

                    b.Property<bool?>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<bool?>("LiveStatus");

                    b.Property<int?>("LocalGovernmentareaId");

                    b.Property<string>("MaidenName");

                    b.Property<int?>("MaritalStatusId");

                    b.Property<string>("MiddleName");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NOKFileName");

                    b.Property<string>("Nationality");

                    b.Property<int?>("Payrollclass");

                    b.Property<string>("PhotoPath");

                    b.Property<int?>("Rank1Id");

                    b.Property<int?>("ReligionId");

                    b.Property<string>("SVC_NO");

                    b.Property<string>("Sex");

                    b.Property<int?>("SortId");

                    b.Property<int?>("SpecialisationAreaId");

                    b.Property<int?>("StateId");

                    b.Property<string>("Title");

                    b.Property<string>("accountno");

                    b.Property<int?>("bank");

                    b.Property<string>("bankbranch");

                    b.Property<int?>("branchid");

                    b.Property<DateTime?>("commissiondate");

                    b.Property<int?>("commissionid");

                    b.Property<int?>("commissiontypeid");

                    b.Property<int?>("countryid");

                    b.Property<DateTime?>("dateemployed");

                    b.Property<DateTime?>("dateleft");

                    b.Property<string>("email");

                    b.Property<int?>("entrymodeid");

                    b.Property<int?>("geozoneid");

                    b.Property<string>("homeaddress");

                    b.Property<string>("hubbies");

                    b.Property<int>("rank");

                    b.Property<int?>("religiondenominationid");

                    b.Property<DateTime?>("senioritydate");

                    b.Property<int?>("staffbranchid");

                    b.Property<int?>("tribeId");

                    b.HasKey("PersonID");

                    b.HasIndex("Rank1Id");

                    b.ToTable("person");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Pf_InvestRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(10,3)");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("DueDate");

                    b.Property<string>("InvestmentType");

                    b.Property<int?>("IssuanceBankId");

                    b.Property<DateTime?>("Maturingdate");

                    b.Property<string>("Tenure");

                    b.Property<string>("Voucher");

                    b.Property<string>("chequeno");

                    b.Property<string>("closecode");

                    b.Property<int>("companyid");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("interest");

                    b.Property<decimal?>("maturedamt")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int?>("receivingBankId");

                    b.HasKey("Id");

                    b.ToTable("pf_InvestRegisters");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Pf_LoanRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(10,3)");

                    b.Property<DateTime?>("ApproveDate");

                    b.Property<int?>("BankID");

                    b.Property<int?>("ChequeNo");

                    b.Property<DateTime?>("EffectiveDate");

                    b.Property<DateTime?>("ExpDate");

                    b.Property<int?>("FundTypeID");

                    b.Property<string>("LoanAppNo");

                    b.Property<int?>("LoanTypeID");

                    b.Property<int?>("PersonID");

                    b.Property<string>("Status");

                    b.Property<string>("StatusAndStatusDate");

                    b.Property<string>("Tenure");

                    b.Property<string>("VoucherNo");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("remarks");

                    b.HasKey("Id");

                    b.ToTable("pf_LoanRegisters");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Pf_fund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("fundacct");

                    b.Property<string>("incomeacct");

                    b.Property<string>("interestacct");

                    b.Property<int>("processingMonth");

                    b.Property<int>("processingYear");

                    b.Property<string>("profitacct");

                    b.Property<DateTime?>("startDate");

                    b.Property<string>("trusteeacct");

                    b.HasKey("Id");

                    b.ToTable("pf_Funds");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Pf_fundRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FundCode");

                    b.Property<string>("Interest");

                    b.Property<string>("Period");

                    b.Property<string>("Rate");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.HasKey("Id");

                    b.ToTable("pf_FundRates");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Pf_loanType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<int?>("FundTypeID");

                    b.Property<string>("Interest");

                    b.Property<string>("Tenure");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("incomeacct");

                    b.Property<string>("interestacct");

                    b.Property<string>("liabilityacct");

                    b.Property<string>("loanacct");

                    b.Property<string>("trusteeacct");

                    b.HasKey("Id");

                    b.ToTable("Pf_loanType");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("ranks");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Relationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("Relationship1Id");

                    b.HasKey("Id");

                    b.HasIndex("Relationship1Id")
                        .IsUnique()
                        .HasFilter("[Relationship1Id] IS NOT NULL");

                    b.ToTable("relationships");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.RoleMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<int>("MenuId");

                    b.Property<int>("RoleId");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.ac_accounttypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ac_type");

                    b.Property<string>("ac_typedesc");

                    b.HasKey("Id");

                    b.ToTable("ac_accounttypes");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_balsheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bl_code");

                    b.Property<string>("bl_desc");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.HasKey("Id");

                    b.HasIndex("bl_code")
                        .IsUnique()
                        .HasFilter("[bl_code] IS NOT NULL");

                    b.ToTable("npf_Balsheets");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_chart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("acctcode");

                    b.Property<string>("balSheetCode");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("description");

                    b.Property<bool>("ispersonel");

                    b.Property<string>("mainAccountCode");

                    b.Property<string>("subtype");

                    b.HasKey("Id");

                    b.ToTable("npf_Charts");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_contributions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Createdby");

                    b.Property<DateTime>("Datecreated");

                    b.Property<decimal>("amount01")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount02")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount03")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount04")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount05")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount06")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount07")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount08")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount09")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount10")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount11")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount12")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount13")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount14")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount15")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount16")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount17")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount18")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount19")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount20")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount21")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("amount22")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("fundtype");

                    b.Property<string>("loantype");

                    b.HasKey("Id");

                    b.ToTable("npf_Contributions");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_fundType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("fundname");

                    b.HasKey("Id");

                    b.ToTable("npffundType");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_history", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("acctcode");

                    b.Property<decimal?>("cramt")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<decimal?>("dbamt")
                        .HasColumnType("decimal(10,3)");

                    b.Property<DateTime?>("docdate");

                    b.Property<string>("docno");

                    b.Property<string>("doctype");

                    b.Property<string>("fundcode");

                    b.Property<decimal?>("netamt")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("period");

                    b.Property<string>("remarks");

                    b.HasKey("Id");

                    b.ToTable("npf_Histories");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_ledger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("acctcode");

                    b.Property<decimal>("adbbalance")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("balpl")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("crbalance")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("fundcode");

                    b.Property<decimal>("opbalance")
                        .HasColumnType("decimal(10,3)");

                    b.HasKey("Id");

                    b.ToTable("npf_Ledgers");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.npf_mainact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("description");

                    b.Property<string>("maincode");

                    b.Property<string>("npf_balsheet_bl_code");

                    b.Property<string>("subtype");

                    b.HasKey("Id");

                    b.ToTable("mainacts");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.py_bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyAcctNo");

                    b.Property<string>("ContactMgrAccountant");

                    b.Property<string>("address");

                    b.Property<string>("bankcode");

                    b.Property<string>("bankname");

                    b.Property<string>("branchcode");

                    b.Property<string>("branchname");

                    b.Property<string>("cbn_branch");

                    b.Property<string>("cbn_code");

                    b.Property<string>("contact");

                    b.Property<string>("createdby");

                    b.Property<DateTime?>("datecreated");

                    b.Property<string>("email");

                    b.Property<string>("remarks");

                    b.Property<string>("telephone");

                    b.HasKey("Id");

                    b.ToTable("py_Banks");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.sub_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("subtype");

                    b.Property<string>("subtypedesc");

                    b.HasKey("Id");

                    b.HasIndex("subtype")
                        .IsUnique()
                        .HasFilter("[subtype] IS NOT NULL");

                    b.ToTable("sub_Types");
                });

            modelBuilder.Entity("NavyAccountCore.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("code");

                    b.Property<string>("description");

                    b.HasKey("Id");

                    b.ToTable("company");
                });

            modelBuilder.Entity("NavyAccountCore.Entities.pf_loandisc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("createdby");

                    b.Property<DateTime>("datecreated");

                    b.Property<decimal>("extpay")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("fundcode");

                    b.Property<string>("groupcode");

                    b.Property<string>("incomeact");

                    b.Property<decimal>("interest")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("intract");

                    b.Property<string>("liabilityact");

                    b.Property<string>("loanact");

                    b.Property<decimal>("loandiscr")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("loanpay")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("loantype");

                    b.Property<decimal>("loanvar")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("principal")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("prvloan")
                        .HasColumnType("decimal(10,3)");

                    b.Property<string>("trusteeact");

                    b.HasKey("Id");

                    b.ToTable("pf_loandisc");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<int>");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasIndex("RoleId");

                    b.HasDiscriminator().HasValue("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Beneficiary", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.Person", "Person")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NavyAccountCore.Core.Entities.Relationship", "Relationship")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("RelationshipId");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Menu", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.MenuGroup", "MenuGroup")
                        .WithMany("Menus")
                        .HasForeignKey("MenuGroupId");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Person", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.Rank", "Rank1")
                        .WithMany("People")
                        .HasForeignKey("Rank1Id");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.Relationship", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.Relationship", "Relationship1")
                        .WithOne("Relationship2")
                        .HasForeignKey("NavyAccountCore.Core.Entities.Relationship", "Relationship1Id");
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.RoleMenu", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.Menu", "Menu")
                        .WithMany("RoleMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NavyAccountCore.Core.Entities.Role", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NavyAccountCore.Core.Entities.UserRole", b =>
                {
                    b.HasOne("NavyAccountCore.Core.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NavyAccountCore.Core.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
