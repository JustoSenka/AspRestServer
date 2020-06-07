using Langs.Data.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;

public static class GlobalExtensionMethods
{
    public static int? ToNullable(this int val)
    {
        if (val == default)
            return null;

        return val;
    }
    public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var index = 0;
        foreach (var item in source)
        {
            if (predicate.Invoke(item))
                return index;

            index++;
        }

        return -1;
    }

    public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> range)
    {
        foreach (var el in range)
            set.Add(el);
    }

    public static T WithID<T>(this IEnumerable<T> source, int ID) where T : IHaveID
    {
        return source.FirstOrDefault(e => e.ID == ID);
    }

    public static T WithID<T>(this IEnumerable<T> source, IHaveID objWithID) where T : IHaveID
    {
        return source.WithID(objWithID.ID);
    }
}
