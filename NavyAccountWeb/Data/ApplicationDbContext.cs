using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;

namespace NavyAccountWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>, INavyAccountDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(150000);
        }

        public DbContext Instance => this;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<npf_balsheet> npf_Balsheets { get; set; }
        public DbSet<npf_chart> npf_Charts { get; set; }
        public DbSet<npf_history> npf_Histories { get; set; }
        public DbSet<npf_ledger> npf_Ledgers { get; set; }
        public DbSet<Person> person { get; set; }
        public DbSet<Pf_fund> pf_Funds { get; set; }
        public DbSet<Pf_loanType> Pf_loanType { get; set; }
        public DbSet<Pf_fundRate> pf_FundRates { get; set; }
        public DbSet<Pf_InvestRegister> pf_InvestRegisters { get; set; }
        public DbSet<Pf_LoanRegister> pf_LoanRegisters { get; set; }
        public DbSet<py_bank> py_Banks { get; set; }
        public DbSet<Rank> ranks { get; set; }
        public DbSet<Relationship> relationships { get; set; }
        public DbSet<npf_mainact> mainacts { get; set; }
        public DbSet<ac_accounttypes> ac_accounttypes { get; set; }

        public DbSet<sub_type> sub_Types { get; set; }

        public DbSet<npf_fundType> npffundType { get; set; }

        public DbSet<npf_contributions> npf_Contributions { get; set; }
        public DbSet<LoanStatus> loanStatus { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Npf_ClaimRegister> Npf_ClaimRegisters { get; set; }
        public DbSet<pf_loandisc> pf_loandisc { get; set; }
        public DbSet<loanPerRank> loanPerRank { get; set; }
        public DbSet<npf_contrdisc> npf_contrdisc { get; set; }
        public DbSet<npf_NavipContribution> npf_NavipContribution { get; set; }
        public DbSet<npf_navip> npf_navip { get; set; }
        public DbSet<npf_LoanTypeReview> npf_loantypereview { get; set; }
        public DbSet<npf_loanstatus> npf_loanstatus { get; set; }
        public DbQuery<V_TRIALBALANCE> V_TRIALBALANCEs { get; set; }
        public DbSet<npf_Stocks> npf_Stocks { get; set; }
        public DbSet<sr_ClaimRecord> sr_ClaimRecord { get; set; }
        public DbSet<sr_GuardianRecord> sr_GuardianRecord { get; set; }
        public DbSet<sr_ParentRecord> sr_ParentRecord { get; set; }
        public DbSet<sr_PaymentRecord> sr_PaymentRecord { get; set; }
        public DbSet<sr_SchoolRecord> sr_SchoolRecord { get; set; }
        public DbSet<sr_StudentRecord> sr_StudentRecord { get; set; }
        public DbSet<sr_ClassRecord> sr_ClassRecord { get; set; }
        public DbSet<sr_SchoolFeeTB> sr_SchoolFeeTB { get; set; }
        public DbSet<sr_StudentRecordHist> sr_StudentRecordHist { get; set; }
        public DbSet<sr_SchoolRecordControl> sr_SchoolRecordControl { get; set; }
        public DbSet<sr_state> sr_state{ get; set; }
        public DbSet<sr_lga> sr_lga { get; set; }
        public DbSet<sr_Defaulter> sr_Defaulter { get; set; }
       


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Menu>()
                .HasIndex(x => x.Code)
                .IsUnique();

            builder.Entity<RoleMenu>()
                .Property(e => e.Id)
                .UseSqlServerIdentityColumn();

            

            builder.Entity<Menu>()
                .Property(e => e.Id)
                .UseSqlServerIdentityColumn();

            builder.Entity<Country>()
                .Property(e => e.Id)
                .UseSqlServerIdentityColumn();

            builder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.Entity<RoleMenu>()
                .HasKey(x => x.Id);

            builder.Entity<RoleMenu>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RoleMenus)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<RoleMenu>()
                .HasOne(x => x.Menu)
                .WithMany(x => x.RoleMenus)
                .HasForeignKey(x => x.MenuId);

            builder.Entity<npf_NavipContribution>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();


            builder.Entity<Beneficiary>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<npf_balsheet>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<npf_balsheet>()
               .HasIndex(x => x.bl_code)
               .IsUnique();

            builder.Entity<sub_type>()
               .HasIndex(x => x.subtype)
               .IsUnique();
            builder.Entity<sub_type>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<npf_chart>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<sr_PaymentRecord>()
             .Property(e => e.Amount)
              .HasColumnType("money");

            builder.Entity<sr_StudentRecord>()
            .Property(e => e.ClaimAmount)
             .HasColumnType("money");

            builder.Entity<sr_ClassRecord>()
            .Property(e => e.Schoolfee)
             .HasColumnType("money");

            builder.Entity<sr_ClaimRecord>()
             .Property(e => e.Amount)
              .HasColumnType("money");


            builder.Entity<npf_history>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<npf_history>()
              .Property(e => e.dbamt)
               .HasColumnType("money");
            builder.Entity<npf_history>()
             .Property(e => e.netamt)
              .HasColumnType("money");

           
            builder.Entity<npf_contributions>()
             .Property(e => e.amount01)
              .HasColumnType("money");
            builder.Entity<npf_contributions>()
             .Property(e => e.amount02)
              .HasColumnType("money");
            builder.Entity<npf_contributions>()
             .Property(e => e.amount03)
              .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount04)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount05)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount06)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount07)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount08)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount09)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount10)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount11)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount12)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount13)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount14)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount15)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount16)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount17)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount18)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount19)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount20)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount21)
               .HasColumnType("money"); builder.Entity<npf_contributions>()
              .Property(e => e.amount22)
               .HasColumnType("money");


            builder.Entity<loanPerRank>()
            .Property(e => e.amount01)
             .HasColumnType("money");
            builder.Entity<loanPerRank>()
             .Property(e => e.amount02)
              .HasColumnType("money");
            builder.Entity<loanPerRank>()
             .Property(e => e.amount03)
              .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount04)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount05)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount06)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount07)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount08)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount09)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount10)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount11)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount12)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount13)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount14)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount15)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount16)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount17)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount18)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount19)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount20)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount21)
               .HasColumnType("money"); builder.Entity<loanPerRank>()
              .Property(e => e.amount22)
               .HasColumnType("money");

            builder.Entity<loanPerRank>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();


            builder.Entity<npf_history>()
           .Property(e => e.cramt)
            .HasColumnType("money");

            builder.Entity<npf_contributions>()
                .Property(e => e.Id)
                .UseSqlServerIdentityColumn();

            builder.Entity<npf_ledger>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();
           
            builder.Entity<npf_ledger>()
            .Property(e => e.opbalance)
             .HasColumnType("money");

            builder.Entity<npf_ledger>()
           .Property(e => e.adbbalance)
            .HasColumnType("money");
            builder.Entity<npf_ledger>()
           .Property(e => e.crbalance)
            .HasColumnType("money");
            builder.Entity<npf_ledger>()
           .Property(e => e.balpl)
            .HasColumnType("money");


            builder.Entity<Person>()
               .Property(e => e.PersonID)
               .UseSqlServerIdentityColumn();

            builder.Entity<LoanStatus>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<Person>()
               .HasOne(x => x.Rank1)
               .WithMany(x => x.People);

            builder.Entity<Person>()
               .HasMany(x => x.Beneficiaries);

            builder.Entity<Pf_fund>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<Pf_fundRate>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<Pf_InvestRegister>()
               .Property(e => e.Amount)
               .HasColumnType("money");

            builder.Entity<Pf_InvestRegister>()
               .Property(e => e.maturedamt)
               .HasColumnType("money");


            builder.Entity<Pf_InvestRegister>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<npf_fundType>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<Pf_LoanRegister>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<Pf_LoanRegister>()
              .Property(e => e.Amount)
               .HasColumnType("money");

            builder.Entity<py_bank>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();
            builder.Entity<Rank>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<npf_mainact>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<Pf_loanType>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<Company>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<Npf_ClaimRegister>()
                .Property(e => e.Id)
                .UseSqlServerIdentityColumn();
            
            builder.Entity<Npf_ClaimRegister>()
           .Property(e => e.AmountDue)
            .HasColumnType("money");
            builder.Entity<Npf_ClaimRegister>().Property(e => e.TotalContribution).HasColumnType("money");
            builder.Entity<Npf_ClaimRegister>().Property(e => e.interest).HasColumnType("money");
            builder.Entity<Npf_ClaimRegister>().Property(e => e.loan).HasColumnType("money");
            builder.Entity<Npf_ClaimRegister>().Property(e => e.amountPaid).HasColumnType("money");
            builder.Entity<Npf_ClaimRegister>().Property(e => e.amountReceived).HasColumnType("money");
          

            builder.Entity<pf_loandisc>()
              .Property(e => e.Id)
              .UseSqlServerIdentityColumn();

            builder.Entity<pf_loandisc>()
             .Property(e => e.principal)
             .HasColumnType("money");

            builder.Entity<pf_loandisc>()
           .Property(e => e.interest)
           .HasColumnType("money");

            builder.Entity<pf_loandisc>()
           .Property(e => e.loanpay)
           .HasColumnType("money");

            builder.Entity<pf_loandisc>()
           .Property(e => e.prvloan)
           .HasColumnType("money");

            builder.Entity<pf_loandisc>()
           .Property(e => e.extpay)
           .HasColumnType("money");

            builder.Entity<pf_loandisc>()
           .Property(e => e.loanvar)
           .HasColumnType("money");

            builder.Entity<pf_loandisc>()
           .Property(e => e.loandiscr)
           .HasColumnType("money");


            builder.Entity<npf_contrdisc>()
             .Property(e => e.Id)
             .UseSqlServerIdentityColumn();

            builder.Entity<npf_contrdisc>()
          .Property(e => e.prvamt)
          .HasColumnType("money");

            builder.Entity<npf_contrdisc>()
           .Property(e => e.amountpay)
           .HasColumnType("money");

            builder.Entity<npf_contrdisc>()
           .Property(e => e.extamt)
           .HasColumnType("money");

            builder.Entity<npf_contrdisc>()
           .Property(e => e.amtvar)
           .HasColumnType("money");

           builder.Entity<npf_contrdisc>()
          .Property(e => e.amtdiscr)
          .HasColumnType("money");

            builder.Entity<npf_navip>()
                  .Property(e => e.surval)
                  .HasColumnType("money");


            builder.Entity<npf_navip>()
               .Property(e => e.Id)
               .UseSqlServerIdentityColumn();

            builder.Entity<npf_navip>()
               .Property(e => e.reviewdate1)
               .HasColumnType("date");

            builder.Entity<npf_navip>()
             .Property(e => e.reviewdate2)
             .HasColumnType("date");

            builder.Entity<npf_navip>()
             .Property(e => e.calcdate1)
             .HasColumnType("date");

            builder.Entity<npf_navip>()
             .Property(e => e.calcdate1)
             .HasColumnType("date");

            builder.Query<V_TRIALBALANCE>().ToView("V_TRIALBALANCE");
        }
    }
}
