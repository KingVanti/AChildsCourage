namespace AChildsCourage
{

    public static class Aliasing
    {

        public class Alias<T>
        {

            private T Element { get; set; }


            public static implicit operator T(Alias<T> alias) => alias.Element;

            public static implicit operator Alias<T>(T element) => new Alias<T>() { Element = element };

        }

    }

}