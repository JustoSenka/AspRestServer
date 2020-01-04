using LangData.Objects;
using System;
using System.Collections.Generic;

namespace LangServices
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Word> GetWords();
        IEnumerable<Language> GetLanguages();
        IEnumerable<Definition> GetDefinitions();
        IEnumerable<Translation> GetTranslations();

        Book GetBook(int id);
        Word GetWord(int id);
        Language GetLanguage(int id);
        Definition GetDefinition(int id);
        Translation GetTranslation(int id);

        IEnumerable<Book> GetBooksWithData();
        IEnumerable<Word> GetWordsWithData();
        IEnumerable<Definition> GetDefinitionsWithData();
        IEnumerable<Translation> GetTranslationsWithData();

        Book AddBook(Book obj);
        Word AddWord(Word obj);
        Language AddLanguage(Language obj);
        Definition AddDefinition(Definition obj);
        Translation AddTranslation(Translation obj);

        void AddBooks(IEnumerable<Book> objs);
        void AddWords(IEnumerable<Word> objs);
        void AddLanguages(IEnumerable<Language> objs);
        void AddDefinitions(IEnumerable<Definition> objs);
        void AddTranslations(IEnumerable<Translation> objs);

        void RemoveBook(Book obj);
        void RemoveWord(Word obj);
        void RemoveLanguage(Language obj);
        void RemoveDefinition(Definition obj);
        void RemoveTranslation(Translation obj);

        void UpdateBook(Book obj);
        void UpdateWord(Word obj);
        void UpdateLanguage(Language obj);
        void UpdateDefinition(Definition obj);
        void UpdateTranslation(Translation obj);
    }
}
