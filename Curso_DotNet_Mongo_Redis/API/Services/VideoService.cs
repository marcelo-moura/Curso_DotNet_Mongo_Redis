using API.Core;
using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using AutoMapper;

namespace API.Services
{
    public class VideoService : IVideoService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Video> _videoRepository;

        public VideoService(IMapper mapper, IMongoRepository<Video> videoRepository)
        {
            _mapper = mapper;
            _videoRepository = videoRepository;
        }

        public List<VideoViewModel> Get()
        {
            return _mapper.Map<List<VideoViewModel>>(_videoRepository.Get().ToList());
        }

        public VideoViewModel Get(string id)
        {
            return _mapper.Map<VideoViewModel>(_videoRepository.Get(id));
        }

        public VideoViewModel GetBySlug(string slug)
        {
            return _mapper.Map<VideoViewModel>(_videoRepository.Get(slug));
        }

        public Result<VideoViewModel> GetPagedSearch(int page, int qtd)
        {
            return _mapper.Map<Result<VideoViewModel>>(_videoRepository.FindPagedSearch(page, qtd));
        }

        public VideoViewModel Create(VideoViewModel videoEntrada)
        {
            var entity = new Video(videoEntrada.Hat, videoEntrada.Title, videoEntrada.Author,
                                  videoEntrada.Thumbnail, videoEntrada.UrlVideo, videoEntrada.Status);

            _videoRepository.Create(entity);
            return Get(entity.Id);
        }

        public void Update(string id, VideoViewModel videoEntrada)
        {
            _videoRepository.Update(id, _mapper.Map<Video>(videoEntrada));
        }

        public void Remove(string id)
        {
            _videoRepository.Remove(id);
        }
    }
}
