using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.EndPoints;

namespace MOS.Api.Controllers
{
    [Route(Endpoints.ApiEndpoint + "[Controller]/")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        protected ILogger<T> _logger;
        protected IConfiguration _configuration;
        public BaseController(IConfiguration configuration, ILogger<T> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
    }
}
