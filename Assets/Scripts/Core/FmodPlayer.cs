using System.Collections;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage
{

    public class FmodPlayer : MonoBehaviour
    {

        private const float WaitTime = 2f;
        private bool charSprintStopIsPlaying;
        private EventInstance footsteps;
        private EventInstance footstepsSprint;
        private EventInstance staminaEventInstance;


        private void Start()
        {
            staminaEventInstance = RuntimeManager.CreateInstance(CharSprintNearEnd);
            staminaEventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            staminaEventInstance.start();
        }


        public void OnDestroy() => 
            staminaEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


        public void PlayFootsteps()
        {
            footsteps = RuntimeManager.CreateInstance(FootstepsPath);
            footsteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            footsteps.start();
            footsteps.release();
        }

        public void PlayFootsteps_sprint()
        {
            footstepsSprint = RuntimeManager.CreateInstance(FootstepsSprintPath);
            footstepsSprint.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            footstepsSprint.start();
            footstepsSprint.release();
        }

        [Sub(nameof(FlashlightEntity.OnFlashlightToggled))]
        public void OnFlashLightToggled(object _, FlashlightToggleEventArgs eventArgs) =>
            RuntimeManager.PlayOneShot(eventArgs.IsTurnedOn ? FlashlightOnPath : FlashlightOffPath, transform.position);


        public void PlayPickUp() => RuntimeManager.PlayOneShot(PickUpPath, GetComponent<Transform>().position);

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        public void PlayCouragePickUp(object _, CouragePickedUpEventArgs eventArgs)
        {
            switch (eventArgs.Variant)
            {
                case CourageVariant.Spark:
                    RuntimeManager.PlayOneShot(CourageSparkPath, GetComponent<Transform>().position);
                    break;

                case CourageVariant.Orb:
                    RuntimeManager.PlayOneShot(CourageOrbPath, GetComponent<Transform>().position);
                    break;

                default:
                    Debug.Log("doesnt exist");
                    break;
            }
        }

        public void PlayChar_GetHit() => 
            RuntimeManager.PlayOneShot(CharGetHitPath, GetComponent<Transform>().position);

        public void PlayChar_Death() =>
            RuntimeManager.PlayOneShot(CharDeathPath, GetComponent<Transform>().position);


        [Sub(nameof(CharControllerEntity.OnMovementStateChanged))][UsedImplicitly]
        private void OnCharMovementStateChanged(object _, MovementStateChangedEventArgs eventArgs)
        {
            if (eventArgs.Previous == MovementState.Sprinting && eventArgs.Current != MovementState.Sprinting) PlaySprint_stop();
        }

        private void PlaySprint_stop()
        {
            //Debug.Log("stop");
            if (charSprintStopIsPlaying) return;
            StartCoroutine(SprintTimer());
            RuntimeManager.PlayOneShot(CharSprintStop, GetComponent<Transform>().position);
        }


        [Sub(nameof(CharStaminaEntity.OnStaminaChanged))][UsedImplicitly]
        private void OnStaminaChanged(object _1, CharStaminaChangedEventArgs eventArgs)
        {
            staminaEventInstance.setParameterByName("stamina", eventArgs.Stamina);

            if (eventArgs.Stamina == 0)
            {
                //Debug.Log("full stop");
                PlaySprintDepleted();
                StartCoroutine(SprintTimer());
            }
        }

        private void PlaySprintDepleted() => 
            RuntimeManager.PlayOneShot(CharSprintDepleted, GetComponent<Transform>().position);


        private IEnumerator SprintTimer()
        {
            charSprintStopIsPlaying = true;

            yield return new WaitForSeconds(WaitTime);
            charSprintStopIsPlaying = false;
        }


        #region eventpaths

        private const string FootstepsPath = "event:/char/steps";
        private const string FootstepsSprintPath = "event:/char/sprint";
        private const string PickUpPath = "event:/UI/Item/ItemPickup";
        private const string FlashlightOnPath = "event:/UI/Flashlight/Flashlight_ON";
        private const string FlashlightOffPath = "event:/UI/Flashlight/Flashlight_OFF";
        private const string CourageSparkPath = "event:/Courage/CurageSpark";
        private const string CourageOrbPath = "event:/Courage/CurageOrb";
        private const string CharGetHitPath = "event:/char/getHit";
        private const string CharDeathPath = "event:/char/death";
        private const string CharSprintStop = "event:/char/stamina/panting_midSprint";
        private const string CharSprintDepleted = "event:/char/stamina/panting_depleted";
        private const string CharSprintNearEnd = "event:/char/stamina/sprint_nearEnd";

        #endregion

    }

}