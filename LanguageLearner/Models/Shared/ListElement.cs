namespace LanguageLearner.Models.Shared
{
    public struct ListElement
    {
        public string Text { get; private set; }
        public string AspAction { get; private set; }
        public string AspController { get; private set; }
        public string AspRoute { get; private set; }

        public bool HasLinkAction { get; private set; }

        public ListElement(string Text, string AspAction, string AspController, string AspRoute)
        {
            HasLinkAction = true;
            this.Text = Text;
            this.AspAction = AspAction;
            this.AspController = AspController;
            this.AspRoute = AspRoute;
        }
        public ListElement(string Text)
        {
            HasLinkAction = false;
            this.Text = Text;

            this.AspAction = "";
            this.AspController = "";
            this.AspRoute = "";
        }
    }
}
