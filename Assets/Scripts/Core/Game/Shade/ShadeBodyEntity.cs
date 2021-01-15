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

        private bool isAwareOfChar;

        private bool IsInAttackRange
        {
            set => animator.SetBool(isInAttackRangeKey, value);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.Map(IsChar) && isAwareOfChar)
                collision.gameObject.GetComponent<CharControllerEntity>().Kill();
        }

        private bool IsChar(Collision2D collision) =>
            collision.gameObject.CompareTag(EntityTags.Char);

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
            IsInAttackRange = Vector2.Distance(transform.position, eventArgs.NewPosition) < attackAnimationRange && isAwareOfChar;

        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))]
        private void OnShadeAwarenessChanged(object _, AwarenessChangedEventArgs eventArgs) =>
            isAwareOfChar = eventArgs.Level == AwarenessLevel.Aware;

    }

}