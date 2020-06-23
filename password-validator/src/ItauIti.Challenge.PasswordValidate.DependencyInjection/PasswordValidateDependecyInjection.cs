using ItauIti.Challenge.PasswordValidate.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ItauIti.Challenge.PasswordValidate.DependencyInjection
{
    public static class PasswordValidateDependecyInjection
    {
        public static void AddPasswordValidation(this IServiceCollection services, Action<ConfigurationPasswordValidator> config)
        {
            var configuration = new ConfigurationPasswordValidator();
            config(configuration);
            services.AddSingleton(configuration.Initiaze());
        }
    }
}
