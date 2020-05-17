using Langs.Data.Objects;
using Langs.Models;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace Langs.Controllers
{
    public class EntitiesController : Controller
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly IUserService UserService;
        public EntitiesController(IWordsService WordsService, ILanguagesService LanguagesService, IUserService UserService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
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
            return ViewEditWordModel(model);
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

            return model.AlertType == default ? RedirectToAction("Word", new { id = model.Word.ID }) : ViewEditWordModel(model);
        }

        [HttpPost]
        public IActionResult DeleteWord(EditEntityModel model)
        {
            try
            {
                WordsService.Remove(WordsService.Get(model.Word.ID));
            }
            catch (Exception e)
            {
                model.AlertMessage = "Something went wrong: " + e.Message;
                model.AlertType = AlertType.Error;
            }

            return model.AlertType == default ? RedirectToAction("Index", "Words") : ViewEditWordModel(model);
        }

        [HttpPost]
        public IActionResult AddTranslationToWord(EditEntityModel model, int id)
        {
            try
            {
                var word = WordsService.Get(model.Word.ID);
                var translation = WordsService.Get(id);
                word.AddTranslation(translation);

                WordsService.Update(word);

                model.AlertMessage = "Translation successfully added: " + translation.Text;
                model.AlertType = AlertType.Success;
            }
            catch (Exception e)
            {
                model.AlertMessage = "Something went wrong: " + e.Message;
                model.AlertType = AlertType.Error;
            }

            return ViewEditWordModel(model);
        }

        [HttpPost]
        public IActionResult RemoveTranslationFromWord(EditEntityModel model, int id)
        {
            TryRemoveTranslationById(model, id);
            return ViewEditWordModel(model);
        }

        private IActionResult ViewEditWordModel(EditEntityModel model)
        {
            model.Word.Language = LanguagesService.Get(model.LanguageID);
            model.AvailableLanguages = LanguagesService.GetAll().ToArray();

            // During submit, the translations array is lost because they are not in the form
            model.Word.MasterWord = WordsService.Get(model.Word.ID).MasterWord;

            return View("EditWord", model);
        }

        #endregion // Words

        private void TryRemoveTranslationById(EditEntityModel model, int id)
        {
            try
            {
                var translation = model.Word.Translations.First(w => w.ID == model.Word.ID);
                model.Word.RemoveTranslation(translation);
                WordsService.Update(model.Word);
                
                model.ExpandTranslationList = true;
            }
            catch (Exception e)
            {
                model.AlertMessage = "Something went wrong: " + e.Message;
                model.AlertType = AlertType.Error;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
