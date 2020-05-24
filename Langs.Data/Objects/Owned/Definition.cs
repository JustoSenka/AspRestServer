using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [Owned]
    [DebuggerDisplay("Description: {Description}")]
    public class Definition
    {
        public Definition() { }
        public Definition(string description)
        {
            Description = description;
        }

        public virtual string Description { get; set; }
    }
}
