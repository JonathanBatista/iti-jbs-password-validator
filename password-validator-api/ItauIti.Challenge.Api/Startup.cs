using ItauIti.Challenge.PasswordValidate.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Itau Iti - Sistema Validador de senhas",
                        Version = "v1",
                        Description = " ItauIti Api - Serviços para validação de senha",
                        Contact = new OpenApiContact
                        {
                            Name = "Jonathan Batista",
                            Email = "jonathan.silbatista@gmail.com"
                        }
                    });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Itau Iti - Sistema Validador de senhas - V1");
            });

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
