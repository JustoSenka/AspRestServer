using Langs.Models;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Langs.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly ITranslationsService TranslationsService;
        private readonly IDefinitionsService DefinitionsService;
        public AdminController(IWordsService WordsService, ILanguagesService LanguagesService, ITranslationsService TranslationsService, IDefinitionsService DefinitionsService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.TranslationsService = TranslationsService;
            this.DefinitionsService = DefinitionsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Clean()
        {

            return RedirectToAction("Index");
        }

        public IActionResult AddWords()
        {

            return RedirectToAction("Index");
        }
        public IActionResult Migrate()
        {

            return RedirectToAction("Index");
        }

        public IActionResult Delete()
        {

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
