using System.Collections;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Infrastructure;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace AChildsCourage
{

    public class FmodPlayer : MonoBehaviour
    {

#pragma warning disable 649

        [SerializeField] private StaminaEntity stamina;

#pragma warning restore 649

        private readonly float Material = 0;
        private readonly float waitTime = 1.5f;
        private bool Char_sprint_stop_Is_playing;
        private EventInstance Footsteps;
        private EventInstance Stamina_eventInstance;


        private void Start()
        {
            Stamina_eventInstance = RuntimeManager.CreateInstance(Char_sprint_nearEnd);
            Stamina_eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            Stamina_eventInstance.start();
        }


        /*
    void MaterialCheck()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, distance, 1 << 1);

        if (hit.collider)
        {
            if (hit.collider.tag == "earth")
            {
                Material = 1f;
            }
            else if (hit.collider.tag == "stone")
            {
                Material = 2f;
            }
            else
            {
                Material = 1f;
            }
        }
    }
    */


        public void Update() => Stamina_eventInstance.setParameterByName("stamina", stamina.stamina);

        public void OnDestroy() => Stamina_eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


        public void PlayFootstepsEvent()
        {
            Footsteps = RuntimeManager.CreateInstance(Footsteps_Path);
            Footsteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            Footsteps.setParameterByName("Material", Material);
            Footsteps.start();
            Footsteps.release();
        }

        [Sub(nameof(Flashlight.OnFlashlightToggled))]
        public void OnFlashLightToggled(object _, FlashlightToggleEventArgs eventArgs) =>
            RuntimeManager.PlayOneShot(eventArgs.IsTurnedOn ? Flashlight_ON_Path : Flashlight_OFF_Path, transform.position);


        public void PlayPickUp() => RuntimeManager.PlayOneShot(PickUp_Path, GetComponent<Transform>().position);

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        public void PlayCouragePickUp(object _, CouragePickedUpEventArgs eventArgs)
        {
            switch (eventArgs.Variant)
            {
                case CourageVariant.Spark:
                    RuntimeManager.PlayOneShot(CourageSpark_Path, GetComponent<Transform>().position);
                    break;

                case CourageVariant.Orb:
                    RuntimeManager.PlayOneShot(CourageOrb_Path, GetComponent<Transform>().position);
                    break;

                default:
                    Debug.Log("doesnt exist");
                    break;
            }
        }

        public void PlayChar_GetHit() => RuntimeManager.PlayOneShot(Char_getHit_Path, GetComponent<Transform>().position);

        public void PlayChar_Death() => RuntimeManager.PlayOneShot(Char_Death_Path, GetComponent<Transform>().position);


        [Sub(nameof(CharControllerEntity.OnMovementStateChanged))]
        private void OnCharMovementStateChanged(object _, MovementStateChangedEventArgs eventArgs)
        {
            if(eventArgs.Previous == MovementState.Sprinting && eventArgs.Current != MovementState.Sprinting)
                PlaySprint_stop();
        }
        
        private void PlaySprint_stop()
        {
            if (Char_sprint_stop_Is_playing == false)
            {
                StartCoroutine(SprintTimer());
                RuntimeManager.PlayOneShot(Char_sprint_stop, GetComponent<Transform>().position);
            }
        }

        public void PlaySprint_depleted() => RuntimeManager.PlayOneShot(Char_sprint_depleted, GetComponent<Transform>().position);


        private IEnumerator SprintTimer()
        {
            Char_sprint_stop_Is_playing = true;

            yield return new WaitForSeconds(waitTime);
            Char_sprint_stop_Is_playing = false;
        }


        #region eventpaths

        private const string Footsteps_Path = "event:/char/steps";
        private const string PickUp_Path = "event:/UI/Item/ItemPickup";
        private const string Flashlight_ON_Path = "event:/UI/Flashlight/Flashlight_ON";
        private const string Flashlight_OFF_Path = "event:/UI/Flashlight/Flashlight_OFF";
        private const string Blankie_ON_Path = "event:/UI/Blankie/Blankie_ON";
        private const string Blankie_OFF_Path = "event:/UI/Blankie/Blankie_OFF";
        private const string ItemSwap_Path = "event:/UI/Item/ItemSwap";
        private const string CourageSpark_Path = "event:/Courage/CurageSpark";
        private const string CourageOrb_Path = "event:/Courage/CurageOrb";
        private const string Char_getHit_Path = "event:/char/getHit";
        private const string Char_Death_Path = "event:/char/death";
        private const string Char_sprint_stop = "event:/char/stamina/panting_midSprint";
        private const string Char_sprint_depleted = "event:/char/stamina/panting_depleted";
        private const string Char_sprint_nearEnd = "event:/char/stamina/sprint_nearEnd";

        #endregion

    }

}