using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IWordsService : IService<Word>
    {
        IEnumerable<Word> GetWordsWithData();
    }
}
