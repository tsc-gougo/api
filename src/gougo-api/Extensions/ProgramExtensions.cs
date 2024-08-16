using Application.Interfaces.Business;
using Application.Interfaces.Mails;
using FluentValidation.AspNetCore;
using Infrastructure.Services.Mails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Serilog;

namespace gougo_api.Extensions;

public static class ProgramExtensions
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GOUGO Api",
                Description = "TSC API FOR GOUGO"
            });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT token"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddSerilogConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        return services;
    }

    public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration,
        bool isDevelopment = false)
    {
        var config = configuration.GetSection(nameof(SecurityOption)).Get<SecurityOption>();

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(JwtBearerDefaults.AuthenticationScheme, p =>
            {
                p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                p.RequireAuthenticatedUser();
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config?.Issuer,
                    ValidAudience = config?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config?.Key ?? "")),
                    ClockSkew = isDevelopment
                        ? TimeSpan.FromDays(2)
                        : TimeSpan.FromMinutes(config?.ExpirationInMinutes ?? 0)
                });

        return services;
    }

    public static IServiceCollection AddConfOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CorsConfigOptions>(configuration.GetSection(CorsConfigOptions.Cors));
        services.Configure<SecurityOption>(configuration.GetSection(nameof(SecurityOption)));
        services.Configure<MailConfigOption>(configuration.GetSection(nameof(MailConfigOption)));

        return services;
    }

    public static IServiceCollection AddCorsWithConfiguration(this
        IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = new CorsConfigOptions();
        configuration.GetSection(CorsConfigOptions.Cors).Bind(corsOptions);

        var originsAllowed = corsOptions.OriginsAllowed ?? new List<string> { "*" };
        var methodsAllowed = corsOptions.MethodsAllowed ?? new List<string> { "*" };

        var corsPolicy = new CorsPolicyBuilder()
            .WithOrigins(string.Join(",", originsAllowed))
            .AllowAnyHeader()
            .WithMethods(string.Join(",", methodsAllowed))
            .Build();

        services.AddCors(c => c.AddDefaultPolicy(corsPolicy));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBusinessService, BusinessService>();
        services.AddScoped<IMailService, MailService>();

        return services;
    }

    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddFluentValidationRulesToSwagger();

        return services;
    }
}