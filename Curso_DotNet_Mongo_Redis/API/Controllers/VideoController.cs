using API.Entities.ViewModels;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IVideoService _videoService;

        public VideoController(ILogger<VideoController> logger, IVideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult<List<VideoViewModel>> Get()
        {
            return _videoService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetVideo")]
        public ActionResult<VideoViewModel> Get(string id)
        {
            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            return video;
        }

        [HttpPost]
        public ActionResult<VideoViewModel> Create(VideoViewModel videoEntrada)
        {
            var result = _videoService.Create(videoEntrada);
            return CreatedAtRoute("GetVideo", new { id = result.Id.ToString() });
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, VideoViewModel videoEntrada)
        {
            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            _videoService.Update(id, videoEntrada);

            return CreatedAtRoute("GetVideo", new { id = id }, videoEntrada);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            _videoService.Remove(id);

            return Ok(new { result = "Video deletado com sucesso!" });
        }

    }
}
