using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class DatabasesRecreations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ac_accounttypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ac_type = table.Column<string>(nullable: true),
                    ac_typedesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ac_accounttypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "loanStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loanStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mainacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    maincode = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    npf_balsheet_bl_code = table.Column<string>(nullable: true),
                    subtype = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mainacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "npf_Balsheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bl_code = table.Column<string>(nullable: true),
                    bl_desc = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_Balsheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "npf_Charts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    acctcode = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    mainAccountCode = table.Column<string>(nullable: true),
                    subtype = table.Column<string>(nullable: true),
                    ispersonel = table.Column<bool>(nullable: false),
                    balSheetCode = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_Charts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "npf_Contributions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    loantype = table.Column<string>(nullable: true),
                    fundtype = table.Column<string>(nullable: true),
                    amount01 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount02 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount03 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount04 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount05 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount06 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount07 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount08 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount09 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount10 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount11 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount12 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount13 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount14 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount15 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount16 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount17 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount18 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount19 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount20 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount21 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    amount22 = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    Createdby = table.Column<string>(nullable: true),
                    Datecreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_Contributions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "npf_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fundcode = table.Column<string>(nullable: true),
                    acctcode = table.Column<string>(nullable: true),
                    docno = table.Column<string>(nullable: true),
                    docdate = table.Column<DateTime>(nullable: true),
                    period = table.Column<string>(nullable: true),
                    doctype = table.Column<string>(nullable: true),
                    dbamt = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    cramt = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    netamt = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "npf_Ledgers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fundcode = table.Column<string>(nullable: true),
                    acctcode = table.Column<string>(nullable: true),
                    opbalance = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    adbbalance = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    crbalance = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    balpl = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_Ledgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "npffundType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fundname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npffundType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pf_FundRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FundCode = table.Column<string>(nullable: true),
                    Period = table.Column<string>(nullable: true),
                    Interest = table.Column<string>(nullable: true),
                    Rate = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_FundRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pf_Funds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    startDate = table.Column<DateTime>(nullable: true),
                    processingYear = table.Column<int>(nullable: false),
                    processingMonth = table.Column<int>(nullable: false),
                    interestacct = table.Column<string>(nullable: true),
                    incomeacct = table.Column<string>(nullable: true),
                    fundacct = table.Column<string>(nullable: true),
                    trusteeacct = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_Funds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pf_InvestRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    companyid = table.Column<int>(nullable: false),
                    IssuanceBankId = table.Column<int>(nullable: true),
                    receivingBankId = table.Column<int>(nullable: true),
                    Voucher = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    Maturingdate = table.Column<DateTime>(nullable: true),
                    Tenure = table.Column<string>(nullable: true),
                    InvestmentType = table.Column<string>(nullable: true),
                    closecode = table.Column<string>(nullable: true),
                    interest = table.Column<string>(nullable: true),
                    chequeno = table.Column<string>(nullable: true),
                    maturedamt = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_InvestRegisters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pf_LoanRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: true),
                    LoanTypeID = table.Column<int>(nullable: true),
                    FundTypeID = table.Column<int>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Tenure = table.Column<string>(nullable: true),
                    ChequeNo = table.Column<int>(nullable: true),
                    BankID = table.Column<int>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ExpDate = table.Column<DateTime>(nullable: true),
                    VoucherNo = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_LoanRegisters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pf_loanType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Tenure = table.Column<string>(nullable: true),
                    Interest = table.Column<string>(nullable: true),
                    FundTypeID = table.Column<int>(nullable: true),
                    liabilityacct = table.Column<string>(nullable: true),
                    interestacct = table.Column<string>(nullable: true),
                    incomeacct = table.Column<string>(nullable: true),
                    loanacct = table.Column<string>(nullable: true),
                    trusteeacct = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pf_loanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "py_Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bankcode = table.Column<string>(nullable: true),
                    branchcode = table.Column<string>(nullable: true),
                    bankname = table.Column<string>(nullable: true),
                    branchname = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    CompanyAcctNo = table.Column<string>(nullable: true),
                    ContactMgrAccountant = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    telephone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    contact = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    cbn_code = table.Column<string>(nullable: true),
                    cbn_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_py_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ranks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "relationships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Relationship1Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_relationships_relationships_Relationship1Id",
                        column: x => x.Relationship1Id,
                        principalTable: "relationships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sub_Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    subtype = table.Column<string>(nullable: true),
                    subtypedesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MenuGroupId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_MenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SVC_NO = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Initials = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    homeaddress = table.Column<string>(nullable: true),
                    dateemployed = table.Column<DateTime>(nullable: true),
                    bank = table.Column<int>(nullable: true),
                    accountno = table.Column<string>(nullable: true),
                    bankbranch = table.Column<string>(nullable: true),
                    Factory = table.Column<string>(nullable: true),
                    Payrollclass = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    ExitTypeid = table.Column<int>(nullable: true),
                    dateleft = table.Column<DateTime>(nullable: true),
                    branchid = table.Column<int>(nullable: true),
                    staffbranchid = table.Column<int>(nullable: true),
                    Departmentid = table.Column<int>(nullable: true),
                    MaidenName = table.Column<string>(nullable: true),
                    Commandid = table.Column<int>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    MaritalStatusId = table.Column<int>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    LiveStatus = table.Column<bool>(nullable: true),
                    IdentificationId = table.Column<int>(nullable: true),
                    countryid = table.Column<int>(nullable: true),
                    LocalGovernmentareaId = table.Column<int>(nullable: true),
                    geozoneid = table.Column<int>(nullable: true),
                    FingerPrint = table.Column<byte[]>(nullable: true),
                    IdentityType = table.Column<string>(nullable: true),
                    IdentifyNumber = table.Column<string>(nullable: true),
                    IdentityExpiryDate = table.Column<DateTime>(nullable: true),
                    ReligionId = table.Column<int>(nullable: true),
                    GSMNumber = table.Column<string>(nullable: true),
                    tribeId = table.Column<int>(nullable: true),
                    Birthplace = table.Column<string>(nullable: true),
                    religiondenominationid = table.Column<int>(nullable: true),
                    BloodGroupid = table.Column<int>(nullable: true),
                    entrymodeid = table.Column<int>(nullable: true),
                    commissiontypeid = table.Column<int>(nullable: true),
                    commissionid = table.Column<int>(nullable: true),
                    commissiondate = table.Column<DateTime>(nullable: true),
                    senioritydate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    SpecialisationAreaId = table.Column<int>(nullable: true),
                    hubbies = table.Column<string>(nullable: true),
                    NOKFileName = table.Column<string>(nullable: true),
                    SortId = table.Column<int>(nullable: true),
                    rank = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Rank1Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_person_ranks_Rank1Id",
                        column: x => x.Rank1Id,
                        principalTable: "ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenus_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    RelationshipId = table.Column<int>(nullable: true),
                    dateofbirth = table.Column<DateTime>(nullable: true),
                    FullAddress = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    HomeNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PlaceOfWork = table.Column<string>(nullable: true),
                    NextofkinType = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_relationships_RelationshipId",
                        column: x => x.RelationshipId,
                        principalTable: "relationships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_PersonID",
                table: "Beneficiaries",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_RelationshipId",
                table: "Beneficiaries",
                column: "RelationshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Code",
                table: "Menus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuGroupId",
                table: "Menus",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_npf_Balsheets_bl_code",
                table: "npf_Balsheets",
                column: "bl_code",
                unique: true,
                filter: "[bl_code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_person_Rank1Id",
                table: "person",
                column: "Rank1Id");

            migrationBuilder.CreateIndex(
                name: "IX_relationships_Relationship1Id",
                table: "relationships",
                column: "Relationship1Id",
                unique: true,
                filter: "[Relationship1Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_MenuId",
                table: "RoleMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_RoleId",
                table: "RoleMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_sub_Types_subtype",
                table: "sub_Types",
                column: "subtype",
                unique: true,
                filter: "[subtype] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ac_accounttypes");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "loanStatus");

            migrationBuilder.DropTable(
                name: "mainacts");

            migrationBuilder.DropTable(
                name: "npf_Balsheets");

            migrationBuilder.DropTable(
                name: "npf_Charts");

            migrationBuilder.DropTable(
                name: "npf_Contributions");

            migrationBuilder.DropTable(
                name: "npf_Histories");

            migrationBuilder.DropTable(
                name: "npf_Ledgers");

            migrationBuilder.DropTable(
                name: "npffundType");

            migrationBuilder.DropTable(
                name: "pf_FundRates");

            migrationBuilder.DropTable(
                name: "pf_Funds");

            migrationBuilder.DropTable(
                name: "pf_InvestRegisters");

            migrationBuilder.DropTable(
                name: "pf_LoanRegisters");

            migrationBuilder.DropTable(
                name: "Pf_loanType");

            migrationBuilder.DropTable(
                name: "py_Banks");

            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "sub_Types");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "relationships");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ranks");

            migrationBuilder.DropTable(
                name: "MenuGroups");
        }
    }
}
