using LangData.Objects;
using LangServices;
using LanguageLearner.Models;
using LanguageLearner.Models.Words;
using LanguageLearner.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class WordsController : Controller
    {
        private readonly IBookService BookService;
        public WordsController(IBookService BookService)
        {
            this.BookService = BookService;
        }

        public IActionResult Index()
        {
            var model = new WordsModel()
            {
                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };

            return View(model);
        }

        // Redirecting to Show so language ids appear inside url.
        public IActionResult ShowRedirect(WordsModel WordsModel)
        {
            return RedirectToAction("Show", new { WordsModel.LanguageFromID, WordsModel.LanguageToID });
        }
        public IActionResult Show(int LanguageFromID, int LanguageToID)
        {
            var model = new WordsModel()
            {
                LanguageFromID = LanguageFromID,
                LanguageToID = LanguageToID,

                From = BookService.GetLanguage(LanguageFromID),
                To = BookService.GetLanguage(LanguageToID),

                AvailableLanguages = BookService.GetLanguages().ToArray(),
            };


            var translations = BookService.GetTranslationsWithData().Where(t => t.Word.Language.ID == LanguageFromID && t.Definition.Language.ID == LanguageToID);
            model.Definitions = translations.Select(t => t.Definition).ToArray();
            model.Words = translations.Select(t => t.Word).ToArray();

            return View(model);
        }

        public IActionResult AddWords(AddWordsModel AddWordsModel)
        {
            var model = AddWordsModel ?? new AddWordsModel();

            model.AvailableLanguages = BookService.GetLanguages().ToArray();

            // This means we came here from submitting a form while adding words. Process addition
            if (!string.IsNullOrEmpty(AddWordsModel.SubmitButtonName))
            {
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
                    AddWordsModel.LogMessage = "[Error] Incorrect button name. No words were added.";
                    AddWordsModel.LogType = LogType.Error;
                }
            }

            return View(model);
        }

        public IActionResult AddWordsSingle(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                new[] { AddWordsModel.SingleWordText },
                new[] { AddWordsModel.SingleDefinitionText });

            AddWordsModel.LogMessage = log.Msg;
            AddWordsModel.LogType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        public IActionResult AddWordsArea(AddWordsModel AddWordsModel)
        {
            var lines = AddWordsModel.WordsCombinedArea.Split(Environment.NewLine);
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                lines.Select(s => s.Split("-")[0].Trim()),
                lines.Select(s => s.Split("-")[1].Trim()));

            AddWordsModel.LogMessage = log.Msg;
            AddWordsModel.LogType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        public IActionResult AddWordsSeparateArea(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                AddWordsModel.WordsArea3.Split(Environment.NewLine),
                AddWordsModel.DefinitionsArea3.Split(Environment.NewLine));

            AddWordsModel.LogMessage = log.Msg;
            AddWordsModel.LogType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

        public IActionResult AddWordsSeparateAreaDescription(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                AddWordsModel.WordsArea4.Split(Environment.NewLine),
                AddWordsModel.DefinitionsArea4.Split(Environment.NewLine),
                AddWordsModel.DescriptionsArea4.Split(Environment.NewLine));

            AddWordsModel.LogMessage = log.Msg;
            AddWordsModel.LogType= log.LogType;

            return View("AddWords", AddWordsModel);
        }

        private (string Msg, LogType LogType) AddWords(int languageFromID, int languageToID, IEnumerable<string> words, IEnumerable<string> definitions, IEnumerable<string> descriptions = null)
        {
            return AddWords(BookService.GetLanguage(languageFromID), BookService.GetLanguage(languageToID), words, definitions, descriptions);
        }

        private (string Msg, LogType LogType) AddWords(Language from, Language to, IEnumerable<string> words, IEnumerable<string> definitions, IEnumerable<string> descriptions = null)
        {
            var count1 = words?.Count();
            var count2 = definitions?.Count();
            var count3 = descriptions?.Count();

            var isInputCorrect = count1 != null && count1 == count2 && (count1 == count3 || count3 == null);

            if (!isInputCorrect)
                return ("Incorrect word format", LogType.Error);

            IEnumerable<(string Word, string Definition, string Description)> collection = descriptions != null ?
                words.Zip(definitions, (w, d) => (w, d)).Zip(descriptions, (tuple, ds) => (tuple.w, tuple.d, ds)) :
                words.Zip(definitions, (w, d) => (w, d, ""));

            CreateTranslations(from, to, collection);

            return ($"Successfully added {count1} words!", LogType.Success);
        }

        private void CreateTranslations(Language from, Language to, IEnumerable<(string Word, string Definition, string Description)> collection)
        {
            var translations = collection.Select(t => new Translation(new Word(t.Word, from), new Definition(t.Definition, to, t.Description))).ToArray();
            BookService.AddTranslations(translations);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
