using LangData.Context;
using LangData.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LangServices
{
    public class BookService : IBookService
    {
        private BookContext m_Context;

        public BookService(BookContext context)
        {
            m_Context = context;
        }

        public IEnumerable<Book> GetBooks() => m_Context.Books;
        public IEnumerable<Word> GetWords() => m_Context.Words;
        public IEnumerable<Language> GetLanguages() => m_Context.Languages;
        public IEnumerable<Definition> GetDefinitions() => m_Context.Definitions;
        public IEnumerable<Translation> GetTranslations() => m_Context.Translations;

        public Book GetBook(int id) => m_Context.Books
            .Include(p => p.Words)
                .ThenInclude(word => word.Translations)
                    .ThenInclude(t => t.Definition)
            .SingleOrDefault(e => e.ID == id);

        public Word GetWord(int id) => m_Context.Words
            .Include(word => word.Language)
            .Include(word => word.Translations)
                .ThenInclude(t => t.Definition)
                    .ThenInclude(d => d.Language)
            .Include(word => word.Translations)
                .ThenInclude(t => t.Word)
                    .ThenInclude(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

        public Language GetLanguage(int id) => m_Context.Languages.SingleOrDefault(e => e.ID == id);

        public Definition GetDefinition(int id) => m_Context.Definitions
            .Include(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

        public Translation GetTranslation(int id) => m_Context.Translations
            .Include(t => t.Definition)
                .ThenInclude(d => d.Language)
            .Include(t => t.Word)
                .ThenInclude(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

        public IEnumerable<Book> GetBooksWithData()
        {
            return m_Context.Books
                .Include(p => p.Words)
                    .ThenInclude(word => word.Language)
                .Include(p => p.Words)
                    .ThenInclude(word => word.Translations)
                        .ThenInclude(t => t.Definition)
                            .ThenInclude(d => d.Language)
                .Include(p => p.Words)
                    .ThenInclude(word => word.Translations)
                        .ThenInclude(t => t.Word)
                            .ThenInclude(d => d.Language);
        }

        public IEnumerable<Definition> GetDefinitionsWithData()
        {
            return m_Context.Definitions
                .Include(p => p.Language)
                    .Include(p => p.Translations)
                        .ThenInclude(t => t.Word)
                            .ThenInclude(d => d.Language)
                    .Include(p => p.Translations)
                        .ThenInclude(t => t.Definition)
                            .ThenInclude(d => d.Language);
        }

        public IEnumerable<Word> GetWordsWithData()
        {
            return m_Context.Words
                .Include(p => p.Language)
                .Include(p => p.Translations)
                    .ThenInclude(t => t.Word)
                        .ThenInclude(d => d.Language)
                .Include(p => p.Translations)
                    .ThenInclude(t => t.Definition)
                        .ThenInclude(d => d.Language);
        }

        public IEnumerable<Translation> GetTranslationsWithData()
        {
            return m_Context.Translations
                .Include(t => t.Word)
                    .ThenInclude(d => d.Language)
                .Include(t => t.Definition)
                    .ThenInclude(d => d.Language);
        }

        public void AddBook(Book obj)
        {
            m_Context.Books.Add(obj);
            m_Context.SaveChanges();
        }

        public void AddDefinition(Definition obj)
        {
            m_Context.Definitions.Add(obj);
            m_Context.SaveChanges();
        }

        public void AddLanguage(Language obj)
        {
            m_Context.Languages.Add(obj);
            m_Context.SaveChanges();
        }

        public void AddWord(Word obj)
        {
            m_Context.Words.Add(obj);
            m_Context.SaveChanges();
        }

        public void AddTranslation(Translation obj)
        {
            m_Context.Translations.Add(obj);
            m_Context.SaveChanges();
        }

        public void UpdateBook(Book obj)
        {
            m_Context.Books.Update(obj);
            m_Context.SaveChanges();
        }

        public void UpdateDefinition(Definition obj)
        {
            m_Context.Definitions.Update(obj);
            m_Context.SaveChanges();
        }

        public void UpdateLanguage(Language obj)
        {
            m_Context.Languages.Update(obj);
            m_Context.SaveChanges();
        }

        public void UpdateWord(Word obj)
        {
            m_Context.Words.Update(obj);
            m_Context.SaveChanges();
        }

        public void UpdateTranslation(Translation obj)
        {
            m_Context.Translations.Update(obj);
            m_Context.SaveChanges();
        }

        public void RemoveBook(Book obj)
        {
            m_Context.Books.Remove(obj);
            m_Context.SaveChanges();
        }

        public void RemoveDefinition(Definition obj)
        {
            m_Context.Definitions.Remove(obj);
            m_Context.SaveChanges();
        }

        public void RemoveLanguage(Language obj)
        {
            m_Context.Languages.Remove(obj);
            m_Context.SaveChanges();
        }

        public void RemoveWord(Word obj)
        {
            m_Context.Words.Remove(obj);
            m_Context.SaveChanges();
        }

        public void RemoveTranslation(Translation obj)
        {
            m_Context.Translations.Remove(obj);
            m_Context.SaveChanges();
        }
    }
}
