using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private float distance = 0.1f;
    private float Material;

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


    void PlayFootstepsEvent(string path)
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footsteps, transform, GetComponent<Rigidbody2D>());
        Footsteps.setParameterByName("Material", Material);
        Footsteps.start();
        Footsteps.release();
        // FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);  
    }

}


