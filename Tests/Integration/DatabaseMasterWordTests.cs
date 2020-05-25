using Langs.Data.Objects;
using NUnit.Framework;
using System;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseMasterWordTests : IntegrationTest
    {
        [TestCase(true, 3)]
        [TestCase(true, 5)]
        [TestCase(false, 3)]
        [TestCase(false, 5)]
        public void AddingLink_BetweenTwoWords_WillMergeTheirMasterWords(bool wordBHasMoreTranslations, int amountOfWords)
        {
            // Arrange
            PrepareTwoWords(out MasterWord masterA, out Word wordA, out MasterWord masterB, out Word wordB, (a, b) =>
            {
                if (amountOfWords == 5)
                {
                    new Word(a, "smh1", new Language("Any1"));
                    new Word(b, "smh2", new Language("Any2"));
                }

                if (wordBHasMoreTranslations)
                    new Word(b, "smh", new Language("Any3"));
                else
                    new Word(a, "smh", new Language("Any3"));
            });

            var mwcount = MasterWordsService.GetAll().Count();

            // Act
            WordsService.AddTranslation(wordA, wordB);

            // Assert
            var newA = WordsService.Get(wordA.ID);
            var newB = WordsService.Get(wordB.ID);
            var masterWordToSurvive = wordBHasMoreTranslations ? masterB : masterA;

            Assert.AreEqual(wordA.MasterWord, wordB.MasterWord, "Master words should be the same. Old ref");
            Assert.AreEqual(newA.MasterWord, newB.MasterWord, "Master words should be the same. New ref");
            Assert.AreEqual(amountOfWords, masterWordToSurvive.Words.Count, "all words should be on same master now");

            Assert.AreEqual(masterWordToSurvive, newB.MasterWord, "Master word was taken from incorrect word");
            Assert.AreEqual(mwcount - 1, MasterWordsService.GetAll().Count(), "Master word count should go down");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddingLink_BetweenTwoWords_WillRefuseToMergeIfItsAlreadyTranslatedToThatLanguage(bool inverseAddition)
        {
            // Arrange
            var langEn = new Language("English");
            var langEsp = new Language("Spanish");
            var langJp = new Language("Japanese");

            var masterA = new MasterWord();
            var masterB = new MasterWord();

            var wordA1 = new Word(masterA, "Hi orig", langEn);
            var wordA2 = new Word(masterA, "Hola", langEsp);

            var wordB1 = new Word(masterA, "Hi duplicate", langEn);
            var wordB2 = new Word(masterA, "おはよう", langJp);

            // Act
            Assert.Throws<InvalidOperationException>(() =>
            {
                if (inverseAddition) WordsService.AddTranslation(wordA2, wordB2);
                else WordsService.AddTranslation(wordB2, wordA2);
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddingLink_BetweenTwoWords_WillUpdateRefsInBooksToCorrectMasterWord(bool bookLinksToBoth)
        {
            // Arrange
            PrepareTwoWords(out MasterWord masterA, out Word wordA, out MasterWord masterB, out Word wordB);

            var book = new Book("name", wordA.Language);
            book.AddWord(masterB); // masterB is the one being destroyed

            if (bookLinksToBoth)
                book.AddWord(masterA);

            BooksService.Add(book);

            // Act
            WordsService.AddTranslation(wordA, wordB);

            // Assert
            var newBook = BooksService.Get(book.ID);

            Assert.AreEqual(masterA, book.Words.First(), "Master words should be the same. Old ref");
            Assert.AreEqual(masterA, newBook.Words.First(), "Master words should be the same. New ref");
            Assert.AreEqual(1, book.Words.Count(), "Master words should be the same. Old ref");
            Assert.AreEqual(1, newBook.Words.Count(), "Master words should be the same. New ref");

            Assert.AreEqual(book, masterA.Books.First(), "MasterA should link to correct book. Old ref");
            Assert.AreEqual(null, masterB.Books.First(), "MasterB should not have a book since it was destroyed");
        }

        [Test]
        public void RemovingLink_BetweenTwoWords_WillCreateNewMasterWordForIt()
        {
            // Arrange
            var masterA = new MasterWord();
            var wordA = new Word(masterA, "Hi", new Language("English"));
            var wordB = new Word(masterA, "Hola", new Language("Spanish"));
            MasterWordsService.Add(masterA);

            // Act
            WordsService.RemoveTranslation(wordA, wordB);

            // Assert
            var newA = WordsService.Get(wordA.ID);
            var newB = WordsService.Get(wordB.ID);

            Assert.AreNotEqual(newA.MasterWord, newB.MasterWord, "Master words should differ. New Ref");
            Assert.AreNotEqual(wordA.MasterWord, wordB.MasterWord, "Master words should differ. Old Ref");

            Assert.AreEqual(1, masterA.Words.Count(), "MasterWord word count should be 1");
        }

        [TestCase(true, true)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void RemovingLink_BetweenTwoWords_WillRelinkWordsInBooks(bool enBookLang, bool invertRemoval)
        {
            // Arrange
            var langEn = new Language("English");
            var langEsp = new Language("Spanish");

            var masterA = new MasterWord();
            var wordA = new Word(masterA, "Hi", langEn);
            var wordB = new Word(masterA, "Hola", langEsp);

            var book = new Book("name", enBookLang ? langEn : langEsp);
            book.AddWord(masterA);

            MasterWordsService.Add(masterA);
            BooksService.Add(book);

            // Act
            if (invertRemoval) WordsService.RemoveTranslation(wordA, wordB);
            else WordsService.RemoveTranslation(wordB, wordA);

            // Assert
            var newBook = BooksService.Get(book.ID);

            Assert.AreEqual(1, book.WordCount, "Book word count should be 1. Old Ref");
            Assert.AreEqual(1, newBook.WordCount, "Book word count should be 1. New Ref");

            Assert.AreEqual(newBook.Words.First().Words.First().Language, newBook.Language,
                "After removing translation between two words, book should keep the master word which aligns with book language. New Ref");

            var wordInBook = enBookLang ? wordA : wordB;
            var wordWithoutBook = enBookLang ? wordB : wordA;
            Assert.AreEqual(book, wordInBook.MasterWord.Books.First(), "word old ref should be in a book");
            Assert.AreEqual(0, wordWithoutBook.MasterWord.Books.Count(), "other word should have no books");
        }

        private void PrepareTwoWords(out MasterWord masterA, out Word wordA, out MasterWord masterB, out Word wordB, Action<MasterWord, MasterWord> ac = null)
        {
            masterA = new MasterWord();
            wordA = new Word(masterA, "Hi", new Language("English"));
            masterB = new MasterWord();
            wordB = new Word(masterB, "Hola", new Language("Spanish"));

            ac?.Invoke(masterA, masterB);

            MasterWordsService.Add(masterA);
            MasterWordsService.Add(masterB);
        }
    }
}


