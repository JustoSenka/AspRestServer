using LangData.Objects;
using LangServices;
using LanguageLearner.Models;
using LanguageLearner.Models.Books;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService BookService;
        public BooksController(IBookService BookService)
        {
            this.BookService = BookService;
        }

        public IActionResult Index()
        {
            var books = BookService.GetBooks().ToArray();
            var bookModel = new BookIndexModel() { Books = books };

            return View(bookModel);
        }

        public IActionResult Book(int id)
        {
            var book = BookService.GetBook(id);
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
            var book = new Book(name, description);
            book = BookService.AddBook(book);
            return RedirectToAction("Book", new { id = book.ID });
        }

        //---


        public IActionResult DisplayWords()
        {
            var books = BookService.GetBooksWithData();
            var bookModel = new BookModel() { Book = books.First() };

            return View(bookModel);
        }

        public IActionResult EditWord()
        {
            var books = BookService.GetBooksWithData();
            var bookModel = new BookModel() { Book = books.First() };

            return View(bookModel);
        }

        public IActionResult AddWords()
        {
            var books = BookService.GetBooksWithData();
            var bookModel = new BookModel() { Book = books.First() };

            return View(bookModel);
        }

        [HttpPost]
        public IActionResult AddWord()
        {
            // Add word
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
