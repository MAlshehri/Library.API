using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(Guid authorId);
        Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        Task<IEnumerable<Book>> GetBooksForAuthor(Guid authorId);
        Task<Book> GetBookForAuthor(Guid authorId, Guid bookId);
        void AddBookForAuthor(Guid authorId, Book book);
        void UpdateBookForAuthor(Book book);
        void DeleteBook(Book book);
        bool Save();
    }
}
