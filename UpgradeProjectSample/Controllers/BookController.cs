using UpgradeProjectSample.Books;
using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpgradeProjectSample.Controllers;
[Authorize(AuthenticationSchemes = UserTokens.AuthSchemes)]
public class BookController(IBookService bookService,
    ISearchBooks books) : Controller
{
    [Route("books/createsample")]
    public async Task<string> CreateSample(string name)
    {
        await bookService.CreateSampleAsync(name);
        return "Create Sample";
    }
    [Route("books/update")]
    public async Task<string> UpdateSample(int id)
    {
        await bookService.UpdateBookAsync(id);
        return "Update sample";
    }
    [Route("authors/update")]
    public async Task<string> UpdateAuthorSample(int id)
    {
        await bookService.UpdateAuthorAsync(id);
        return "Update Author sample";
    }
    [Route("books")]
    public async Task<List<SearchedBook>> GetBooks(string bookName, int[] languageIds, string authorName)
    {
        return await bookService.GetAsync(
            new SearchBookCriteria(bookName, languageIds ?? [], authorName));
    }
    [Route("books2")]
    public async Task<List<Book>> GetByLanguageIds(int[] languageIds)
    {
        return await bookService.GetByLanguageIdsAsync(languageIds);
    }
    [HttpGet]
    [Route("books/messages")]
    public async Task<IActionResult> GetMessage()
    {
        return Json(await books.GetAllAsync());
    }
}

