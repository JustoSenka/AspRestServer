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
        private readonly ITranslationsService TranslationsService;
        private readonly IDefinitionsService DefinitionsService;
        public WordsController(IWordsService WordsService, ILanguagesService LanguagesService, ITranslationsService TranslationsService, IDefinitionsService DefinitionsService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.TranslationsService = TranslationsService;
            this.DefinitionsService = DefinitionsService;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new WordsModel
                {
                    AvailableLanguages = LanguagesService.GetAll().ToArray()
                };

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { Exception = e, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
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
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
                From = LanguagesService.Get(LanguageFromID),
                To = LanguagesService.Get(LanguageToID),
            };

            PopulateModelWithWords(LanguageFromID, LanguageToID, model);

            return View(model);
        }

        private void PopulateModelWithWords(int LanguageFromID, int LanguageToID, WordsModel model)
        {
            // If both languages selected, show translations
            if (LanguageFromID != 0 && LanguageToID != 0)
            {
                var translations = TranslationsService.GetTranslationsWithLanguages().Where(t => t.Word.Language.ID == LanguageFromID && t.Definition.Language.ID == LanguageToID);
                model.Definitions = translations.Select(t => t.Definition).ToArray();
                model.Words = translations.Select(t => t.Word).ToArray();
            }
            // Of only one language selected, just list words/definitions in that language
            else if (LanguageFromID != 0)
            {
                model.Words = WordsService.GetAll().Where(w => w.Language.ID == LanguageFromID).ToArray();
            }
            else if (LanguageToID != 0)
            {
                model.Definitions = DefinitionsService.GetAll().Where(d => d.Language.ID == LanguageToID).ToArray();
            }
            else
            {
                model.Words = WordsService.GetAll().ToArray();
                model.Definitions = DefinitionsService.GetAll().ToArray();
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

        public IActionResult AddWordsSingle(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                new[] { AddWordsModel.SingleWordText },
                new[] { AddWordsModel.SingleDefinitionText });

            AddWordsModel.AlertMessage = log.Msg;
            AddWordsModel.AlertType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

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

        public IActionResult AddWordsSeparateArea(AddWordsModel AddWordsModel)
        {
            var log = AddWords(AddWordsModel.LanguageFromID, AddWordsModel.LanguageToID,
                AddWordsModel.WordsArea3.Split(Environment.NewLine),
                AddWordsModel.DefinitionsArea3.Split(Environment.NewLine));

            AddWordsModel.AlertMessage = log.Msg;
            AddWordsModel.AlertType = log.LogType;

            return View("AddWords", AddWordsModel);
        }

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

        private (string Msg, AlertType LogType) AddWords(int languageFromID, int languageToID, IEnumerable<string> words, IEnumerable<string> definitions, IEnumerable<string> descriptions = null)
        {
            return AddWords(LanguagesService.Get(languageFromID), LanguagesService.Get(languageToID), words, definitions, descriptions);
        }

        private (string Msg, AlertType LogType) AddWords(Language from, Language to, IEnumerable<string> words, IEnumerable<string> definitions, IEnumerable<string> descriptions = null)
        {
            var count1 = words?.Count();
            var count2 = definitions?.Count();
            var count3 = descriptions?.Count();

            var isInputCorrect = count1 != null && count1 == count2 && (count1 == count3 || count3 == null);

            if (!isInputCorrect)
                return ("Incorrect word format", AlertType.Error);

            IEnumerable<(string Word, string Definition, string Description)> collection = descriptions != null ?
                words.Zip(definitions, (w, d) => (w, d)).Zip(descriptions, (tuple, ds) => (tuple.w, tuple.d, ds)) :
                words.Zip(definitions, (w, d) => (w, d, ""));

            CreateTranslations(from, to, collection);

            return ($"Successfully added {count1} words!", AlertType.Success);
        }

        private void CreateTranslations(Language from, Language to, IEnumerable<(string Word, string Definition, string Description)> collection)
        {
            var translations = collection.Select(t => new Translation(new Word(t.Word, from), new Definition(t.Definition, to, t.Description))).ToArray();
            TranslationsService.Add(translations);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
