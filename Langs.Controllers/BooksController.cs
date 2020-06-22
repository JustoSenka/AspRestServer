using Langs.Data.Objects;
using Langs.Models;
using Langs.Models.Books;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Langs.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBooksService BooksService;
        private readonly IAccountService AccountService;
        private readonly ILanguagesService LanguagesService;
        private readonly IMasterWordsService MasterWordsService;
        public BooksController(IBooksService BooksService, IAccountService AccountService, ILanguagesService LanguagesService, IMasterWordsService MasterWordsService)
        {
            this.BooksService = BooksService;
            this.AccountService = AccountService;
            this.LanguagesService = LanguagesService;
            this.MasterWordsService = MasterWordsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = BooksService.GetAllWithWordCount();
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
            var book = BooksService.Get(id);
            if (book == default)
                ShowErrorViewForNotFoundBook(id);

            var langFrom = AccountService.GetPrefferedSecondaryLanguage();
            var langTo = AccountService.GetPrefferedNativeLanguage();
            var langAdd = AccountService.GetAdditionalLanguage();

            var bookModel = new BookModel()
            {
                Book = (book.ID, book.Name, book.Language.Name, book.Description, book.WordCount),
                Languages = (langFrom.Name, langTo.Name, langAdd?.Name ?? ""),
                WordsToAdd = MasterWordsService.GetAllWithWords()
                    .Except(book.Words)
                    .Select(w => (w.ID, Word: w[book.LanguageID])) // Return ID to master word specifically, since it will be used for linking, while text is translated one
                    .Where(t => t.Word != default)
                    .Select(t => (t.ID, t.Word.Text))
                    .ToArray(),
                Words = book.Words.Select(w =>
                    {
                        var from = w[langFrom];
                        var to = w[langTo];
                        var tuple = (w.ID, from?.ID ?? 0, from?.Text ?? "", to?.ID ?? 0, to?.Text ?? "", 0, "");
                        if (langAdd != default)
                        {
                            var other = w[langAdd];
                            tuple.Item6 = other?.ID ?? 0;
                            tuple.Item7 = other?.Text ?? "";
                        }
                        return tuple;
                    }).ToArray()
            };

            return View(bookModel);
        }

        [HttpGet]
        public IActionResult NewBook()
        {
            var model = new EditBookModel()
            {
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
                BookLanguageID = AccountService.GetLogonAccount().LearningLanguageID ?? 0,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CompleteNewBook(EditBookModel model)
        {
            if (!HandleErrorsBeforeCompletingBook(model))
                return View("NewBook", model);

            // Add book
            var book = new Book(model.BookName, LanguagesService.Get(model.BookLanguageID), model.BookDescription);
            book = BooksService.Add(book);
            return RedirectToAction("Book", new { id = book.ID });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = BooksService.Get(id);
            if (book == default)
                ShowErrorViewForNotFoundBook(id);

            var model = new EditBookModel()
            {
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
                BookID = id,
                BookName = book.Name,
                BookDescription = book.Description,
                BookLanguageID = book.Language.ID,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveBook(EditBookModel model)
        {
            if (!HandleErrorsBeforeCompletingBook(model))
                return View("Edit", model);

            var book = BooksService.Get(model.BookID);
            if (book == default)
                ShowErrorViewForNotFoundBook(model.BookID);

            // Add book

            book.Name = model.BookName;
            book.Description = model.BookDescription;
            book.Language = LanguagesService.Get(model.BookLanguageID);
            BooksService.Update(book);

            return RedirectToAction("Book", new { id = book.ID });
        }

        [HttpPost]
        public IActionResult DeleteBook(int id)
        {
            var book = BooksService.Get(id);
            if (book == default)
                ShowErrorViewForNotFoundBook(id);

            BooksService.Remove(book);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Called from link in Books/Book view to remove word
        /// Redirects back to book main page.
        /// </summary>
        /// <param name="id">Master word id to remove</param>
        /// <param name="bookId">Book id which has the master word</param>
        public IActionResult RemoveWord(int id, int bookId)
        {
            var book = BooksService.Get(bookId);
            if (book == default)
                ShowErrorViewForNotFoundBook(bookId);

            try
            {
                book.RemoveWord(id);
                BooksService.Update(book);
            }
            catch
            {
                return ShowErrorViewForNotFoundWord(id);
            }

            return RedirectToAction("Book", new { id = bookId });
        }

        /// <summary>
        /// Add word to selected book. Called from Books/Book view.
        /// Redirects back to book main page.
        /// </summary>
        /// <param name="id">Master word id to add</param>
        /// <param name="bookId">Book id which to add the master word</param>
        public IActionResult AddWord(int id, int bookId)
        {
            var book = BooksService.Get(bookId);
            if (book == default)
                ShowErrorViewForNotFoundBook(bookId);

            var word = MasterWordsService.Get(id);
            if (word == default)
                ShowErrorViewForNotFoundWord(id);

            book.AddWord(word);
            BooksService.Update(book);

            return RedirectToAction("Book", new { id = bookId });
        }

        private bool HandleErrorsBeforeCompletingBook(EditBookModel model)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
