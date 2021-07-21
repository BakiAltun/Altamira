
namespace Vimo.Web.Api.Infrastructure.Filters
{
    using FluentValidation;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Net;
    using Vimo.ApplicationCore.Exceptions;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            var json = new JsonErrorResponse
            {
                Messages = new[] { "An error occur.Try it again." }
            };

            if (env.IsDevelopment())
            {
                json.DeveloperMessage = context.Exception.ToString();
            }

            if (context.Exception is ValidationException validationException)
            {
                json.Messages = validationException.Errors.Select(x => x.PropertyName + " : " + x.ErrorMessage).ToArray();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is NotFoundException notFoundException)
            {
                json.Messages = new[] { "Kayıt bulunamadı" };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            context.Result = new ObjectResult(json);
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public string DeveloperMessage { get; set; }
        }
    }
}