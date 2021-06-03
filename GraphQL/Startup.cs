using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using ConferencePlanner.GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConferencePlanner.GraphQL
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // This code registers the ApplicationDbContext service so it can be injected into resolvers.
            services
               // running a query to fetch the same data three times in parallel uses the same DBContext and can lead to the exception by the DBContext.

               //We have the option to either set the execution engine to execute serially (?sequentially I think), which is terrible for performance or to use DBContext pooling in combination with field scoped services.

               //Using DBContext pooling allows us to issue a DBContext instance for each field needing one.But instead of creating a DBContext instance for every field and throwing it away after using it, we are renting so fields and requests can reuse it(Pooling).

               //.AddDbContext<ApplicationDbContext>( // instead of this, use pooled version for better performance
               .AddPooledDbContextFactory<ApplicationDbContext>( // By default the DBContext pool will keep 128 DBContext instances in its pool.
                                                                 //options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=conferences;Trusted_Connection=True;MultipleActiveResultSets=true;"));
                options => options.UseSqlite("Data Source=conferences.db")
            );


            // setup GraphQL and register our query and mutation root types
            services
               .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<SpeakerType>()
                .AddDataLoader<SpeakerByIdDataLoader>()
                .AddDataLoader<SessionByIdDataLoader>()
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // configure our GraphQL middleware so that the server knows how to execute GraphQL requests
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}