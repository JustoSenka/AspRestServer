using LangData.Context;
using LangData.Objects;
using System.Collections.Generic;

namespace LangServices
{
    public class BookService : IBookService
    {
        private BookContext m_Context;

        public BookService(BookContext context)
        {
            m_Context = context;
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

        public IEnumerable<Book> GetBooks()
        {
            return m_Context.Books;
        }

        public IEnumerable<Definition> GetDefinitions()
        {
            return m_Context.Definitions;
        }

        public IEnumerable<Language> GetLanguages()
        {
            return m_Context.Languages;
        }

        public IEnumerable<Word> GetWords()
        {
            return m_Context.Words;
        }

        public IEnumerable<Translation> GetTranslations()
        {
            return m_Context.Translations;
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
