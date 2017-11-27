using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var authors = _libraryRepository.GetAuthors();
            var authorsDto = Mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Json(authorsDto);
        }

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            return Json(Mapper.Map<AuthorDto>(_libraryRepository.GetAuthor(id)));
        }
    }
}