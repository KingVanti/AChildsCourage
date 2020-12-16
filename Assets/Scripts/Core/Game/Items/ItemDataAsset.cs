using UnityEngine;

namespace AChildsCourage.Game.Items
{

    [CreateAssetMenu(fileName = "Item data", menuName = "A Child's Courage/Item data")]
    public class ItemDataAsset : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int id;

#pragma warning restore 649

        #endregion

        #region Properties

        public ItemData Data => new ItemData((ItemId) id, name);

        #endregion

    }

}