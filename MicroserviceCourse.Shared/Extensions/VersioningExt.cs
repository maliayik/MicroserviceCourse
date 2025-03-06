using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceCourse.Shared.Extensions
{
    public static class VersioningExt
    {
        /// <summary>
        /// Versiyonlama işlemleri için gerekli ayarlamaları yapar
        /// </summary>
        public static IServiceCollection AddVersioingExt(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0); // default versiyon
                options.AssumeDefaultVersionWhenUnspecified = true; // versiyon belirtilmediğinde default versiyonu alır
                options.ReportApiVersions = true; // api version headerda gösterir
                options.ApiVersionReader =
                    new UrlSegmentApiVersionReader(); // versiyon bilgisini urldeki segmentten okur

                //birden fazla versiyon okuma yöntemi hem headerdan hemde querystringden okur
                //options.ApiVersionReader =
                //    ApiVersionReader.Combine(new HeaderApiVersionReader(), new QueryStringApiVersionReader());


            }).AddApiExplorer(options =>
            {
                //swaggerda versiyon bilgisini gösterir
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }


        /// <summary>
        /// Api version middleware
        /// </summary>
        public static ApiVersionSet AddVersionSetExt(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .HasApiVersion(new ApiVersion(2, 0))
                .ReportApiVersions()
                .Build();
            return apiVersionSet;
        }
    }
}
