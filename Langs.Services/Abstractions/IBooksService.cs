using Langs.Data.Objects;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IBooksService : IService<Book>
    {
        IEnumerable<Book> GetAllWithLanguage();
        IEnumerable<Book> GetAllWithWordCount();
        IEnumerable<Book> GetAllWithMasterWords();
        IEnumerable<Book> GetAllWithData();
    }
}
