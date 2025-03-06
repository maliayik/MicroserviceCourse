using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MicroserviceCourse.Shared.Extensions
{
    public static class EndpointResultExt
    {
        /// <summary>
        /// Generic ServiceResult dönmeye yarayan extension method
        /// </summary>
        public static IResult ToGenericResult<T>(this ServiceResult<T> result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.OK => Results.Ok(result.Data),
                HttpStatusCode.Created => Results.Created(result.UrlAsCreated, result.Data),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!),
            };
        }

        //Generic olmayan hali
        public static IResult ToGenericResult(this ServiceResult result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!),
            };
        }
    }
}
