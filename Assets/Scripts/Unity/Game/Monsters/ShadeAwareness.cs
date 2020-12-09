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

        private Visibility currentCharacterVisibility;
        private Awareness currentAwareness;

        #endregion

        #region Methods

        public void OnCharacterInVisionChanges(Visibility characterVisibility)
        {
            currentCharacterVisibility = characterVisibility;
        }


        private void Update()
        {
            if (currentCharacterVisibility == Visibility.NotVisible)
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