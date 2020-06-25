namespace Langs.Models.Practice
{
    public class PracticeModel
    {
        public int SelectedBookID { get; set; }
        public (int ID, string Name, int WordCount)[] Books { get; set; }
        public (int Top, int Bottom) WordRange { get; set; }

        /*

        public int AccountID { get; set; }
        public (int FromID, int ToID, string FromText, string ToText, string ToAlternateSpelling, string ToPronunciation)[] Words { get; set; }
        public (int ID, string Name) Book { get; set; }

    */
    }
}
