

using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Data
{
    public interface INavyAccountDbContext : IDbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<Menu> Menus { get; set; }
        DbSet<RoleMenu> RoleMenus { get; set; }



        DbSet<Beneficiary> Beneficiaries { get; set; }
        DbSet<npf_balsheet> npf_Balsheets { get; set; }
        DbSet<npf_chart> npf_Charts { get; set; }
        DbSet<npf_history> npf_Histories { get; set; }
        DbSet<npf_ledger> npf_Ledgers { get; set; }

        DbSet<Person> person { get; set; }
        DbSet<Pf_fund> pf_Funds { get; set; }
        DbSet<Pf_loanType> Pf_loanType { get; set; }
        DbSet<Pf_fundRate> pf_FundRates { get; set; }
        DbSet<Pf_InvestRegister> pf_InvestRegisters { get; set; }
        DbSet<Pf_LoanRegister> pf_LoanRegisters { get; set; }
        DbSet<py_bank> py_Banks { get; set; }
        DbSet<Rank> ranks { get; set; }
        DbSet<Relationship> relationships { get; set; }
         DbSet<npf_mainact> mainacts { get; set; }
      
        DbSet<ac_accounttypes> ac_accounttypes { get; set; }
        DbSet<sub_type> sub_Types { get; set; }
        DbSet<npf_contributions> npf_Contributions { get; set; }
        DbSet<npf_fundType> npffundType { get; set; }
        DbSet<LoanStatus> loanStatus { get; set; }
    
        DbSet<Company> company { get; set; }
        DbSet<pf_loandisc> pf_loandisc { get; set; }

        DbSet<Npf_ClaimRegister> Npf_ClaimRegisters { get; set; }
        DbSet<loanPerRank> loanPerRank { get; set; }
        DbSet<npf_contrdisc> npf_contrdisc { get; set; }
        DbSet<npf_NavipContribution> npf_NavipContribution { get; set; }
        DbSet<npf_navip> npf_navip { get; set; }
        DbSet<npf_LoanTypeReview> npf_loantypereview { get; set; }
        DbSet<npf_loanstatus> npf_loanstatus { get; set; }

        DbQuery<V_TRIALBALANCE> V_TRIALBALANCEs { get; set; }

        DbSet<sr_ClaimRecord> sr_ClaimRecord { get; set; }
        DbSet<sr_GuardianRecord> sr_GuardianRecord { get; set; }
        DbSet<sr_ParentRecord> sr_ParentRecord { get; set; }
        DbSet<sr_PaymentRecord> sr_PaymentRecord { get; set; }
        DbSet<sr_SchoolRecord> sr_SchoolRecord { get; set; }
        DbSet<sr_SchoolRecordControl> sr_SchoolRecordControl { get; set; }
        DbSet<sr_SchoolFeeTB> sr_SchoolFeeTB { get; set; }
        DbSet<sr_StudentRecord> sr_StudentRecord { get; set; }
        DbSet<sr_ClassRecord> sr_ClassRecord { get; set; }
        DbSet<sr_state> sr_state{ get; set; }
        DbSet<sr_lga> sr_lga { get; set; }
    }
}
