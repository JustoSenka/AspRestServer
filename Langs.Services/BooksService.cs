using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public class BooksService : BaseService<Book>, IBooksService
    {
        protected override DbSet<Book> EntitiesProxy => m_Context.Books;
        public BooksService(DatabaseContext context) : base(context) { }

        public override Book Get(int id) => GetBooksWithData().SingleOrDefault(e => e.ID == id);

        public IEnumerable<Book> GetWithWordCount() => m_Context.Books
            .Include(p => p._BookWordCollection);

        public IEnumerable<Book> GetBooksWithData() => m_Context.Books
            .Include(p => p._BookWordCollection)
                .ThenInclude(c => c.MasterWord)
                    .ThenInclude(c => c.Words)
                        .ThenInclude(t => t.Explanations)
                            .ThenInclude(t => t.LanguageTo)
            .Include(p => p._BookWordCollection)
                .ThenInclude(c => c.MasterWord)
                    .ThenInclude(c => c.Words)
                        .ThenInclude(t => t.Definition)
            .Include(p => p._BookWordCollection)
                .ThenInclude(c => c.MasterWord)
                    .ThenInclude(c => c.Words)
                        .ThenInclude(t => t.Language);
    }
}
