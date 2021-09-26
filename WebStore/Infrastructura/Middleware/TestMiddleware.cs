using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebStore.Infrastructura.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TestMiddleware> _logger;

        public TestMiddleware(RequestDelegate next, ILogger<TestMiddleware> Logger)
        {
            _next = next;
            _logger = Logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Пример устройства промежуточного ПО

            //context.Response.WriteAsJsonAsync();

            //Предобработка

             var processing=_next(context);

             //Обработка параллельно

             await processing; // Ожидание завершения обработки следующей частью конвейера

             // Постобработка данных
        }
    }
}
