using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AChildsCourage
{

    public static partial class F
    {

        internal static TItem For<TItem>(this TItem input, int times, Func<TItem, TItem> function)
        {
            var result = input;

            for (var _ = 0; _ < times; _++) result = function(result);

            return result;
        }

        internal static TItem DoIf<TItem>(this TItem input, Func<TItem, TItem> function, bool predicate) =>
            predicate ? function(input) : input;

        internal static void ForEach<TItem>(this IEnumerable<TItem> elements, Action<TItem> action)
        {
            foreach (var element in elements) action(element);
        }
        
        internal static TAccumulate Cycle<TAccumulate>(this TAccumulate seed, Func<TAccumulate, TAccumulate> func, int times)
        {
            var accumulate = seed;

            for (var i = 0; i < times; i++) accumulate = func(accumulate);

            return accumulate;
        }

        internal static TResult Match<TItem, TResult>(this IEnumerable<TItem> items, Func<IEnumerable<TItem>, TResult> notEmpty, Func<TResult> empty) =>
            items.ToImmutableArray()
                 .Map(its => its.Any()
                          ? notEmpty(its)
                          : empty());

        internal static bool If(bool predicate) =>
            predicate;

        internal static void Then(this bool predicate, Action action)
        {
            if (predicate) action();
        }

        internal static TItem FirstByDescending<TItem>(this IEnumerable<TItem> items, Func<TItem, float> selector) =>
            items.OrderByDescending(selector).First();

        internal static IEnumerable<TItem> Where<TItem, TP1>(this IEnumerable<TItem> items, Func<TP1, TItem, bool> predicate, TP1 p1) =>
            items.Where(item => predicate(p1, item));

        internal static IEnumerable<TRes> Select<TItem, TP1, TRes>(this IEnumerable<TItem> items, Func<TP1, TItem, TRes> selector, TP1 p1) =>
            items.Select(item => selector(p1, item));

        internal static int Count<TItem, TP1>(this IEnumerable<TItem> items, Func<TP1, TItem, bool> selector, TP1 p1) =>
            items.Count(item => selector(p1, item));

        internal static IEnumerable<TItem> IfEmpty<TItem>(this IEnumerable<TItem> items, Func<IEnumerable<TItem>> replacement) =>
            !items.Any() ? replacement() : items;

        internal static Func<TP1, TRes> Fun<TP1, TRes>(Func<TP1, TRes> func)
            => func;

        internal static IEnumerable<T> AsSingleEnumerable<T>(this T item) { yield return item; }

    }

}