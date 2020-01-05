using LangData.Objects;
using System.Collections.Generic;

namespace LangServices
{
    public interface IBooksService : IService<Book>
    {
        IEnumerable<Book> GetBooksWithData();
    }
}
