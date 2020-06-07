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
    public class WordsController : Controller
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly IBooksService BooksService;
        public WordsController(IWordsService WordsService, ILanguagesService LanguagesService, IBooksService BooksService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.BooksService = BooksService;
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
                Books = BooksService.GetAll().Select(w => (w.ID, w.Name, w.Language.Name)).ToArray(),
                From = LanguagesService.Get(LanguageFromID),
                To = LanguagesService.Get(LanguageToID),
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

        public IActionResult AddWords(AddWordsModel AddWordsModel)
        {
            var model = AddWordsModel ?? new AddWordsModel();
            model.AvailableLanguages = LanguagesService.GetAll().ToArray();

            // This means we came here from submitting a form while adding words. Process addition
            if (!string.IsNullOrEmpty(AddWordsModel.SubmitButtonName))
            {
                // Show error if no languages selected
                if (AddWordsModel.LanguageFromID == 0 || AddWordsModel.LanguageToID == 0)
                {
                    model.ShowNotSelectedError = true;
                    return View(model);
                }

                // Add entered words and return success message
                if (AddWordsModel.SubmitButtonName == "1")
                    return AddWordsSingle(AddWordsModel);

                else if (AddWordsModel.SubmitButtonName == "2")
                    return AddWordsArea(AddWordsModel);

                else if (AddWordsModel.SubmitButtonName == "3")
                    return AddWordsSeparateArea(AddWordsModel);

                else if (AddWordsModel.SubmitButtonName == "4")
                    return AddWordsSeparateAreaDescription(AddWordsModel);

                else
                {
                    Debug.WriteLine("Incorrect button name: " + AddWordsModel.SubmitButtonName);
                    AddWordsModel.AlertMessage = "[Error] Incorrect button name. No words were added.";
                    AddWordsModel.AlertType = AlertType.Error;
                }
            }

            return View(model);
        }

        [NonAction]
        public IActionResult AddWordsSingle(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                new[] { AddWordsModel.SingleWordText },
                new[] { AddWordsModel.SingleDefinitionText });

            AddWordsModel.AlertMessage = log.Msg;
            AddWordsModel.AlertType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        [NonAction]
        public IActionResult AddWordsArea(AddWordsModel AddWordsModel)
        {
            var lines = AddWordsModel.WordsCombinedArea.Split(Environment.NewLine);
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                lines.Select(s => s.Split("-")[0].Trim()),
                lines.Select(s => s.Split("-")[1].Trim()));

            AddWordsModel.AlertMessage = log.Msg;
            AddWordsModel.AlertType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        [NonAction]
        public IActionResult AddWordsSeparateArea(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                AddWordsModel.WordsArea3.Split(Environment.NewLine),
                AddWordsModel.DefinitionsArea3.Split(Environment.NewLine));

            AddWordsModel.AlertMessage = log.Msg;
            AddWordsModel.AlertType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        [NonAction]
        public IActionResult AddWordsSeparateAreaDescription(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                AddWordsModel.WordsArea4.Split(Environment.NewLine),
                AddWordsModel.DefinitionsArea4.Split(Environment.NewLine),
                AddWordsModel.DescriptionsArea4.Split(Environment.NewLine));

            AddWordsModel.AlertMessage = log.Msg;
            AddWordsModel.AlertType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        private (string Msg, AlertType LogType) AddWords(int languageFromID, int languageToID, IEnumerable<string> words, IEnumerable<string> translations, IEnumerable<string> descriptions = null)
        {
            return AddWords(LanguagesService.Get(languageFromID), LanguagesService.Get(languageToID), words, translations, descriptions);
        }

        private (string Msg, AlertType LogType) AddWords(Language from, Language to, IEnumerable<string> words, IEnumerable<string> translations, IEnumerable<string> descriptions = null)
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

            CreateTranslations(from, to, collection);

            return ($"Successfully added {count1} words!", AlertType.Success);
        }

        private void CreateTranslations(Language from, Language to, IEnumerable<(string Word, string Definition, string Description)> collection)
        {
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
