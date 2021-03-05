using ParqueAPICentral.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ParqueAPICentral.Repositories;
using ParqueAPICentral.Services;
using Microsoft.AspNetCore.Authorization;
using ParqueAPICentral.Authorization;

namespace ParqueAPICentral
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
            services.AddControllersWithViews();
            services.AddCors();
            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireClaim("User"));
                options.AddPolicy("UserOperatorPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("Roles", policy =>
                {
                    policy.Requirements.Add(new ClaimsRequirement("Admin", "User"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            services.AddScoped<IFaturaRepository, FaturaRepository>();
            services.AddTransient<FaturaService>();

            services.AddScoped<IMoradaRepository, MoradaRepository>();
            services.AddTransient<MoradaService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddTransient<ClienteService>();

            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddTransient<ReservaService>();

            services.AddScoped<IReservaCentralRepository, ReservaCentralRepository>();
            services.AddTransient<ReservaCentralService>();

            services.AddScoped<IParquesRepository, ParquesRepository>();
            services.AddTransient<ParquesService>();

            services.AddScoped<ISubAluguerRepository, SubAluguerRepository>();
            services.AddTransient<SubAluguerService>();

            // services.AddScoped<ILugarRepository, LugarRepository>();
            services.AddTransient<LugaresService>();


            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
           



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}