using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Models.SeedData;
using Npgsql;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace UpgradeProjectSample.Books
{
    public class BookService: IBookService
    {
        private readonly SampleContext context;
        private readonly ILogger<BookService> logger;
        private readonly IAuthors authors;
        private readonly IBooks books;
        private readonly ISearchBooks searchBooks;
        public BookService(SampleContext context,
            ILogger<BookService> logger,
            IAuthors authors, IBooks books,
            ISearchBooks searchBooks)
        {
            this.context = context;
            this.logger = logger;
            this.authors = authors;
            this.books = books;
            this.searchBooks = searchBooks;
        }
        public async Task CreateSampleAsync(string bookName)
        {
            using var transaction = await this.context.Database.BeginTransactionAsync();
            try
            {
                var author = await this.authors.GetOrCreateAsync("sample author");
                await this.books.CreateAsync(author,
                    new Book
                    {
                        Name = bookName,
                        LanguageId = LanguageData.GetEnglish().Id,
                    });
                await transaction.CommitAsync();
            }
            catch(NpgsqlException ex)
            {
                logger.LogError(ex.Message);
                await transaction.RollbackAsync();
            }
        }
        public async Task UpdateBookAsync(int id)
        {
            using var transaction = await this.context.Database.BeginTransactionAsync();
            
            try
            {
                var target = await this.books.GetTrackedBookAsync(id);
                if(target == null)
                {
                    return;
                }
                target.LastUpdateDate = DateTime.Now.ToUniversalTime();
                await this.context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch(NpgsqlException ex)
            {
                logger.LogError(ex.Message);
                await transaction.RollbackAsync();
            }
        }
        public async Task UpdateAuthorAsync(int id)
        {
            using var transaction = await this.context.Database.BeginTransactionAsync();
            try
            {
                var target = await this.authors.GetTrackedAuthorAsync(id);
                if(target == null)
                {
                    return;
                }
                target.Id += 1;
                target.Name += $"_{id}";
                await this.context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(NpgsqlException ex)
            {
                logger.LogError(ex.Message);
                await transaction.RollbackAsync();
            }
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await this.searchBooks.GetAllAsync();
        }
        public async Task<List<SearchedBook>> GetAsync(SearchBookCriteria criteria)
        {
            return await this.searchBooks.GetAsync(criteria);
        }
        public async Task<List<Book>> GetByLanguageIdsAsync(int[] languageIds)
        {
            return await this.searchBooks.GetByLanguageIdsAsync(languageIds);
        }
    }
}
