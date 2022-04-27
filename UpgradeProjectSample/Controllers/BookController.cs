using UpgradeProjectSample.Books;
using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UpgradeProjectSample.Controllers
{
    [Authorize(AuthenticationSchemes = UserTokens.AuthSchemes)]
    public class BookController: Controller
    {
        private readonly ILogger<BookController> logger;
        private readonly IBookService bookService;
        private readonly ISearchBooks books;
        public BookController(ILogger<BookController> logger,
            IBookService bookService,
            ISearchBooks books)
        {
            this.logger = logger;
            this.bookService = bookService;
            this.books = books;
        }
        [Route("books/createsample")]
        public async Task<string> CreateSample(string name)
        {
            await this.bookService.CreateSampleAsync(name);
            return "Create Sample";
        }
        [Route("books/update")]
        public async Task<string> UpdateSample(int id)
        {
            await this.bookService.UpdateBookAsync(id);
            return "Update sample";
        }
        [Route("authors/update")]
        public async Task<string> UpdateAuthorSample(int id)
        {
            await this.bookService.UpdateAuthorAsync(id);
            return "Update Author sample";
        }
        [Route("books")]
        public async Task<List<SearchedBook>> GetBooks(string bookName, int[] languageIds, string authorName)
        {
            return await this.bookService.GetAsync(new SearchBookCriteria(bookName, languageIds ?? new int[0], authorName));
        }
        [HttpGet]
        [Route("books/messages")]
        public async Task<IActionResult> GetMessage()
        {
            return Json(await this.books.GetAllAsync());
        }
        [HttpPost]
        [Route("books/messages")]
        public async Task<IActionResult> GenerateMessage([FromBody] Book book)
        {
            return Json(await this.books.GetAllAsync());
        }
    }
}

