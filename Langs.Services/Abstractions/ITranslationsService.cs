using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface ITranslationsService : IService<Translation>
    {
        IEnumerable<Translation> GetTranslationsWithText();
        IEnumerable<Translation> GetTranslationsWithLanguages();
    }
}
