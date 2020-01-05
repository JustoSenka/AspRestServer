using LangData.Objects;
using System.Collections.Generic;

namespace LangServices
{
    public interface IWordsService : IService<Word>
    {
        IEnumerable<Word> GetWordsWithData();
    }
}
