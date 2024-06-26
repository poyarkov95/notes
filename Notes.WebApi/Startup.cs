using System;
using System.IO;
using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.Model.Profile;
using BusinessLogic.Service.Implementation;
using BusinessLogic.Service.Interface;
using BusinessLogic.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Notes.WebApi.Middleware;
using Postgres.Entity;
using Postgres.Repository.Implementation;
using Postgres.Repository.Interface;

namespace Notes.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            
            services.AddSingleton<DapperContext>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            //validation
            services.AddScoped<IValidator<NoteModel>, NoteValidator>();
            services.AddScoped<IValidator<UserRegisterModel>, UserRegisterValidator>();

            services.AddSingleton(
                provider => new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new NotesModelProfile());
                        cfg.AddProfile(new UserModelProfile());
                    })
                    .CreateMapper());

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(
                        new StringEnumConverter(new CamelCaseNamingStrategy()));
                });
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            
            services.AddAuthentication("BasicSchema")
                .AddBasicAuthentication("BasicSchema", "BasicSchema", o => { });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = string.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
            });

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}