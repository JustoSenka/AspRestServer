using Langs.Data.Objects;
using Langs.Models.Practice;
using Langs.Models.Words;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace Langs.Controllers
{
    public class PracticeController : BaseController
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IWordsService WordsService;
        private readonly IBooksService BooksService;
        private readonly IAccountService AccountService;
        public PracticeController(IWordsService WordsService, ILanguagesService LanguagesService, IBooksService BooksService, IAccountService AccountService)
        {
            this.WordsService = WordsService;
            this.LanguagesService = LanguagesService;
            this.BooksService = BooksService;
            this.AccountService = AccountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PracticeModel
            {
                Books = BooksService.GetAllWithWordCount().Select(b => (b.ID, b.Name, b.WordCount)).ToArray(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Run(PracticeModel model)
        {
            var book = BooksService.Get(model.SelectedBookID);
            if (book == default)
                return ShowErrorViewForNotFoundBook(model.SelectedBookID);

            var langFrom = AccountService.GetPrefferedNativeLanguage();
            var langTo = AccountService.GetPrefferedSecondaryLanguage();

            var words = book.Words.Select(w => (From: w[langFrom], To: w[langTo])) // TODO: Words could be not translated, fail?
                .Select((t, i) => (t.From.ID, t.To.ID, t.From.Text, t.To.Text, t.To.AlternateSpelling, t.To.Pronunciation, Index: i))
                .ToList();

            // TODO: verify correct word range


            var runModel = new RunPracticeModel
            {
                Test = "Test",
                Language = (langFrom.Name, langTo.Name),
                Book = (book.ID, book.Name),
                WordRange = (model.WordRangeTop, model.WordRangeBottom),
                Words = words
                .Where(w => w.Index <= model.WordRangeTop && w.Index >= model.WordRangeBottom)
                .ToArray()
            };

            runModel.PracticeWords = runModel.Words.Select(w => new PracticeWords(w)).ToArray();

            return View(runModel);
        }
    }
}
