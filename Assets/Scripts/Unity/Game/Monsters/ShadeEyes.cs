using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.Monsters.ShadeEvents;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters
{

    public class ShadeEyes : MonoBehaviour
    {

        #region Fields

        public VisibilityEvent onCharacterVisibilityChanged;
        public TilesInViewEvent onTilesInViewChanged;

#pragma warning disable 649

        [SerializeField] private float smallVisionRadius;
        [SerializeField] private float updatesPerSecond;
        [SerializeField] private VisionCone primaryVision;
        [SerializeField] private VisionCone secondaryVision;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private Transform[] characterVisionPoints;

#pragma warning restore 649

        private Visibility characterVisibility;
        private readonly HashSet<TilePosition> currentTilesInView = new HashSet<TilePosition>();

        #endregion

        #region Properties

        public VisionCone PrimaryVision => primaryVision;

        public VisionCone SecondaryVision => secondaryVision;

        public Visibility CharacterVisibility
        {
            get => characterVisibility;
            private set
            {
                if (characterVisibility == value)
                    return;

                characterVisibility = value;
                onCharacterVisibilityChanged.Invoke(characterVisibility);
            }
        }

        public TilesInView CurrentTilesInView => new TilesInView(currentTilesInView);


        private float WaitTime => 1f / updatesPerSecond;

        private Vector3 CurrentPosition => transform.position;

        private IEnumerable<Vector3> CurrentCharacterVisionPoints => characterVisionPoints.Select(p => p.position);

        private Vector3 CurrentTileCenterPosition => CurrentPosition.GetTileCenter();

        #endregion

        #region Methods

        private void Awake()
        {
            StartCoroutine(ContinuallyUpdateVision());
        }

        private IEnumerator ContinuallyUpdateVision()
        {
            while (true)
            {
                yield return new WaitForSeconds(WaitTime);
                UpdateCharacterVision();
                UpdateTilesInVision();
            }
        }

        private void UpdateCharacterVision()
        {
            var visionPoints = CurrentCharacterVisionPoints.ToImmutableArray();

            CharacterVisibility = visionPoints.Any(IsInPrimaryVision)
                ? Visibility.Primary
                : visionPoints.Any(IsInSecondaryVision)
                    ? Visibility.Secondary
                    : Visibility.NotVisible;
        }

        private bool IsInPrimaryVision(Vector3 visionPoint) => IsInView(primaryVision, visionPoint);

        private bool IsInSecondaryVision(Vector3 visionPoint) => IsInView(secondaryVision, visionPoint);

        private bool IsInView(VisionCone cone, Vector3 visionPoint) =>
            IsInViewRadius(cone, visionPoint) &&
            IsInViewAngle(cone, visionPoint) &&
            IsUnobstructed(visionPoint);

        private bool IsInViewRadius(VisionCone cone, Vector3 visionPoint) => Vector3.Distance(CurrentPosition, visionPoint) <= cone.ViewRadius;

        private bool IsInViewAngle(VisionCone cone, Vector3 visionPoint)
        {
            var dirToPoint = visionPoint - CurrentPosition;
            return Vector3.Angle(transform.right, dirToPoint) < cone.ViewAngle / 2f;
        }

        private bool IsUnobstructed(Vector3 visionPoint)
        {
            var dirToPoint = visionPoint - CurrentPosition;
            return !Physics2D.Raycast(CurrentPosition, dirToPoint, dirToPoint.magnitude, obstructionLayers);
        }

        private void UpdateTilesInVision()
        {
            currentTilesInView.Clear();
            currentTilesInView.UnionWith(GetTilePositionsInView());
            onTilesInViewChanged?.Invoke(CurrentTilesInView);
        }

        private IEnumerable<TilePosition> GetTilePositionsInView() => GetPositionsInView().Select(UtilityExtensions.FloorToTile);

        private IEnumerable<Vector3> GetPositionsInView() => GetPositionsInViewCone().Where(PositionIsVisible);

        private IEnumerable<Vector3> GetPositionsInViewCone()
        {
            for (var dX = -secondaryVision.ViewRadius; dX <= secondaryVision.ViewRadius; dX++)
                for (var dY = -secondaryVision.ViewRadius; dY <= secondaryVision.ViewRadius; dY++)
                    yield return new Vector3(CurrentTileCenterPosition.x + dX, CurrentTileCenterPosition.y + dY);
        }

        private bool PositionIsVisible(Vector3 position) => PositionIsInSmallRadius(position)|| IsInView(secondaryVision, position);

        private bool PositionIsInSmallRadius(Vector3 position) => Vector3.Distance(transform.position, position) <= smallVisionRadius;

        #endregion

    }

}