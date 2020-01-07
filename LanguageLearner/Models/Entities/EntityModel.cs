using LangData.Objects;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LanguageLearner.Models
{
    public class EntityModel
    {
        public bool IsEditing { get; set; }

        public Word Word { get; set; }
        public Definition Definition { get; set; }

        public Language[] AvailableLanguages { get; set; }

        public List<SelectListItem> AvailableLanguagesListItems => 
            AvailableLanguages?.Select(e => new SelectListItem(e.Name, e.ID.ToString())).ToList();
    }
}
