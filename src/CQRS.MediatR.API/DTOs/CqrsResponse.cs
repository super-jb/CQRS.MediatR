using System.Net;

namespace CQRS.MediatR.API.DTOs
{
    public record CqrsResponse
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;

        public string ErrorMessage { get; init; }
    }
}
