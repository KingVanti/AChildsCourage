﻿using System;

namespace AChildsCourage
{

    public static partial class F
    {

        public static TResult Map<TItem, TResult>(this TItem item, Func<TItem, TResult> function) =>
            function(item);

        internal static TResult Map<TItem, TResult, TP1>(this TItem item, Func<TP1, TItem, TResult> function, TP1 p1) =>
            function(p1, item);

        internal static TResult Map<TItem, TResult, TP1, TP2>(this TItem item, Func<TP1, TP2, TItem, TResult> function, TP1 p1, TP2 p2) =>
            function(p1, p2, item);

        internal static TResult Map<TItem, TResult, TP1, TP2, TP3>(this TItem item, Func<TP1, TP2, TP3, TItem, TResult> function, TP1 p1, TP2 p2, TP3 p3) =>
            function(p1, p2, p3, item);

        internal static TResult Map<TItem, TResult, TP1, TP2, TP3, TP4>(this TItem item, Func<TP1, TP2, TP3, TP4, TItem, TResult> function, TP1 p1, TP2 p2, TP3 p3, TP4 p4) =>
            function(p1, p2, p3, p4, item);

    }

}