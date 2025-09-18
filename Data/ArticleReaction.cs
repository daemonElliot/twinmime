using System;

namespace TwinMIME.Models
{
    public class ArticleReaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ArticleId { get; set; }
        public bool IsLike { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}