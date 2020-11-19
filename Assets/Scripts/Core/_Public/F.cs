using System;
using System.Collections.Generic;

namespace AChildsCourage
{

    public static class F
    {

        public static T Pipe<T>(T input)
        {
            return input;
        }

        public static T Then<T>(this T input)
        {
            return input;
        }

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
            else
                return input;
        }

        public static void While(this Action action, Func<bool> predecate)
        {
            while (predecate())
                action();
        }

        public static U Into<T, U>(this T item, Func<T, U> function)
        {
            return function(item);
        }

        public static void Into<T>(this T item, Action<T> action)
        {
            action(item);
        }

        public static U IntoWith<T, U, V>(this T item, Func<T, V, U> function, V param)
        {
            return function(item, param);
        }

        public static void IntoWith<T, U>(this T item, Action<T, U> action, U param)
        {
            action(item, param);
        }

        public static IEnumerable<T> AllInto<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
                action(element);

            return elements;
        }

        public static IEnumerable<T> AllInto<T, U>(this IEnumerable<T> elements, Func<T, U> function)
        {
            foreach (var element in elements)
                _ = function(element);

            return elements;
        }

        public static U ThenPipe<T, U>(this T _, U item)
        {
            return item;
        }

        public static U FinallyReturn<T, U>(this T _, U item)
        {
            return _.ThenPipe(item);
        }

        public static bool Negate(this bool b)
        {
            return !b;
        }

    }


}