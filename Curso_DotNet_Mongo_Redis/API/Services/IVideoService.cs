﻿using API.Entities.ViewModels;

namespace API.Services
{
    public interface IVideoService
    {
        public List<VideoViewModel> Get();
        public VideoViewModel Get(string id);
        public VideoViewModel GetBySlug(string slug);
        public VideoViewModel Create(VideoViewModel newsEntrada);        
        public void Update(string id, VideoViewModel newsEntrada);
        public void Remove(string id);
    }
}
