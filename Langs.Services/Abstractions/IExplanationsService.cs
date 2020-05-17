using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IExplanationsService : IService<Explanation>
    {
        IEnumerable<Explanation> GetExplanationWithLanguages();
    }
}
