using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Refit;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails; //Refitten gelen değil asp.net core'un kendi problem details modelini kullanıyouruz.

namespace MicroserviceCourse.Shared
{
    ///Endpointlerde kullanılacak request modellerini kısaltmak için IRequestByServiceResult interface'ini oluşturuyoruz.
    public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult : IRequest<ServiceResult>;


    /// <summary>
    /// Geriye data dönmeyen işlemler için kullanılacak response model
    /// </summary>
    public class ServiceResult
    {
        //Response modelde gözükmesini istemediğimiz dataları serialize işlemine dahil etmiyoruz.
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;
        [JsonIgnore]
        public bool isFail => !IsSuccess;

        //Static factory method
        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = "The requested resource was not found"
                }
            };
        }

        //ServiceResult tipinde generic olmayan hata durumlarında kullanılacak factory methodları burayada ekliyoruz
        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                Fail = problemDetails,
                StatusCode = statusCode
            };
        }


        public static ServiceResult Error(string title, string description, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = (int)statusCode
                }
            };
        }


        public static ServiceResult Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = (int)statusCode
                }
            };
        }


        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }


        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    StatusCode = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    //küçük harf büyük harf duyarlılığını kaldırıyoruz
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult()
            {
                Fail = problemDetails,
                StatusCode = exception.StatusCode
            };
        }

    }

    /// <summary>
    /// Geriye data dönmek için kullanılacak response model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        [JsonIgnore] public string? UrlAsCreated { get; set; }

        //200 OK
        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        //201 Created
        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                Fail = problemDetails,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Sadece title ve description response model
        /// </summary>
        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = (int)statusCode
                }
            };
        }

        /// <summary>
        /// Sadece title alanını dolu olan hresponse model
        /// </summary>
        public new static ServiceResult<T> Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = (int)statusCode
                }
            };
        }

        /// <summary>
        /// Validasyon hataları için kullanacağımız response model
        /// </summary>
        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }

        /// <summary>
        /// Refit üzerinden gelen hataları yakalayıp ServiceResult tipine çevrir
        /// </summary>
        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    StatusCode = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    //küçük harf büyük harf duyarlılığını kaldırıyoruz
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                StatusCode = exception.StatusCode
            };
        }

    }
}
