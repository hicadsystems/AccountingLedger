using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.IRepositories;
using NavyAccountCore.Repositories;

namespace NavyAccountCore.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly INavyAccountDbContext context;
        private readonly IConfiguration _configuration;
        public UnitOfWork(INavyAccountDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this._configuration = configuration;


            Users = new UserRepository(context);
           
            Menus = new MenuRepository(context);
            RoleMenus = new RoleMenuRepository(context);
            MenuGroups = new MenuGroupRepository(context);
            UserRoles = new UserRoleRepository(context);
            FundType = new FundTypeRepo(context);
            actType = new AccountTypeRepository(context);
            balSheet = new BalanceSheetRepository(context);
            mainAccount = new MainAccountRepository(context);
            accountChart = new ChartRepository(context);
            subtype = new SubTypeRepository(context);
            fundTypeCode = new FundTypeRepository(context);
            loanType = new LoanTypeRepo(context);
            rank = new RankRepo(context);
            person = new PersonRepo(context);
            beneficiary = new BeneficiaryRepo(context);
            bank = new BankRepository(context);
            pfundrate = new PfFundRateRepository(context);
            contribution = new NPFContributionRepository(context);
            loanRegisterRepository = new LoanRegisterRepository(context);
            register = new InvestmentRegisterRepository(context);
            loanStatus = new LoanStatusRepository(context);
            schedule = new LoanScheduleRepository(context);
            balance = new TrialBalanceRepository(context);
            accountHistory = new AccountHistoryRepository(context);
            npf_Ledgers = new LedgerRepositoy(context);
            report = new TrialBalanceReportRepository(context);
            history = new TrialBalanceHistoryRepository(context);
            pf_loandisc = new LoandiscRepo(context);
            loanPerRank = new LoanPerRankRepository(context);
            claimregister = new ClaimRepository(context);
            npfHistories = new FinancialDocRepo(context);
            trail = new AuditRailRepository(context);
            npf_contrdisc = new ContrRepo(context);
            surplus = new SurplusRepository(context);
            cam = new ClaimTypeRepository(context);
            navip = new NavipRepository(context);
            loantypereview = new LoanTypeReviewRepo(context);
            trialBalanceReportView = new TrialBalanceReportView(context, configuration);
            student = new StudentRecordRepository(context);
            school = new SchoolRecordRepository(context);
            schoolFee = new SchoolFeeRepository(context);
            payment = new PaymentRepository(context);
            schclaim = new ClaimRecordRepository(context);
            parent = new ParentGuardianRecordRepository(context);
            Guardian = new GuardianRecordRepository(context);
            stock = new StockRepository(context);



        }     

        public IUserRepository Users { get; private set; }
        public IMenuGroupRepository MenuGroups { get; private set; }
        public IUserRoleRepository UserRoles { get; }
        public IMenuRepository Menus { get; }
        public IRoleMenuRepository RoleMenus { get; }
        public IFundType FundType { get; }
        public IAccountType actType { get; }
        public IBalsheet balSheet { get; }
        public ITrialBalanceHistory history { get; }
        public IMainAccount mainAccount { get; }
        public IAccountChart accountChart { get; }
        public IAccountHistory accountHistory { get; }
        public ISubType subtype { get; }
        public IFundTypeCode fundTypeCode { get; }
        public ILoanType loanType { get; }
        public IRank rank { get; }
        public IPerson person { get; }
        public IBeneficiary beneficiary { get; }
        public IBank bank { get; }
        public ILoanStatus loanStatus { get; }
        public IPfundrate pfundrate { get; }
        public ILedgerRepository npf_Ledgers { get; }

        public INPFContribution contribution { get; }
        public ILoanRegisterRepository loanRegisterRepository { get; }
        public IInvestmentRegister register { get; }

        public ITrialBalanceReport report { get; }

        public ILoanSchedule schedule { get; }
        public ITrialBalance balance { get; }
        public ILoandiscRepo pf_loandisc { get; }
        public IFinancialDocRepo npfHistories { get; }

        public IClaimRegister claimregister { get; }

        public ILoanPerRank loanPerRank { get; }
        public IContrRepo npf_contrdisc { get; }
        public IAuditTrail trail { get; }
        public ISuplusRepo surplus { get; }
        public IClaimRepository cam { get; }
        public INavipRepo navip { get; }
        public ILoanTypeReviewRepo loantypereview { get; }

        public ITrialBalanceReportView trialBalanceReportView { get;  }

        public IStudentRecordRepository student { get; }
        public ISchoolRecordRepository school { get; }
        public ISchoolFeeRepository schoolFee { get; }
        public IPaymentRepository payment { get; }
        public IClaimRecordRepository schclaim { get; }
        public IParentGuardianRecordRepository parent { get; }
        public IGuardianRecordRepository Guardian { get; }

        public IStock stock { get; }

        public async Task<bool> Done()
        {
            return await context.Instance.SaveChangesAsync() > 0;
        }
    }
}
