using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;

        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public void AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            _context.Authors.Add(author);

            // the repository fills the id (instead of using identity columns)
            if (!author.Books.Any()) return;
            foreach (var book in author.Books)
            {
                book.Id = Guid.NewGuid();
            }
        }

        public async void AddBookForAuthor(Guid authorId, Book book)
        {
            var author = await GetAuthor(authorId);
            if (author != null)
            {
                // if there isn't an id filled out (ie: we're not upserting),
                // we should generate one
                if (book.Id == Guid.Empty)
                {
                    book.Id = Guid.NewGuid();
                }
                author.Books.Add(book);
            }
        }

        public bool AuthorExists(Guid authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task<Author> GetAuthor(Guid authorId)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Authors.OrderBy(a => a.FirstName).ThenBy(a => a.LastName).ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return await _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToListAsync();
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
        }

        public async Task<Book> GetBookForAuthor(Guid authorId, Guid bookId)
        {
            return await _context.Books.Include(x => x.Author).FirstOrDefaultAsync(b => b.AuthorId == authorId && b.Id == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooksForAuthor(Guid authorId)
        {
            return await _context.Books.Include(x => x.Author).Where(b => b.AuthorId == authorId).OrderBy(b => b.Title).ToListAsync();
        }

        public void UpdateBookForAuthor(Book book)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
