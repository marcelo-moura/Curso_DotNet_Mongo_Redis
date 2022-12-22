using API.Entities;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class ViewModelToEntityProfile : Profile
    {
        public ViewModelToEntityProfile()
        {
            CreateMap<NewsViewModel, News>()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<VideoViewModel, Video>()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<GalleryViewModel, Gallery>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
