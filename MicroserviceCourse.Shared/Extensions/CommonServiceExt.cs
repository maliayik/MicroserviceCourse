using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroserviceCourse.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceCourse.Shared.Extensions
{
    /// <summary>
    /// Tüm mikroservislerde kullanılacak olan genel servislerin eklenmesi için kullanılacak olan sınıf.
    /// </summary>
    public static class CommonServiceExt
    {
        /// <summary>
        /// assembly olarak verilen verilen class'ın içerisindeki servisleri DI container'a ekler.
        /// </summary>
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddScoped<IIdentityService, IdentityServiceFake>();

            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
