using Langs.Data.Objects.Base;
using Langs.Models.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
    public static List<SelectListItem> SelectListItems(this IEnumerable<IListableElement> listable)
    {
        return listable?.Select(ToListItem).ToList();
    }

    public static SelectListItem ToListItem(this IListableElement listable)
    {
        return new SelectListItem(listable.DisplayText, listable.ID.ToString());
    }

    public static IEnumerable<TableElement> SelectLinkTableElements(this IEnumerable<IListableElement> listable)
    {
        return listable?.Select(ToLinkTableElement);
    }

    public static TableElement ToLinkTableElement(this IListableElement listable)
    {
        return TableElement.Link(listable.ID, listable.DisplayText);
    }
}
