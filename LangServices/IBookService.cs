﻿using LangData.Objects;
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

        void AddBook(Book obj);
        void AddWord(Word obj);
        void AddLanguage(Language obj);
        void AddDefinition(Definition obj);
        void AddTranslation(Translation obj);

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
