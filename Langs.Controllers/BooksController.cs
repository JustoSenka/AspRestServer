using Langs.Data.Objects;
using Langs.Models;
using Langs.Models.Books;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace Langs.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService BookService;
        private readonly IAccountService AccountService;
        private readonly ILanguagesService LanguagesService;
        public BooksController(IBooksService BookService, IAccountService AccountService, ILanguagesService LanguagesService)
        {
            this.BookService = BookService;
            this.AccountService = AccountService;
            this.LanguagesService = LanguagesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = BookService.GetWithWordCount();
            var bookModel = new BookIndexModel()
            {
                Books = books.Select(b =>
                (
                    b.ID,
                    b.Name,
                    b.Language.Name,
                    b.Description,
                    b.Words.Count())
                ).ToArray()
            };

            return View(bookModel);
        }

        [HttpGet]
        public IActionResult Book(int id)
        {
            var book = BookService.Get(id);
            if (book == default)
                ShowErrorViewForNotFoundWord(id);

            var langFrom = AccountService.GetPrefferedSecondaryLanguage();
            var langTo = AccountService.GetPrefferedNativeLanguage();
            var langAdd = AccountService.GetAdditionalLanguage();

            var bookModel = new BookModel()
            {
                Book = (book.Name, book.Language.Name, book.Description, book.WordCount),
                Languages = (langFrom.Name, langTo.Name, langAdd?.Name ?? ""),
                Words = book.Words.Select(w =>
                {
                    var from = w[langFrom];
                    var to = w[langTo];
                    var tuple = (from != null ? from.ID : 0, from?.Text ?? "", to != null ? to.ID : 0, to?.Text ?? "", 0, "");
                    if (langAdd != default)
                    {
                        var other = w[langAdd];
                        tuple.Item5 = other.ID;
                        tuple.Item6 = other.Text;
                    }
                    return tuple;
                }).ToArray()
            };

            return View(bookModel);
        }

        [HttpGet]
        public IActionResult NewBook()
        {
            var model = new NewBookModel()
            {
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CompleteNewBook(NewBookModel model)
        {
            if (!HandleErrorsBeforeCompletingBook(model))
                return View("NewBook", model);

            // Add book
            var book = new Book(model.BookName, LanguagesService.Get(model.BookLanguageID), model.BookDescription);
            book = BookService.Add(book);
            return RedirectToAction("Book", new { id = book.ID });
        }

        private bool HandleErrorsBeforeCompletingBook(NewBookModel model)
        {
            if (model.BookLanguageID == 0)
            {
                model.AlertType = AlertType.Error;
                model.AlertMessage = "Language must be set";
            }
            if (string.IsNullOrEmpty(model.BookName))
            {
                model.AlertType = AlertType.Error;
                model.AlertMessage = "Book name cannot be empty";
            }
            if (model.AlertType == AlertType.Error)
            {
                model.AvailableLanguages = LanguagesService.GetAll().ToArray();
                return false;
            }

            return true;
        }

        private IActionResult ShowErrorViewForNotFoundWord(int id)
        {
            return View("Error", new ErrorViewModel
            {
                Exception = new Exception($"Book with ID {id} not found."),
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
