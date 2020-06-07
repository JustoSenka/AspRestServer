using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IMasterWordsService : IService<MasterWord>
    {
        IEnumerable<MasterWord> GetAllWithWords();
        IEnumerable<MasterWord> GetAllWithBookIDs();
        IEnumerable<MasterWord> GetAllWithData();
    }
}
