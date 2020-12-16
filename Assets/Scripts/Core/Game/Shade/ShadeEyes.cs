using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.Shade.ShadeEvents;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
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
                if (characterVisibility == value) return;

                characterVisibility = value;
                onCharacterVisibilityChanged.Invoke(characterVisibility);
            }
        }

        public TilesInView CurrentTilesInView => new TilesInView(currentTilesInView);


        private float WaitTime => 1f / updatesPerSecond;

        private Vector2 CurrentPosition => transform.position;

        private IEnumerable<Vector2> CurrentCharacterVisionPoints => characterVisionPoints.Select(p => (Vector2) p.position);

        private Vector2 CurrentTileCenterPosition => CurrentPosition
                                                     .Map(ToTile)
                                                     .Map(GetTileCenter);

        #endregion

        #region Methods

        private void OnEnable() => StartCoroutine(ContinuallyUpdateVision());

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

            CharacterVisibility = visionPoints.Any(IsInPrimaryVision) ? Visibility.Primary
                : visionPoints.Any(IsInSecondaryVision) ? Visibility.Secondary
                : Visibility.NotVisible;
        }

        private bool IsInPrimaryVision(Vector2 visionPoint) => IsInView(primaryVision, visionPoint);

        private bool IsInSecondaryVision(Vector2 visionPoint) => IsInView(secondaryVision, visionPoint);

        private bool IsInView(VisionCone cone, Vector2 visionPoint) =>
            IsInViewRadius(cone, visionPoint) &&
            IsInViewAngle(cone, visionPoint) &&
            IsUnobstructed(visionPoint);

        private bool IsInViewRadius(VisionCone cone, Vector2 visionPoint) => Vector3.Distance(CurrentPosition, visionPoint) <= cone.ViewRadius;

        private bool IsInViewAngle(VisionCone cone, Vector2 visionPoint)
        {
            var dirToPoint = visionPoint - CurrentPosition;
            return Vector3.Angle(transform.right, dirToPoint) < cone.ViewAngle / 2f;
        }

        private bool IsUnobstructed(Vector2 visionPoint)
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

        private IEnumerable<TilePosition> GetTilePositionsInView() => GetPositionsInView().Select(ToTile);

        private IEnumerable<Vector2> GetPositionsInView() => GetPositionsInViewCone().Where(PositionIsVisible);

        private IEnumerable<Vector2> GetPositionsInViewCone()
        {
            for (var dX = -secondaryVision.ViewRadius; dX <= secondaryVision.ViewRadius; dX++)
                for (var dY = -secondaryVision.ViewRadius; dY <= secondaryVision.ViewRadius; dY++)
                    yield return new Vector2(CurrentTileCenterPosition.x + dX, CurrentTileCenterPosition.y + dY);
        }

        public bool PositionIsVisible(Vector2 position) => PositionIsInSmallRadius(position) || IsInView(secondaryVision, position);

        private bool PositionIsInSmallRadius(Vector2 position) => Vector2.Distance(transform.position, position) <= smallVisionRadius;

        #endregion

    }

}