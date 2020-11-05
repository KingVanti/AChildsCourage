namespace AChildsCourage
{

    public class Weighted<T>
    {

        #region Properties

        public T Object { get; }

        public float Weight { get; }

        #endregion

        #region Constructors

        public Weighted(T @object, float weight)
        {
            Object = @object;
            Weight = weight;
        }

        #endregion

    }

}