using System;

namespace Library.API.Models
{
    public class BookDto
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}