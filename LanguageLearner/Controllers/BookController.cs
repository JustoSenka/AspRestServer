using LangData.Objects;
using LangServices;
using LanguageLearner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService BookService;
        public BookController(IBookService BookService)
        {
            this.BookService = BookService;
        }
        
        public IActionResult Index()
        {
            var books = BookService.GetBooks();
            var bookModel = new BookModel() { Book = books.First() };

            return View(bookModel);
        }

        public IActionResult DisplayWords()
        {
            var books = BookService.GetBooks();
            var bookModel = new BookModel() { Book = books.First() };

            return View(bookModel);
        }

        public IActionResult AddWords()
        {
            var books = BookService.GetBooks();
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
