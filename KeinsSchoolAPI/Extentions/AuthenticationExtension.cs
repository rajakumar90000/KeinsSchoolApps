﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeinsSchoolApps.Extentions
{
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    namespace KeinsSchoolApps.Middleware
    {
        public static class AuthenticationExtension
        {
            public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
            {
                var secret = config.GetSection("JwtConfig").GetSection("secret").Value;

                var key = Encoding.ASCII.GetBytes(secret);
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "localhost",
                        ValidAudience = "localhost"
                    };
                });

                return services;
            }
        }
    }
}
