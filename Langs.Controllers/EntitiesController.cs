using Langs.Data.Objects;
using Langs.Models;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Langs.Controllers
{
    public class EntitiesController : BaseController
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly IAccountService AccountService;
        private readonly IBooksService BooksService;
        private readonly IMasterWordsService MasterWordsService;
        public EntitiesController(IWordsService WordsService, ILanguagesService LanguagesService,
            IAccountService AccountService, IBooksService BooksService, IMasterWordsService MasterWordsService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.AccountService = AccountService;
            this.BooksService = BooksService;
            this.MasterWordsService = MasterWordsService;
        }

        [HttpGet]
        public IActionResult Word(int id, int bookId)
        {
            var word = WordsService.Get(id);
            if (word == default)
                return ShowErrorViewForNotFoundWord(id);

            var master = MasterWordsService.Get(word.MasterWordID);
            var bookIDs = master.BookIDs.ToHashSet();

            var model = FillUpWordModel(new EntityModel(), word);
            model.BooksToAddWordTo = BooksService.GetAll()
                .Select(b => (b.Name, b.ID, b.LanguageID))
                .Where(b => b.LanguageID == word.LanguageID)
                .Where(b => !bookIDs.Contains(b.ID))
                .Select(b => (b.Name, b.ID))
                .ToArray();

            model.AddedToBooks = master.Books.Select(b => (b.Name, b.ID)).ToArray();

            // Display success alert if it's a redirection from AddToBook
            if (bookId != 0)
            {
                var book = BooksService.Get(bookId);
                model.AlertMessage = $"Word '{word.Text}' was successfully added to book '{book?.Name}'";
                model.AlertType = AlertType.Success;
            }

            return View("Word", model);
        }

        [HttpGet]
        public IActionResult EditWord(int id)
        {
            var word = WordsService.Get(id);
            if (word == default)
                return ShowErrorViewForNotFoundWord(id);

            var model = (EditEntityModel)FillUpWordModel(new EditEntityModel(), word);

            return ViewEditWordModel(model, word);
        }

        [HttpGet]
        public IActionResult NewTranslation(int id)
        {
            var word = WordsService.Get(id);
            if (word == default)
                return ShowErrorViewForNotFoundWord(id);

            var model = new NewWordModel();
            model.TranslationForID = id;

            model.AvailableLanguages = LanguagesService.GetAll()
                .Where(lang => word[lang.ID] == default && lang.ID != word.LanguageID).ToArray();

            return View(model);
        }

        [HttpGet, HttpPost]
        public IActionResult AddToBook(int wordId, int bookId)
        {
            var word = WordsService.Get(wordId);
            if (word == default)
                return ShowErrorViewForNotFoundWord(wordId);

            var book = BooksService.Get(bookId);
            if (book == default)
                return ShowErrorViewForNotFoundBook(bookId);

            book.AddWord(word.MasterWord);
            BooksService.Update(book);

            return RedirectToAction(nameof(Word), new { id = word.ID, bookId = book.ID });
        }


        [HttpPost]
        public IActionResult FinishNewTranslation(NewWordModel model)
        {
            var word = WordsService.Get(model.TranslationForID);
            if (word == default)
                return ShowErrorViewForNotFoundWord(model.TranslationForID);

            var language = LanguagesService.Get(model.WordLanguageID);

            var newWord = new Word(word.MasterWord, model.WordText, language);
            EditEntityModel.FillUpWord(model, newWord, language);

            TryOrAlert(model, () => WordsService.Add(newWord));

            if (model.AlertType == default)
            {
                return RedirectToAction("Word", new { id = newWord.ID });
            }
            else
            {
                model.AvailableLanguages = LanguagesService.GetAll()
                    .Where(lang => word[lang.ID] == default && lang.ID != word.LanguageID).ToArray();

                return View("NewTranslation", model);
            }
        }

        [HttpPost]
        public IActionResult UpdateWord(EditEntityModel model)
        {
            var origWord = WordsService.Get(model.WordID);
            if (origWord == default)
                return ShowErrorViewForNotFoundWord(model.WordID);

            var languageFromID = LanguagesService.Get(model.WordLanguageID);
            EditEntityModel.FillUpWord(model, origWord, languageFromID);

            TryOrAlert(model, () => WordsService.Update(origWord));
            return model.AlertType == default ? RedirectToAction("Word", new { id = model.WordID }) : ViewEditWordModel(model, origWord);
        }

        [HttpPost]
        public IActionResult DeleteWord(EditEntityModel model)
        {
            var origWord = WordsService.Get(model.WordID);
            if (origWord == default)
                return ShowErrorViewForNotFoundWord(model.WordID);

            TryOrAlert(model, () => WordsService.Remove(origWord));
            return model.AlertType == default ? RedirectToAction("Index", "Words") : ViewEditWordModel(model, origWord);
        }

        /// <summary>
        /// id - Word ID for translation to add to current word inside the model
        /// </summary>
        [HttpPost]
        public IActionResult AddTranslationToWord(EditEntityModel model, int id)
        {
            var word = WordsService.Get(model.WordID);
            if (word == default)
                return ShowErrorViewForNotFoundWord(model.WordID);

            TryOrAlert(model, () =>
            {
                var wordToAdd = WordsService.Get(id);
                WordsService.AddTranslation(word, wordToAdd);

                model.AlertMessage = "Translation successfully added: " + wordToAdd.Text;
                model.AlertType = AlertType.Success;
            });

            return ViewEditWordModel(model, word);
        }


        /// <summary>
        /// id - Word ID for translation which to remove from current word inside the model
        /// </summary>
        [HttpPost]
        public IActionResult RemoveTranslationFromWord(EditEntityModel model, int id)
        {
            var originalWord = WordsService.Get(model.WordID);
            if (originalWord == default)
                return ShowErrorViewForNotFoundWord(model.WordID);

            TryOrAlert(model, () =>
            {
                var wordToRemove = WordsService.Get(id);
                WordsService.RemoveTranslation(originalWord, wordToRemove);

                model.ExpandTranslationList = true;
            });

            return ViewEditWordModel(model, originalWord);
        }

        /// <summary>
        /// Adds missing information which is not in form and is not submitted via HTTP POST
        /// This is called initially when entering Edit mode on word
        /// or everytime incorrect information is added, and returned back with not cleared dirty modifications
        /// or when removing/linking translations, also keeps dirty modifications to other elements
        /// </summary>
        private IActionResult ViewEditWordModel(EditEntityModel model, Word word)
        {
            // HashSet to filter words to link to not show languages which word is already translated to
            var translatedTo = word.Translations?.Select(e => e.Language.ID).ToHashSet();
            if (translatedTo == null)
                translatedTo = new HashSet<int>();

            // Add self to not translate to same language word
            translatedTo.Add(word.Language.ID);

            model.WordsToLink = WordsService.GetAll()
                .Where(e => !translatedTo.Contains(e.Language.ID)) // ex: Don't show words in english, if it's already translated to english
                .Select(e => (e.Language.Name, e.Text, e.ID)).ToArray();

            // Explanations and Translations are not in Form thus not submitted in POST
            model.Explanations = word.Explanations?.Select(e => (e.LanguageTo.Name, e.Text)).ToArray();
            model.Translations = word.Translations?.Select(e => (e.Language.Name, e.Text, e.ID)).ToArray();

            model.AvailableLanguages = LanguagesService.GetAll().ToArray();

            return View("EditWord", model);
        }


        private EntityModel FillUpWordModel(EntityModel Model, Word word)
        {
            var languageToTranslateTo = AccountService.GetPrefferedNativeLanguage();
            if (word.Language.ID == languageToTranslateTo.ID)
                languageToTranslateTo = AccountService.GetPrefferedSecondaryLanguage();

            var translation = word[languageToTranslateTo];
            if (translation != null)
                translation = WordsService.Get(translation.ID); // Getting word with full data, such as definition, which are not included otherwise

            var model = EntityModel.FillUpModel(Model, word, translation, languageToTranslateTo);
            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
