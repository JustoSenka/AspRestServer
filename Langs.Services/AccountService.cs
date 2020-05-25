using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Langs.Services
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        protected override DbSet<Account> EntitiesProxy => m_Context.Accounts;

        private readonly ILanguagesService LanguagesService;
        public AccountService(DatabaseContext context, ILanguagesService LanguagesService) : base(context)
        {
            this.LanguagesService = LanguagesService;
        }

        public override Account Get(int id) => m_Context.Accounts
            .Include(e => e.NativeLanguage)
            .Include(e => e.LearningLanguage)
            .Include(e => e.AdditionalLanguage)
            .FirstOrDefault(e => e.ID == id);

        /// <summary>
        /// Will always return valid language or default to english or first language in the database
        /// </summary>
        public Language GetPrefferedNativeLanguage()
        {
            var acc = GetLogonAccount();
            var lang = acc.NativeLanguage ?? LanguagesService.GetAll().FirstOrDefault(e => e.Name.Equals("English", StringComparison.InvariantCultureIgnoreCase));
            return lang ?? LanguagesService.GetAll().FirstOrDefault();
        }

        /// <summary>
        /// Will always return valid language or default to english or first language in the database
        /// </summary>
        public Language GetPrefferedSecondaryLanguage()
        {
            var acc = GetLogonAccount();
            var lang = acc.LearningLanguage ?? LanguagesService.GetAll().FirstOrDefault(e => e.Name.Equals("English", StringComparison.InvariantCultureIgnoreCase));
            return lang ?? LanguagesService.GetAll().FirstOrDefault();
        }

        /// <summary>
        /// Will return account additional language or null if not set
        /// </summary>
        public Language GetAdditionalLanguage() => GetLogonAccount().AdditionalLanguage;

        public Account GetLogonAccount()
        {
            var id = GetAll().First().ID;
            return Get(id);
        }
    }
}
