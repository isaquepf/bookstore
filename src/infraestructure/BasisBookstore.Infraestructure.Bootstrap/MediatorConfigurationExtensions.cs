using Basis.Bookstore.Core.Application.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BasisBookstore.Infraestructure.Bootstrap
{
    public static class MediatorConfigurationExtensions
    {        
        public static IServiceCollection ConfigureMediatrServices(this IServiceCollection services)
        {

            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Command<>).GetTypeInfo().Assembly));


            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            //    //typeof(CreateTaskHandler).Assembly,
            //    //typeof(CreateUserHandler).Assembly,
            //    //typeof(AuthUserHandler).Assembly
            //    ));


            return services;
        }
    }
}
