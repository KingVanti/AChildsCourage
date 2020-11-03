namespace AChildsCourage
{

    public interface IRNG
    {

        #region Methods

        float GetValue01();

        float GetValueUnder(float max);

        float GetValueBetween(float min, float max);

        int GetValueUnder(int max);

        int GetValueBetween(int min, int max);

        #endregion

    }

}