using LangServices;
using LanguageLearner.Models;
using LanguageLearner.Models.Words;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class WordsController : Controller
    {
        private readonly IBookService BookService;
        public WordsController(IBookService BookService)
        {
            this.BookService = BookService;
        }

        public IActionResult Index()
        {
            var model = new WordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray()
            };

            return View(model);
        }

        public IActionResult AddWords()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
