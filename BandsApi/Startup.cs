using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandsApi.DbContexts;
using BandsApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Newtonsoft.Json;

namespace BandsApi
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
            var connectionString = Configuration.GetConnectionString("Mysql8Server");
            services.AddDbContext<BandAlbumContext>(options =>
            {
                options
                    .UseMySql(Configuration.GetConnectionString("Mysql8Server"), mySqlOptions => mySqlOptions
                        .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                        .CharSet(CharSet.Utf8Mb4));
            });
            services.AddControllers();
            services.AddScoped<IBandAlbumRepository, BandAlbumRepository>();
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
