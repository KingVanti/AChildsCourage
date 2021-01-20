using System;

namespace AChildsCourage
{

    public static partial class F
    {

        internal static void Do<TItem>(this TItem item, Action<TItem> action) =>
            action(item);

    }

}