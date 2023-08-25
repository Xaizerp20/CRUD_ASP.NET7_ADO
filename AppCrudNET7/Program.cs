using AppCrudNET7.Models;
using AppCrudNET7.Repositories.Contracts;
using AppCrudNET7.Repositories.Implementation;


namespace AppCrudNET7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // Register the scoped service for accessing department-related data.
            // Whenever an instance of IGenericRepository<Department> is required,
            // an instance of DepartmentRepository will be provided within the scope
            // of a single HTTP request. This allows the application to work with
            // department operations using the functionality of DepartmentRepository.

            builder.Services.AddScoped<IGenericRepository<Department>, DepartmentRepository>();
            builder.Services.AddScoped<IGenericRepository<Employee>, EmployeeRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}