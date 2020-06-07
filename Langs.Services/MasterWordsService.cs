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
        public MasterWordsService(IDatabaseContext context) : base(context)
        {
        }

        public IEnumerable<MasterWord> GetAllWithWords()
        {
            return m_Context.MasterWords
                .Include(e => e.Words);
        }

        public IEnumerable<MasterWord> GetAllWithBookIDs()
        {
            return m_Context.MasterWords
                .Include(e => e._BookWordCollection);
        }
        public IEnumerable<MasterWord> GetAllWithData()
        {
            return m_Context.MasterWords
                .Include(e => e._BookWordCollection)
                    .ThenInclude(e => e.Book)
                .Include(e => e.Words)
                    .ThenInclude(e => e.Language);
        }

        public override MasterWord Get(int ID) => GetAllWithData().WithID(ID);
    }
}
