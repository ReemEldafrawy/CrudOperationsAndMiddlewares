
using CrudOperations.Data;
using CrudOperations.Filiters;
using CrudOperations.MiddleWares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using CrudOperations.Authenticaion;

namespace CrudOperations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("Config.Json");
            builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("Basic ",null);
            //var attachments = builder.Configuration.GetSection("Attachments").Get<AttatchmentsOptions>();
            //builder.Services.AddSingleton(attachments);

            //var attachmentoptions = new AttatchmentsOptions();
            //builder.Configuration.GetSection("Attachments").Bind(attachmentoptions);
            //builder.Services.AddSingleton(attachmentoptions);

            builder.Services.Configure<AttatchmentsOptions>(builder.Configuration.GetSection("Attachments"));
            builder.Services.AddLogging(
                
                builder=>builder.AddDebug()
                
                );


            // Add services to the container.
            //Global action
            builder.Services.AddControllers(/*options=>options.Filters.Add<LogActionFiliter>()*/);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDBContext>(Config => Config.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
        var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
app.UseMiddleware<RateLimitingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ProfilingMaddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
