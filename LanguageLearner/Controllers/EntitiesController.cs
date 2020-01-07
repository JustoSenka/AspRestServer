using LangServices;
using LanguageLearner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class EntitiesController : Controller
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly ITranslationsService TranslationsService;
        private readonly IDefinitionsService DefinitionsService;
        public EntitiesController(IWordsService WordsService, ILanguagesService LanguagesService, ITranslationsService TranslationsService, IDefinitionsService DefinitionsService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.TranslationsService = TranslationsService;
            this.DefinitionsService = DefinitionsService;
        }

        [HttpGet]
        public IActionResult Word(int id)
        {
            var model = new EntityModel() { Word = WordsService.Get(id) };
            model.AvailableLanguages = LanguagesService.GetAll().ToArray();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateWord(EntityModel model)
        {
            // var origWord = WordsService.Get(model.Word.ID);
            var newWord = model.Word;
            var didUpdateWord = true;

            if (didUpdateWord)
                return RedirectToAction("Word", new { id = newWord.ID });
            else
            {
                model.AvailableLanguages = LanguagesService.GetAll().ToArray();
                return View("Word", model);
            }
        }

        public IActionResult Definition(int id)
        {
            // bad ^
            var def = DefinitionsService.Get(id);
            var model = new EntityModel() { Definition = def };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
