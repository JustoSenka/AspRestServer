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

        public override IEnumerable<MasterWord> GetAll()
        {
            return m_Context.MasterWords.Include(e => e._BookWordCollection);
        }

        public override MasterWord Get(int ID)
        {
            return GetAll().FirstOrDefault(e => e.ID == ID);
        }
    }
}
