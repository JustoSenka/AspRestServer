using Langs.Models;
using Langs.Models.Accounts;
using Langs.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Langs.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILanguagesService LanguagesService;
        private readonly IAccountService AccountService;
        public AccountController(ILanguagesService LanguagesService, IAccountService AccountService)
        {
            this.LanguagesService = LanguagesService;
            this.AccountService = AccountService;
        }

        public IActionResult Index()
        {
            var account = AccountService.GetLogonAccount();
            var model = new AccountModel()
            {
                AccountName = account.Name,
                NativeLanguage = account.NativeLanguage?.Name,
                LearningLanguage = account.LearningLanguage?.Name,
                AdditionalLanguage = account.AdditionalLanguage?.Name,
            };

            return View(model);
        }

        public IActionResult Edit()
        {
            var account = AccountService.GetLogonAccount();
            var model = new EditAccountModel()
            {
                AvailableLanguages = LanguagesService.GetAll().ToArray(),
                AccountName = account.Name,
                NativeLanguageID = account.NativeLanguageID ?? 0,
                LearningLanguageID = account.LearningLanguageID ?? 0,
                AdditionalLanguageID = account.AdditionalLanguageID ?? 0,
            };

            return View(model);
        }

        public IActionResult SaveAccount(EditAccountModel model)
        {
            var account = AccountService.GetLogonAccount();

            account.Name = model.AccountName;
            account.NativeLanguageID = model.NativeLanguageID.ToNullable();
            account.LearningLanguageID = model.LearningLanguageID.ToNullable();
            account.AdditionalLanguageID = model.AdditionalLanguageID.ToNullable();

            AccountService.Update(account);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
