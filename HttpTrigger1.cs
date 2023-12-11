using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace azure_func_http_repo
{

    public class SomeClass
    {
        public string Id { get; set; }
    }

    public class HttpTrigger1
    {
        private readonly ILogger _logger;

        public HttpTrigger1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger1>();
        }

        [Function("HttpTrigger1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [FromBody] SomeClass someClass,
            FunctionContext context)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            _logger.LogInformation(
                "Serialized json object {Object}",
                JsonSerializer.Serialize(
                    someClass,
                    typeof(SomeClass),
                    new JsonSerializerOptions {WriteIndented = true}));

            return await Task.FromResult(new OkObjectResult(someClass.Id = "50"));
        }
    }
}
