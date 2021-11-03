using System.Net;
using CQRS.MediatR.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQRS.MediatR.API.Filters
{
    public class ResponseMappingFilter : IActionFilter
    {
        /// <summary>
        ///  Will map any ErrorStatusCode to http result status code
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult { Value: CqrsResponse cqrsResponse } && cqrsResponse.StatusCode != HttpStatusCode.OK)
            {
                context.Result = new ObjectResult(new { cqrsResponse.ErrorMessage }) { StatusCode = (int)cqrsResponse.StatusCode };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // no need to implement logic here
        }
    }
}
