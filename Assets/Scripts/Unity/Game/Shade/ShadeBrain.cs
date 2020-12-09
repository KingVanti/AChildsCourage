using System;
using System.Collections;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Shade.Navigation;
using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.Shade.Navigation.MInvestigation;
using static AChildsCourage.Game.Shade.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    [UseDi]
    public class ShadeBrain : MonoBehaviour
    {

        #region Fields

        public Events.Vector3 onTargetPositionChanged;
        
#pragma warning disable 649

        [SerializeField] private float investigationUpdatesPerSecond;
        [SerializeField] private int touchDamage;

#pragma warning restore 649

        private TilesInView currentTilesInVision = new TilesInView(Enumerable.Empty<TilePosition>());
        private MInvestigationHistory.InvestigationHistory investigationHistory = Empty;
        private Coroutine investigationCoroutine;

        #endregion

        #region Properties

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        public int TouchDamage => touchDamage;

        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        #endregion

        #region Methods

        public void OnTilesInVisionChanged(TilesInView tilesInView)
        {
            currentTilesInVision = tilesInView;
        }


        public void OnHuntStarted()
        {
            CancelInvestigation();
        }


        public void OnHuntEnded()
        {
            StartInvestigation();
        }


        public void StartInvestigation()
        {
            investigationCoroutine = StartCoroutine(Investigate());
        }


        private void CancelInvestigation()
        {
            if (investigationCoroutine != null)
                StopCoroutine(investigationCoroutine);
            investigationCoroutine = null;
        }


        private IEnumerator Investigate()
        {
            var investigation = StartNew(FloorStateKeeper.CurrentFloorState, CurrentState, MRng.Random());

            var currentTarget = NextTarget(investigation, Position);
            onTargetPositionChanged.Invoke(currentTarget.GetTileCenter());

            while (!IsComplete(investigation))
            {
                investigation = Progress(investigation, currentTilesInVision);

                var newTarget = NextTarget(investigation, Position);
                if (!newTarget.Equals(currentTarget))
                {
                    onTargetPositionChanged.Invoke(currentTarget.GetTileCenter());
                    currentTarget = newTarget;
                }

                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Complete(investigation);
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }


        private void OnDestroy()
        {
            CancelInvestigation();
        }

        #endregion

    }

}