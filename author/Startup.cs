using author.service.Authors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace author
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime and is used to register services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options => options.ReturnHttpNotAcceptable = true
                ); // Add MVC support
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "author", Version = "v1" });
            }

            );

            services.AddScoped<IAUthorRepo, AuthorService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //JWT Authentication 
            var key = Encoding.ASCII.GetBytes("076chanuka1003067_ThisIsLongSecretKey!!");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

 .AddJwtBearer(x =>
 {
     x.RequireHttpsMetadata = false;
     x.SaveToken = true;
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(key)
     };
 });

            services.AddAuthorization();
        }






        // This method gets called by the runtime and is used to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed error page in dev
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "author v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //for JWT 
            app.UseAuthentication();
            //

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
