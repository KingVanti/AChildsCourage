using System;
using UnityEngine;
using static AChildsCourage.Game.Monsters.MAwareness;

namespace AChildsCourage.Game.Monsters
{

    public class ShadeAwareness : MonoBehaviour
    {

        #region Fields

        #pragma  warning disable 649

        [SerializeField] private float awarenessLossPerSecond;

#pragma warning  restore 649

        private bool playerIsVisible;
        private Awareness currentAwareness;

        #endregion

        #region Methods

        private void Update()
        {
            if (!playerIsVisible)
                LooseAwareness();
        }

        private void LooseAwareness()
        {
            currentAwareness = MAwareness.LooseAwareness(currentAwareness, awarenessLossPerSecond * Time.deltaTime);
        }
        
        #endregion

    }

}