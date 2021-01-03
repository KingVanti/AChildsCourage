using AChildsCourage.Game.Floors.Courage;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Gen
{

    [CreateAssetMenu(menuName = "A Child's Courage/Floor-gen parameters", fileName = "New parameters")]
    public class FloorGenParamsAsset : ScriptableObject
    {

        [SerializeField] [Range(5, 20)] private int roomCount;
        [SerializeField] [Range(0.1f, 20)] private float clumpingFactor;
        [SerializeField] private EnumArray<CourageVariant, int> couragePickupCounts;
        [SerializeField] [Range(1, 10)] private int runeCount;


        public static FloorGenParams CreateParams(int seed, RoomCollection roomCollection, FloorGenParamsAsset asset) =>
            new FloorGenParams(seed,
                               roomCollection,
                               asset.roomCount,
                               asset.clumpingFactor,
                               asset.couragePickupCounts,
                               asset.runeCount);

    }

}