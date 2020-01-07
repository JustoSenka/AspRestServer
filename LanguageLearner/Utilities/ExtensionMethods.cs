using LangData.Objects.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
    public static List<SelectListItem> SelectListItems(this IEnumerable<IListableElement> listable)
    {
        return listable?.Select(e => new SelectListItem(e.DisplayText, e.ID.ToString())).ToList();
    }
}
