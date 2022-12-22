using API.Core;
using API.Entities.ViewModels;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryExternalController : ControllerBase
    {
        private readonly ILogger<GalleryExternalController> _logger;
        private readonly IGalleryService _galleryService;

        public GalleryExternalController(ILogger<GalleryExternalController> logger, IGalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.GetPagedSearch(page, qtd);


        [HttpGet("{slug}")]
        public ActionResult<GalleryViewModel> Get(string slug)
        {
            var news = _galleryService.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }
    }
}
