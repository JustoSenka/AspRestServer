using LangData.Context;
using LangData.Objects;
using System;
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

        public void AddTranslation(Translation obj)
        {
            m_Context.Translations.Add(obj);
            m_Context.SaveChanges();
        }

        public void AddWord(Word obj)
        {
            m_Context.Words.Add(obj);
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

        public IEnumerable<Translation> GetTranslations()
        {
            return m_Context.Translations;
        }

        public IEnumerable<Word> GetWords()
        {
            return m_Context.Words;
        }

        public void MoodifyBook(Book obj)
        {
            throw new NotImplementedException();
        }

        public void MoodifyDefinition(Definition obj)
        {
            throw new NotImplementedException();
        }

        public void MoodifyLanguage(Language obj)
        {
            throw new NotImplementedException();
        }

        public void MoodifyTranslation(Translation obj)
        {
            throw new NotImplementedException();
        }

        public void MoodifyWord(Word obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveBook(Book obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveDefinition(Definition obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveLanguage(Language obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveTranslation(Translation obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveWord(Word obj)
        {
            throw new NotImplementedException();
        }
    }
}
