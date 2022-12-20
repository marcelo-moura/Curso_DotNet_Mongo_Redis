﻿using API.Core;
using API.Entities;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<News, NewsViewModel>();
            CreateMap<Video, VideoViewModel>();

            CreateMap<Result<News>, Result<NewsViewModel>>();
            CreateMap<Result<Video>, Result<VideoViewModel>>();
        }
    }
}
