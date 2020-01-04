using LangData.Objects;
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
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return View(model);
        }

        // Redirecting to Show so language ids appear inside url.
        public IActionResult ShowRedirect(WordsModel WordsModel)
        {
            return RedirectToAction("Show", new { WordsModel.LanguageFromID, WordsModel.LanguageToID });
        }
        public IActionResult Show(int LanguageFromID, int LanguageToID)
        {
            var model = new WordsModel()
            {
                LanguageFromID = LanguageFromID,
                LanguageToID = LanguageToID,

                From = BookService.GetLanguage(LanguageFromID),
                To = BookService.GetLanguage(LanguageToID),

                AvailableLanguages = BookService.GetLanguages().ToArray(),
                Definitions = new Definition[] { new Definition() { Text = "example def" } },
                Words = new Word[] { new Word() { Text = "example word" } },
            };

            return View(model);
        }

        public IActionResult AddWords()
        {
            var model = new AddWordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddWordsSingle()
        {
            var model = new AddWordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return RedirectToAction("AddWords"); 
        }

        [HttpPost]
        public IActionResult AddWordsArea()
        {
            var model = new AddWordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return RedirectToAction("AddWords");
        }

        [HttpPost]
        public IActionResult AddWordsSeparateArea()
        {
            var model = new AddWordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return RedirectToAction("AddWords");
        }

        [HttpPost]
        public IActionResult AddWordsSeparateAreaDescription()
        {
            var model = new AddWordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return RedirectToAction("AddWords");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
