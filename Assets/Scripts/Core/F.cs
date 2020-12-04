using System;
using System.Collections.Generic;

namespace AChildsCourage
{

    public static class F
    {

        public static TItem Take<TItem>(TItem input) => input;

        public static TItem RepeatFor<TItem>(this TItem input, Func<TItem, TItem> function, int times)
        {
            var result = input;

            for (var _ = 0; _ < times; _++)
                result = function(result);

            return result;
        }

        public static void RepeatFor(this Action action, int times)
        {
            for (var _ = 0; _ < times; _++)
                action();
        }

        public static TItem RepeatWhile<TItem>(this TItem input, Func<TItem, TItem> function, Func<bool> predicate)
        {
            var result = input;

            while (predicate())
                result = function(result);

            return result;
        }

        public static TItem RepeatWhile<TItem>(this TItem input, Func<TItem, TItem> function, Func<TItem, bool> predicate)
        {
            var result = input;

            while (predicate(result))
                result = function(result);

            return result;
        }

        public static TItem DoIf<TItem>(this TItem input, Func<TItem, TItem> function, bool predicate)
        {
            return predicate ? function(input) : input;
        }

        public static void While(this Action action, Func<bool> predicate)
        {
            while (predicate())
                action();
        }

        public static TResult Map<TItem, TResult>(this TItem item, Func<TItem, TResult> function) => function(item);

        public static void Do<TItem>(this TItem item, Action<TItem> action)
        {
            action(item);
        }

        public static TResult MapWith<TItem, TResult, TParam>(this TItem item, Func<TItem, TParam, TResult> function, TParam param) => function(item, param);

        public static void ForEach<TItem>(this IEnumerable<TItem> elements, Action<TItem> action)
        {
            foreach (var element in elements)
                action(element);
        }

        public static void ForEach<TItem, TResult>(this IEnumerable<TItem> elements, Func<TItem, TResult> function)
        {
            foreach (var element in elements)
                _ = function(element);
        }

        public static TReplace ThenTake<TItem, TReplace>(this TItem _, TReplace item) => item;

        public static TReturn FinallyReturn<TItem, TReturn>(this TItem _, TReturn item) => _.ThenTake(item);

        public static bool Negate(this bool b) => !b;

        public static TResult? Bind<TItem, TResult>(this TItem? item, Func<TItem, TResult> function) where TItem : struct where TResult : struct => item.HasValue ? function(item.Value) : (TResult?) null;

        public static TResult NullBind<TItem, TResult>(this TItem item, Func<TItem, TResult> function) where TItem : class where TResult : class => item != null ? function(item) : null;

        public static TItem IfNull<TItem>(this TItem? item, TItem replacement) where TItem : struct => item ?? replacement;

    }


}