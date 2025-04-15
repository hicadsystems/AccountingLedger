using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.IRepositories;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Data
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IMenuRepository Menus { get; }
        IRoleMenuRepository RoleMenus { get; }
        IMenuGroupRepository MenuGroups { get; }
        IFundType FundType { get; }
        IAccountType actType { get; }
        IBalsheet balSheet { get; }

        IPfundrate pfundrate { get; }
        ILedgerRepository npf_Ledgers { get; }

        IMainAccount mainAccount { get; }
        IAccountChart accountChart { get; }
        ISubType subtype { get; }
        IFundTypeCode fundTypeCode { get; }
        ILoanType loanType { get; }
        IRank rank { get; }
        IPerson person { get; }
        IBeneficiary beneficiary { get; }
        IBank bank { get; }
        INPFContribution contribution { get; }
        ILoanRegisterRepository loanRegisterRepository { get; }
        IInvestmentRegister register { get; }
        ILoanStatus loanStatus { get; }

        ITrialBalance balance { get; }
        ILoanSchedule schedule { get; }

        IAccountHistory accountHistory { get; }
        ITrialBalanceReport report { get; }
        ITrialBalanceHistory history { get; }
        ILoandiscRepo pf_loandisc { get; }

        IClaimRegister claimregister { get; }
        ILoanPerRank loanPerRank { get; }
        IAuditTrail trail { get; }
        IFinancialDocRepo npfHistories { get; }
        IContrRepo npf_contrdisc { get; }
        ISuplusRepo surplus { get; }
        IClaimRepository cam { get; }
        INavipRepo navip { get; }
        ILoanTypeReviewRepo loantypereview { get; }
        ITrialBalanceReportView trialBalanceReportView { get; }
        IStudentRecordRepository student { get; }
        ISchoolRecordRepository school { get; }
        ISchoolFeeRepository schoolFee { get; }
        IPaymentRepository payment { get; }
        IClaimRecordRepository schclaim { get; }
        IParentGuardianRecordRepository parent { get; }
        IGuardianRecordRepository Guardian { get; }
        IStock stock { get; }
        Task<bool> Done();
    }    
}
