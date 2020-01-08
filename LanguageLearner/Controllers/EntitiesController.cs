using LangServices;
using LanguageLearner.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IUserService UserService;
        public EntitiesController(IWordsService WordsService, ILanguagesService LanguagesService, ITranslationsService TranslationsService,
            IDefinitionsService DefinitionsService, IUserService UserService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.TranslationsService = TranslationsService;
            this.DefinitionsService = DefinitionsService;
            this.UserService = UserService;
        }

        [HttpGet]
        public IActionResult Word(int id)
        {
            var model = new EntityModel()
            {
                Word = WordsService.Get(id),
                PreferredDefaultLanguage = UserService.GetPreferredLanguage(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult EditWord(int id)
        {
            var model = new EditEntityModel()
            {
                Word = WordsService.Get(id),
            };

            model.AvailableLanguages = LanguagesService.GetAll().ToArray();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateWord(EditEntityModel model)
        {
            var origWord = WordsService.Get(model.Word.ID);
            var newWord = model.Word;
            newWord.Translations = origWord.Translations;

            origWord.Text = newWord.Text;
            origWord.AlternateSpelling = newWord.AlternateSpelling;
            origWord.Pronunciation = newWord.Pronunciation;
            origWord.Article = newWord.Article;
            origWord.Language = newWord.Language ?? origWord.Language;

            try
            {
                WordsService.Update(origWord);
            }
            catch (Exception e)
            {
                model.AlertMessage = "Something went wrong: " + e.Message;
                model.AlertType = AlertType.Error;
            }

            if (model.AlertType == default) // Success
                return RedirectToAction("Word", new { id = newWord.ID });
            else
            {
                model.AvailableLanguages = LanguagesService.GetAll().ToArray();
                return View("EditWord", model);
            }
        }

        public IActionResult Definition(int id)
        {
            // bad ^
            var def = DefinitionsService.Get(id);
            var model = new EditEntityModel() { Definition = def };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
