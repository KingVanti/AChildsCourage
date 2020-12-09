using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.MTilePosition;
using Random = UnityEngine.Random;

namespace AChildsCourage.Game.Monsters
{

    public class TileVision : MonoBehaviour
    {

        #region Static Properties

        private static Vector3 RandomVector => new Vector3(0, 0, Random.Range(0, 360));

        #endregion

        #region Static Methods

        private static TilePosition FloorToTile(Vector3 position) =>
            new TilePosition(
                Mathf.FloorToInt(position.x),
                Mathf.FloorToInt(position.y));

        #endregion

        #region Subclasses

        [Serializable]
        public class TilePositionsEvent : UnityEvent<IEnumerable<TilePosition>> { }

        #endregion

        #region Fields

        public TilePositionsEvent OnObservingTilesChanged;

#pragma warning disable 649
        [SerializeField] private int visionRadius;
        [Range(0, 360)] [SerializeField] private float visionAngle;
        [Range(0.01f, 3f)] [SerializeField] private float rotationTime;
        [SerializeField] private float updatesPerSecond;
        [SerializeField] private LayerMask wallLayer;
#pragma warning restore 649

        private bool isWatching;
        private bool isNearTarget;
        private Vector3 targetPos = Vector3.zero;
        private readonly HashSet<TilePosition> currentlyObservedTilePositions = new HashSet<TilePosition>();

        #endregion

        #region Properties

        private Vector3 CurrentTileCenterPosition =>
            new Vector3(
                Mathf.RoundToInt(transform.position.x) + 0.5f,
                Mathf.RoundToInt(transform.position.y) + 0.5f);

        private float WaitTime => 1f / updatesPerSecond;

        #endregion

        #region Methods

        private void Start()
        {
        //    StartCoroutine(Observe(visionRadius));
        }

        private IEnumerator Observe(int radius)
        {
            while (true)
            {
                if (!isWatching)
                    StartCoroutine(TurnToRotation(RandomVector));

                currentlyObservedTilePositions.Clear();
                currentlyObservedTilePositions.UnionWith(GetTilePositionsInView(radius));
                OnObservingTilesChanged?.Invoke(currentlyObservedTilePositions);

                yield return new WaitForSeconds(WaitTime);
            }
        }

        private IEnumerable<TilePosition> GetTilePositionsInView(int radius) => GetPositionsInView(radius).Select(FloorToTile);

        private IEnumerable<Vector3> GetPositionsInView(int radius)
        {
            for (var dX = -radius; dX <= radius; dX++)
                for (var dY = -radius; dY <= radius; dY++)
                {
                    var position = new Vector3(CurrentTileCenterPosition.x + dX, CurrentTileCenterPosition.y + dY);

                    if (PositionIsVisible(position, radius))
                        yield return position;
                }
        }

        private bool PositionIsVisible(Vector3 position, float radius) => IsInCloseRadius(position) || IsInViewCone(position, radius);

        private bool IsInCloseRadius(Vector3 position) => IsInRadius(position, 1);

        private bool IsInViewCone(Vector3 position, float radius) => IsInRadius(position, radius) && IsInAngleRange(position) && HasLineOfSightOn(position);

        private bool IsInRadius(Vector3 position, float radius) => Vector3.Distance(CurrentTileCenterPosition, position) <= radius;

        private bool IsInAngleRange(Vector3 position) => Vector3.Angle(position - transform.position, transform.right) <= visionAngle / 2;

        private bool HasLineOfSightOn(Vector3 position) => !Physics2D.Raycast(transform.position, (position - transform.position).normalized, Vector2.Distance(position, transform.position), wallLayer);


        private void OnDrawGizmos()
        {
            foreach (var position in currentlyObservedTilePositions)
                Gizmos.DrawWireSphere(position.ToVector3() + new Vector3(0.5f, 0.5f), 0.1f);
        }

        public void OnMinimumDistanceEntered(Vector3 target)
        {
            isNearTarget = true;
            targetPos = target;
        }

        public void OnMinimumDistanceLeft()
        {
            isNearTarget = false;
        }


        private IEnumerator TurnToRotation(Vector3 endRotation)
        {
            isWatching = true;

            if (isNearTarget)
            {
                var angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
                endRotation = new Vector3(0, 0, angle);
            }

            float t = 0;
            var startRotation = transform.rotation;

            while (isWatching && t < 1)
            {
                t = Mathf.MoveTowards(t, 1, Time.deltaTime / rotationTime);
                transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(endRotation), t);
                yield return null;
            }

            isWatching = false;
        }

        #endregion

    }

}