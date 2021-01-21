using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBodyEntity : MonoBehaviour
    {

        private static readonly Vector3 outOfBoundsPosition = new Vector3(100, 100, 0);

        
        [Pub] public event EventHandler OnShadeActivated;
        
        [Pub] public event EventHandler OnShadeOutOfBounds;

        [Pub] public event EventHandler OnShadeSteppedOnRune;


        [FindComponent(ComponentFindMode.OnChildren)]
        private new Collider2D collider;


        internal void Banish() =>
            OnShadeSteppedOnRune?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(ShadeAppearance.OnShadeDissolved))] [UsedImplicitly]
        private void OnShadeDissolved(object _1, EventArgs _2) =>
            MoveShadeOutOfBounds();

        private void MoveShadeOutOfBounds()
        {
            transform.position = outOfBoundsPosition;
            OnShadeOutOfBounds?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }

        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))] [UsedImplicitly]
        private void OnShadeSpawned(object _1, EventArgs _2) =>
            Activate();

        private void Activate()
        {
            gameObject.SetActive(true);
            OnShadeActivated.Invoke(this, EventArgs.Empty);
        }

    }

}