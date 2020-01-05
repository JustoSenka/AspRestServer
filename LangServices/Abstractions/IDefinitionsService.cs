using LangData.Objects;
using System.Collections.Generic;

namespace LangServices
{
    public interface IDefinitionsService : IService<Definition>
    {
        IEnumerable<Definition> GetDefinitionsWithData();
    }
}
