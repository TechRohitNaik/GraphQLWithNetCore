using GraphQL.Server.Ui.Voyager;
using GraphQLAPI.DBContext;
using GraphQLAPI.GraphQLArtifacts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //private readonly ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("AppConnectionString")));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext, ILogger<Startup> _logger)
        {
            //Added for executing migration scripts on database
            try
            {
                _logger.LogInformation("Started running migration scripts");
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                if (ex.StackTrace != null)
                    _logger.LogError(ex.StackTrace);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapRazorPages();
            });

            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
    }
}
