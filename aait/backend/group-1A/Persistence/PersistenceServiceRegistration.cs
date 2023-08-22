﻿using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Repositories;
using SocialSync.Application.Contracts;
using SocialSync.Persistence.Repositories.Auth;
using System.Reflection;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMediaDbContext>(opt =>
                 opt.UseNpgsql(configuration.GetConnectionString("SocialMediaApp")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IJwtGenerator,JwtGenerator>();

            return services;
        }
    }
}



//