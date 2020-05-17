using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Langs.Services
{
    public class DefinitionsService : BaseService<Definition>, IDefinitionsService
    {
        protected override DbSet<Definition> EntitiesProxy => m_Context.Definitions;
        public DefinitionsService(DatabaseContext context) : base(context) { }
    }
}
