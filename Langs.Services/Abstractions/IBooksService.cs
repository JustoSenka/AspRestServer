using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IBooksService : IService<Book>
    {
        IEnumerable<Book> GetWithWordCount();
        IEnumerable<Book> GetBooksWithData();
    }
}
