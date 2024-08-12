using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Service;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Basis.Bookstore.Infraestructure.Bootstrap
{
    public static class DataConfigurationExtensions
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["ConnectionStrings:BookstoreConnection"];

            services.AddDbContext<BookstoreContext>(options =>
                options.UseSqlite(connection)
            );

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IPurchaseMethodRepository, PurchaseMethodRepository>();

            services.AddTransient<IBookAuthorRepository, BookAuthorRepository>();
            services.AddTransient<IBookPurchaseMethodRepository, BookPurchaseMethodRepository>();
            services.AddTransient<IBookSubjectRepository, BookSubjectRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IFillDataService, FillDataService>();
        

            return services;
        }       


        public static IServiceProvider ApplyMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BookstoreContext>();
                db.Database.Migrate();
            }

            return services;
        }
    }
}
