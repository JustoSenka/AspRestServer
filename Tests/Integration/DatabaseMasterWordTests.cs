using Langs.Data.Objects;
using NUnit.Framework;
using System;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseMasterWordTests : IntegrationTest
    {
        [Test]
        public void AddingLink_BetweenTwoWords_WillMergeTheirMasterWords(
            [Values(true, false)] bool wordBHasMoreTranslations,
            [Values(3, 5)] int amountOfWords,
            [Values(true, false)] bool cleanCache)
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

            if (cleanCache)
            {
                RefreshServicesAndClearCache();
                wordA = WordsService.GetAll().First(w => w.ID == wordA.ID);
                wordB = WordsService.GetAll().First(w => w.ID == wordB.ID);
            }

            // Act
            WordsService.AddTranslation(wordA, wordB);

            // Assert
            var newA = WordsService.Get(wordA.ID);
            var newB = WordsService.Get(wordB.ID);
            var masterWordToSurvive = wordBHasMoreTranslations ? masterB : masterA;
            masterWordToSurvive = MasterWordsService.Get(masterWordToSurvive.ID);

            Assert.AreEqual(wordA.MasterWord, wordB.MasterWord, "Master words should be the same. Old ref");
            Assert.AreEqual(newA.MasterWord, newB.MasterWord, "Master words should be the same. New ref");
            Assert.AreEqual(amountOfWords, masterWordToSurvive.Words.Count, "all words should be on same master now");

            Assert.AreEqual(masterWordToSurvive, newB.MasterWord, "Master word was taken from incorrect word");
            Assert.AreEqual(mwcount - 1, MasterWordsService.GetAll().Count(), "Master word count should go down");
        }

        [Test]
        public void AddingLink_BetweenTwoWords_WillRefuseToMergeIfItsAlreadyTranslatedToThatLanguage(
            [Values(true, false)] bool inverseAddition,
            [Values(true, false)] bool cleanCache)
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

            MasterWordsService.Add(masterA);
            MasterWordsService.Add(masterB);

            if (cleanCache)
            {
                RefreshServicesAndClearCache();
                wordA2 = WordsService.GetAll().First(w => w.ID == wordA2.ID);
                wordB2 = WordsService.GetAll().First(w => w.ID == wordB2.ID);
            }

            // Act
            Assert.Throws<InvalidOperationException>(() =>
            {
                if (inverseAddition) WordsService.AddTranslation(wordA2, wordB2);
                else WordsService.AddTranslation(wordB2, wordA2);
            });
        }

        [Test]
        public void AddingLink_BetweenTwoWords_WillUpdateRefsInBooksToCorrectMasterWord(
            [Values(true, false)] bool bookLinksToBoth,
            [Values(true, false)] bool cleanCache)
        {
            // Arrange
            PrepareTwoWords(out MasterWord masterA, out Word wordA, out MasterWord masterB, out Word wordB);

            var book = new Book("name", wordA.Language);
            book.AddWord(masterB); // masterB is the one being destroyed

            if (bookLinksToBoth)
                book.AddWord(masterA);

            BooksService.Add(book);

            if (cleanCache)
            {
                RefreshServicesAndClearCache();
                wordA = WordsService.GetAll().First(w => w.ID == wordA.ID);
                wordB = WordsService.GetAll().First(w => w.ID == wordB.ID);
            }

            // Act
            WordsService.AddTranslation(wordA, wordB);

            // Assert
            book = BooksService.Get(book.ID);
            masterA = MasterWordsService.Get(masterA.ID);
            masterB = MasterWordsService.Get(masterB.ID);

            Assert.AreEqual(masterA.ID, book.Words.First().ID, "Master words should be the same");
            Assert.AreEqual(1, book.Words.Count(), "Master words should be the same");

            Assert.AreEqual(book, masterA.Books.First(), "MasterA should link to correct book");
            Assert.IsNull(masterB, "MasterB should have been destroyed");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void RemovingLink_BetweenTwoWords_WillCreateNewMasterWordForIt(bool cleanCache)
        {
            // Arrange
            var masterA = new MasterWord();
            var wordA = new Word(masterA, "Hi", new Language("English"));
            var wordB = new Word(masterA, "Hola", new Language("Spanish"));
            MasterWordsService.Add(masterA);

            if (cleanCache)
            {
                RefreshServicesAndClearCache();
                wordA = WordsService.GetAll().First(w => w.ID == wordA.ID);
                wordB = WordsService.GetAll().First(w => w.ID == wordB.ID);
            }

            // Act
            WordsService.RemoveTranslation(wordA, wordB);

            // Assert
            var newA = WordsService.Get(wordA.ID);
            var newB = WordsService.Get(wordB.ID);
            masterA = MasterWordsService.Get(masterA.ID);

            Assert.AreNotEqual(newA.MasterWord, newB.MasterWord, "Master words should differ. New Ref");
            Assert.AreNotEqual(wordA.MasterWord, wordB.MasterWord, "Master words should differ. Old Ref");

            Assert.AreEqual(1, masterA.Words.Count(), "MasterWord word count should be 1");
        }

        [Test]
        public void RemovingLink_BetweenTwoWords_WillRelinkWordsInBooks(
            [Values(true, false)] bool enBookLang,
            [Values(true, false)] bool invertRemoval)
        {
            // Arrange
            var langEn = new Language("English");
            var langEsp = new Language("Spanish");

            var masterA = new MasterWord();
            var wordA = new Word(masterA, "Hi", langEn);
            var wordB = new Word(masterA, "Hola", langEsp);
            new Word(masterA, "some word", new Language("some lang"));

            var book = new Book("name", enBookLang ? langEn : langEsp);
            book.AddWord(masterA);

            MasterWordsService.Add(masterA);
            BooksService.Add(book);


            // Act
            RefreshServicesAndClearCache();
            wordA = WordsService.GetAll().First(w => w.ID == wordA.ID);
            wordB = WordsService.GetAll().First(w => w.ID == wordB.ID);

            if (invertRemoval) WordsService.RemoveTranslation(wordA, wordB);
            else WordsService.RemoveTranslation(wordB, wordA);

            // Assert
            book = BooksService.Get(book.ID);

            Assert.AreEqual(1, book.WordCount, "Book word count should be 1. Old Ref");
            Assert.AreEqual(1, book.WordCount, "Book word count should be 1. New Ref");

            Assert.IsNotNull(book.Words.First()[book.Language],
                "After removing translation between two words, book should keep the master word which aligns with book language. New Ref");

            var wordInBook = enBookLang ? wordA : wordB;
            var wordWithoutBook = enBookLang ? wordB : wordA;
            Assert.AreEqual(book, wordInBook.MasterWord.Books.First(), "word should be in a book");
            Assert.AreEqual(book.Language, wordInBook.Language, "book and word languages should be the same");
            Assert.AreEqual(book.Words.First(), wordInBook.MasterWord, "master word should be the same of word left of correct language");
            Assert.AreEqual(0, wordWithoutBook.MasterWord.Books.Count(), "other word should have no books");
        }

        [Test]
        public void RemovingLink_BetweenTwoWords_FirstArgWillKeepTheMasterWithAllTranslations()
        {
            // Arrange
            var masterA = new MasterWord();
            var wordInFirstArg = new Word(masterA, "Hi", new Language("English"));
            var translation = new Word(masterA, "Hola", new Language("Spanish"));
            new Word(masterA, "some word", new Language("some lang"));

            MasterWordsService.Add(masterA);

            // Act
            WordsService.RemoveTranslation(wordInFirstArg, translation);

            // Assert
            var newMaster = MasterWordsService.Get(masterA.ID);
            var newWord = WordsService.Get(wordInFirstArg.ID);
            var newTranslation = WordsService.Get(translation.ID);

            Assert.AreEqual(2, newMaster.Words.Count, "Master should have two words left");
            Assert.AreEqual(newMaster, newWord.MasterWord, "Word in first argument should keep the master word");

            Assert.AreEqual(1, newTranslation.MasterWord.Words.Count, "Translation master should only have 1 translation");
            Assert.AreNotEqual(newTranslation.MasterWord, newWord.MasterWord, "Word in second argument should get new master created");
        }

        [Test]
        public void RemovingWord_WithNotLoadedMaster_StillWorksFine()
        {
            // Arrange
            var masterA = new MasterWord();
            var wordA = new Word(masterA, "Hi", new Language("English"));

            MasterWordsService.Add(masterA);
            RefreshServicesAndClearCache();

            // Act
            WordsService.Remove(WordsService.GetAll().WithID(wordA));

            // Assert
            Assert.IsNull(MasterWordsService.Get(masterA.ID), "Master should be destroyed");
            Assert.IsNull(WordsService.Get(wordA.ID), "Word should be destroyed");
        }

        [Test]
        public void RemovingAllWords_DestroysMasterWordAsWell_UsingRemoveRange([Values(true, false)] bool cleanCache)
        {
            // Arrange
            var masterA = new MasterWord();
            new Word(masterA, "Hi", new Language("English"));
            new Word(masterA, "Hola", new Language("Spanish"));

            MasterWordsService.Add(masterA);

            if (cleanCache)
                RefreshServicesAndClearCache();

            // Act
            WordsService.Remove(WordsService.GetAll().ToArray());

            // Assert
            masterA = MasterWordsService.Get(masterA.ID);
            Assert.IsNull(masterA, "Master should be destroyed");
        }

        [Test]
        public void WordsService_IsMergePossible_WorksCorrectly(
            [Values(true, false)] bool cleanCache,
            [Values(true, false)] bool wordAlreadyTranslated)
        {
            // Arrange
            var langEn = new Language("English");
            var langEsp = new Language("Spanish");
            var langJp = new Language("Japanese");

            var masterA = new MasterWord();
            var masterB = new MasterWord();

            new Word(masterA, "Hi orig", langEn);
            if (wordAlreadyTranslated)
                new Word(masterB, "Hi duplicate", langEn);

            var wordA2 = new Word(masterA, "Hola", langEsp);
            var wordB2 = new Word(masterB, "おはよう", langJp);

            MasterWordsService.Add(masterA);
            MasterWordsService.Add(masterB);

            if (cleanCache)
            {
                RefreshServicesAndClearCache();
                wordA2 = WordsService.Get(wordA2.ID);
                wordB2 = WordsService.Get(wordB2.ID);
            }

            // Act
            var res = WordsService.IsMergePossible(wordA2.MasterWord, wordB2.MasterWord);

            // Assert
            Assert.AreEqual(wordAlreadyTranslated, !res, "If word is already translated via another language, merge should not be possible");
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


