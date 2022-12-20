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
        }
    }
}
