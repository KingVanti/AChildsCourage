using UnityEngine;

namespace AChildsCourage.Game.Items
{

    [CreateAssetMenu(fileName = "Item data", menuName = "A Child's Courage/Item data")]
    public class ItemDataAsset : ScriptableObject
    {

        #region Properties

        public ItemData Data => new ItemData((ItemId) id, name);

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private int id;
        [SerializeField] private new string name;

#pragma warning restore 649

        #endregion

    }

}