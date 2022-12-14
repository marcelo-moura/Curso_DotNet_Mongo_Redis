using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly INewsService _newsService;

        public NewsController(ILogger<NewsController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult<List<NewsViewModel>> Get()
        {
            return _newsService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetNews")]
        public ActionResult<NewsViewModel> Get(string id)
        {
            var news = _newsService.Get(id);

            if (news is null)
                return NotFound();

            return news;
        }

        [HttpPost]
        public ActionResult<NewsViewModel> Create(NewsViewModel newsEntrada)
        {
            var result = _newsService.Create(newsEntrada);
            return CreatedAtRoute("GetNews", new { id = result.Id.ToString() });
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, NewsViewModel newsEntrada)
        {
            var news = _newsService.Get(id);

            if (news is null)
                return NotFound();

            _newsService.Update(id, newsEntrada);

            return CreatedAtRoute("GetNews", new { id = id }, newsEntrada);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var news = _newsService.Get(id);

            if (news is null)
                return NotFound();

            _newsService.Remove(id);

            return Ok(new { result = "Noticia deletada com sucesso!" });
        }

    }
}
