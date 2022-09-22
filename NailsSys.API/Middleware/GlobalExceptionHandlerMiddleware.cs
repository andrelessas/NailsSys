using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NailsSys.Core.Notificacoes;
using Newtonsoft.Json;

namespace NailsSys.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;

        public GlobalExceptionHandlerMiddleware(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // LogExcecao.GravaExcecaoTxt(exception, context);

            context.Response.ContentType = "application/json";

            var json = new ErrorReponse();

            if (exception is ExcecoesPersonalizadas)
            {
                json.StatusCode = 400;

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                json.Message = exception.Message;
            }
            else
            {
                json.StatusCode = 500;

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                json.Message = exception.Message;
                // json.MensagemBruta = exception.Message;
            }
            return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
    }
}
