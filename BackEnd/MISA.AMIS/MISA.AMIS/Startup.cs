using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.ApplicationCore.EmailServices.Entities;
using MISA.ApplicationCore.EmailServices.Interfaces;
using MISA.ApplicationCore.EmailServices.Services;
using MISA.ApplicationCore.Interfaces.Infarstructure;
using MISA.ApplicationCore.Interfaces.Service;
using MISA.ApplicationCore.Services;
using MISA.Infarstructure;

namespace MISA.AMIS
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
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();
            services.AddControllers().AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.AMIS", Version = "v1" });
            });

            // Cấu hình email config
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            // Thực hiện DI cho mail service
            services.AddScoped<IEmailSender, EmailSender>();

            // Thực hiện DI cho base
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // Thực hiện DI cho thực thể nhân viên
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            // Thực hiện DI cho thực thể khách hàng
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Thực hiện DI cho việc nhập khẩu khách hàng
            services.AddScoped<IImportCustomerRepository, ImportCustomerRepository>();
            services.AddScoped<IImportCustomerService, ImportCustomerService>();

            // Thực hiện DI cho việc xuất khẩu khách hàng
            services.AddScoped<IExportCustomerRepository, ExportCustomerRepository>();
            services.AddScoped<IExportCustomerService, ExportCustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.AMIS v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ApiCorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
