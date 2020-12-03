using System;
using System.Collections.Generic;

namespace AChildsCourage
{

    public static class F
    {

        public static T Take<T>(T input) => input;

        public static T RepeatFor<T>(this T input, Func<T, T> function, int times)
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

        public static T RepeatWhile<T>(this T input, Func<T, T> function, Func<bool> predicate)
        {
            var result = input;

            while (predicate())
                result = function(result);

            return result;
        }

        public static T RepeatWhile<T>(this T input, Func<T, T> function, Func<T, bool> predicate)
        {
            var result = input;

            while (predicate(result))
                result = function(result);

            return result;
        }

        public static T DoIf<T>(this T input, Func<T, T> function, bool predecate)
        {
            if (predecate)
                return function(input);
            return input;
        }

        public static void While(this Action action, Func<bool> predecate)
        {
            while (predecate())
                action();
        }

        public static TU Map<T, TU>(this T item, Func<T, TU> function) => function(item);

        public static void Do<T>(this T item, Action<T> action)
        {
            action(item);
        }

        public static TU MapWith<T, TU, TV>(this T item, Func<T, TV, TU> function, TV param) => function(item, param);

        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
                action(element);
        }

        public static void ForEach<T, TU>(this IEnumerable<T> elements, Func<T, TU> function)
        {
            foreach (var element in elements)
                _ = function(element);
        }

        public static TU ThenTake<T, TU>(this T _, TU item) => item;

        public static TU FinallyReturn<T, TU>(this T _, TU item) => _.ThenTake(item);

        public static bool Negate(this bool b) => !b;

        public static TU? Bind<T, TU>(this T? item, Func<T, TU> function) where T : struct where TU : struct => item.HasValue ? function(item.Value) : (TU?) null;

        public static TU NullBind<T, TU>(this T item, Func<T, TU> function) where T : class where TU : class => item != null ? function(item) : null;

        public static T IfNull<T>(this T? item, T replacement) where T : struct => item ?? replacement;

    }


}