using System.Collections;
using AChildsCourage.Game.Shade;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {

        [SerializeField] private float activeTime;
        [SerializeField] private float litTimeAfterUsed;
        [SerializeField] private EnumArray<RuneState, Sprite> stateSprites;
        [SerializeField] private Material litMaterial;

        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent(ComponentFindMode.OnChildren)]
        private Light2D runeLight;

        private RuneState state;


        private RuneState State
        {
            get => state;
            set
            {
                state = value;
                spriteRenderer.sprite = stateSprites[state];
            }
        }

        private bool WasUsed => State == RuneState.Used;

        private bool IsActive => State == RuneState.Active;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (WasUsed) return;

            if (IsActive && IsShade(other, out var shade))
                OnShadeEnter(shade);
            else if (IsChar(other)) OnCharEnter();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsChar(other)) OnCharExit();
        }

        private static bool IsShade(Component other, out ShadeBodyEntity shade)
        {
            shade = other.GetComponent<ShadeBodyEntity>();
            return other.gameObject.CompareTag(EntityTags.Shade);
        }

        private static bool IsChar(Component other) =>
            other.gameObject.CompareTag(EntityTags.Char);

        private void OnCharEnter() =>
            Activate();

        private void OnCharExit() =>
            this.StartOnly(WaitAndDeactivate);

        private IEnumerator WaitAndDeactivate()
        {
            yield return new WaitForSeconds(activeTime);
            Deactivate();
        }

        private void OnShadeEnter(ShadeBodyEntity shade) =>
            UseRuneOn(shade);

        private void Activate() =>
            State = RuneState.Active;

        private void Deactivate() =>
            State = RuneState.Inactive;

        private void UseRuneOn(ShadeBodyEntity shade)
        {
            shade.Banish();
            State = RuneState.Used;
            this.DoAfter(TurnOffLight, litTimeAfterUsed);
        }

        private void TurnOffLight()
        {
            spriteRenderer.material = litMaterial;
            runeLight.intensity = 0.1f;
        }


        private enum RuneState
        {

            Inactive,
            Active,
            Used

        }

    }

}