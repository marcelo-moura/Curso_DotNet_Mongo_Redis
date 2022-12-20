using API.Core;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsExternalController : ControllerBase
    {
        private readonly ILogger<NewsExternalController> _logger;
        private readonly INewsService _newsService;

        public NewsExternalController(ILogger<NewsExternalController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult<List<NewsViewModel>> Get()
        {
            return _newsService.Get();
        }

        [HttpGet("{slug}")]
        public ActionResult<NewsViewModel> Get(string slug)
        {
            var news = _newsService.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<NewsViewModel>> GetPagedSearch(int page, int qtd)
        {
            return _newsService.GetPagedSearch(page, qtd);
        }
    }
}
