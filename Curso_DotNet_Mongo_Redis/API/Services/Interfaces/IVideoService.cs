﻿using API.Core;
using API.Entities.ViewModels;

namespace API.Services.Interfaces
{
    public interface IVideoService
    {
        public List<VideoViewModel> Get();
        public VideoViewModel Get(string id);
        public VideoViewModel GetBySlug(string slug);
        public Result<VideoViewModel> GetPagedSearch(int page, int qtd);
        public VideoViewModel Create(VideoViewModel videoEntrada);
        public void Update(string id, VideoViewModel videoEntrada);
        public void Remove(string id);
    }
}
