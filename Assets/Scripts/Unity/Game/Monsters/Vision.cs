using System;
using System.Collections;
using System.Collections.Generic;
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

        private bool isWatching = false;

        private bool isNearTarget = false;
        private Vector3 targetPos = Vector3.zero;

        #endregion

        #region Properties

        private Vector3 CurrentVector3Position {
            get {
                return new Vector3(Mathf.RoundToInt(transform.position.x) + 0.5f, Mathf.RoundToInt(transform.position.y) + 0.5f, 0);
            }
        }

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

            List<Vector3> tiles = new List<Vector3>();
            List<TilePosition> tilePositions = new List<TilePosition>();

            for (int i = -radius; i <= radius; i++) {

                for (int j = -radius; j <= radius; j++) {

                    Vector3 tile = new Vector3(CurrentVector3Position.x + i, CurrentVector3Position.y + j);

                    if (Vector3.Distance(CurrentVector3Position, tile) <= radius) {

                        if (Vector3.Angle(tile - transform.position, transform.right) <= visionAngle / 2) {

                            if (!Physics2D.Raycast(transform.position, (tile - transform.position).normalized, Vector2.Distance(tile, transform.position), wallLayer)) {
                                tiles.Add(tile);
                            }

                        }

                    }

                }

            }

            for (int i = -1; i <= 1; i++) {

                for (int j = -1; j <= 1; j++) {

                    Vector3 tile = new Vector3(CurrentVector3Position.x + i, CurrentVector3Position.y + j);
                    tiles.Add(tile);

                }

            }

            foreach (Vector3 tile in tiles) {
                tilePositions.Add(new TilePosition(Mathf.FloorToInt(tile.x), Mathf.FloorToInt(tile.y)));
            }

            return tilePositions;

        }

        private void OnDrawGizmos() {

            foreach (TilePosition t in CurrentlyObservingTilePositions)
                Gizmos.DrawWireSphere(t.ToVector3(), 0.1f);

        }

        public void OnMinimumDistanceEntered(Vector3 target) {
            isNearTarget = true;
            targetPos = target;
        }

        public void OnMinimumDistanceLeft() {
            isNearTarget = false;
        }


        IEnumerator Observe(int radius) {

            while (IsObserving) {

                if (!isWatching) {
                    StartCoroutine(Watch(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)), rotationSpeed));
                }

                CurrentlyObservingTilePositions = GetVisibleTilePositions(radius);
                OnObservingTilesChanged?.Invoke(CurrentlyObservingTilePositions);
                yield return new WaitForSeconds(1f / updatesPerSecond);
            }

        }

        IEnumerator Watch(Vector3 endRotation, float completionTime) {

            isWatching = true;

            if (isNearTarget) {

                float angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
                endRotation = new Vector3(0,0,angle);

            }

            float elapsedTime = 0;
            Quaternion startRotation = transform.rotation;

            while (isWatching && elapsedTime < completionTime) {
                
                transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(endRotation), (elapsedTime / completionTime));
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
