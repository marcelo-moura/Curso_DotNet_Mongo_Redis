using API.Core;
using API.Entities.ViewModels;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly ILogger<GalleryController> _logger;
        private readonly IGalleryService _galleryService;

        public GalleryController(ILogger<GalleryController> logger, IGalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet]
        public ActionResult<List<GalleryViewModel>> Get()
        {
            return _galleryService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetGallery")]
        public ActionResult<GalleryViewModel> Get(string id)
        {
            var gallery = _galleryService.Get(id);

            if (gallery is null)
                return NotFound();

            return gallery;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.GetPagedSearch(page, qtd);

        [HttpPost]
        public ActionResult<GalleryViewModel> Create(GalleryViewModel galleryEntrada)
        {
            var result = _galleryService.Create(galleryEntrada);

            return CreatedAtRoute("GetGallery", new { id = result.Id.ToString() }, result);
        }


        [HttpPut("{id:length(24)}")]
        public ActionResult<GalleryViewModel> Update(string id, GalleryViewModel galleryEntrada)
        {
            var gallery = _galleryService.Get(id);

            if (gallery is null)
                return NotFound();

            galleryEntrada.PublishDate = gallery.PublishDate;

            _galleryService.Update(id, galleryEntrada);

            return CreatedAtRoute("GetGallery", new { id = id }, galleryEntrada);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var gallery = _galleryService.Get(id);

            if (gallery is null)
                return NotFound();

            _galleryService.Remove(gallery.Id);

            var result = new
            {
                message = "Galeria deletada com sucesso!"
            };

            return Ok(result);
        }
    }
}
