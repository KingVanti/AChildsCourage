using System;
using UnityEngine;
using static AChildsCourage.Game.Monsters.MAwareness;

namespace AChildsCourage.Game.Monsters
{

    public class ShadeAwareness : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float awarenessLossPerSecond;

#pragma warning  restore 649

        private bool canSeeCharacter;
        private Awareness currentAwareness;

        #endregion

        #region Methods

        public void OnCharacterInVisionChanges(bool characterIsInVision)
        {
            canSeeCharacter = characterIsInVision;
        }


        private void Update()
        {
            if (!canSeeCharacter)
                LooseAwareness();
            else
                GainAwareness();
        }

        private void LooseAwareness()
        {
            currentAwareness = MAwareness.LooseAwareness(currentAwareness, awarenessLossPerSecond * Time.deltaTime);
        }

        private void GainAwareness()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}