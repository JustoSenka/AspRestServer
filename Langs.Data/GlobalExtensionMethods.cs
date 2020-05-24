using System;
using System.Collections.Generic;

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
}
