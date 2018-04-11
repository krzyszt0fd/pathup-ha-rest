using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Complete.Repository;

namespace NotesApp.Complete
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
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IDynamoDBContext, DynamoDBContext>();
            services.AddMvc();
            //https://github.com/aws/aws-sdk-net/issues/624
            AWSConfigsDynamoDB.Context.TableNamePrefix = Configuration["dynamoDBContextTableNamePrefix"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use((ctx, next) =>
            {
                KeyValuePair<string, string> serverName = Configuration.AsEnumerable().FirstOrDefault(c => c.Key == "serverName");
                if (serverName.Key != null)
                {
                    ctx.Response.Headers.Add("server-name", serverName.Value);
                }
                return next.Invoke();
            });


            app.UseMvc();
        }
    }
}
