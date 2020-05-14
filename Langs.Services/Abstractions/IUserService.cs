using Langs.Data.Objects;

namespace Langs.Services
{
    public interface IUserService
    {
        Language GetPreferredLanguage();
    }
}