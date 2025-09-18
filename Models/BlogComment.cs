using System;

namespace TwinMIME.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ArticleId { get; set; }
    }
}