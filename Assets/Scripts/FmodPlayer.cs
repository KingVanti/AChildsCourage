using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Items;
using FMOD.Studio;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private float distance = 0.1f;
    private float Material;

    const string Footsteps_Path = "event:/char/steps";
    const string PickUp_Path = "event:/UI/Item/ItemPickup";
    const string Flashlight_ON_Path = "event:/UI/Flashlight/Flashlight_ON";
    const string Flashlight_OFF_Path = "event:/UI/Flashlight/Flashlight_OFF";
    const string Blankie_ON_Path = "event:/UI/Blankie/Blankie_ON";
    const string Blankie_OFF_Path = "event:/UI/Blankie/Blankie_OFF";
    const string ItemSwap_Path = "event:/UI/Item/ItemSwap";
    const string CourageSpark_Path = "event:/Courage/CurageSpark";
    const string CourageOrb_Path = "event:/Courage/CurageOrb";
    const string Char_getHit_Path = "event:/char/getHit";
    const string Char_Death_Path = "event:/char/death";
    const string Char_sprint_stop = "event:/char/stamina/panting_midSprint";
    const string Char_sprint_depleted = "event:/char/stamina/panting_depleted";


    bool Flashlight_status = false;
    bool blankie_status = false;

    private EventInstance Footsteps;

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

    public void PlayFootstepsEvent()
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance(Footsteps_Path);
        Footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Footsteps.setParameterByName("Material", Material);
        Footsteps.start();
        Footsteps.release();
    }

    public void PlayItems(ItemData itemData)
    {
        switch (itemData.Id)
        {
            case 0:             //flashlight
                PlayFlashlight();
                break;

            case 1:             //Blankie
                PlayBlankie();
                break;
            default:
                Debug.Log("ItemID wrong");
                break;

        }

    }

    public void PlayPickUp()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PickUp_Path, GetComponent<Transform>().position);
    }

    public void PlayBlankie()
    {


        if (blankie_status)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Blankie_ON_Path, GetComponent<Transform>().position);
            blankie_status = !blankie_status;
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(Blankie_OFF_Path, GetComponent<Transform>().position);
            blankie_status = !blankie_status;
        }

    }

    public void PlayFlashlight()
    {


        if (Flashlight_status)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Flashlight_ON_Path, GetComponent<Transform>().position);
            Flashlight_status = !Flashlight_status;
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(Flashlight_OFF_Path, GetComponent<Transform>().position);
            Flashlight_status = !Flashlight_status;
        }

    }

    public void PlayItemSwap()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ItemSwap_Path, GetComponent<Transform>().position);
    }

    public void PlayCouragePickUp(CouragePickupEntity couragePickup)
    {

        switch (couragePickup.Variant)
        {
            case CourageVariant.Spark:
                FMODUnity.RuntimeManager.PlayOneShot(CourageSpark_Path, GetComponent<Transform>().position);
                break;

            case CourageVariant.Orb:
                FMODUnity.RuntimeManager.PlayOneShot(CourageOrb_Path, GetComponent<Transform>().position);
                break;

            default:
                Debug.Log("doesnt exist");
                break;

        }
    }

    public void PlayChar_GetHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Char_getHit_Path, GetComponent<Transform>().position);
    }

    public void PlayChar_Death()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Char_Death_Path, GetComponent<Transform>().position);
    }

    public void PlaySprint_stop()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Char_sprint_stop, GetComponent<Transform>().position);
    }

    public void PlaySprint_depleted()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Char_sprint_depleted, GetComponent<Transform>().position);
    }
}


