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
        public BooksController(IBooksService BookService)
        {
            this.BookService = BookService;
        }

        public IActionResult Index()
        {
            var books = BookService.GetWithWordCount();
            var bookModel = new BookIndexModel()
            {
                Books = books.Select(b =>
                (
                    b.ID,
                    b.Name,
                    b.Description,
                    b.Words.Count())
                ).ToArray()
            };

            return View(bookModel);
        }

        public IActionResult Book(int id)
        {
            var book = BookService.Get(id);
            /*if (book == default)
                return RedirectToAction("Error");*/

            var bookModel = new BookModel() { Book = book };
            return View(bookModel);
        }

        public IActionResult NewBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CompleteNewBook(string name, string description)
        {
            // TODO: Lang is now mandatory, please fix
            var book = new Book(name, null, description);
            book = BookService.Add(book);
            return RedirectToAction("Book", new { id = book.ID });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
