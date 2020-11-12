using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private float distance = 0.1f;
    private float Material;

    const string Footsteps_Path = "event:/char/steps";
    const string PickUp_Path = "event:/UI/ItemPickup";
    const string Flashlight_Path = "event:/UI/Flashlight";
    const string Blankie_Path = "event:/UI/Blankie";


    private void FixedUpdate()
    {
        MaterialCheck();
        Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);
    }

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


    void PlayFootstepsEvent()
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(Footsteps_Path);
        Footsteps.setParameterByName("Material", Material);
        Footsteps.start();
        Footsteps.release();
        // FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);  
    }
    void PlayPickUp(string path)
    {
         FMODUnity.RuntimeManager.PlayOneShot(PickUp_Path, GetComponent<Transform>().position);  
    }

    void PlayBlankie(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(Blankie_Path, GetComponent<Transform>().position);
    }

    void PlayFlashlight(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(Flashlight_Path, GetComponent<Transform>().position);
    }

}


