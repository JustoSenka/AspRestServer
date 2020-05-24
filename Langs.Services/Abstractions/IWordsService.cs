using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IWordsService : IService<Word>
    {
        IEnumerable<Word> GetWordsWithData();

        void AddTranslation(Word word, Word translation);
        void RemoveTranslation(Word word, Word translation);
    }
}
