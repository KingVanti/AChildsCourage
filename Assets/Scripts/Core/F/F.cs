using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AChildsCourage
{

    public static partial class F
    {

        public static TItem For<TItem>(this TItem input, int times, Func<TItem, TItem> function)
        {
            var result = input;

            for (var _ = 0; _ < times; _++) result = function(result);

            return result;
        }

        public static TItem While<TItem>(this TItem input, Func<bool> predicate, Func<TItem, TItem> function)
        {
            var result = input;

            while (predicate()) result = function(result);

            return result;
        }

        public static TItem While<TItem>(this TItem input, Func<TItem, bool> predicate, Func<TItem, TItem> function) =>
            predicate(input)
                ? function(input).While(predicate, function)
                : input;

        public static TItem DoIf<TItem>(this TItem input, Func<TItem, TItem> function, bool predicate) =>
            predicate ? function(input) : input;

        public static TItem DoIf<TItem>(this TItem input, Func<TItem, TItem> function, Func<TItem, bool> predicate) =>
            predicate(input) ? function(input) : input;

        public static void While(this Action action, Func<bool> predicate)
        {
            while (predicate()) action();
        }

        public static void ForEach<TItem>(this IEnumerable<TItem> elements, Action<TItem> action)
        {
            foreach (var element in elements) action(element);
        }

        public static void ForEach<TItem, TResult>(this IEnumerable<TItem> elements, Func<TItem, TResult> function)
        {
            foreach (var element in elements) _ = function(element);
        }

        public static TReplace ThenTake<TItem, TReplace>(this TItem _, TReplace item) =>
            item;

        public static TReturn FinallyReturn<TItem, TReturn>(this TItem _, TReturn item) =>
            _.ThenTake(item);

        public static bool Negate(this bool b) =>
            !b;

        public static TResult? Bind<TItem, TResult>(this TItem? item, Func<TItem, TResult> function) where TItem : struct where TResult : struct =>
            item.HasValue ? function(item.Value) : (TResult?) null;

        public static TResult NullBind<TItem, TResult>(this TItem item, Func<TItem, TResult> function) where TItem : class where TResult : class =>
            item != null ? function(item) : null;

        public static TItem IfNull<TItem>(this TItem? item, TItem replacement) where TItem : struct =>
            item ?? replacement;

        public static TAccumulate AggregateI<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<int, TAccumulate, TSource, TAccumulate> func)
        {
            var accumulate = seed;
            var sourceArray = source.ToArray();

            for (var i = 0; i < sourceArray.Length; i++) accumulate = func(i, accumulate, sourceArray[i]);

            return accumulate;
        }

        public static TAccumulate Cycle<TAccumulate>(this TAccumulate seed, Func<TAccumulate, TAccumulate> func, int times)
        {
            var accumulate = seed;

            for (var i = 0; i < times; i++) accumulate = func(accumulate);

            return accumulate;
        }

        public static TResult Match<TItem, TResult>(this IEnumerable<TItem> items, Func<IEnumerable<TItem>, TResult> notEmpty, Func<TResult> empty) =>
            items.ToImmutableArray()
                 .Map(its => its.Any()
                          ? notEmpty(its)
                          : empty());

        public static bool If(bool predicate) =>
            predicate;

        public static void Then(this bool predicate, Action action)
        {
            if (predicate) action();
        }

        public static TItem FirstBy<TItem>(this IEnumerable<TItem> items, Func<TItem, float> selector) =>
            items.OrderBy(selector).First();

        public static TItem FirstByDescending<TItem>(this IEnumerable<TItem> items, Func<TItem, float> selector) =>
            items.OrderByDescending(selector).First();

        public static IEnumerable<TItem> Where<TItem, TP1>(this IEnumerable<TItem> items, Func<TP1, TItem, bool> predicate, TP1 p1) =>
            items.Where(item => predicate(p1, item));

        public static IEnumerable<TRes> Select<TItem, TP1, TRes>(this IEnumerable<TItem> items, Func<TP1, TItem, TRes> selector, TP1 p1) =>
            items.Select(item => selector(p1, item));

        public static int Count<TItem, TP1>(this IEnumerable<TItem> items, Func<TP1, TItem, bool> selector, TP1 p1) =>
            items.Count(item => selector(p1, item));

        public static IEnumerable<TItem> IfEmpty<TItem>(this IEnumerable<TItem> items, Func<IEnumerable<TItem>> replacement) =>
            !items.Any() ? replacement() : items;

        public static Func<TP1, TRes> Fun<TP1, TRes>(Func<TP1, TRes> func)
            => func;

        public static IEnumerable<T> AsSingleEnumerable<T>(this T item) { yield return item; }

    }

}