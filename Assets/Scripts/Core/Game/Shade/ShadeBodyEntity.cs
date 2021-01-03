using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBodyEntity : MonoBehaviour
    {

        private static readonly Vector3 outOfBoundsPosition = new Vector3(100, 100, 0);


        [Pub] public event EventHandler OnShadeOutOfBounds;

        [Pub] public event EventHandler OnShadeSteppedOnRune;


        [FindComponent] private new Collider2D collider;


        public void Banish()
        {
            collider.enabled = false;
            OnShadeSteppedOnRune?.Invoke(this, EventArgs.Empty);
        }


        [Sub(nameof(ShadeAppearance.OnShadeDissolved))]
        private void OnShadeDissolved(object _1, EventArgs _2) =>
            MoveShadeOutOfBounds();

        private void MoveShadeOutOfBounds()
        {
            transform.position = outOfBoundsPosition;
            OnShadeOutOfBounds?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }

        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))]
        private void OnShadeSpawned(object _1, EventArgs _2) =>
            Activate();

        private void Activate()
        {
            gameObject.SetActive(true);
            collider.enabled = true;
        }

    }

}