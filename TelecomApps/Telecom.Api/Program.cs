
using Microsoft.EntityFrameworkCore;
using Stripe;
using Telecom.Infrastructure.Database;
using Telecom.Infrastructure.Services;
using Telecom.Application.Services;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Infrastructure.Repositories;
using Telecom.Domain.Configuration;
using Telecom.Application.Mapper;

namespace Telecom.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            // Add services to the container.

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(AppDbContext))));

            // Configure Stripe
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            services.AddScoped<TokenService>();
            services.AddScoped<BalanceService>();
            services.AddScoped<ChargeService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, Application.Services.AccountService>();
            services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IRechargeService, RechargeService>();
        }
    }
}
