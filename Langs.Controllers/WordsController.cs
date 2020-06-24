using Langs.Data.Objects;
using Langs.Models;
using Langs.Models.Words;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Langs.Controllers
{
    public class WordsController : BaseController
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly IBooksService BooksService;
        private readonly IAccountService AccountService;
        public WordsController(IWordsService WordsService, ILanguagesService LanguagesService, IBooksService BooksService, IAccountService AccountService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.BooksService = BooksService;
            this.AccountService = AccountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new WordsModel
            {
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
                Books = BooksService.GetAllWithLanguage().Select(w => (w.ID, w.Name, w.Language.Name)).ToArray()
            };

            return View(model);
        }

        // Redirecting to Show so language ids appear inside url.
        [HttpPost]
        public IActionResult ShowRedirect(WordsModel WordsModel)
        {
            return RedirectToAction("Show", new { WordsModel.LanguageFromID, WordsModel.LanguageToID, WordsModel.SelectedBookID });
        }

        [HttpGet]
        public IActionResult Show(int LanguageFromID, int LanguageToID, int SelectedBookID)
        {
            var book = BooksService.Get(SelectedBookID);
            var model = new WordsModel()
            {
                LanguageFromID = LanguageFromID,
                LanguageToID = LanguageToID,
                SelectedBookID = SelectedBookID,
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
                From = LanguagesService.Get(LanguageFromID),
                To = LanguagesService.Get(LanguageToID),
                Books = BooksService.GetAllWithLanguage().Select(w => (w.ID, w.Name, w.Language.Name)).ToArray(),
            };

            PopulateModelWithWords(LanguageFromID, LanguageToID, SelectedBookID, model);

            return View(model);
        }

        private void PopulateModelWithWords(int LanguageFromID, int LanguageToID, int SelectedBookID, WordsModel model)
        {
            var words = WordsService.GetWordsWithData();
            if (SelectedBookID != 0)
                words = words.Where(w => w.MasterWord.Books.Any(b => b.ID == SelectedBookID));

            // If both languages selected, show translations
            if (LanguageFromID != 0 && LanguageToID != 0)
            {
                model.Translations = words
                    .Where(w => w.Language.ID == LanguageFromID)
                    .Select(w => (left: w, right: w[LanguageToID]))
                    .Where(t => t.right != null)
                    .Select(t => (t.left.ID, t.left.Text, t.right.ID, t.right.Text)).ToArray();

            }
            // Of only one language selected, just list words/definitions in that language
            else if (LanguageFromID != 0)
            {
                model.Translations = words
                    .Where(w => w.Language.ID == LanguageFromID)
                    .Where(w => w.Translations != null && w.Translations.Count() > 0)
                    .SelectMany(w => w.Translations.Select(t => (w, t)))
                    .Select(tuple => (tuple.w.ID, tuple.w.Text, tuple.t.ID, tuple.t.Text)).ToArray();
            }
            else if (LanguageToID != 0)
            {
                model.Translations = words
                    .Where(w => w.Translations != null && w.Translations.Count() > 0)
                    .SelectMany(w => w.Translations.Where(t => t.Language.ID == LanguageToID).Select(t => (w, t)))
                    .Select(tuple => (tuple.w.ID, tuple.w.Text, tuple.t.ID, tuple.t.Text)).ToArray();
            }
            else
            {
                model.Words = words
                    .Select(w => (w.ID, w.Language.Name, w.Text, w.AlternateSpelling, w.Pronunciation)).ToArray();
            }
        }

        public IActionResult AddWords(AddWordsModel model)
        {
            model = model ?? new AddWordsModel();
            model.AvailableLanguages = LanguagesService.GetAll().ToArray();
            model.Books = BooksService.GetAllWithLanguage().Select(w => (w.ID, w.Name, w.Language.Name)).ToArray();

            // This means we came here from submitting a form while adding words. Process addition
            if (!string.IsNullOrEmpty(model.SubmitButtonName))
            {
                // Show error if no languages selected
                if (model.LanguageFromID == 0 || model.LanguageToID == 0)
                {
                    model.ShowNotSelectedError = true;
                    return View(model);
                }

                // Add entered words and return success message
                if (model.SubmitButtonName == "1")
                    return AddWordsSingle(model);

                else if (model.SubmitButtonName == "2")
                    return AddWordsArea(model);

                else if (model.SubmitButtonName == "3")
                    return AddWordsSeparateArea(model);

                else if (model.SubmitButtonName == "4")
                    return AddWordsSeparateAreaDescription(model);

                else
                {
                    Debug.WriteLine("Incorrect button name: " + model.SubmitButtonName);
                    model.AlertMessage = "[Error] Incorrect button name. No words were added.";
                    model.AlertType = AlertType.Error;
                }
            }

            model.LanguageFromID = AccountService.GetPrefferedNativeLanguage().ID;
            model.LanguageToID = AccountService.GetPrefferedSecondaryLanguage().ID;

            return base.View(model);
        }

        [NonAction]
        public IActionResult AddWordsSingle(AddWordsModel model)
        {
            var log = AddWords(model.SelectedBookID, model.LanguageFromID, model.LanguageToID,
                new[] { model.SingleWordText },
                new[] { model.SingleDefinitionText });

            model.AlertMessage = log.Msg;
            model.AlertType = log.LogType;

            return View("AddWords", model);
        }

        [NonAction]
        public IActionResult AddWordsArea(AddWordsModel model)
        {
            var lines = model.WordsCombinedArea
                .Split(Environment.NewLine)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Split("-"))
                .Where(s => s.Length == 2)
                .ToList();

            var log = AddWords(model.SelectedBookID, model.LanguageFromID, model.LanguageToID,
                lines.Select(s => s[0].Trim()),
                lines.Select(s => s[1].Trim()));

            model.AlertMessage = log.Msg;
            model.AlertType = log.LogType;

            return View("AddWords", model);
        }

        [NonAction]
        public IActionResult AddWordsSeparateArea(AddWordsModel model)
        {
            var log = AddWords(model.SelectedBookID, model.LanguageFromID, model.LanguageToID,
                model.WordsArea3.Split(Environment.NewLine),
                model.DefinitionsArea3.Split(Environment.NewLine));

            model.AlertMessage = log.Msg;
            model.AlertType = log.LogType;

            return View("AddWords", model);
        }

        [NonAction]
        public IActionResult AddWordsSeparateAreaDescription(AddWordsModel model)
        {
            var log = AddWords(model.SelectedBookID, model.LanguageFromID, model.LanguageToID,
                model.WordsArea4.Split(Environment.NewLine),
                model.DefinitionsArea4.Split(Environment.NewLine),
                model.DescriptionsArea4.Split(Environment.NewLine));

            model.AlertMessage = log.Msg;
            model.AlertType = log.LogType;

            return View("AddWords", model);
        }

        private (string Msg, AlertType LogType) AddWords(int bookToAddID, int languageFromID, int languageToID, IEnumerable<string> words, IEnumerable<string> translations, IEnumerable<string> descriptions = null)
        {
            return AddWords(BooksService.Get(bookToAddID), LanguagesService.Get(languageFromID), LanguagesService.Get(languageToID), words, translations, descriptions);
        }

        private (string Msg, AlertType LogType) AddWords(Book bookToAdd, Language from, Language to, IEnumerable<string> words, IEnumerable<string> translations, IEnumerable<string> descriptions = null)
        {
            var count1 = words?.Count();
            var count2 = translations?.Count();
            var count3 = descriptions?.Count();

            var isInputCorrect = count1 != null && count1 == count2 && (count1 == count3 || count3 == null);

            if (!isInputCorrect)
                return ("Incorrect word format", AlertType.Error);

            IEnumerable<(string Word, string Definition, string Description)> collection = descriptions != null ?
                words.Zip(translations, (w, d) => (w, d)).Zip(descriptions, (tuple, ds) => (tuple.w, tuple.d, ds)) :
                words.Zip(translations, (w, d) => (w, d, ""));

            CreateTranslations(bookToAdd, from, to, collection);

            return ($"Successfully added {count1} words!", AlertType.Success);
        }

        private void CreateTranslations(Book bookToAdd, Language from, Language to, IEnumerable<(string Word, string Definition, string Description)> collection)
        {
            var masterList = new List<MasterWord>();
            using (WordsService.BatchRequests())
            {
                foreach (var (wordText, translationText, description) in collection)
                {
                    var masterWord = new MasterWord();
                    var word = new Word(masterWord, wordText, from);
                    var translation = new Word(masterWord, translationText, to);

                    if (!string.IsNullOrWhiteSpace(description))
                        translation.Definition = new Definition(description);

                    WordsService.Add(word);
                    WordsService.Add(translation);

                    masterList.Add(masterWord);
                }
            }

            if (bookToAdd != default)
            {
                using (BooksService.BatchRequests())
                {
                    foreach (var m in masterList)
                        bookToAdd.AddWord(m);

                    BooksService.Update(bookToAdd);
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
