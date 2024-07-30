using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VenturesLab.BacklogTasks.Core.Service;

namespace BasisBookstore.Infraestructure.Bootstrap
{
    public static class DataConfigurationExtensions
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["ConnectionStrings:BackloglistConnection"];

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

            return services;
        }
    }
}
