using API.Core;
using API.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
    public class Gallery : BaseEntity
    {
        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("legend")]
        public string Legend { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("tags")]
        public string Tags { get; private set; }

        [BsonElement("thumb")]
        public string Thumb { get; private set; }

        [BsonElement("galleryImages")]
        public IList<string> GalleryImages { get; set; }

        public Gallery(string title, string legend, string author, string tags, EStatus status, IList<String> galleryImages, string thumb)
        {
            Title = title;
            Legend = legend;
            Author = author;
            Thumb = thumb;
            Tags = tags;
            PublishDate = DateTime.Now;
            Slug = Utils.GenerateSlug(Title);
            Status = status;
            GalleryImages = galleryImages;

            ValidateEntity();
        }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O título não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Legend, "A legenda não pode estar vazia!");

            AssertionConcern.AssertArgumentLength(Title, 90, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Legend, 40, "A legenda deve ter até 40 caracteres!");
        }
    }
}
