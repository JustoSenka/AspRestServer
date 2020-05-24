using Langs.Data.Objects;

namespace Langs.Services
{
    public interface IAccountService : IService<Account>
    {
        Language GetPrefferedNativeLanguage();
        Language GetPrefferedSecondaryLanguage();
        Account GetLogonAccount();
    }
}