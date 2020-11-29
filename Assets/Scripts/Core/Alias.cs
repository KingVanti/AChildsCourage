namespace AChildsCourage
{

    public static class Aliasing
    {

        public class Alias<T>
        {

            private T Element { get; set; }


            public static implicit operator T(Alias<T> alias)
            {
                return alias.Element;
            }

            public static implicit operator Alias<T>(T element)
            {
                return new Alias<T> { Element = element };
            }

        }

    }

}