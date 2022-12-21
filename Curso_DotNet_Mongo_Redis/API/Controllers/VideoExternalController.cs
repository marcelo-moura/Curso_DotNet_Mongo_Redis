using API.Core;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoExternalController : ControllerBase
    {
        private readonly ILogger<VideoExternalController> _logger;
        private readonly IVideoService _videoService;

        public VideoExternalController(ILogger<VideoExternalController> logger, IVideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult<List<VideoViewModel>> Get()
        {
            return _videoService.Get();
        }

        [HttpGet("{slug}")]
        public ActionResult<VideoViewModel> Get(string slug)
        {
            var news = _videoService.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<VideoViewModel>> GetPagedSearch(int page, int qtd)
        {
            return _videoService.GetPagedSearch(page, qtd);
        }
    }
}
