using AnimalShelter.Interfaces;
using AnimalShelter.Services;
using Serilog;
using TestProject02.Daos;

namespace AnimalShelter;

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
        services.AddControllersWithViews();
        services.AddSession();
        services.AddSingleton<IAccountDao>(RegisterDao.GetInstance(Configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<IAnimalsDao>(AnimalsDao.GetInstance(Configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<IAnimalService, AnimalService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IHomeService, HomeService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseSession();
        app.UseStaticFiles();

        app.UseSerilogRequestLogging();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}