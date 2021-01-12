using System;
using AChildsCourage.Game.Char;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBodyEntity : MonoBehaviour
    {

        private static readonly Vector3 outOfBoundsPosition = new Vector3(100, 100, 0);
        private static readonly int isInAttackRangeKey = Animator.StringToHash("IsInAttackRange");


        [Pub] public event EventHandler OnShadeOutOfBounds;

        [Pub] public event EventHandler OnShadeSteppedOnRune;


        [SerializeField] private float attackAnimationRange;

        [FindComponent(ComponentFindMode.OnChildren)]
        private new Collider2D collider;
        [FindComponent] private Animator animator;


        private bool IsInAttackRange
        {
            set => animator.SetBool(isInAttackRangeKey, value);
        }


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

        [Sub(nameof(CharControllerEntity.OnPositionChanged))]
        private void OnCharPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            IsInAttackRange = Vector2.Distance(transform.position, eventArgs.NewPosition) < attackAnimationRange;

    }

}