using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public BooksController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooksForAuthor(Guid authorId)
        {
            var authorBooks = await _libraryRepository.GetBooksForAuthor(authorId);
            if (authorBooks == null)
                return NotFound();
            return Ok(Mapper.Map<IEnumerable<BookDto>>(authorBooks));
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookForAuthor(Guid authorId, Guid bookId)
        {
            var authorBook = await _libraryRepository.GetBookForAuthor(authorId, bookId);
            if (authorBook == null)
                return NotFound();
            return Ok(Mapper.Map<BookDto>(authorBook));
        }

        [HttpPost()]
        public IActionResult CreateBookForAuthor(Guid authorId, [FromBody] CreateBookDto model)
        {
            if(model == null || !_libraryRepository.AuthorExists(authorId))
            {
                return BadRequest();
            }

            var book = Mapper.Map<Book>(model);
            _libraryRepository.AddBookForAuthor(authorId, book);

            if(!_libraryRepository.Save())
            {
                throw new Exception("Error");
            }

            return Created($"api/authors/{authorId}/books/{book.Id}", Mapper.Map<BookDto>(book));

        }
    }
}