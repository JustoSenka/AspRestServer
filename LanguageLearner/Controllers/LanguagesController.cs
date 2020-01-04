using LangData.Objects;
using LangServices;
using LanguageLearner.Models;
using LanguageLearner.Models.Languages;
using LanguageLearner.Utilities;
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

        public IActionResult ErrorIndex(string errorMsg)
        {
            var langs = BookService.GetLanguages().ToArray();
            var model = new LanguagesModel() { AvailableLanguages = langs, AlertMessage = errorMsg, AlertType = AlertType.Error };

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult EditLanguages(string buttonName, string languageName)
        {
            if (buttonName == "New")
            {
                if (string.IsNullOrEmpty(languageName))
                    return RedirectToAction("ErrorIndex", new { errorMsg = "Language name cannot be empty." });

                var lang = new Language(languageName);
                lang = BookService.AddLanguage(lang);
            }
            else
            {                
                var id = int.Parse(buttonName.Split("_")[1]);
                var lang = BookService.GetLanguage(id);

                if (buttonName.StartsWith("Rename"))
                {
                    if (string.IsNullOrEmpty(languageName))
                        return RedirectToAction("ErrorIndex", new { errorMsg = "Language name cannot be empty." });

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
