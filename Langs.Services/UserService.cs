using Langs.Data.Objects;
using System;
using System.Linq;

namespace Langs.Services
{
    public class UserService : IUserService
    {
        private readonly ILanguagesService LanguagesService;
        public UserService(ILanguagesService LanguagesService)
        {
            this.LanguagesService = LanguagesService;
        }

        public Language GetPreferredLanguage()
        {
            var eng = LanguagesService.GetAll().FirstOrDefault(e => e.Name.Equals("English", StringComparison.InvariantCultureIgnoreCase));
            return eng ?? LanguagesService.GetAll().FirstOrDefault();
        }
    }
}
