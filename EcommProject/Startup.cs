using ecommer.Mapper;
using EcommProject.Data;
using EcommProject.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using EcommProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace ecommer
{
    public static class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            ConfigureServices(builder);
            var app = builder.Build();

           
           /* if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                SeedData(app);
            }
            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<DataSeeder>();
                    service.seed();
                }

            }*/
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
           

            builder.Services.AddDbContext<EcommStoreContext>
                (options => options.UseSqlServer("Server=DESKTOP-2I9MOJ6;Database=EcommerSt;Integrated Security=True"));
            builder.Services.AddControllers();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();

            builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddTransient<DataSeeder>();
            builder.Services.AddAutoMapper(typeof(Startup));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:7027";
            });
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();

        }
    }
}
