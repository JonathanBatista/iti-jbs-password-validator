using ItauIti.Challenge.PasswordValidate.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ItauIti.Challenge.Api
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
            services.AddControllers();
            services.AddPasswordValidation(config =>
            {
                config.AddShouldHaveLowercaseValidation();
                config.AddShouldHaveUppercaseValidation();
                config.AddShouldHaveNumberValidation();
                config.AddShouldHaveSpecialCharacterValidation();
                config.AddShouldHaveMinimumLengthValidation(9);
                config.AddShouldNotRepeatCharactersValidation();
                config.AddCustomValidator((string input) => !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
