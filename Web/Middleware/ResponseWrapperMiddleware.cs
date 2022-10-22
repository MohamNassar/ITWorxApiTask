using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middleware
{
    internal class ResponseEnvelopeResultExecutor : ObjectResultExecutor
    {
        public ResponseEnvelopeResultExecutor(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory, ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
        {
        }

        public override Task ExecuteAsync(ActionContext context, ObjectResult result)
        {
            var response = new SuccessResponse
            {
                StatusCode = result.StatusCode,

            };
            if (result?.Value is ValidationProblemDetails problemDetails && problemDetails.Errors.Any())
            {
                response.Messages = problemDetails.Errors;
            }
            else
            {
                response.Results = result?.Value;
            }
            TypeCode typeCode = Type.GetTypeCode(result.Value.GetType());
            if (typeCode == TypeCode.Object)
                result.Value = response;

            return base.ExecuteAsync(context, result);
        }
    }
}
