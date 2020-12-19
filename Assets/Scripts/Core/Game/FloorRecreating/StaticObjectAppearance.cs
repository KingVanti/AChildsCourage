using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    [CreateAssetMenu(fileName = "New static object", menuName = "A Child's Courage/Static Object")]
    public class StaticObjectAppearance : ScriptableObject
    {



        [SerializeField] private Sprite sprite;

#pragma warning  restore 649

        public Sprite Sprite => sprite;

    }

}