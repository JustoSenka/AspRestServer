using Langs.Data.Objects.Base;

namespace Langs.Data.Objects
{
    public class Definition : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Definition() { }
        public Definition(string description)
        {
            Description = description;
        }

        public virtual string Description { get; set; }

        // -----------------
        string IDisplayText.DisplayText => Description;
    }
}
