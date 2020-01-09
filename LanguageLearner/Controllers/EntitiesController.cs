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

        #region Words

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

            model.LanguageID = model.Word.Language.ID;
            model.AvailableLanguages = LanguagesService.GetAll().ToArray();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateWord(EditEntityModel model)
        {
            var origWord = WordsService.Get(model.Word.ID);
            origWord.Text = model.Word.Text;
            origWord.AlternateSpelling = model.Word.AlternateSpelling;
            origWord.Pronunciation = model.Word.Pronunciation;
            origWord.Article = model.Word.Article;
            origWord.Language = LanguagesService.Get(model.LanguageID);

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
                return RedirectToAction("Word", new { id = model.Word.ID });
            else
            {
                model.AvailableLanguages = LanguagesService.GetAll().ToArray();
                return View("EditWord", model);
            }
        }

        #endregion // Words

        #region Definitions

        [HttpGet]
        public IActionResult Definition(int id)
        {
            var model = new EntityModel()
            {
                Definition = DefinitionsService.Get(id),
                PreferredDefaultLanguage = UserService.GetPreferredLanguage(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult EditDefinition(int id)
        {
            var model = new EditEntityModel()
            {
                Definition = DefinitionsService.Get(id),
            };

            model.LanguageID = model.Definition.Language.ID;
            model.AvailableLanguages = LanguagesService.GetAll().ToArray();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateDefinition(EditEntityModel model)
        {
            var origDef = DefinitionsService.Get(model.Definition.ID);
            origDef.Text = model.Definition.Text;
            origDef.Description = model.Definition.Description;
            origDef.Language = LanguagesService.Get(model.LanguageID);

            try
            {
                DefinitionsService.Update(origDef);
            }
            catch (Exception e)
            {
                model.AlertMessage = "Something went wrong: " + e.Message;
                model.AlertType = AlertType.Error;
            }

            if (model.AlertType == default) // Success
                return RedirectToAction("Definition", new { id = model.Definition.ID });
            else
            {
                model.AvailableLanguages = LanguagesService.GetAll().ToArray();
                return View("EditDefinition", model);
            }
        }

        #endregion // Definitions

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
