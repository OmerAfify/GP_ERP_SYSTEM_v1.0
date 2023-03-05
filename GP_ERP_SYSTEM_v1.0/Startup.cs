using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.UnitOfWork;
using Domains.Interfaces.IUnitOfWork;
using ERP_BusinessLogic.Context;
using ERP_BusinessLogic.Services;
using ERP_Domians.IServices;
using GP_ERP_SYSTEM_v1._0.Errors;
using GP_ERP_SYSTEM_v1._0.Helpers.AutomapperProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GP_ERP_SYSTEM_v1._0
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //Controllers + Self referencing loop handling
            services.AddControllers().AddNewtonsoftJson(opt=>
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           
            //swagger settings
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GP_ERP_SYSTEM_v1._0", Version = "v1" });
            });

            //DbContext settings
           services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ERP_DB_connectionString")));


            //Automapper Settings 
            services.AddAutoMapper(typeof(ApplicationMapper));

            //Unit of work Dependency Injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            //Service Config
            services.AddScoped<ISupplierOrderService, SupplierOrderService>();
            services.AddScoped<IManufacturingOrderService, ManufacturingOrderService>();
            services.AddScoped<IDistributionOrderService, DistributionOrderService>();



            //Overriding ApiController ModelState Default Behavior
            services.Configure<ApiBehaviorOptions>(opt => {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                 .SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                    return new BadRequestObjectResult(new ErrorValidationResponse { Errors = errors });

                };

            });

            //CORS Policy 
            services.AddCors(opt=> 
            { opt.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()); });


        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GP_ERP_SYSTEM_v1._0 v1"));
            }


            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

           //app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
