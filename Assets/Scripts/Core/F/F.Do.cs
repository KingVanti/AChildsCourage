using System;

namespace AChildsCourage
{

    public static partial class F
    {

        public static void Do<TItem>(this TItem item, Action<TItem> action) =>
            action(item);


        public static void Do<TItem, TP1>(this TItem item, Action<TP1, TItem> action, TP1 p1) =>
            action(p1, item);

    }

}