using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public class ExplanationService : BaseService<Explanation>, IExplanationsService
    {
        protected override DbSet<Explanation> EntitiesProxy => m_Context.Explanations;
        public ExplanationService(DatabaseContext context) : base(context) { }

        public override Explanation Get(int id) => m_Context.Explanations.Include(d => d.LanguageTo).FirstOrDefault(t => t.ID == id);

        public IEnumerable<Explanation> GetExplanationWithLanguages()
        {
            return m_Context.Explanations.Include(d => d.LanguageTo);
        }
    }
}
