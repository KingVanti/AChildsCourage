using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.Game.Shade.MShadeVision;
using static AChildsCourage.Game.Shade.MTilesInView;
using static AChildsCourage.Game.Shade.MVisibility;
using static AChildsCourage.Game.Shade.MVisionCone;

namespace AChildsCourage.Game.Shade
{

    public class ShadeEyesEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<CharVisibilityChangedEventArgs> OnCharVisibilityChanged;

        [Pub] public event EventHandler<TilesInViewChangedEventArgs> OnTilesInViewChanged;


        [SerializeField] private float updatesPerSecond;
        [SerializeField] private VisionCone[] visionCones;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private Transform[] characterVisionPoints;

        private Visibility charVisibility;


        public IEnumerable<VisionCone> VisionCones => visionCones;

        private float WaitTime => 1f / updatesPerSecond;

        private float LargestViewRadius => visionCones.Max(c => c.ViewRadius);

        private Visibility CharVisibility
        {
            get => charVisibility;
            set
            {
                if (CharVisibility == value) return;

                charVisibility = value;
                OnCharVisibilityChanged?.Invoke(this, new CharVisibilityChangedEventArgs(CharVisibility));
            }
        }

        private Vector2 CurrentPosition => transform.position;

        private TilePosition CurrentTilePosition => CurrentPosition.Map(ToTile);

        private ShadeVision Vision => new ShadeVision(Head, visionCones);

        private ShadeHead Head => new ShadeHead(transform.position, transform.right, ObstacleExistsBetween);

        private TilesInView TilesInView
        {
            set => OnTilesInViewChanged?.Invoke(this, new TilesInViewChangedEventArgs(value));
        }

        private IEnumerable<Vector2> CurrentCharacterVisionPoints => characterVisionPoints.Select(p => (Vector2) p.position);

        private void OnEnable() =>
            this.DoContinually(UpdateVision, WaitTime);
        
        public bool CanSee(Vector2 point) =>
            CanSeePoint(Vision, point);

        private Visibility CalculateCharacterVisibility() =>
            CurrentCharacterVisionPoints
                .Select(point => GetPointVisibility(Vision, point))
                .Map(GetHighestValue);

        public TilesInView CalculateTilesInView() =>
            FindPositionsInRadius(CurrentTilePosition, LargestViewRadius)
                .Where(CanSee)
                .Map(ToTilesInView);

        private bool CanSee(TilePosition position) =>
            CanSeePoint(Vision, GetTileCenter(position));

        private bool ObstacleExistsBetween(Vector2 point1, Vector2 point2)
        {
            var dirToPoint = point2 - point1;
            return Physics2D.Raycast(point1, dirToPoint, dirToPoint.magnitude, obstructionLayers);
        }

        private void UpdateVision()
        {
            CharVisibility = CalculateCharacterVisibility();
            TilesInView = CalculateTilesInView();
        }

    }

}