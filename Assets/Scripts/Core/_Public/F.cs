using System;

namespace AChildsCourage
{

    public static class F
    {

        public static T RepeatFeed<T>(this T input, Func<T, T> function, int times)
        {
            var result = input;

            for (var _ = 0; _ < times; _++)
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


    }


}