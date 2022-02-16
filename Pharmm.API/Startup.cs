using System.Globalization;
using FluentValidation.AspNetCore;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Pharmm.API.Helper;
using Pharmm.API.ServiceCollections;
using Swashbuckle.AspNetCore.SwaggerUI;
using Utility.Authorize.Helper;
using Utility.HTTP.Helper;
using Utility.JwtHelper.Helper;
using Utility.Validation.Filter;

namespace Pharmm.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public static IConfiguration StaticConfig { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors();

            services.AddControllers(config =>
            {
                config.Filters.Add(new ErrorValidationFilter());
            })
                .AddNewtonsoftJson()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                    options.ValidatorOptions.LanguageManager.Culture = new CultureInfo("Id");
                    //validasi child otomatis
                    options.ImplicitlyValidateChildProperties = true;
                    options.ValidatorOptions.DisplayNameResolver = (type, member, expression) =>
                    {
                        if (member != null)
                        {
                            return member.Name.Humanize();
                        }

                        return null;
                    };
                });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pharmm.API", Version = "v1" });

                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "Pharmm.API.xml");
                //c.IncludeXmlComments(filePath);


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                    "\r\n\r\nExample: Bearer " + JwtHelper.generateJwtToken(7,Configuration.GetValue<string>("JwtSettings:Secret")),
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}

                    }
                });
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.EnableAnnotations();
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pharmm.API", Version = "v1" });
            //});

            services.AddServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            HttpHelper.SetHttpContextAccessor(accessor);
            
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.InjectStylesheet("/swagger-themes/theme-material.css");
                    c.DocExpansion(DocExpansion.None);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmm.API v1");
                });
            //}

            //path lokasi berkas upload
            //string berkasPath = "/home/his/pharmm_data";

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(berkasPath),
            //    RequestPath = berkasPath //masking path
            //});

            ////Enable directory browsing
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(berkasPath),
            //    RequestPath = "/home/his/pharmm_data"
            //});


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

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
