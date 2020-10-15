using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int _id;

#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get { return _id; } set { _id = value; } }

        #endregion

    }

}