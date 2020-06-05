namespace Langs.Models.Shared
{
    public struct ListElement
    {
        public int ID { get; private set; }
        public string Text { get; private set; }
        public string AspAction { get; private set; }
        public string AspController { get; private set; }

        public bool HasLinkAction { get; private set; }

        public bool OverridesController => !string.IsNullOrEmpty(AspController);
        public bool OverridesAction => !string.IsNullOrEmpty(AspAction);

        public ListElement(string Text, string AspAction, string AspController, int ID)
        {
            HasLinkAction = true;
            this.Text = Text;
            this.AspAction = AspAction;
            this.AspController = AspController;
            this.ID = ID;
        }
        public ListElement(string Text)
        {
            HasLinkAction = false;
            this.Text = Text;

            this.AspAction = "";
            this.AspController = "";
            this.ID = 0;
        }
    }
}
