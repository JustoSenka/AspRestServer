using Langs.Models.Practice;
using Langs.Models.Words;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
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
    }
}
