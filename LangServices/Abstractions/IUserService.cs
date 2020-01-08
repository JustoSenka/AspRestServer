using LangData.Objects;

namespace LangServices
{
    public interface IUserService
    {
        Language GetPreferredLanguage();
    }
}