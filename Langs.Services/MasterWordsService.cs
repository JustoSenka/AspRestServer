using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public class MasterWordsService : BaseService<MasterWord>, IMasterWordsService
    {
        protected override DbSet<MasterWord> EntitiesProxy => m_Context.MasterWords;
        public MasterWordsService(DatabaseContext context) : base(context)
        {
        }
    }
}
