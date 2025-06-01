using Infraestructure.DatabaseContext;
using Infraestructure.OpenApiConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace Infraestructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Authentication JWT
            string criptoKey = configuration.GetValue<string>("Cripto:Key") ?? throw new ArgumentNullException(nameof(criptoKey));


            var key = Encoding.ASCII.GetBytes(criptoKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = validated =>
                    {
                        return Task.CompletedTask;
                    },
                };
            });
            #endregion

            #region Open API & Scalar
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, _, _) =>
                {
                    document.Info.Title = "Lanchonete API";
                    document.Info.Description = "API para lanchonete e pedidos.";
                    document.Info.Version = "v1";

                    return Task.CompletedTask;
                });

                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });

            services.Configure<ScalarOptions>(options =>
            {
                options
                    .WithTitle("Lanchonete API")
                    .WithSidebar(true);

                options.Servers = [];

                options.AddServer(new ScalarServer("https://localhost:8081", "Endpoint com proteção SSL"));
                options.AddServer(new ScalarServer("http://localhost:8080", "Endpoint sem proteção SSL"));

                options.WithDefaultHttpClient(ScalarTarget.Shell, ScalarClient.Curl);
            });
            #endregion

            #region Database EFCore
            var connectionString = configuration.GetConnectionString("DBConnectionString");

            services.AddDbContext<ApplicationDatabaseContext>(options => options.UseNpgsql(connectionString));
            #endregion

            return services;
        }
    }
}
