namespace Langs.Models.Practice
{
    public class PracticeModel
    {
        public (int ID, string Name, int WordCount)[] Books { get; set; }
        public int SelectedBookID { get; set; }
        public int WordRangeTop { get; set; }
        public int WordRangeBottom { get; set; }
    }
}
