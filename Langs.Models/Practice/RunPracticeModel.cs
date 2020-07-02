namespace Langs.Models.Practice
{
    public class RunPracticeModel
    {
        public (string From, string To) Language { get; set; }
        public (int ID, string Name) Book { get; set; }
        public (int Top, int Bottom) WordRange { get; set; } // TODO: What if word indices change while running practice
     
        public (int FromID, int ToID, string FromText, string ToText, string ToAlternateSpelling, string ToPronunciation, int Index)[] Words { get; set; }
    }
}
