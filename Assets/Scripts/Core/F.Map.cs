using System;

namespace AChildsCourage
{

    public static partial class F
    {

        public static TResult Map<TItem, TResult>(this TItem item, Func<TItem, TResult> function) =>
            function(item);


        public static TResult Map<TItem, TResult, TP1>(this TItem item, Func<TItem, TP1, TResult> function, TP1 p1) =>
            function(item, p1);

        public static TResult Map<TItem, TResult, TP1, TP2>(this TItem item, Func<TItem, TP1, TP2, TResult> function, TP1 p1, TP2 p2) =>
            function(item, p1, p2);

        public static TResult Map<TItem, TResult, TP1, TP2, TP3>(this TItem item, Func<TItem, TP1, TP2, TP3, TResult> function, TP1 p1, TP2 p2, TP3 p3) =>
            function(item, p1, p2, p3);

    }

}