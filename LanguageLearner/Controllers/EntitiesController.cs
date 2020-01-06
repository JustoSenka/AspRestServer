using LangServices;
using LanguageLearner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Word(int id)
        {
            var word = WordsService.Get(id);
            var model = new EntityModel() { Word = word };
            return View(model);
        }

        public IActionResult Word(int id, EntityModel model)
        {
            var word = WordsService.Get(id);
            model = model ?? new EntityModel() { Word = word };
            return View(model);
        }

        public IActionResult Definition(int id)
        {
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
