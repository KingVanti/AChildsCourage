using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters {
    public class Vision : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private int visionRadius;
        [Range(0, 360)] [SerializeField] private float visionAngle;
        [Range(0.01f, 3f)] [SerializeField] private float rotationSpeed;
        [SerializeField] private float updatesPerSecond;
        [SerializeField] private LayerMask wallLayer;
#pragma warning restore 649

        public TilePositionsEvent OnObservingTilesChanged;

        private bool isWatching ;
        private bool isNearTarget ;
        private Vector3 targetPos = Vector3.zero;

        #endregion

        #region Properties

        private Vector3 CurrentVector3Position => new Vector3(Mathf.RoundToInt(transform.position.x) + 0.5f, Mathf.RoundToInt(transform.position.y) + 0.5f, 0);

        private bool IsObserving { get; set; } = true;

        private List<TilePosition> CurrentlyObservingTilePositions {
            get; set;
        } = new List<TilePosition>();

        #endregion

        #region Methods

        private void Start() {
            StartCoroutine(Observe(visionRadius));
        }

        private List<TilePosition> GetVisibleTilePositions(int radius) {

            var tiles = new List<Vector3>();

            for (var i = -radius; i <= radius; i++) {

                for (var j = -radius; j <= radius; j++) {

                    var tile = new Vector3(CurrentVector3Position.x + i, CurrentVector3Position.y + j);

                    if (!(Vector3.Distance(CurrentVector3Position, tile) <= radius))
                        continue;
                    if (!(Vector3.Angle(tile - transform.position, transform.right) <= visionAngle / 2))
                        continue;
                    
                    if (!Physics2D.Raycast(transform.position, (tile - transform.position).normalized, Vector2.Distance(tile, transform.position), wallLayer)) {
                        tiles.Add(tile);
                    }

                }

            }

            for (var i = -1; i <= 1; i++) {

                for (var j = -1; j <= 1; j++) {

                    var tile = new Vector3(CurrentVector3Position.x + i, CurrentVector3Position.y + j);
                    tiles.Add(tile);

                }

            }

            return tiles
                   .Select(tile => new TilePosition(
                                    Mathf.FloorToInt(tile.x),
                                    Mathf.FloorToInt(tile.y)))
                   .ToList();
        }

        private void OnDrawGizmos() {

            foreach (var t in CurrentlyObservingTilePositions)
                Gizmos.DrawWireSphere(t.ToVector3(), 0.1f);

        }

        public void OnMinimumDistanceEntered(Vector3 target) {
            isNearTarget = true;
            targetPos = target;
        }

        public void OnMinimumDistanceLeft() {
            isNearTarget = false;
        }


      private  IEnumerator Observe(int radius) {

            while (IsObserving) {

                if (!isWatching) {
                    StartCoroutine(Watch(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)), rotationSpeed));
                }

                CurrentlyObservingTilePositions = GetVisibleTilePositions(radius);
                OnObservingTilesChanged?.Invoke(CurrentlyObservingTilePositions);
                yield return new WaitForSeconds(1f / updatesPerSecond);
            }

        }

    private    IEnumerator Watch(Vector3 endRotation, float completionTime) {

            isWatching = true;

            if (isNearTarget) {

                var angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
                endRotation = new Vector3(0,0,angle);

            }

            float elapsedTime = 0;
            var startRotation = transform.rotation;

            while (isWatching && elapsedTime < completionTime) {
                
                transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(endRotation), elapsedTime / completionTime);
                elapsedTime += Time.deltaTime;
                yield return null;

            }

            isWatching = false;

        }

        #endregion

        #region Subclasses

        [Serializable]
        public class TilePositionsEvent : UnityEvent<List<TilePosition>> { }

        #endregion

    }

}
