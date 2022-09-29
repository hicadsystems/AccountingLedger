using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NavyAccountWeb.Filters;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.Services;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Core.Repositories;
using NavyAccountWeb.Data;
using System;
using Wkhtmltopdf.NetCore;
using ReflectionIT.Mvc.Paging;

namespace NavyAccountWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            services.AddDbContext<ApplicationDbContext>(options =>
                
            options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            /*services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();*/
            //services.AddControllersWithViews();
            //services.AddRazorPages();

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddWkhtmltopdf("wkhtmltopdf");
            services.AddAutoMapper(typeof(Startup));
            services.Configure<ServerSettings>(Configuration.GetSection("ServerSettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuGroupRepository, MenuGroupRepository>();
            services.AddScoped<IMenuGroupService, MenuGroupService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPerson, PersonRepo>();
            services.AddScoped<ILoanRegisterRepository, LoanRegisterRepository>();
            services.AddScoped<ILoanType, LoanTypeRepo>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFundTypeService, FundTypeService>();
            services.AddScoped<IBalanceSheetService, BalanceSheetService>();
            services.AddScoped<IMainAccountService, MainAccountService>();
            services.AddScoped<IChartofAccountService, ChartofAccountService>();
            services.AddScoped<IFundTypeCodeService, FundTypeCodeService>();
            services.AddScoped<ILoanTypeService, LoanTypeService>();
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ProcesUpload, ProcesUpload>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            services.AddScoped<INavyAccountDbContext, ApplicationDbContext>();
            services.AddScoped<IPFundRateService, pFundRateService>();
            services.AddScoped<ILoanRegisterService, LoanRegisterService>();
            services.AddScoped<IContributionServices, ContributionServices>();
            services.AddScoped<ITrialbalanceReportService, TrialBalanceReportService>();
            services.AddScoped<ILoanStatusService, LoanStatusService>();
            services.AddScoped<IInvestmentRegisterServices, InvestRegisterServices>();
            services.AddScoped<ILoanScheduleservices, LoanScheduleServices>();
            services.AddScoped<IAccountHistoryService, AccountHistoryService>();
            services.AddScoped<TrialBalanceUpload, TrialBalanceUpload>();
            services.AddScoped<ILoandiscService, LoandiscService>();
            services.AddScoped<ILedgerService, LedgerService>();
            services.AddScoped<IFinancialDocService, FinacialDocService>();
            services.AddSingleton(typeof(DinkToPdf.Contracts.IConverter), new DinkToPdf.SynchronizedConverter(new DinkToPdf.PdfTools()));
            services.AddScoped<IClaimRegisterService, ClaimRegisterService>();
            services.AddScoped<ILoanPerRankService, LoanPerRankService>();
            services.AddScoped<IAuditTrailServices, AuditTrailservice>();
            services.AddScoped<IContrService, ContrdiscService>();
            services.AddScoped<ISurplusService, SurplusService>();
            services.AddScoped<IClaimTypeServices, ClaimTypeServices>();
            services.AddScoped <INavipService, NavipService>();
            services.AddScoped<ILoantypeReviewService, LoantypeReviewService>();


            services.AddScoped<ISchoolRecordService, SchoolRecordService>();
            services.AddScoped<IStudentRecordService, StudentRecordService>();
            services.AddScoped<IParentRecordService, ParentRecordService>();
            services.AddScoped<IGuardianRecordService, GuardianRecordService>();
            services.AddScoped<IPaymentRecordService, PaymentRecordService>();
            services.AddScoped<IClaimRecordService, ClaimRecordService>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/";
            });


            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                        (_) => "This field is required.");
                options.Filters.Add(typeof(ModelStateFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession(options=> {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });

            services.AddPaging();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager, RoleManager<Role> roleManager,
           IConfiguration config, IUnitOfWork unitOfWork)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            //var IsCentralServer = config.GetValue<bool>("ServerSettings:CentralServer");
            //if (!IsCentralServer)
            //{
            //   Seeder.SeedData(userManager, roleManager, config, unitOfWork);
            //}


            app.UseAuthentication();
            app.UseSession();
            //app.UseAuthorization();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });*/
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

         
           

        }
    }
}
