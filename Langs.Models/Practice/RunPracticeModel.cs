using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Langs.Models.Practice
{
    public class RunPracticeModel
    {
        public string Test { get; set; }

        public (string From, string To) Language { get; set; }

        [BindProperty]
        public (int ID, string Name) Book { get; set; }
        public (int Top, int Bottom) WordRange { get; set; } // TODO: What if word indices change while running practice

        public (int FromID, int ToID, string FromText, string ToText, string ToAlternateSpelling, string ToPronunciation, int Index)[] Words { get; set; }

        public IEnumerable<PracticeWords> PracticeWords { get; set; }
    }

    public struct PracticeWords
    {
        public int FromID;
        public int ToID;
        public string FromText;
        public string ToText;
        public string ToAlternateSpelling;
        public string ToPronunciation;
        public int Index;

        public PracticeWords((int fromID, int toID, string fromText, string toText, string toAlternateSpelling, string toPronunciation, int index) t)
        {
            FromID = t.fromID;
            ToID = t.toID;
            FromText = t.fromText;
            ToText = t.toText;
            ToAlternateSpelling = t.toAlternateSpelling;
            ToPronunciation = t.toPronunciation;
            Index = t.index;
        }
    }
}
