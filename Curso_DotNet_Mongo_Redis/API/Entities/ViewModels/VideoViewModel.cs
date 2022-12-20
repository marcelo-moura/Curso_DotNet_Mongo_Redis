﻿using API.Entities.Enums;

namespace API.Entities.ViewModels
{
    public class VideoViewModel
    {
        public string Id { get; set; }
        public string Hat { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }
        public string UrlVideo { get; set; }
        public string Slug { get; set; }
        public EStatus Status { get; set; }
    }
}
