using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing_spacial : MonoBehaviour
{
    private float distance;
    private float Material;
    [SerializeField] public GameObject player;
    float dir;
    FMOD.Studio.EventInstance test_spacial;

    private void Start()
    {


        // FMODUnity.RuntimeManager.PlayOneShot("event:/test/test_spacial", GetComponent<Transform>().position);
    }

    private void FixedUpdate()
    {
        test_spacial = FMODUnity.RuntimeManager.CreateInstance("event:/test/test_spacial");

        distance = Vector3.Distance(player.transform.position, transform.position);   
        dir = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;

        test_spacial.setParameterByName("Distance", distance);
        test_spacial.setParameterByName("Direction", dir);
        test_spacial.start();
        test_spacial.release();

       // Debug.Log(distance);
        Debug.Log(dir);

    }



}


