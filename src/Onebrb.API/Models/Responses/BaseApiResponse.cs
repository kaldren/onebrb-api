using System.Net;

namespace Onebrb.API.Models.Responses
{
    public class BaseApiResponse<T> where T : class
    {
        public string? Message { get; set; }

        public string? ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T? Data { get; set; }
    }
}
