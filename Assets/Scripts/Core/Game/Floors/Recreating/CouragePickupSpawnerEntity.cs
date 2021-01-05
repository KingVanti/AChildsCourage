using System.Collections.Immutable;
using AChildsCourage.Game.Floors.Courage;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public class CouragePickupSpawnerEntity : MonoBehaviour
    {

        [SerializeField] private GameObject couragePickupPrefab;

        [FindService] private CouragePickupAppearanceRepo.LoadCouragePickupAppearances loadCouragePickupAppearances;

        private ImmutableDictionary<CourageVariant, CouragePickupAppearance> couragePickupAppearances;


        private ImmutableDictionary<CourageVariant, CouragePickupAppearance> CouragePickupAppearances =>
            couragePickupAppearances ?? (couragePickupAppearances = loadCouragePickupAppearances().ToImmutableDictionary(a => a.Variant));


        public void Spawn(TilePosition position, CouragePickupData pickup)
        {
            var entity = InstantiatePickup(position);
            var appearance = CouragePickupAppearances[pickup.Variant];
            entity.Initialize(pickup.Variant, appearance);
        }

        private CouragePickupEntity InstantiatePickup(TilePosition tilePosition) =>
            Infrastructure.Spawn(couragePickupPrefab, tilePosition.Map(GetCenter), transform)
                          .GetComponent<CouragePickupEntity>();

    }

}