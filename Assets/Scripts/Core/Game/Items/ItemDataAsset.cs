using UnityEngine;

namespace AChildsCourage.Game.Items
{

    [CreateAssetMenu(fileName = "Item data", menuName = "A Child's Courage/Item data")]
    public class ItemDataAsset : ScriptableObject
    {

        #region Properties

        public ItemData Data => new ItemData((ItemId) _id, _name);

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private int _id;
        [SerializeField] private string _name;

#pragma warning restore 649

        #endregion

    }

}