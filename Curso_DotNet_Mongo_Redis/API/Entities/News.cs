using API.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
    public class News : BaseEntity
    {
        [BsonElement("hat")]
        public string Hat { get; private set; }

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("text")]
        public string Text { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("img")]
        public string Img { get; private set; }

        [BsonElement("link")]
        public string Link { get; private set; }

        [BsonElement("publishDate")]
        public DateTime PublishDate { get; private set; }

        [BsonElement("active")]
        public EStatus Status { get; private set; }

        public News(string hat, string title, string text, string author, string img, string link, EStatus status)
        {
            Hat = hat;
            Title = title;
            Text = text;
            Author = author;
            Img = img;
            Link = link;
            PublishDate = DateTime.Now;
            Status = status;
        }

        public EStatus ChangeStatus(EStatus status) => status switch
        {
            EStatus.Active => EStatus.Active,
            EStatus.Inactive => EStatus.Inactive,
            EStatus.Draft => EStatus.Draft,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
