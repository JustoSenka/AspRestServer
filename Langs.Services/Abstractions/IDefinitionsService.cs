using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IDefinitionsService : IService<Definition>
    {
        IEnumerable<Definition> GetDefinitionsWithData();
    }
}
