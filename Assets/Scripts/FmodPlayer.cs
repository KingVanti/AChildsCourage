using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private float distance = 0.1f;
    private float Material;

    const string Footsteps_Path = "event:/char/steps";
    const string PickUp_Path = "event:/UI/ItemPickup";
    const string Flashlight_ON_Path = "event:/UI/Flashlight/Flashlight_ON";
    const string Flashlight_OFF_Path = "event:/UI/Flashlight/Flashlight_OFF";
    const string Blankie_ON_Path = "event:/UI/Blankie/Blankie_ON";
    const string Blankie_OFF_Path = "event:/UI/Blankie/Blankie_OFF";


    /*   private void FixedUpdate()
       {
           MaterialCheck();
           Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);
       }
    */
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


    public void PlayFootstepsEvent()
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(Footsteps_Path);
        Footsteps.setParameterByName("Material", Material);
        Footsteps.start();
        Footsteps.release();
        // FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);  
    }

    public void PlayItems(int itemID)
    {
        switch (itemID)
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
        bool blankie_status = false;

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
        bool Flashlight_status = false;

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



}


