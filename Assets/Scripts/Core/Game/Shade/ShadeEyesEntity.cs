using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Game.Shade.ShadeVision;
using static AChildsCourage.Game.Char.Visibility;

namespace AChildsCourage.Game.Shade
{

    public class ShadeEyesEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<CharVisibilityChangedEventArgs> OnCharVisibilityChanged;


        [SerializeField] private float updatesPerSecond;
        [SerializeField] private VisionCone[] visionCones;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private Transform[] characterVisionPoints;

        private Visibility charVisibility;


        public IEnumerable<VisionCone> VisionCones => visionCones;

        private float WaitTime => 1f / updatesPerSecond;
        
        private Visibility CharVisibility
        {
            get => charVisibility;
            set
            {
                if (CharVisibility.Equals(value)) return;

                charVisibility = value;
                OnCharVisibilityChanged?.Invoke(this, new CharVisibilityChangedEventArgs(CharVisibility));
            }
        }

        private Vector2 CurrentPosition => transform.position;

        private TilePosition CurrentTilePosition => CurrentPosition.Map(ToTile);

        private ShadeVision Vision => new ShadeVision(Head, visionCones);

        private ShadeHead Head => new ShadeHead(transform.position, transform.right, ObstacleExistsBetween);

        private IEnumerable<Vector2> CurrentCharacterVisionPoints => characterVisionPoints.Select(p => (Vector2) p.position);


        private void OnEnable() =>
            this.DoContinually(UpdateVision, WaitTime);

        public bool CanSee(Vector2 point) =>
            Vision.Map(CanSeePoint, point);

        private Visibility CalculateCharacterVisibility() =>
            CurrentCharacterVisionPoints.Select(point => GetPointVisibility(Vision, point))
                                        .Map(GetHighestValue);

        private bool CanSee(TilePosition position) =>
            Vision.Map(CanSeePoint, position.Map(GetCenter));

        private bool ObstacleExistsBetween(Vector2 point1, Vector2 point2)
        {
            var dirToPoint = point2 - point1;
            return Physics2D.Raycast(point1, dirToPoint, dirToPoint.magnitude, obstructionLayers);
        }

        private void UpdateVision() => CharVisibility = CalculateCharacterVisibility();

    }

}