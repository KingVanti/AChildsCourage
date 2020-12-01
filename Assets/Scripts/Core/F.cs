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

        public static U Map<T, U>(this T item, Func<T, U> function) => function(item);

        public static void Do<T>(this T item, Action<T> action)
        {
            action(item);
        }

        public static U IntoWith<T, U, V>(this T item, Func<T, V, U> function, V param) => function(item, param);

        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
                action(element);
        }

        public static void ForEach<T, U>(this IEnumerable<T> elements, Func<T, U> function)
        {
            foreach (var element in elements)
                _ = function(element);
        }

        public static U ThenTake<T, U>(this T _, U item) => item;

        public static U FinallyReturn<T, U>(this T _, U item) => _.ThenTake(item);

        public static bool Negate(this bool b) => !b;

        public static U? Bind<T, U>(this T? item, Func<T, U> function) where T : struct where U : struct => item.HasValue ? function(item.Value) : (U?) null;

        public static U NullBind<T, U>(this T item, Func<T, U> function) where T : class where U : class => item != null ? function(item) : null;

        public static T IfNull<T>(this T? item, T replacement) where T : struct => item ?? replacement;

    }


}