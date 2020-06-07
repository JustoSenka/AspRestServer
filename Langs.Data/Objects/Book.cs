using Langs.Data.Objects.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Book: {Name} Count: {WordCount}")]
    public class Book : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Book() { }
        public Book(string name, Language Language, string description = "")
        {
            Name = name;
            Description = description;
            this.Language = Language;
        }

        [Required, MaxLength(k_WordLength)]
        public virtual string Name { get; set; }

        [Required, ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        public virtual int LanguageID { get; set; }

        [MaxLength(200)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Don't use me. I'm here because EF CORE does not support many to many relations. Use provided interfaces instead.
        /// </summary>
        public virtual HashSet<BookWord> _BookWordCollection { get; set; } = new HashSet<BookWord>();

        // API -----------
         
        [NotMapped]
        public int WordCount => _BookWordCollection.Count;

        public void AddWord(MasterWord word)
        {
            if (_BookWordCollection.Any(bw => bw.MasterWordId == word.ID || bw.MasterWord == word))
                return;

            _BookWordCollection.Add(new BookWord() { Book = this, BookId = ID, MasterWord = word, MasterWordId = word.ID });
        }

        public void RemoveWord(MasterWord word) => RemoveWord(word.ID);
        public void RemoveWord(int masterWordID)
        {
            var first = _BookWordCollection.First(b => b.MasterWordId == masterWordID);
            _BookWordCollection.Remove(first);
        }

        public void ReplaceWord(MasterWord originalWord, MasterWord replacement)
        {
            RemoveWord(originalWord);
            AddWord(replacement);

            // Apparently doing it only on the book, then saving MasterWord will update everything correctly
            // replacement.AddBook(this);
        }

        [NotMapped]
        public IEnumerable<MasterWord> Words
        {
            get => _BookWordCollection?.Select(c => c.MasterWord);
            set => _BookWordCollection = value.Select(w => new BookWord() { Book = this, BookId = ID, MasterWord = w, MasterWordId = w.ID }).ToHashSet();
        }

        string IDisplayText.DisplayText => Name;


    }
}
