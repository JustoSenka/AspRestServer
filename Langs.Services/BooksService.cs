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

        public override Book Get(int id) => m_Context.Books
            .Include(p => p.BookWordCollection)
                .ThenInclude(c => c.Word)
                    .ThenInclude(word => word.Translations)
                        .ThenInclude(t => t.Definition)
            .SingleOrDefault(e => e.ID == id);

        public IEnumerable<Book> GetWithWordCount() => m_Context.Books
            .Include(p => p.BookWordCollection);

        public IEnumerable<Book> GetBooksWithData() => m_Context.Books
            .Include(p => p.BookWordCollection)
                .ThenInclude(c => c.Word)
                    .ThenInclude(word => word.Language)
            .Include(p => p.BookWordCollection)
                .ThenInclude(c => c.Word)
                    .ThenInclude(word => word.Translations)
                        .ThenInclude(t => t.Definition)
                            .ThenInclude(d => d.Language)
            .Include(p => p.BookWordCollection)
                .ThenInclude(c => c.Word)
                    .ThenInclude(word => word.Translations)
                        .ThenInclude(t => t.Word)
                            .ThenInclude(d => d.Language);
    }
}
