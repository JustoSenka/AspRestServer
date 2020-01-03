using LangData.Objects;
using LangServices;
using LanguageLearner.Models;
using LanguageLearner.Models.Languages;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly IBookService BookService;
        public LanguagesController(IBookService BookService)
        {
            this.BookService = BookService;
        }

        public IActionResult Index()
        {
            var langs = BookService.GetLanguages().ToArray();
            var model = new LanguagesModel() { AvailableLanguages = langs };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditLanguages(string buttonName, string languageName)
        {
            if (buttonName == "New")
            {
                var lang = new Language(languageName);
                lang = BookService.AddLanguage(lang);
            }
            else
            {
                var id = int.Parse(buttonName.Split("_")[1]);
                var lang = BookService.GetLanguage(id);

                if (buttonName.StartsWith("Rename"))
                {
                    lang.Name = languageName;
                    BookService.UpdateLanguage(lang);
                }
                else if (buttonName.StartsWith("Delete"))
                {
                    BookService.RemoveLanguage(lang);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddNewLanguage(string name)
        {
            var lang = new Language(name);
            lang = BookService.AddLanguage(lang);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveLanguage(int id)
        {
            BookService.RemoveLanguage(BookService.GetLanguage(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RenameLanguage(int id, string name)
        {
            BookService.RemoveLanguage(BookService.GetLanguage(id));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
