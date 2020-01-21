using LangData.Objects;
using System.Collections.Generic;

namespace LangServices
{
    public interface ITranslationsService : IService<Translation>
    {
        IEnumerable<Translation> GetTranslationsWithText();
        IEnumerable<Translation> GetTranslationsWithLanguages();
    }
}
