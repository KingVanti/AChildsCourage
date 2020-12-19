using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AChildsCourage
{

    public static class F
    {

        public static TItem Take<TItem>(TItem input) => input;

        public static TItem For<TItem>(this TItem input, int times, Func<TItem, TItem> function)
        {
            var result = input;

            for (var _ = 0; _ < times; _++) result = function(result);

            return result;
        }

        public static void For(this Action action, int times)
        {
            for (var _ = 0; _ < times; _++) action();
        }

        public static TItem While<TItem>(this TItem input, Func<bool> predicate, Func<TItem, TItem> function)
        {
            var result = input;

            while (predicate()) result = function(result);

            return result;
        }

        public static TItem While<TItem>(this TItem input, Func<TItem, bool> predicate, Func<TItem, TItem> function)
        {
            var result = input;

            while (predicate(result)) result = function(result);

            return result;
        }

        public static TItem DoIf<TItem>(this TItem input, Func<TItem, TItem> function, bool predicate) => predicate ? function(input) : input;

        public static void While(this Action action, Func<bool> predicate)
        {
            while (predicate()) action();
        }

        public static TResult Map<TItem, TResult>(this TItem item, Func<TItem, TResult> function) => function(item);

        public static TResult Map<TItem, TResult, TP1>(this TItem item, Func<TItem, TP1, TResult> function, TP1 p1) => function(item, p1);

        public static TResult Map<TItem, TResult, TP1, TP2>(this TItem item, Func<TItem, TP1, TP2, TResult> function, TP1 p1, TP2 p2) => function(item, p1, p2);

        public static void Do<TItem>(this TItem item, Action<TItem> action) => action(item);

        public static void ForEach<TItem>(this IEnumerable<TItem> elements, Action<TItem> action)
        {
            foreach (var element in elements) action(element);
        }

        public static void ForEach<TItem, TResult>(this IEnumerable<TItem> elements, Func<TItem, TResult> function)
        {
            foreach (var element in elements) _ = function(element);
        }

        public static TReplace ThenTake<TItem, TReplace>(this TItem _, TReplace item) => item;

        public static TReturn FinallyReturn<TItem, TReturn>(this TItem _, TReturn item) => _.ThenTake(item);

        public static bool Negate(this bool b) => !b;

        public static TResult? Bind<TItem, TResult>(this TItem? item, Func<TItem, TResult> function) where TItem : struct where TResult : struct => item.HasValue ? function(item.Value) : (TResult?) null;

        public static TResult NullBind<TItem, TResult>(this TItem item, Func<TItem, TResult> function) where TItem : class where TResult : class => item != null ? function(item) : null;

        public static TItem IfNull<TItem>(this TItem? item, TItem replacement) where TItem : struct => item ?? replacement;

        public static TAccumulate AggregateI<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<int, TAccumulate, TSource, TAccumulate> func)
        {
            var accumulate = seed;
            var sourceArray = source.ToArray();

            for (var i = 0; i < sourceArray.Length; i++) accumulate = func(i, accumulate, sourceArray[i]);

            return accumulate;
        }

        public static TAccumulate AggregateTimes<TAccumulate>(TAccumulate seed, Func<TAccumulate, TAccumulate> func, int times)
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

    }

}