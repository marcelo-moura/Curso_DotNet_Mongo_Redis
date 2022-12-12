using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }
    }
}
